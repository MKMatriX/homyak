using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;

namespace lab9emacs
{
    public partial class Form1 : Form
    {
        Form2 FormDictionaries;
        Font textFont;
        const int MAX_FONT_SIZE = 72;
        const int MIN_FONT_SIZE = 11;
        String defaultText;
        

        // kmp - algorithm (https://ru.wikipedia.org/wiki/%D0%90%D0%BB%D0%B3%D0%BE%D1%80%D0%B8%D1%82%D0%BC_%D0%9A%D0%BD%D1%83%D1%82%D0%B0_%E2%80%94_%D0%9C%D0%BE%D1%80%D1%80%D0%B8%D1%81%D0%B0_%E2%80%94_%D0%9F%D1%80%D0%B0%D1%82%D1%82%D0%B0)
        // make prefix out of search pattern
        // эту дюжину строчек можно долго перечитывать, но разобраться получится лишь через пару часов)
        // собственно это формирует префикс массив (см. вики) из текста для поиска
        private int[] getPrefix(string s)
        {
            int[] result = new int[s.Length];
            result[0] = 0;
            int index = 0;
            for (int i = 1; i < s.Length; i++)
            {
                while (index >= 0 && s[index] != s[i]) { index--; }
                index++;
                result[i] = index;
            }
            return result;
        }

        // а это формирует префикс массив из текста приклеивая к нему спереди перфикс массив из текста.
        public void FindSubstring(string pattern)
        {
            Color highlight = ColorTranslator.FromHtml("#00FFAA");
            richTextBox1.SelectAll();
            richTextBox1.SelectionColor = ColorTranslator.FromHtml("#000");
            string text = richTextBox1.Text;
            // поскольку string это массив символов, то формально мы задание выполнили по инструкции)
            // тем более обращаемся мы с ним именно как с массивом символов)
            //int res = -1;
            int[] pf = getPrefix(pattern);
            int index = 0;
            for (int i = 0; i < text.Length; i++)
            {
                while (index > 0 && pattern[index] != text[i]) { index = pf[index - 1]; }
                if (pattern[index] == text[i]) index++;
                if (index == pattern.Length)
                {
                    richTextBox1.Select(i - index + 1, pattern.Length);
                    richTextBox1.SelectionColor = highlight;
                    index = 0; // restart search
                    //return res = i - index + 1;
                }
            }
            richTextBox1.Select(0,0);
            //return res;
        }

        public void ReplaceSubstring(string oldPattern, string newPattern)
        {
            richTextBox1.Text = richTextBox1.Text.Replace(oldPattern, newPattern);
        }

        public Form1()
        {
            InitializeComponent();
            textFont = richTextBox1.SelectionFont;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (.txt)|*.txt";
            ofd.Title = "Открыть файл";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(ofd.FileName, Encoding.GetEncoding(1251));
                defaultText = sr.ReadToEnd();
                richTextBox1.Text = defaultText ;
                sr.Close();
            }
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            defaultText = "";
            richTextBox1.Clear();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public string[] textToWords()
        {
            string text = richTextBox1.Text;
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '\n' };
            return text.Split(delimiterChars);
        }

        public int maxWordLength()
        {
            // все слова преобразуем к их длиннам и находим максимальное
            return textToWords().Select(w => w.Length).Max();
        }


        private void словаМаксимальнойДлинныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int maxLength = maxWordLength();
            string message = textToWords().Where(w => w.Length == maxLength)
                .Aggregate("", (acc, w) => acc + w + "\n");
            MessageBox.Show(message);
        }

        // как часто встречаются слова
        public Dictionary<string, int> wordsCount()
        {
            string[] words = textToWords(); // разбиваем текст на массив слов

            return words.GroupBy<string, string, int>(k => k, c => 1)
                .Select(f => new KeyValuePair<string, int>(f.Key, f.Sum())) // суммируем группировку
                .ToDictionary(k => k.Key, c => c.Value); // и переводим в тип - словарь
        }

        private void словаВТекстеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // тут используется reduce из функционального программирования, т.е. мы сводим массив к одному значению
            string message = wordsCount().Aggregate("", (acc, w) => acc + w.Key + ": " + w.Value + ";\n");
            MessageBox.Show(message);
        }

        private string getWordsInfo()
        {
            string wordsCountText = wordsCount().Aggregate("", (acc, w) => acc + w.Key + ": " + w.Value + ";\n");
            return "Длинна самого длинного слова: " + maxWordLength() + "\n Список слов и их количесвто: \n" + wordsCountText+"\n";
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (.txt)|*.txt";
            sfd.Title = "Сохранить файл";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false, Encoding.GetEncoding(1251));
                String saveText = "original:\n" + defaultText;
                saveText += "\n\nText info:\n" + getWordsInfo();
                saveText += "\n\nChanged text:\n" + richTextBox1.Text;
                sw.Write(saveText);
                sw.Close();
            }
        }

        private void отменадействияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void отменадействияToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void вставкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void выделитьвсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDictionaries = new Form2(this);
            FormDictionaries.Show();
        }

        private void увеличитьШрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fontName = textFont.Name;
            float fontSize = textFont.SizeInPoints;
            if (fontSize < MAX_FONT_SIZE)
            {
                fontSize++;
                textFont = new System.Drawing.Font(fontName, fontSize);
                richTextBox1.SelectAll();
                richTextBox1.SelectionFont = textFont;
                richTextBox1.Select(0, 0);
            }
        }

        private void уменьшитьШрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fontName = textFont.Name;
            float fontSize = textFont.SizeInPoints;
            if (fontSize > MIN_FONT_SIZE)
            {
                fontSize--;
                textFont = new System.Drawing.Font(fontName, fontSize);
                richTextBox1.SelectAll();
                richTextBox1.SelectionFont = textFont;
                richTextBox1.Select(0, 0);
            }
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument documentToPrint = new PrintDocument();
            documentToPrint.PrintPage += new PrintPageEventHandler(DocumentToPrint_PrintPage);
            printDialog.Document = documentToPrint;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                documentToPrint.Print();
            }
        }

        private void DocumentToPrint_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringReader reader = new StringReader(richTextBox1.Text);
            float LinesPerPage = 0;
            float YPosition = 0;
            int Count = 0;
            float LeftMargin = e.MarginBounds.Left;
            float TopMargin = e.MarginBounds.Top;
            string Line = null;
            Font PrintFont = this.richTextBox1.Font;
            SolidBrush PrintBrush = new SolidBrush(Color.Black);

            LinesPerPage = e.MarginBounds.Height / PrintFont.GetHeight(e.Graphics);

            while (Count < LinesPerPage && ((Line = reader.ReadLine()) != null))
            {
                YPosition = TopMargin + (Count * PrintFont.GetHeight(e.Graphics));
                e.Graphics.DrawString(Line, PrintFont, PrintBrush, LeftMargin, YPosition, new StringFormat());
                Count++;
            }

            if (Line != null)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
            PrintBrush.Dispose();
        }

        private void предварительныйпросмотрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument documentToPrint = new PrintDocument();
            documentToPrint.PrintPage += new PrintPageEventHandler(DocumentToPrint_PrintPage);
            printPreviewDialog1.Document = documentToPrint;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printPreviewDialog1.ShowDialog();
            }
        }

        private void курсивToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Italic);
            richTextBox1.Select(0,0);
        }

        private void светлоеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
            richTextBox1.Select(0, 0);
        }

        private void полужирноеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.Select(0, 0);
        }

        private void подчеркнутоеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Underline);
            richTextBox1.Select(0, 0);
        }

        private void DeleteLine(int a_line)
        {
            int start_index = richTextBox1.GetFirstCharIndexFromLine(a_line);
            int count = richTextBox1.Lines[a_line].Length;

            // Eat new line chars
            if (a_line < richTextBox1.Lines.Length - 1)
            {
                count += richTextBox1.GetFirstCharIndexFromLine(a_line + 1) -
                    ((start_index + count - 1) + 1);
            }

            richTextBox1.Text = richTextBox1.Text.Remove(start_index, count);
        }

        private void следующийСимволToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        private void строкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*int linenumber = (richTextBox1.SelectedText.Length > 0)?
                richTextBox1.GetLineFromCharIndex(richTextBox1.Text.IndexOf(richTextBox1.SelectedText)) :
                richTextBox1.GetLineFromCharIndex(richTextBox1.GetFirstCharIndexOfCurrentLine());*/
            int linenumber = richTextBox1.GetLineFromCharIndex(richTextBox1.GetFirstCharIndexOfCurrentLine());
            DeleteLine(linenumber);
        }

        private void всеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void абзацToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int linenumber = richTextBox1.GetLineFromCharIndex(richTextBox1.GetFirstCharIndexOfCurrentLine());
            if (richTextBox1.Lines[linenumber].Length == 0)
            {
                linenumber += (linenumber > 0) ? -1 : 1;
            }

            int startLine = linenumber;
            while ((startLine > 0) && (richTextBox1.Lines[startLine].Length != 0))
                startLine--;
            int start_index = richTextBox1.GetFirstCharIndexFromLine(startLine);

            int endLine = linenumber;
            while (endLine < richTextBox1.Lines.Length-1 && richTextBox1.Lines[endLine].Length != 0)
                endLine++;
            int end_index = richTextBox1.GetFirstCharIndexFromLine(endLine) + richTextBox1.Lines[endLine].Length;

            int count = end_index - start_index;
            
            if (endLine < richTextBox1.Lines.Length - 1)
            {
                count += richTextBox1.GetFirstCharIndexFromLine(endLine + 1) - ((start_index + count - 1) + 1);
            }

            /*MessageBox.Show(
                "start_index: " + start_index+ "\n"+
                "startLine: " + startLine + "\n" +
                "endLine: " + endLine + "\n" +
                "end_index: " + end_index + "\n"
                );*/

            
            richTextBox1.Text = richTextBox1.Text.Remove(start_index, count);
        }
    }
}

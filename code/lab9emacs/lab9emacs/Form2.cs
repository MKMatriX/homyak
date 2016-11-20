using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lab9emacs
{
    public partial class Form2 : Form
    {
        Form1 f1; //Создаем переменную, для хранения ссылки на первую форму 
        public Form2(Form1 refForm1) //Меняем конструктор – указываем параметр 
        //- имя для ссылки на первую форму 
        {
            InitializeComponent();
            f1 = refForm1; //переменной f1 присваиваем значение ссылки 
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pattern = textBox1.Text;
            if (pattern.Length > 0)
            {
                f1.FindSubstring(pattern);
            }
            f1.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string pattern = textBox1.Text;
            if (pattern.Length > 0)
            {
                f1.ReplaceSubstring(pattern, textBox2.Text);
            }
            f1.Show();
            this.Close();
        }
    }
}

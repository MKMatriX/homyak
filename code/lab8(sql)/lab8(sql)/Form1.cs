using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// Как люди вообще могут сидеть в этой IDE даже vi удобней чем она, а ему уже лет 30.

namespace lab8_sql_
{
    

    public struct Student : IComparable
    {
        public string name;
        public string lastName;
        public string thirdName;
        public int birthYear;
        public string adress;
        public string school;

        public int CompareTo(object obj)
        {
            Student rival = (Student)obj;
            return this.birthYear - rival.birthYear;
        }

        public override string ToString() // for human readable output
        {
            string res = "";
            res += "Имя: " + this.name+"\n";
            res += "Фамилия: " + this.lastName + "\n";
            res += "Отчество: " + this.thirdName + "\n";
            res += "Год рождения: " + this.birthYear.ToString() + "\n";
            res += "Адресс: " + this.adress + "\n";
            res += "Школа: " + this.school + "\n";
            return res;
        }

        public string ToCsvString() // we will use that to save oustput same as input
        {
            string res = "";
            res += this.name + ";";
            res += this.lastName + ";";
            res += this.thirdName + ";";
            res += this.birthYear.ToString() + ";";
            res += this.adress + ";";
            res += this.school + "\r\n";
            return res;
        }
    }

    public partial class Form1 : Form
    {
        private Student[] students = new Student[1000];
        private string inputContent;
        private const int rowLength = 6;

        // function to add an item to our table
        public Student lineToStudent(string line)
        {
            string[] cells = line.Split(";".ToCharArray());
            if (cells.Length != 6) {
                // if row isn't valid then we will throw an  exption
                throw new Exception("invalid number of parameters");
            }
            Student row = new Student(); // how I missed jss 'call'
            row.name = cells[0].Trim();
            row.lastName = cells[1].Trim();
            row.thirdName = cells[2].Trim();
            row.birthYear = Convert.ToInt32(cells[3].Trim(), 10); // this too will throw an error that we should catch
            row.adress = cells[4].Trim();
            row.school = cells[5].Trim();

            // add some sense here, for example for a birthYear
            if (row.birthYear < 1900 || row.birthYear > 2020)
            {
                throw new Exception("suspicious birth year: "+ row.birthYear);
            }
            return row;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Saving...";
            this.saveFile();
            
        }

        private void eventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Press left top button to start ^_^";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void saveFile()
        {
            string output = "";
            string selectedSchool = (comboBox1.SelectedItem as ComboboxItem).Text;
            Student[] selectedSchoolStudents = this.students.Where(student => student.school == selectedSchool).ToArray(); // some more functional programming
            for (int i = 0; i < selectedSchoolStudents.Length; i++)
            {
                output += selectedSchoolStudents[i].ToCsvString();
            }

            using (StreamWriter file = new StreamWriter("output.txt"))
            {
                file.WriteLine(output);
            }
            label1.Text = "Save complete, found " + selectedSchoolStudents.Length.ToString() + " stunedt(s)";
        }

        public void addSchoolsToList()
        {
            string[] schools = this.students.Select(student => student.school).Distinct().ToArray(); // some part of functional programming

            for (int i = 0; i < schools.Length; i++)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = schools[i];
                item.Value = schools[i]; // who the hell cares?
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedIndex = 0;
        }

        public void loadFile() {
            label1.Text = "Loading file...";
            try
            {
                using (StreamReader sr = new StreamReader("input.txt"))
                {
                    String inputFile = sr.ReadToEnd();
                    this.inputContent = inputFile;
                    // label1.Text = inputFile;
                    label1.Text = "Parsing file...";
                    string[] lines = inputFile.Split("\n".ToCharArray());

                    List<Student> tmpList = new List<Student>(); // to avoid hardcoded length of the students array
                    label1.Text = "";
                    for (int i = 0; i < lines.Length; i++)
                    {
                        try
                        {
                            // it's our function, and it still can throw an error
                            Student row = this.lineToStudent(lines[i]);
                            tmpList.Add(row);
                            label1.Text += "Student " + "(" + i + ") added successfuly:\n" + row.ToString() + "\n";
                        }
                        catch (Exception er)
                        {
                            // so we'll handle its log
                            string err = "Line(" + i + ") parse error: \n >>> " + er.Message + "\n";
                            label1.Text += err;
                        }

                    }
                    this.students = tmpList.ToArray();
                    Array.Sort(this.students);
                    this.addSchoolsToList();
                }
            }
            catch (Exception er)
            {
                string err = "Catched error, the file could not be read: \n" + er.Message;
                label1.Text = err;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.loadFile();
        }
    }

    public class ComboboxItem // one of ways to add items to combobox
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}

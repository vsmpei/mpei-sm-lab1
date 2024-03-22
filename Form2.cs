using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MS_LR_1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // имя файла логов 
            string file_name = "log.txt";

            // строки из файла 
            string[] Lines;

            // проверка существования 
            if (File.Exists(file_name))
            {
                // получаем сразу все строки из файла
                Lines = System.IO.File.ReadAllLines(file_name);

                // выводим на textBox
                foreach (string str in Lines)
                {
                    // построчно...
                    textBox1.AppendText(str + Environment.NewLine);
                }
            }
            else // иначе...
            {
                MessageBox.Show("Файл логов отсуствует!", "Ошибка");
            }
        }
    }
}

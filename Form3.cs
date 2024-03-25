using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MS_LR_1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // какой-то текст
            label1.Text = "МЯУ МЯУ МЯУ МЯУ МЯУ";
            // начало конца
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // начинаем представление
            timer1.Start();
            // что то невообразимое
            Random random = new Random();
            // оно
            int A = random.Next(0, 255);
            // меняет
            int R = random.Next(0, 255);
            // цвет
            int G = random.Next(0, 255);
            // вау
            int B = random.Next(0, 255);
            // финал...
            label1.ForeColor = Color.FromArgb(A, R, G, B);
        }
    }
}

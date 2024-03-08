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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // ЗАПОЛНЕНИЕ НУЛЯМИ ВЕСОВ МАТРИЦЫ
        private void initial_zero()
        {
            // размерность таблицы
            int N = dataGridView1.Rows.Count;
            int M = dataGridView1.Columns.Count;

            // заполняем нулями веса, все кроме строки и столбца стратегий
            for (int i = 2; i < N; i++)
            {
                for (int j = 2; j < M; j++)
                {
                        // присваиваем значение 
                        dataGridView1.Rows[i].Cells[j].Value = 0;
                }
            }

        }

        // ЗАПОЛНЕНИЕ РАНДОМНЫМИ ЗНАЧЕНИЯМИ ВЕСОВ МАТРИЦЫ
        private void initial_random()
        {
            // размерность таблицы
            int N = dataGridView1.Rows.Count;
            int M = dataGridView1.Columns.Count;

            // рандомное значение 
            Random rndm_weight= new Random();

            // заполняем нулями веса, все кроме строки и столбца стратегий
            for (int i = 2; i < N; i++)
            {
                for (int j = 2; j < M; j++)
                {
                    // присваиваем случаное значение в диапазоне от -10 до 10 весам
                    dataGridView1.Rows[i].Cells[j].Value = rndm_weight.Next(-10, 10);
                }
            }

        }

        // ПОИСК МАКСИМИНА
        private int find_maximin()
        {
            // размерность таблицы
            int N = dataGridView1.Rows.Count;
            int M = dataGridView1.Columns.Count;

            // текущее минимальное значение 

            // результирующее значение
            int maximin = 0;
            // текущее минимальное 
            int min = 0;

            // рандомное значение 
            Random rndm_weight = new Random();
    
            // цикл по строкам
            for (int i = 2; i < N; i++)
            {
                // первое минимальное значение в строке
                // СТОЛБЕЦ ФИКСИРОВАННЫЙ, Т.К. ВСЕ СТРОКИ НАЧИНАЮТСЯ С ОДНОГО СТОЛБЦА...
                min = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);

                // цикл по столбцам
                for (int j = 2; j < M; j++)
                {
                    // если текущее минимальное значение больше найденного
                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value) < min)
                    {
                        // новое минимальное значение  
                        min = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                    }
                }

                // если вторая строка
                if (i==2)
                {
                    // присваиваем начальное значение максимина
                    maximin = min;
                }
                // если текущее минимальное значение больше текущего максимального
                if (min > maximin)
                {
                    // присваиваем...
                    maximin = min;
                }
            }

            // возращаем найденный максимин
            return maximin;

        }


        // ПОИСК МИНИМАКСА
        private int find_minimax()
        {
            // размерность таблицы
            int N = dataGridView1.Rows.Count;
            int M = dataGridView1.Columns.Count;

            // текущее минимальное значение 

            // результирующее значение
            int minimax = 0;
            // текущее минимальное 
            int max = 0;

            // рандомное значение 
            Random rndm_weight = new Random();

            // цикл по столбцам
            for (int j = 2; j < M; j++)
            {
                // первое максимальное значение в строке
                // СТРОКА ФИКСИРОВАННАЯ, Т.К. ВСЕ СТОЛБЦЫ НАЧИНАЮТСЯ С ОДНОЙ СТРОКИ...
                max = Convert.ToInt32(dataGridView1.Rows[2].Cells[j].Value);

                // цикл по строкам
                for (int i = 2; i < N; i++)
                {
                    // если текущее максимальное значение меньше найденного
                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value) > max)
                    {
                        // новое максимальное значение  
                        max = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                    }
                }

                // если второй столбец
                if (j == 2)
                {
                    // присваиваем начальное значение минимакса
                    minimax = max;
                }
                // если текущее максимальное значение меньше текущего максимального
                if (max < minimax)
                {
                    // присваиваем...
                    minimax = max;
                }
            }

            // возращаем найденный минимакс
            return minimax;

        }


        // событие при создании формы
        private void Form1_Load(object sender, EventArgs e)
        {
            // успешное создание формы 
            MessageBox.Show("Дорогой игрок, добро пожаловать...(заглушка для пояснений)", "Сообщение");

            // стандартные значения имен игроков  
            textBox1.Text = "Бородин";
            textBox2.Text = "Студент";
            // стандартный размер иговой матрицы
            textBox3.Text = "5";
            textBox4.Text = "5";
            // стандартные значения задания стратегии 
            textBox5.Text = "Бородин";
            textBox6.Text = "1";
            textBox7.Text = "Поставить 2";
            // сыграем?
            textBox8.Text = "0";
            textBox9.Text = "0";
            // стандартные значения задания весов 
            textBox10.Text = "Поставить 2";
            textBox11.Text = "Давить на жалость";
            textBox12.Text = "1000";

        }

        //---------------------------------------------------------------------------------------------------------------------
        // С О З Д А Н И Е  И Г Р О В О Г О  П О Л Я 
        //---------------------------------------------------------------------------------------------------------------------
        private void button1_Click_1(object sender, EventArgs e)
        {

            if ((Convert.ToInt32(textBox3.Text) > 2) && (Convert.ToInt32(textBox4.Text) > 2))
            {
                // количество столбцов 
                dataGridView1.ColumnCount = Convert.ToInt32(textBox3.Text)+2;
                // количество строк 
                dataGridView1.RowCount = Convert.ToInt32(textBox4.Text)+2;

                // первый игрок 
                dataGridView1.Rows[0].Cells[1].Value = textBox1.Text;

                // второй игрок 
                dataGridView1.Rows[1].Cells[0].Value = textBox2.Text;

                // заполняем нулями...
                initial_zero();

                MessageBox.Show("Игра создана!", "Сообщение");
            }
            else 
            {
                MessageBox.Show("Количество строк и столбцов должно быть больше 2!", "Ошибка");
            }
        }

        //---------------------------------------------------------------------------------------------------------------------
        //  Д О Б А В Л Е Н И Е  С Т Р А Т Е Г И И 
        //---------------------------------------------------------------------------------------------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            // имя игрока
            string name = textBox5.Text;

            // номер стратегии / индекс 
            int ind = Convert.ToInt32(textBox6.Text);

            // флаг проверки диапазона значений 
            bool check = true;

            // проверка диапазона 
            if ((ind > dataGridView1.Rows.Count) && (ind > dataGridView1.Columns.Count))
            {
                check = false;
                MessageBox.Show("Выход за пределы платежной матрицы!", "Ошибка");
            }

            // заполнение стратегии для конкретного игрока
            // для первого, т.к. индекс имени игрока ПОСТОЯНЕН
            if ((Convert.ToString(dataGridView1.Rows[0].Cells[1].Value) == name) && (check == true))
            {
                dataGridView1.Rows[1].Cells[ind+1].Value = textBox7.Text;
            }
            // для ВТОРОГО, т.к. индекс имени игрока ПОСТОЯНЕН
            else if ((Convert.ToString(dataGridView1.Rows[1].Cells[0].Value) == name) && (check == true))
            {
                dataGridView1.Rows[ind+1].Cells[1].Value = textBox7.Text;
            }
            // если игрок не был найден и значение находится в диапазоне
            else if (check == true)
            {
                MessageBox.Show("Игрок с таким именем не найден!", "Ошибка");
            }

        }

        //---------------------------------------------------------------------------------------------------------------------
        //  Д О Б А В Л Е Н И Е  В Е С А  
        //--------------------------------------------------------------------------------------------------------------------
        private void button6_Click(object sender, EventArgs e)
        {

            // размерность таблицы
            int N = dataGridView1.Rows.Count;
            int M = dataGridView1.Columns.Count;

            // стратегии, для которых добавляются веса
            string str1 = textBox10.Text;
            string str2 = textBox11.Text;

            // добавляемый вес
            int val = Convert.ToInt32(textBox12.Text);

            // индексы-координаты ячейки для записи
            int ind_x = 0; 
            int ind_y = 0;

            // флаги проверки нахождения стратегий
            bool check1 = false; 
            bool check2 = false;

            // поиск индекса первой стратегии с ЗАДАННЫМ ИМЕНЕМ в таблице
            for (int i = 1; i < M; i++)
            {
                if (Convert.ToString(dataGridView1.Rows[1].Cells[i].Value) == str1)
                {
                    ind_y = i;
                    check1 = true;
                }
            }

            // поиск индекса второй стратегии с ЗАДАННЫМ ИМЕНЕМ в таблице
            for (int j = 1; j < N; j++)
            {
                if (Convert.ToString(dataGridView1.Rows[j].Cells[1].Value) == str2)
                {
                    ind_x = j;
                    check2 = true;
                }
            }

            // проверка и добавление веса в таблицу
            if ((check2 == true) && (check2 == true))
            {
                dataGridView1.Rows[ind_x].Cells[ind_y].Value = val;
                MessageBox.Show("Вес добавлен!", "Сообщение");
            }
            else
            {
                MessageBox.Show("Одна или обе стратегии отсутствуют в таблице!", "Ошибка");
            }
  
        }

        //---------------------------------------------------------------------------------------------------------------------
        //  П О И С К  М А К С И М И Н А  И  М И Н И М А К С А   
        //--------------------------------------------------------------------------------------------------------------------
        private void button3_Click(object sender, EventArgs e)
        {
            // поиск максимина 
            int maximin = find_maximin();
            // вывод максиммина в текстбокс
            textBox8.Text = Convert.ToString(maximin);
            // поиск минимакса 
            int minimax = find_minimax();
            // вывод минимакса в текстбокс
            textBox9.Text = Convert.ToString(minimax);
            // сообщение об поиске максимина и минимакса
            MessageBox.Show("Поиск максимина и минимакса!", "Сообщение");
        }

        //---------------------------------------------------------------------------------------------------------------------
        //  В Ы Г Р У З К А  В  Ф А Й Л   
        //--------------------------------------------------------------------------------------------------------------------
        private void button7_Click(object sender, EventArgs e)
        {
            // размерность таблицы
            int N = dataGridView1.Rows.Count;
            int M = dataGridView1.Columns.Count;

            // имя файла 
            string file_name = "Game_weights.txt";
            // строка для записи в файл
            string res_str = "";

            // инициализируем поток 
            StreamWriter sw = new StreamWriter(file_name);

            // по всей платежной матрице, но не таблице!
            for (int i = 2; i < N; i++)
            {
                for (int j = 2; j < M; j++)
                {
                    // склеиваем в строку
                    res_str += Convert.ToString(dataGridView1.Rows[i].Cells[j].Value);
                    res_str += " ";
                }
                // записываем очередную строку в файл
                sw.WriteLine(res_str);
                // очищаем строку 
                res_str = "";
            }

            // закрываем поток
            sw.Close();

            // выводим сообщение
            MessageBox.Show("Платежная матрица выгружена в файл!", "Сообщение");
        }

        //---------------------------------------------------------------------------------------------------------------------
        //  Р А Н Д О М Н Ы Е  З Н А Ч Е Н И Я  В Е С О В   
        //--------------------------------------------------------------------------------------------------------------------
        private void button8_Click(object sender, EventArgs e)
        {
            initial_random();
            MessageBox.Show("Рандомные значения весов сгенерированы!", "Сообщение");
        }
    }
}

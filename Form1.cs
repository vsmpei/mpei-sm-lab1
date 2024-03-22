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

            // рандомное значение для записи в матрицу 
            // int rand;

            // рандомное значение 
            Random rndm_weight = new Random();

            // заполняем нулями веса, все кроме строки и столбца стратегий
            for (int i = 2; i < N; i++)
            {
                for (int j = 2; j < M; j++)
                //for (int j = i; j < M; j++)
                {
                    // присваиваем случаное значение в диапазоне от -10 до 10 весам
                    dataGridView1.Rows[i].Cells[j].Value = rndm_weight.Next(-10, 10);

                    // присваиваем случаное значение в диапазоне СИММЕТРИЧНО
                    //rand = rndm_weight.Next(-10, 10);
                    //dataGridView1.Rows[i].Cells[j].Value = rand;
                    //dataGridView1.Rows[j].Cells[i].Value = -1*rand;
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
                //min = Convert.ToInt32(dataGridView1[i, 2].Value);

                // цикл по столбцам
                for (int j = 2; j < M; j++)
                {
                    //MessageBox.Show(Convert.ToString(dataGridView1[j, i].Value));
                    // если текущее минимальное значение больше найденного
                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value) < min)
                    //if (Convert.ToInt32(dataGridView1[i, j].Value) < min)
                    {
                        // новое минимальное значение  
                        min = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                        //min = Convert.ToInt32(dataGridView1[i, j].Value);
                    }
                }
                //MessageBox.Show(Convert.ToString(min));
                // если вторая строка
                if (i == 2)
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
                //max = Convert.ToInt32(dataGridView1[2, j].Value);

                // цикл по строкам
                for (int i = 2; i < N; i++)
                {
                    // если текущее максимальное значение меньше найденного
                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value) > max)
                    //if (Convert.ToInt32(dataGridView1[i, j].Value) > max)
                    {
                        // новое максимальное значение  
                        max = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                        //max = Convert.ToInt32(dataGridView1[i, j].Value);
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

        // УДАЛЕНИЕ СТРОГО ДОМИНИРУЮЩИХ СТРАТЕГИЙ
        private void remove_strictly_dominant()
        {

            // размерность таблицы
            int N = dataGridView1.Rows.Count;
            int M = dataGridView1.Columns.Count;

            // номера строк для удаления 
            SortedSet<int> row_nums = new SortedSet<int>();
            // номера столбцов для удаления 
            SortedSet<int> column_nums = new SortedSet<int>();

            // НАЧИНАЯ с первой строки для первого цикла 
            int i = 2;
            // НАЧИНАЯ со второй строки для второго цикла 
            int j = 3;
            // для по элементного сравнения строк (по столбцам) 
            int k;

            // флаг выхода и цикла по элементам, в случае
            // если хотя бы элемент строки i+1 больше элемента iой
            bool flag_k = true;

            // дополнительный счетчик
            //int kol;

            //---------------------------------------------------------------------------------------------------------
            // П О  С Т Р О К А М 
            // c первой строки
            while (i < N - 1)
            {

                // начиная со следующего...
                j = i + 1;

                flag_k = true; // переходим к следующему циклу проверок 


                // со второй строки 
                while ((j < N) && (flag_k == true))
               // while (j < N)
                {
                    // с первого столбца  
                    k = 2;
                    // пока все ок 
                    //flag_k = true;

                    // по столбцам (по-элементно)
                    while ((k < M) && (flag_k == true))
                    {
                        MessageBox.Show(Convert.ToString(dataGridView1.Rows[i].Cells[k].Value) + " и " + Convert.ToString(dataGridView1.Rows[j].Cells[k].Value));

                        // если хотя бы один элемент у первой строки СТРОГО больше чем у второй...
                        if (Convert.ToInt32(dataGridView1.Rows[i].Cells[k].Value) < Convert.ToInt32(dataGridView1.Rows[j].Cells[k].Value))
                        {
                            //MessageBox.Show(Convert.ToString(dataGridView1.Rows[i].Cells[k].Value) + " и " + Convert.ToString(dataGridView1.Rows[j].Cells[k].Value));
                            // выходим из цикла по элементам
                            flag_k = false;
                        }

                        //MessageBox.Show(Convert.ToString(k));
                        // переходим к следующему элементу 
                        k++;
                    }

                    //MessageBox.Show(Convert.ToString(j));

                    // переходим к следующей строке
                    j++;
                }

                // если НЕ было выхода из цикла =>
                // => данная строка больше всех остальных
                // по КАЖДОМУ элементу!
                if (flag_k == true)
                {
                    // записываем номер строки 
                    row_nums.Add(i);
                }

                
                // переходим к следующей строке
                i++;
            }

            // если вообще были найдены такие строки...
            if (row_nums.Count != 0)
            {
                // цикл по стеку
                foreach (var ind in row_nums.Reverse())
                {
                    // удаляем строки с найденными номерами...
                    //dataGridView1.Rows.RemoveAt(ind);
                    MessageBox.Show(Convert.ToString(ind));
                }
            }

        }

        // УДАЛЕНИЕ СЛАБО ДОМИНИРУЮЩИХ СТРАТЕГИЙ
        private void remove_weakly_dominant()
        {
            // размерность таблицы
            int N = dataGridView1.Rows.Count;
            int M = dataGridView1.Columns.Count;

            // номера строк для удаления 
            SortedSet<int> row_nums = new SortedSet<int>();
            // номера столбцов для удаления 
            SortedSet<int> column_nums = new SortedSet<int>();

            // НАЧИНАЯ с первой строки для первого цикла 
            int i = 2;
            // НАЧИНАЯ со второй строки для второго цикла 
            int j = 3;
            // для по элементного сравнения строк (по столбцам) 
            int k;

            // флаг выхода и цикла по элементам, в случае
            // если хотя бы элемент строки i+1 больше элемента iой
            bool flag_k = true;

            // дополнительный счетчик
            //int kol;

            //---------------------------------------------------------------------------------------------------------
            // П О  С Т Р О К А М 
            // c первой строки
            while (i < N - 1)
            {

                // начиная со следующего...
                j = i + 1;

                flag_k = true; // переходим к следующему циклу проверок 


                // со второй строки 
                while ((j < N) && (flag_k == true))
                // while (j < N)
                {
                    // с первого столбца  
                    k = 2;
                    // пока все ок 
                    //flag_k = true;

                    // по столбцам (по-элементно)
                    while ((k < M) && (flag_k == true))
                    {
                        MessageBox.Show(Convert.ToString(dataGridView1.Rows[i].Cells[k].Value) + " и " + Convert.ToString(dataGridView1.Rows[j].Cells[k].Value));

                        // если хотя бы один элемент у первой строки СТРОГО больше чем у второй...
                        if (Convert.ToInt32(dataGridView1.Rows[i].Cells[k].Value) <= Convert.ToInt32(dataGridView1.Rows[j].Cells[k].Value))
                        {
                            //MessageBox.Show(Convert.ToString(dataGridView1.Rows[i].Cells[k].Value) + " и " + Convert.ToString(dataGridView1.Rows[j].Cells[k].Value));
                            // выходим из цикла по элементам
                            flag_k = false;
                        }

                        //MessageBox.Show(Convert.ToString(k));
                        // переходим к следующему элементу 
                        k++;
                    }

                    //MessageBox.Show(Convert.ToString(j));

                    // переходим к следующей строке
                    j++;
                }

                // если НЕ было выхода из цикла =>
                // => данная строка больше всех остальных
                // по КАЖДОМУ элементу!
                if (flag_k == true)
                {
                    // записываем номер строки 
                    row_nums.Add(i);
                }


                // переходим к следующей строке
                i++;
            }

            // если вообще были найдены такие строки...
            if (row_nums.Count != 0)
            {
                // цикл по стеку
                foreach (var ind in row_nums.Reverse())
                {
                    // удаляем строки с найденными номерами...
                    //dataGridView1.Rows.RemoveAt(ind);
                    MessageBox.Show(Convert.ToString(ind));
                }
            }

        }

        // событие при создании формы
        private void Form1_Load(object sender, EventArgs e)
        {
            // успешное создание формы 
            MessageBox.Show("Дорогой пользователь, добро пожаловать! Только что Вы зашли в приложение, написанное" +
                            " в качестве проекта для лабораторной работы №1 по дисциплине \"Операционные системы\". " +
                            "Данное приложение не поддерживает биматричные модели, однако позволяет делать много чего другого. "+
                            "Например, детально настраивать игровую матрицу, а так же искать максимин, минимакс, удалять" +
                            " строго доминирующие, а так же слабо доминирующие стратегии!" , "Сообщение");

            // стандартные значения имен игроков  
            textBox1.Text = "Бородин";
            textBox2.Text = "Студент";
            // стандартный размер иговой матрицы
            textBox3.Text = "5";
            textBox4.Text = "4";
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

            // удаляем все СТАРЫЕ строки
            dataGridView1.Rows.Clear();
            // удаляем все СТАРЫЕ столбцы
            dataGridView1.Columns.Clear();

            if ((Convert.ToInt32(textBox3.Text) > 2) && (Convert.ToInt32(textBox4.Text) > 2))
            {
                // очищаем dataGridView1
                // dataGridView1.Rows.Clear();

                // количество стратегий первого игрока  
                dataGridView1.RowCount = Convert.ToInt32(textBox3.Text) + 2;

                // количество второго игрока
                dataGridView1.ColumnCount = Convert.ToInt32(textBox4.Text) + 2;

                // первый игрок 
                dataGridView1.Rows[0].Cells[1].Value = textBox1.Text;

                // второй игрок 
                dataGridView1.Rows[1].Cells[0].Value = textBox2.Text;

                // заполняем нулями...
                initial_zero();
                //MessageBox.Show(Convert.ToString(dataGridView1.Rows.Count), "Сообщение");
                MessageBox.Show("Матрица игры создана!", "Сообщение");
            }
            else
            {
                MessageBox.Show("Количество строк и столбцов должно быть больше 2!", "Ошибка");
            }
        }

        //---------------------------------------------------------------------------------------------------------------------
        //  Д О Б А В Л Е Н И Е  С Т Р А Т Е Г И И 
        //----------------------------------------------------------------------------------------------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            // имя игрока
            string name = textBox5.Text;

            // номер стратегии / индекс 
            int ind = Convert.ToInt32(textBox6.Text) + 1; // +1 т.к. таблица смещена относительно первой ячейки (0,0)

            // заполнение стратегии для конкретного игрока
            // для первого, т.к. индекс имени игрока ПОСТОЯНЕН
            if (Convert.ToString(dataGridView1.Rows[0].Cells[1].Value) == name)
            {
                // проверка диапазона 
                // не должен быть равен или меньше 0 ИЛИ быть больше чем размер таблицы, -1 т.к. платежная матрица СМЕЩЕНА
                if (!((ind > dataGridView1.Columns.Count - 1) || (ind <= 0)))
                {
                    dataGridView1.Rows[1].Cells[ind].Value = textBox7.Text;
                }
                else
                {
                    MessageBox.Show("Выход за пределы платежной матрицы!", "Ошибка");
                }
            }
            // для ВТОРОГО, т.к. индекс имени игрока ПОСТОЯНЕН
            else if (Convert.ToString(dataGridView1.Rows[1].Cells[0].Value) == name)
            {
                // проверка диапазона 
                // не должен быть равен или меньше 0 ИЛИ быть больше чем размер таблицы, -1 т.к. платежная матрица СМЕЩЕНА
                if (!((ind > dataGridView1.Rows.Count - 1) || (ind <= 0)))
                {
                    dataGridView1.Rows[ind].Cells[1].Value = textBox7.Text;
                }
                else
                {
                    MessageBox.Show("Выход за пределы платежной матрицы!", "Ошибка");
                }
            }
            // если игрок не был найден и значение находится в диапазоне
            else
            {
                MessageBox.Show("Игрок с таким именем не найден!", "Ошибка");
            }
        }

        //---------------------------------------------------------------------------------------------------------------------
        //  Д О Б А В Л Е Н И Е  В Е С А  
        //---------------------------------------------------------------------------------------------------------------------
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
            int ind_1 = 0;
            int ind_2 = 0;

            // флаги проверки нахождения стратегий
            bool check1 = false;
            bool check2 = false;


            // поиск индекса первой стратегии с ЗАДАННЫМ ИМЕНЕМ в таблице
            for (int j = 1; j < N; j++)
            {
                if (Convert.ToString(dataGridView1.Rows[j].Cells[1].Value) == str1)
                {
                    ind_1 = j;
                    check2 = true;
                }
            }

            // поиск индекса второй стратегии с ЗАДАННЫМ ИМЕНЕМ в таблице
            for (int i = 1; i < M; i++)
            {
                if (Convert.ToString(dataGridView1.Rows[1].Cells[i].Value) == str2)
                {
                    ind_2 = i;
                    check1 = true;
                }
            }

            // проверка и добавление веса в таблицу
            if ((check2 == true) && (check2 == true))
            {
                dataGridView1.Rows[ind_1].Cells[ind_2].Value = val;
                MessageBox.Show("Вес добавлен!", "Сообщение");
            }
            else
            {
                MessageBox.Show("Одна или обе стратегии отсутствуют в таблице!", "Ошибка");
            }

        }

        //---------------------------------------------------------------------------------------------------------------------
        //  П О И С К  М А К С И М И Н А  И  М И Н И М А К С А   
        //---------------------------------------------------------------------------------------------------------------------
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
        //---------------------------------------------------------------------------------------------------------------------
        private void button7_Click(object sender, EventArgs e)
        {
            // размерность таблицы
            int N = dataGridView1.Rows.Count;
            int M = dataGridView1.Columns.Count;

            // если матрица инициализирована 
            if ((M > 0) && (N > 0))
            {

                // имя файла 
                string file_name = "GAME_RESULT.txt";
                // строка для записи в файл
                string res_str = "";

                // инициализируем поток 
                StreamWriter sw = new StreamWriter(file_name);

                // выводим игроков в файл
                sw.WriteLine("Игроки: \n");
                // имя ВТОРОГО игрока
                res_str = Convert.ToString(dataGridView1.Rows[0].Cells[1].Value);
                sw.WriteLine(res_str);
                res_str = "";
                // имя ВТОРОГО игрока
                res_str = Convert.ToString(dataGridView1.Rows[1].Cells[0].Value) + "\n";
                sw.WriteLine(res_str);
                res_str = "";

                // выводим стратегии в файл
                sw.WriteLine("Стратегии: \n");
                // вывод стратегий ВТОРОГО игрока
                for (int j = 1; j < N; j++)
                {
                    // если стартегия не пустая
                    if (Convert.ToString(dataGridView1.Rows[j].Cells[1].Value) != "")
                    {
                        res_str = Convert.ToString(dataGridView1.Rows[j].Cells[1].Value);
                        sw.WriteLine(res_str);
                    }
                    else // пустая стратегия
                    {
                        res_str = "missing_content";
                        sw.WriteLine(res_str);
                    }
                }
                // отступ в файле
                sw.WriteLine("\n");
                // очищем строку 
                res_str = "";
                // вывод стратегий ВТОРОГО игрока
                for (int i = 1; i < M; i++)
                {
                    if (Convert.ToString(dataGridView1.Rows[1].Cells[i].Value) != "")
                    {
                        res_str = Convert.ToString(dataGridView1.Rows[1].Cells[i].Value);
                        sw.WriteLine(res_str);
                    }
                    else // пустая стратегия
                    {
                        res_str = "missing_content";
                        sw.WriteLine(res_str);
                    }
                }

                // отступ в файле
                sw.WriteLine("\n");
                // очищем строку 
                res_str = "";

                // вывод весов в файл
                res_str = "Веса: \n";
                sw.WriteLine(res_str);
                res_str = "";
                // записываем в файл веса
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
            else // иначе...
            {
                MessageBox.Show("Сначала инициализируйте материцу!", "Ошибка");
            }
        }

        //---------------------------------------------------------------------------------------------------------------------
        //  Р А Н Д О М Н Ы Е  З Н А Ч Е Н И Я  В Е С О В   
        //---------------------------------------------------------------------------------------------------------------------
        private void button8_Click(object sender, EventArgs e)
        {
            initial_random();
            MessageBox.Show("Рандомные значения весов сгенерированы!", "Сообщение");
        }

        //---------------------------------------------------------------------------------------------------------------------
        //  З А Г Р У З К А  И З  Ф А Й Л А    
        //---------------------------------------------------------------------------------------------------------------------
        private void button9_Click(object sender, EventArgs e)
        {
            // имя файла 
            string file_name = "GAME_INPUT.txt";
            // строка для чтения из файла
            string read_str = "";
            // массив строк "чисел" из файла, которые будут в дальнейшем приведены к int
            String[] chisl;
            // размерность будущей матрицы
            int N, M; // число строк и столбцов 

            // инициализируем поток чтения 
            StreamReader sr = new StreamReader(file_name);

            // читаем первую строку с количеством стратегий первого игрока
            N = Convert.ToInt32(sr.ReadLine())+2; // отступы, первая и вторая строка заняты...
            // читаем вторую строку с количеством стратегий второго игрока
            M = Convert.ToInt32(sr.ReadLine())+2; // отступы, первая и вторая строка заняты...

            // количество стратегий первого игрока  
            dataGridView1.RowCount = N; 
            // количество стратегий второго игрока
            dataGridView1.ColumnCount = M;

            // читаем третью строку с именем ПЕРВОГО игрока 
            dataGridView1.Rows[1].Cells[0].Value = sr.ReadLine();
            // читаем четвертую строку с именем ВТОРОГО игрока
            dataGridView1.Rows[0].Cells[1].Value = sr.ReadLine();

            // ввод стратегий ПЕРВОГО игрока
            for (int i = 2; i < N; i++)
            {
                // читаем из потока очередную строку
                read_str = sr.ReadLine();

                // если поток вернул нам null
                if (String.IsNullOrEmpty(read_str))
                {
                    dataGridView1.Rows[i].Cells[1].Value = "missing_content"; // значит строка пустая
                }
                // во всех остальных случаях
                else
                {
                    dataGridView1.Rows[i].Cells[1].Value = read_str;
                }
                
            }

            // ввод стратегий ВТОРОГО игрока
            for (int j = 2; j < M; j++)
            {
                // читаем из потока очередную строку
                read_str = sr.ReadLine();

                // если поток вернул нам null
                if (String.IsNullOrEmpty(read_str))
                {
                    dataGridView1.Rows[1].Cells[j].Value = "missing_content"; // значит строка пустая
                }
                // во всех остальных случаях
                else
                {
                    dataGridView1.Rows[1].Cells[j].Value = read_str;
                }

            }

            //пустая строка
            read_str = "";

            // цикл по строкам
            for (int i = 2; i < N; i++)
            {
                // читаем первую строку
                read_str = sr.ReadLine();
                // разбиваем на отдельные строки 
                chisl = read_str.Split(' ');

                // цикл по столбцам 
                for (int j = 2; j < M; j++)
                {
                    // присваиваем значение клетке
                    // почему -2? Потому что таблица смещена относительно [0,0]
                    // а массив строк начинается 0
                    dataGridView1.Rows[i].Cells[j].Value = Convert.ToInt32(chisl[j-2]);
                }

            }

            // закрываем поток чтения
            sr.Close();

            MessageBox.Show("Игра была загружена из файла!", "Сообщение");
        }

        //---------------------------------------------------------------------------------------------------------------------
        // У Д А Л Е Н И Е  С Т Р О Г О  Д О М И Н И Р У Ю Щ И Х  С Т Р А Т Е Г И Й     
        //---------------------------------------------------------------------------------------------------------------------
        private void button4_Click(object sender, EventArgs e)
        {
            remove_strictly_dominant();
            MessageBox.Show("Строго доминирующие стратегии удалены!", "Сообщение");
        }

        //---------------------------------------------------------------------------------------------------------------------
        // У Д А Л Е Н И Е  С Л А Б О  Д О М И Н И Р У Ю Щ И Х  С Т Р А Т Е Г И Й     
        //---------------------------------------------------------------------------------------------------------------------
        private void button5_Click(object sender, EventArgs e)
        {
            remove_weakly_dominant();
            MessageBox.Show("Слабо доминирующие стратегии удалены!", "Сообщение");
        }
    }
}

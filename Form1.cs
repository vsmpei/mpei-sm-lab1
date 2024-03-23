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

        // ПРОВЕРКА ИНИЦИАЛИЗАЦИИ МАТРИЦЫ (DataGridView)
        private bool initial_check()
        {
            // размерность матрицы
            int N = dataGridView1.Rows.Count;
            int M = dataGridView1.Columns.Count;

            // проверка размерности матрицы
            if ((M > 0) && (N > 0))
            {
                // если инициализирована 
                return true;
            }
            else // иначе...
            {
                // если НЕ инициализирована
                return false;
            }

        }

        // СУЩЕСТВОВАНИЯ ФАЙЛА С ЗАДАННЫМ ИМЕНЕМ 
        private bool file_check(string file_name)
        {
            // проверка существования 
            if (File.Exists(file_name))
            {
                // если существует
                return true;
            }
            else // иначе...
            {
                return false;
            }
        }

        // ЗАГРУЗКА ИЗ ФАЙЛА 
        private bool load_from_file (string file_name)
        {
            // строка для чтения из файла
            string read_str = "";
            // массив строк "чисел" из файла, которые будут в дальнейшем приведены к int
            String[] chisl;
            // размерность будущей матрицы
            int N, M; // число строк и столбцов 

            // инициализируем поток чтения 
            StreamReader sr = new StreamReader(file_name);

            // читаем первую строку с количеством стратегий первого игрока
            N = Convert.ToInt32(sr.ReadLine()) + 2; // отступы, первая и вторая строка заняты...
            // читаем вторую строку с количеством стратегий второго игрока
            M = Convert.ToInt32(sr.ReadLine()) + 2; // отступы, первая и вторая строка заняты...

            // проверка считанной размерности из матрицы
            if ((M > 2) || (N > 2))
            {
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
                        dataGridView1.Rows[i].Cells[j].Value = Convert.ToInt32(chisl[j - 2]);
                    }
                 
                }
                // закрываем поток чтения
                sr.Close();

                return true;
            }
            else // ошибка при чтении размерности матрицы весов
            {
                MessageBox.Show("Размерность матрицы весов в файле указана ошибочно!", "Ошибка");
                // закрываем поток чтения
                sr.Close();

                return false;
            }

        }

        // ВЫГРУЗКА В ФАЙЛ
        private void write_into_file(string file_name)
        {
            // если матрица инициализирована 
            if (initial_check())
            {
                // размерность таблицы
                int N = dataGridView1.Rows.Count;
                int M = dataGridView1.Columns.Count;

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
                for (int j = 2; j < N; j++)
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
                for (int i = 2; i < M; i++)
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
                MessageBox.Show("Игра выгружена в файл!", "Сообщение");
            }
            else // иначе...
            {
                MessageBox.Show("Сначала инициализируйте матрицу!", "Ошибка");
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

            // количество строк, которые доминируют над данной 
            int outsider_rating = 0;
            // количество столбцов, над которыми доминирует данный
            int dominance_rating = 0; // ~dominance rating
             
            // флаг выхода и цикла по элементам, в случае
            // если хотя бы элемент строки j больше элемента i
            bool flag_k = true;

            // П О  С Т Р О К А М 
            // c первой строки
            while (i < N)
            {

                // начиная со второй...
                j = 2;
                // переходим к следующему циклу проверок 
                outsider_rating = 0; 

                // со второй строки 
                //while ((j < N) && (flag_k == true))
                while (j < N) 
                {
                    // с первого столбца  
                    k = 2;

                    // новый цикл проверок 
                    flag_k = true;

                    // нет смысла сравнивать саму с собой
                    if (j != i)
                    {

                        // по столбцам (по-элементно)
                        while ((k < M) && (flag_k == true))
                        {
                            // проверка сравнения
                            //MessageBox.Show(Convert.ToString(dataGridView1.Rows[i].Cells[k].Value) + " и " + Convert.ToString(dataGridView1.Rows[j].Cells[k].Value));

                            // если хотя бы один элемент у строки i БОЛЬШЕ ИЛИ РАВЕН элементу строки j => она НЕ ДОМИНИРУЕМА СТРОГО
                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells[k].Value) >= Convert.ToInt32(dataGridView1.Rows[j].Cells[k].Value))
                            {
                                //MessageBox.Show(Convert.ToString(dataGridView1.Rows[i].Cells[k].Value) + " и " + Convert.ToString(dataGridView1.Rows[j].Cells[k].Value));
                                // выходим из цикла по элементам
                                flag_k = false;
                            }

                            //MessageBox.Show(Convert.ToString(k));
                            // переходим к следующему элементу 
                            k++;
                        }

                        // если флаг не был опущен =>
                        // => строка i ДОМИНИРУЕМА СТРОГО
                        if (flag_k == true)
                        {
                            // увеличиваем рейтинг лузера 
                            outsider_rating++;
                        }
                    }

                    //MessageBox.Show(Convert.ToString(j));

                    // переходим к следующей строке
                    j++;
                }

                // если рейтинг лузера > 0, т.е.
                // над данной строкой доминирует хотя бы одна другая строка =>
                // => записываем её индекс в множество
                if (outsider_rating > 0)
                {
                    // записываем номер строки 
                    row_nums.Add(i);
                }
       
                // переходим к следующей строке (паре строк)
                i++;
            }

            // если вообще были найдены такие строки...
            if (row_nums.Count != 0)
            {
                // цикл по стеку
                foreach (var ind in row_nums.Reverse())
                {
                    // удаляем строки с найденными номерами...
                    dataGridView1.Rows.RemoveAt(ind);
                    //MessageBox.Show(Convert.ToString(ind));
                }
            }


            // обновляем размерность таблицы
            N = dataGridView1.Rows.Count;
            M = dataGridView1.Columns.Count;

            // НАЧИНАЯ с первого столбца для первого цикла 
            i = 2;
            // НАЧИНАЯ со второго столбца для второго цикла 
            j = 3;
            // количество столбцов, над которыми доминирует данный
            dominance_rating = 0; // ~dominance ratin  
            // флаг выхода и цикла по элементам, в случае
            // если хотя бы элемент столбца j больше элемента столбца i
            flag_k = true;

            // П О  С Т О Л Б Ц А М  
            // c первого столбца строки
            while (i < M)
            {
                // начиная со второго...
                j = 2;
                // переходим к следующему циклу проверок 
                dominance_rating = 0;

                // со второго столбца
                while (j < M)
                {
                    // с первой строки  
                    k = 2;

                    // новый цикл проверок 
                    flag_k = true;

                    // нет смысла сравнивать сам с собой
                    if (j != i)
                    {

                        // по строкам (по-элементно)
                        while ((k < N) && (flag_k == true))
                        {
                            // проверка сравнения
                            //MessageBox.Show(Convert.ToString(dataGridView1.Rows[k].Cells[i].Value) + " и " + Convert.ToString(dataGridView1.Rows[k].Cells[j].Value));

                            // если хотя бы один элемент у i столбца МЕНЬШЕ чем у jго столбца => jй доминирует
                            if (Convert.ToInt32(dataGridView1.Rows[k].Cells[i].Value) <= Convert.ToInt32(dataGridView1.Rows[k].Cells[j].Value))
                            {
                                //MessageBox.Show(Convert.ToString(dataGridView1.Rows[i].Cells[k].Value) + " и " + Convert.ToString(dataGridView1.Rows[j].Cells[k].Value));
                                // выходим из цикла по элементам
                                flag_k = false;
                            }

                            //MessageBox.Show(Convert.ToString(k));
                            // переходим к следующему элементу 
                            k++;
                        }

                        // если флаг не был опущен =>
                        // => столбец i СТРОГО ДОМИНИРУЕТ над столбцом j
                        if (flag_k == true)
                        {
                            // увеличиваем доминирование
                            dominance_rating++;
                        }
                    }

                    //MessageBox.Show(Convert.ToString(j));

                    // переходим к следующему столбцу
                    j++;
                }

                // если рейтинг доминирования > 0, т.е.
                // данный столбец доминирует хотя бы над одним столбцом =>
                // => записываем его индекс в множество
                if (dominance_rating > 0)
                {
                    // записываем номер столбца 
                    column_nums.Add(i);
                }

                // переходим к следующему столбцу (паре столбцов)
                i++;
            }

            // если вообще были найдены такие строки...
            if (column_nums.Count != 0)
            {
                // цикл по стеку
                foreach (var ind in column_nums.Reverse())
                {
                    // удаляем столбцы с найденными номерами...
                    dataGridView1.Columns.RemoveAt(ind);
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

            // количество строк, которые доминируют над данной 
            int outsider_rating = 0;
            // количество столбцов, над которыми доминирует данный
            int dominance_rating = 0; // ~dominance rating

            // флаг выхода и цикла по элементам, в случае
            // если хотя бы элемент строки j больше элемента i
            bool flag_k = true;

            // П О  С Т Р О К А М 
            // c первой строки
            while (i < N)
            {

                // начиная со второй...
                j = 2;
                // переходим к следующему циклу проверок 
                outsider_rating = 0;

                // со второй строки 
                //while ((j < N) && (flag_k == true))
                while (j < N)
                {
                    // с первого столбца  
                    k = 2;

                    // новый цикл проверок 
                    flag_k = true;

                    // нет смысла сравнивать саму с собой
                    if (j != i)
                    {

                        // по столбцам (по-элементно)
                        while ((k < M) && (flag_k == true))
                        {
                            // проверка сравнения
                            //MessageBox.Show(Convert.ToString(dataGridView1.Rows[i].Cells[k].Value) + " и " + Convert.ToString(dataGridView1.Rows[j].Cells[k].Value));

                            // если хотя бы один элемент у строки i БОЛЬШЕ чем у строки j
                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells[k].Value) > Convert.ToInt32(dataGridView1.Rows[j].Cells[k].Value))
                            {
                                //MessageBox.Show(Convert.ToString(dataGridView1.Rows[i].Cells[k].Value) + " и " + Convert.ToString(dataGridView1.Rows[j].Cells[k].Value));
                                // выходим из цикла по элементам
                                flag_k = false;
                            }

                            //MessageBox.Show(Convert.ToString(k));
                            // переходим к следующему элементу 
                            k++;
                        }

                        // если флаг не был опущен =>
                        // => строка j строка доминирует над i
                        if (flag_k == true)
                        {
                            // увеличиваем рейтинг лузера 
                            outsider_rating++;
                        }
                    }

                    //MessageBox.Show(Convert.ToString(j));

                    // переходим к следующей строке
                    j++;
                }

                // если рейтинг лузера > 0, т.е.
                // над данной строкой доминирует хотя бы одна другая строка =>
                // => записываем её индекс в множество
                if (outsider_rating > 0)
                {
                    // записываем номер строки 
                    row_nums.Add(i);
                }

                // переходим к следующей строке (паре строк)
                i++;
            }

            // если вообще были найдены такие строки...
            if (row_nums.Count != 0)
            {
                // цикл по стеку
                foreach (var ind in row_nums.Reverse())
                {
                    // удаляем строки с найденными номерами...
                    dataGridView1.Rows.RemoveAt(ind);
                    //MessageBox.Show(Convert.ToString(ind));
                }
            }


            // обновляем размерность таблицы
            N = dataGridView1.Rows.Count;
            M = dataGridView1.Columns.Count;

            // НАЧИНАЯ с первого столбца для первого цикла 
            i = 2;
            // НАЧИНАЯ со второго столбца для второго цикла 
            j = 3;
            // количество столбцов, над которыми доминирует данный
            dominance_rating = 0; // ~dominance ratin  
            // флаг выхода и цикла по элементам, в случае
            // если хотя бы элемент столбца j больше элемента столбца i
            flag_k = true;

            // П О  С Т О Л Б Ц А М  
            // c первого столбца строки
            while (i < M)
            {
                // начиная со второго...
                j = 2;
                // переходим к следующему циклу проверок 
                dominance_rating = 0;

                // со второго столбца
                while (j < M)
                {
                    // с первой строки  
                    k = 2;

                    // новый цикл проверок 
                    flag_k = true;

                    // нет смысла сравнивать сам с собой
                    if (j != i)
                    {

                        // по строкам (по-элементно)
                        while ((k < N) && (flag_k == true))
                        {
                            // проверка сравнения
                            //MessageBox.Show(Convert.ToString(dataGridView1.Rows[k].Cells[i].Value) + " и " + Convert.ToString(dataGridView1.Rows[k].Cells[j].Value));

                            // если хотя бы один элемент у первого столбца меньше чем у второго...
                            if (Convert.ToInt32(dataGridView1.Rows[k].Cells[i].Value) < Convert.ToInt32(dataGridView1.Rows[k].Cells[j].Value))
                            {
                                //MessageBox.Show(Convert.ToString(dataGridView1.Rows[i].Cells[k].Value) + " и " + Convert.ToString(dataGridView1.Rows[j].Cells[k].Value));
                                // выходим из цикла по элементам
                                flag_k = false;
                            }

                            //MessageBox.Show(Convert.ToString(k));
                            // переходим к следующему элементу 
                            k++;
                        }

                        // если флаг не был опущен =>
                        // => столбец i доминирует над столбцом j
                        if (flag_k == true)
                        {
                            // увеличиваем доминирование
                            dominance_rating++;
                        }
                    }

                    //MessageBox.Show(Convert.ToString(j));

                    // переходим к следующему столбцу
                    j++;
                }

                // если рейтинг доминирования > 0, т.е.
                // данный столбец доминирует хотя бы над одним столбцом =>
                // => записываем его индекс в множество
                if (dominance_rating > 0)
                {
                    // записываем номер столбца 
                    column_nums.Add(i);
                }

                // переходим к следующему столбцу (паре столбцов)
                i++;
            }

            // если вообще были найдены такие строки...
            if (column_nums.Count != 0)
            {
                // цикл по стеку
                foreach (var ind in column_nums.Reverse())
                {
                    // удаляем столбцы с найденными номерами...
                    dataGridView1.Columns.RemoveAt(ind);
                }
            }

        }

        // МЕТОД, СОЗДАЮЩИЙ И ВЕДУЩИЙ ФАЙЛ ЛОГОВ
        private void log_writer(int log_type)
        {
            // имя файла логов 
            string file_name = "log.txt";

            // инициализируем поток 
            StreamWriter sw = new StreamWriter(file_name, true);

            // ведение логов в зависимости от пришедшего типа
            switch (log_type)
            {
                case 0:
                    sw.WriteLine(DateTime.Now + " " + "Сообщение: " + "Иницицализация приложения");
                    break;
                case 1:
                    sw.WriteLine(DateTime.Now + " " + "Сообщение: " + "Иницицализация новой игры");
                    break;
                case 2:
                    sw.WriteLine(DateTime.Now + " " + "Сообщение: " + "Задание новой стратегии");
                    break;
                case 3:
                    sw.WriteLine(DateTime.Now + " " + "Сообщение: " + "Добавление весов");
                    break;
                case 4:
                    sw.WriteLine(DateTime.Now + " " + "Сообщение: " + "Загрузка игры из файла");
                    break;
                case 5:
                    sw.WriteLine(DateTime.Now + " " + "Сообщение: " + "Выгрузка игры в файл");
                    break;
                case 6:
                    sw.WriteLine(DateTime.Now + " " + "Сообщение: " + "Случайная генерация весов");
                    break;
                case 7:
                    sw.WriteLine(DateTime.Now + " " + "Сообщение: " + "Поиск максимина");
                    break;
                case 8:
                    sw.WriteLine(DateTime.Now + " " + "Сообщение: " + "Поиск минимакса");
                    break;
                case 9:
                    sw.WriteLine(DateTime.Now + " " + "Сообщение: " + "Удаление строго доминируемых стратегий");
                    break;
                case 10:
                    sw.WriteLine(DateTime.Now + " " + "Сообщение: " + "Удаление слабо доминируемых стратегий");
                    break;
                case 101:
                    sw.WriteLine(DateTime.Now + " " + "Ошибка: " + "Попытка выполнить операцию при отсутствии инциализации");
                    break;
                case 102:
                    sw.WriteLine(DateTime.Now + " " + "Ошибка: " + "Попытка чтения отсутствующего файла");
                    break;
                case 103:
                    sw.WriteLine(DateTime.Now + " " + "Ошибка: " + "Содержимое файла с данными повреждено");
                    break;
                case 104:
                    sw.WriteLine(DateTime.Now + " " + "Ошибка: " + "Попытка добавления стратегии для несуществующего игрока");
                    break;
                case 105:
                    sw.WriteLine(DateTime.Now + " " + "Ошибка: " + "Попытка добавления веса отсутствующий паре стратегий");
                    break;
                default:
                    sw.WriteLine(DateTime.Now + " " + "Предупреждение: " + "Неизвестная ошибка");
                    break;
            }

            // закрытие потока
            sw.Close();
        }

        // СОБЫТИЕ ПРИ СОЗДАНИИ ФОРМЫ
        private void Form1_Load(object sender, EventArgs e)
        {
            // вызов метода, ведущего лог файл
            log_writer(0);

            // успешное создание формы 
            MessageBox.Show("Дорогой пользователь, добро пожаловать! Только что Вы зашли в приложение, написанное" +
                            " в качестве проекта для лабораторной работы №1 по дисциплине \"Операционные системы\". " +
                            "Данное приложение не поддерживает биматричные модели, однако позволяет делать много чего другого. "+
                            "Например, детально настраивать игровую матрицу, а так же искать максимин, минимакс, удалять" +
                            " строго доминирующие, а так же слабо доминирующие стратегии!" , "Сообщение");

            // имя формы 
            this.Text = "Лабораторная работа 1";

            // полупрозрачные GroupBox
            groupBox1.BackColor = Color.FromArgb(100, 255, 255, 255);
            groupBox2.BackColor = Color.FromArgb(100, 255, 255, 255);
            groupBox3.BackColor = Color.FromArgb(100, 255, 255, 255);
            groupBox4.BackColor = Color.FromArgb(100, 255, 255, 255);
            groupBox5.BackColor = Color.FromArgb(100, 255, 255, 255);

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
            textBox10.Text = "Давить на жалость"; 
            textBox11.Text = "Поставить 2"; 
            textBox12.Text = "1000";
            // стандартные значения для загрузки / выгрузки файлов
            textBox13.Text = "GAME_INPUT.txt";
            textBox14.Text = "GAME_RESULTS.txt";

            // добавление строк ВРУЧНУЮ
            dataGridView1.AllowUserToAddRows = false;
        }

        //---------------------------------------------------------------------------------------------------------------------
        // С О З Д А Н И Е  И Г Р О В О Г О  П О Л Я 
        //---------------------------------------------------------------------------------------------------------------------
        private void button1_Click_1(object sender, EventArgs e)
        {
            // вызов метода ведения логов
            log_writer(1);

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
            // вызов метода ведения логов
            log_writer(2);

            // проверка инициализации
            if (initial_check())
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
                    log_writer(104);
                    MessageBox.Show("Игрок с таким именем не найден!", "Ошибка");
                }
            }
            else // иначе...
            {
                log_writer(101);
                MessageBox.Show("Сначала инициализируйте матрицу!", "Ошибка");
            }
        }

        //---------------------------------------------------------------------------------------------------------------------
        //  Д О Б А В Л Е Н И Е  В Е С А  
        //---------------------------------------------------------------------------------------------------------------------
        private void button6_Click(object sender, EventArgs e)
        {

            // проверка инициализации матрицы
            if (initial_check())
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
                    // вызов метода ведения логов
                    log_writer(3);
                    MessageBox.Show("Вес добавлен!", "Сообщение");
                }
                else
                {
                    log_writer(105);
                    MessageBox.Show("Одна или обе стратегии отсутствуют в таблице!", "Ошибка");
                }
            }
            else // иначе...
            {
                log_writer(101);
                MessageBox.Show("Сначала инициализируйте матрицу!", "Ошибка");
            }

        }

        //---------------------------------------------------------------------------------------------------------------------
        //  П О И С К  М А К С И М И Н А  И  М И Н И М А К С А   
        //---------------------------------------------------------------------------------------------------------------------
        private void button3_Click(object sender, EventArgs e)
        {
            // проверка инициализации матрицы
            if (initial_check())
            {
                // поиск максимина 
                int maximin = find_maximin();
                // вывод максиммина в текстбокс
                textBox8.Text = Convert.ToString(maximin);
                // вызов метода ведения логов
                log_writer(7);
                // поиск минимакса 
                int minimax = find_minimax();
                // вывод минимакса в текстбокс
                textBox9.Text = Convert.ToString(minimax);
                // вызов метода ведения логов
                log_writer(8);
                // сообщение об поиске максимина и минимакса
                MessageBox.Show("Поиск максимина и минимакса!", "Сообщение");
            }
            else // иначе...
            {
                log_writer(101);
                MessageBox.Show("Сначала инициализируйте матрицу!", "Ошибка");
            }    
        }

        //---------------------------------------------------------------------------------------------------------------------
        //  В Ы Г Р У З К А  В  Ф А Й Л   
        //---------------------------------------------------------------------------------------------------------------------
        private void button7_Click(object sender, EventArgs e)
        {
            // вызов метода ведения логов
            log_writer(5);

            // имя файла 
            string file_name = textBox14.Text;

            // вызов метода выгрузки в файл
            write_into_file(file_name);
           
        }

        //---------------------------------------------------------------------------------------------------------------------
        //  Р А Н Д О М Н Ы Е  З Н А Ч Е Н И Я  В Е С О В   
        //---------------------------------------------------------------------------------------------------------------------
        private void button8_Click(object sender, EventArgs e)
        {

            // проверка инициализации матрицы
            if (initial_check())
            {
                // вызов метода ведения логов
                log_writer(6);
                initial_random();
                MessageBox.Show("Рандомные значения весов сгенерированы!", "Сообщение");
            }
            else // иначе...
            {
                log_writer(101);
                MessageBox.Show("Сначала инициализируйте матрицу!", "Ошибка");
            }
        }

        //---------------------------------------------------------------------------------------------------------------------
        //  З А Г Р У З К А  И З  Ф А Й Л А    
        //---------------------------------------------------------------------------------------------------------------------
        private void button9_Click(object sender, EventArgs e)
        {

            // имя файла 
            string file_name = textBox13.Text;
            
            // проверка существования файла 
            if(file_check(file_name)) // если существует
            {
                if (load_from_file(file_name))
                {
                    // вызов метода ведения логов
                    log_writer(4);
                    MessageBox.Show("Игра была загружена из файла!", "Сообщение");
                }
                else 
                {
                    // вызов метода ведения логов
                    log_writer(103);
                    MessageBox.Show("Игра не была загружена из файла!", "Сообщение");
                }
            }
            else // иначе...
            {
                // вызов метода ведения логов
                log_writer(102);
                MessageBox.Show("Файл с данным именем отсутствует!", "Ошибка");
            }
            
        }

        //---------------------------------------------------------------------------------------------------------------------
        // У Д А Л Е Н И Е  С Т Р О Г О  Д О М И Н И Р У Ю Щ И Х  С Т Р А Т Е Г И Й     
        //---------------------------------------------------------------------------------------------------------------------
        private void button4_Click(object sender, EventArgs e)
        {
            // проверка инициализации матрицы
            if (initial_check())
            {
                remove_strictly_dominant();
                log_writer(9);
                MessageBox.Show("Строго доминируемые стратегии удалены!", "Сообщение");
            }
            else // иначе...
            {
                log_writer(101);
                MessageBox.Show("Сначала инициализируйте матрицу!", "Ошибка");
            }
        }

        //---------------------------------------------------------------------------------------------------------------------
        // У Д А Л Е Н И Е  С Л А Б О  Д О М И Н И Р У Ю Щ И Х  С Т Р А Т Е Г И Й     
        //---------------------------------------------------------------------------------------------------------------------
        private void button5_Click(object sender, EventArgs e)
        {
            // проверка инициализации матрицы
            if (initial_check())
            {
                remove_weakly_dominant();
                log_writer(10);
                MessageBox.Show("Слабо доминируемые стратегии удалены!", "Сообщение");
            }
            else // иначе...
            {
                log_writer(101);
                MessageBox.Show("Сначала инициализируйте матрицу!", "Ошибка");
            }
        }

        //---------------------------------------------------------------------------------------------------------------------
        // П Р О С М О Т Р  И С Т О Р И И  И З М Е Н Е Н И Й      
        //---------------------------------------------------------------------------------------------------------------------
        private void button10_Click(object sender, EventArgs e)
        {
            // инициализируем новую форму
            Form2 fr = new Form2();
            // заголовок новой формы
            fr.Text = "История изменений";
            // показываем форму с историей
            fr.Show();
        }
    }
}

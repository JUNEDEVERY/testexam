using System;

namespace Задание_2
{
    class Program
    {
        //Интерполирование
        static void Main(string[] args)
        {
            Console.Write("Введите  количество коэффициентов: ");
            int k = Convert.ToInt32(Console.ReadLine()); // Количество переменных
            double[,] f = new double[2, k]; // Значение функции
            Console.WriteLine("Заполните значения:"); // Заполнение таблицы значений функции
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    if (i == 0)
                    {
                        Console.Write("x[{0}]=> ", j + 1);
                    }
                    else Console.Write("q(x)[{0}]=> ", j + 1);
                    f[i, j] = Convert.ToDouble(Console.ReadLine());
                }
            }
            int s = k - 1; // Степень искомого многочлена
            double[,] massive = new double[k, k]; // Масссив значений
            int l = 0; // Счётчик для прохождения по таблицы значений функции
            for (int i = 0; i < k; i++)
            {
                int st = s; // Степень
                for (int j = 0; j < k; j++)
                {
                    massive[i, j] = Math.Pow(f[0, l], st);
                    st--;
                }
                l++;
            }
            double oprMain; // Определитель матрицы
            oprMain = opredel(massive); // Вызов метода, который находит определитель
            if (oprMain == 0) // Проверка на то, что если определитель = 0
            {
                Console.WriteLine("Решить данное уравнение методом Крамера нельзя, так как определитель = 0");
                return;
            }
            else // Если не равен нулю, то находим с помощью метода Крамера
            {
                double[] result = new double[massive.GetLength(1)]; // Массив, который будет хранить значения коэффицентов
                for (int i = 0; i < massive.GetLength(1); i++) // цикл, который сформирует новый массив, где столбец поменен на столбец свободных коэфициентов, а также посчитает коэфициент
                {
                    double[,] massive1 = new double[massive.GetLength(0), massive.GetLength(1)]; // Новый массив, где столбец поменен на столбец свободных коэфициентов
                    for (int p = 0; p < massive.GetLength(0); p++) // Копирование исходной матрицы
                    {
                        for (int j = 0; j < massive.GetLength(1); j++)
                        {
                            massive1[p, j] = massive[p, j];
                        }
                    }
                    double[] kolvo = new double[massive.GetLength(1)]; // Переменная, которая хранит определители новых матриц
                    for (int j = 0; j < massive.GetLength(0); j++) // Замена столбца, на столбец свободных коэфициентов
                    {
                        massive1[j, i] = f[1, j];
                    }
                    kolvo[i] = opredel(massive1);
                    result[i] = kolvo[i] / oprMain;
                }
                for (int i = 0; i < result.Length; i++) // Вывод значений всех коэфициентов
                {
                    Console.WriteLine(i + 1 + ") коэфициент: " + result[i]);
                }
                int st = s; // Переменная для степени
                for (int i = 0; i < result.Length - 1; i++) // Вывод уравнения
                {
                    Console.Write(Math.Round(result[i], 4) + "*x^" + st + " + ");
                    st--;
                }
                Console.Write(Math.Round(result[result.Length - 1], 4));
            }
        }
        static double opredel(double[,] massive) // Метод, который вычисляет определитель
        {
            double opr = 0; // Переменная, которая подсчитывает определитель
            if (massive.GetLength(1) != 2) // Если определитель больше 2 порядка
            {
                double[,] massive1 = new double[massive.GetLength(0) - 1, massive.GetLength(1) - 1]; // Новый массив у которого нужно найти определитель
                for (int j = 0; j < massive.GetLength(1); j++) // Цикл, по первой строчке массива
                {
                    int y = 0;
                    int p = 0;
                    for (int q = 0; q < massive.GetLength(0); q++) // Цикл,который формирует новый массив у которого нужно найти определитель
                    {
                        for (int z = 0; z < massive.GetLength(1); z++)
                        {
                            if (q != 0 && z != j)
                            {
                                massive1[y, p] = massive[q, z];
                                if (p == massive.GetLength(1) - 1)
                                {
                                    p = 0;
                                    y++;
                                }
                                else
                                {
                                    p++;
                                }
                            }
                        }
                        if (p == massive.GetLength(1) - 1)
                        {
                            p = 0;
                            y++;
                        }
                    }
                    if (j % 2 == 0) // Если элемент, находится на чётном месте, то + иначе -
                    {
                        opr += massive[0, j] * opredel(massive1);
                    }
                    else
                    {
                        opr -= massive[0, j] * opredel(massive1);
                    }
                }
            }
            else if (massive.GetLength(1) == 2) // Если определитель 2 порядка, то его можно вычислить
            {
                opr = massive[0, 0] * massive[1, 1] - massive[0, 1] * massive[1, 0];
            }
            return opr;
        }
    }
}

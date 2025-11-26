using System;
using System.Reflection;
using System.Security.Cryptography;
namespace Lab_4
{
    class Program
    {
        /// <summary>
        /// функция для ввода числа пользователем 
        /// </summary>
        /// <param name="text">строка перед вводом</param>
        /// <returns>число</returns>
        static int GetNum(string text = "")
        {
            // возможность конвертации в int и проверка переполнения
            bool isConvert = false, isOverflowed = true; 
            int n = -1;

            do
            {
                try
                {
                    Console.Write(text);
                    n = int.Parse(Console.ReadLine());
                    isConvert = true;
                    isOverflowed = false;
                }
                catch (OverflowException) // проверка переполнения типа int
                {
                    Console.WriteLine("Число выходит за границы!\n");
                    isOverflowed = true;
                }
                catch (FormatException) // проверка типа введенных данных
                {
                    Console.WriteLine("Некорректный ввод!\n");
                    isConvert = false;
                }
            } while (!isConvert || isOverflowed);

            return n;
        }

        /// <summary>
        /// функция для ввода длины массива пользователем 
        /// </summary>
        /// <returns>длина</returns>
        static int GetLength()
        {
            int length;

            do
            {
                length = GetNum("Введите длину массива: ");
                if (length < 0)
                {
                    Console.WriteLine("Длина массива должна быть положительной!\n");
                }
            } while (length < 0);
            return length;
        }

        /// <summary>
        /// функция для печати меню
        /// </summary>
        /// <param name="k">набор сообщений</param>
        static void PrintMenu(int k)
        {
            switch (k)
            {
                case 1:
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Добро пожаловать! Данная программа умеет работать с массивами, для начала работы выберите способ формирования массива.");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                    break;
                case 2:
                    Console.WriteLine("1. Сформировать массив с помощью ДСЧ.");
                    Console.WriteLine("2. Сформировать массив вручную.");
                    Console.WriteLine("3. Выход\n");
                    break;
                case 3:
                    Console.WriteLine("Выберите пункт из меню ниже:");
                    Console.WriteLine("1. Распечатать массив.");
                    Console.WriteLine("2. Выполнить удаление элемента.");
                    Console.WriteLine("3. Выполнить добавление элементов.");
                    Console.WriteLine("4. Выполнить перестановку элементов.");
                    Console.WriteLine("5. Выполнить поиск элемента.");
                    Console.WriteLine("6. Выполнить сортировку простым включением.");
                    Console.WriteLine("7. Выполнить бинарный поиск элемента.");
                    Console.WriteLine("8. Выполнить сортировку Хоара.");
                    Console.WriteLine("9. Выполнить сортировку Шелла.");
                    Console.WriteLine("10. Назад\n");
                    break;
            }
        }

        /// <summary>
        /// фунцкия для формирования массива с помощью ДСЧ
        /// </summary>
        /// <param name="length">длина массива</param>
        /// <param name="min">минимум</param>
        /// <param name="max">максимум</param>
        /// <returns>массив сформированный с помощью ДСЧ</returns>
        static int[] CreateArrayRandom(int length, int min, int max) 
        {
            int[] array = new int[length];
            Random rand = new Random();
            
            for (int i = 0; i < length; i++)
            {
                array[i] = rand.Next(min, max); // формирование элемента с помощью ДСЧ
            }
            
            return array;
        }
        
        /// <summary>
        /// фунцкия для формирования массива вручную
        /// </summary>
        /// <param name="length">длина массива</param>
        /// <returns>массив сформированный вручную</returns>
        static int[] CreateArrayUser(int length)
        {
            int[] array = new int[length];
            
            for (int i = 0; i < length; i++)
            {
                array[i] = GetNum(); // ввод элемента с клавиатуры
            }
            
            return array;
        }

        /// <summary>
        /// функция для печати массива
        /// </summary>
        /// <param name="array">массив для печати</param>
        /// <param name="length">длина массива</param>
        static void PrintArray(int[] array, int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write(array[i] + " "); // печать элемента
            }
        }

        /// <summary>
        /// функция для удаления элемента из массива 
        /// </summary>
        /// <param name="array">массив для удаления</param>
        /// <param name="length">длина массива</param>
        /// <param name="delete">элемент для удаления</param>
        /// <returns>новый массив и новая длина</returns>
        static (int[], int) DeleteUnit(int[] array, int length, int delete)
        {
            int[] newArray = new int[length - 1];

            // перенос массива до delete
            for (int i = 0; i < delete; i++)
            {
                newArray[i] = array[i];
            }
            
            // перенос массива после delete
            for (int i = delete; i < length - 1; i++)
            {
                newArray[i] = array[i + 1];
            }

            return (newArray, length - 1);
        }


        /// <summary>
        /// функция для добавления элемента в массива 
        /// </summary>
        /// <param name="array">массив для добавления</param>
        /// <param name="length">длина массива</param>
        /// <param name="n">место начала добавления</param>
        /// <param name="k">количество элементов для добавления</param>
        /// <returns>массив и длина</returns>
        static (int[], int) AddElements(int[] array, int length, int n, int k)
        {
            int startAdd = n - 1; // место начала добавления
            if (n == 0)
                startAdd = n;

            int endAdd = startAdd + k; // место конца добавления
            length += k; // обновление длины массива            
            int[] newArray = new int[length]; // создание нового массива
            Console.WriteLine("Введите элементы: ");
            
            for (int i = 0; i <= startAdd; i++) // перенос массива до места добавления
            {
                newArray[i] = array[i];
            }
            
            for (int i = startAdd + 1; i <= endAdd; i++) // добавление в массив
            {
                newArray[i] = GetNum();
            }
            
            for (int i = endAdd + 1; i < length; i++) // перенос массива после места добавления
            {
                newArray[i] = array[i - k];
            }

            return (newArray, length);

        }

        /// <summary>
        /// циклический сдвиг массива влево
        /// </summary>
        /// <param name="array">массив для сдвига</param>
        /// <param name="length">длина массива</param>
        /// <param name="m">количество элементом для сдвига</param>
        /// <returns>массив</returns>
        static int[] ShiftArray(int[] array, int length, int m)
        {
            m = m % length;
            // сдвиг нужное количество раз
            for (int p = 0; p < m; p++)
            {
                int first = array[0]; // запоминание первого элемента

                // сдвиг всех элементов влево на одну позицию
                for (int i = 0; i < length - 1; i++)
                {
                    array[i] = array[i + 1];
                }

                array[length - 1] = first; // помещаем первый элемент в конец
            }
            
            return array;
        }

        /// <summary>
        /// бинарный поиск
        /// </summary>
        /// <param name="array">массив для поиска</param>
        /// <param name="length">длина массива</param>
        /// <param name="target">элемент для поиска</param>
        static void DoBinarySearch(int[] array, int length, int target)
        {
            int left = 0;
            int right = length - 1;
            int count = 0; // счетчик

            while (left <= right)
            {
                int mid = left + (right - left) / 2; // ищем середину массива

                if (array[mid] == target) // элемент найден
                {
                    count++;
                    Console.WriteLine($"\nНомер найденного элемента: {mid + 1}. Кол-во требуемых сравнений: {count}.\n");
                    return; 
                }
                else if (array[mid] < target) // ищем в правой части
                {
                    left = mid + 1;
                    count++;
                }
                else // ищем в левой части
                {
                    right = mid - 1;
                    count++;
                }
            }
            Console.WriteLine("\nДанного элемента нет в массиве.\n");
        }

        /// <summary>
        /// сортировка массива простым включением (вставками)
        /// </summary>
        /// <param name="array">массив для сортировки</param>
        /// <param name="length">длина массива</param>
        static void DoInsertionSort(int[] array, int length)
        {
            for (int i = 1; i < length; i++)
            {
                // текущий элемент для вставки
                int sorted = i - 1;

                // сдвиг элементов, если они больше
                while (sorted > -1 && array[sorted] > array[sorted + 1])
                {
                    // обмен ячеек местами
                    (array[sorted], array[sorted + 1]) = (array[sorted + 1], array[sorted]);
                    sorted--;
                }
            }
        }

        /// <summary>
        /// поиск в неотсортированном массиве
        /// </summary>
        /// <param name="array">массив для поиска</param>
        /// <param name="target">элемент для поиска</param>
        static void SearchUnit(int[] array, int target)
        {
            int count = 0; // счетчик

            for (int i = 0; i < array.Length; i++)
            {
                count++;
                if (array[i] == target)
                {
                    Console.WriteLine($"\nИндекс найденного элемента: {i + 1}. Кол-во требуемых сравнений: {count}.\n");
                    return;
                }
            }

            Console.WriteLine("\nДанного элемента нет в массиве.\n");
        }

        /// <summary>
        /// часть для сортировки методом Хоара
        /// </summary>
        /// <param name="array">массив для сортировки</param>
        /// <param name="left">левая граница</param>
        /// <param name="right">правая граница</param>
        /// <returns>конец левой части массива</returns>
        static int PartSortHoar(int[] array, int left, int right)
        {
            int pivot = array[(left + right) / 2]; // нахождение опорного элемента

            while (left <= right)
            {
                while (array[left] < pivot) 
                    left++; // смещение левого указателя
                
                while (array[right] > pivot) 
                    right--; // смещение правого указателя
                
                if (left <= right)
                {
                    // обмен ячеек местами
                    (array[left], array[right]) = (array[right], array[left]);
                    left++;
                    right--;
                }
            }
            return left;
        }

        /// <summary>
        /// рекурсия для сортировки методом Хоара
        /// </summary>
        /// <param name="array">массив для сортировки</param>
        /// <param name="start">левая граница</param>
        /// <param name="end">правая граница</param>
        static void QuickSortHoar(int[] array, int start, int end)
        {
            if (start >= end) 
                return;

            int rightStart = PartSortHoar(array, start, end);
            QuickSortHoar(array, start, rightStart - 1); // рекурсивный вызов сортировки для левой части массива
            QuickSortHoar(array, rightStart, end); // рекурсивный вызов сортировки для правой части массива
        }
        
        /// <summary>
        /// быстрая сортировка Хоара
        /// </summary>
        /// <param name="array">массив для сортировки</param>
        static void DoQuickSortHoar(int[] array)
        {
            QuickSortHoar(array,0,array.Length-1);
        }

        /// <summary>
        /// быстрая сортировка методом Шелла (улучшенная сортировка вставками)
        /// </summary>
        /// <param name="array">массив для сортировки</param>
        /// <param name="length">длина массива</param>
        static void DoShellSort(int[] array, int length)
        {
            // начало с большого шага, затем меньше
            for (int gap = length / 2; gap > 0; gap /= 2)
            {
                // выполнение сортировки вставками с этим интервалом
                for (int i = gap; i < length; i++)
                {
                    int temp = array[i];
                    int j = i;

                    // перестановка элементов, находящихся на расстоянии gap
                    while (j >= gap && array[j - gap] > temp)
                    {
                        array[j] = array[j - gap];
                        j -= gap;
                    }
                    array[j] = temp;
                }
            }
        }
        
        static void Main(string[] args)
        {
            bool exit = false; // условие возврата или конца программы
            int length;
            int[] array;
            PrintMenu(1); // печать приветствия

            do 
            {
                PrintMenu(2); // печать меню для формирования массива
                int choice = GetNum("Введите число - пункт из меню: "); // выбор пункта из меню
                
                switch (choice)
                {
                    case 1:
                        {
                            Console.WriteLine("\nВы выбрали формирование массива с помощью ДСЧ.");
                            length = GetLength();
                            Console.WriteLine("Введите ограничения для элементов масива: ");
                            int min = GetNum("min: ");
                            int max = GetNum("max: ");
                            array = CreateArrayRandom(length, min, max);
                            Console.WriteLine("Массив сформирован.\n");

                            do
                            {
                                PrintMenu(3); // печать меню с функциями для изменения массива
                                choice = GetNum("Введите число - пункт из меню: ");

                                switch (choice)
                                {
                                    case 1:
                                        {
                                            Console.WriteLine("\nВы выбрали печать массива.\n");

                                            if (length == 0)
                                            {
                                                Console.WriteLine("Массив пустой!\n");
                                                break;
                                            }

                                            Console.Write("Ваш массив: ");
                                            PrintArray(array, array.Length);
                                            Console.WriteLine("\n");
                                            break;
                                        }
                                    case 2:
                                        {
                                            Console.WriteLine("\nВы выбрали удаление элемента массива.");
                                            int delete;
                                            
                                            if (length == 0)
                                            {
                                                Console.WriteLine("Массив пустой!\n");
                                                break;
                                            }

                                            do
                                            {
                                                delete = GetNum("Введите номер элемента который нужно удалить: ") - 1;
                                                if (delete < 0 || delete >= length)
                                                {
                                                    Console.WriteLine($"Ошибка: введите число от 1 до {length}!");
                                                }
                                            } while (delete < 0 || delete >= length);
                                            
                                            int item = array[delete];
                                            (array, length) = DeleteUnit(array, length, delete);
                                            Console.WriteLine($"Элемент {item} удален.\n");
                                            break;
                                        }
                                    case 3:
                                        {
                                            int k = 0, n = 0;
                                            Console.WriteLine("\nВы выбрали добавление элементов в массив.");

                                            do
                                            {
                                                k = GetNum("Введите количество элементов: ");
                                                if (k < 0)
                                                {
                                                    Console.WriteLine("Количество элементов должно быть положительным!");
                                                    continue;
                                                }
                                                n = GetNum("Введите номер элемента после которого нужно добавить элементы: ");
                                                if (n <= 0 || n >= length)
                                                {
                                                    Console.WriteLine("Неверно введен номер элемента!");
                                                    continue;
                                                }
                                            } while (k < 0 || n <= 0 || n >= length);

                                            if (k == 0)
                                            {
                                                Console.WriteLine("Элементы добавлены.\n");
                                                break;
                                            }

                                            (array, length) = AddElements(array, length, n, k);
                                            Console.WriteLine("Элементы добавлены.\n");
                                            break;
                                        }
                                    case 4:
                                        {
                                            int m;
                                            Console.WriteLine("\nВы выбрали сдвиг массива.");

                                            do
                                            {
                                                m = GetNum("Введите на сколько элементов нужно сдвинуть массив: "); // ввод количества сдвигов

                                                if (m < 0)
                                                {
                                                    Console.WriteLine("Количество элементов должно быть неотрицателным!");
                                                    continue;
                                                }
                                            } while (m < 0);

                                            if (m == 0)
                                            {
                                                Console.WriteLine("Элементы сдвинуты.\n");
                                                break;
                                            }

                                            ShiftArray(array, length, m);
                                            Console.WriteLine("Элементы сдвинуты.\n");
                                            break;
                                        }
                                    case 5:
                                        {
                                            Console.WriteLine("\nВы выбрали поиск элемента.");
                                            int target = GetNum("Введите значение элемента для поиска: ");
                                            SearchUnit(array, target);
                                            break;
                                        }
                                    case 6:
                                        {
                                            Console.WriteLine("\nВы выбрали сортировку массива простым включением");
                                            DoInsertionSort(array, length);
                                            Console.WriteLine("Массив отсортирован.\n");
                                            break;
                                        }
                                    case 7:
                                        {
                                            Console.WriteLine("\nВы выбрали бинарный поиск элемента.");
                                            DoQuickSortHoar(array); // сортировка массива методом Хоара
                                            int target = GetNum("Введите значение элемента для поиска: ");
                                            DoBinarySearch(array, length, target);
                                            break;
                                        }
                                    case 8:
                                        {
                                            Console.WriteLine("\nВы выбрали сортировку массива методом Хоара.");
                                            DoQuickSortHoar(array);
                                            Console.WriteLine("Массив отсортирован.\n");
                                            break;
                                        }
                                    case 9:
                                        {
                                            Console.WriteLine("\nВы выбрали сортировку массива методом Шелла.");
                                            DoShellSort(array, length);
                                            Console.WriteLine("Массив отсортирован.\n");
                                            break;
                                        }
                                    case 10:
                                        {
                                            exit = true;
                                            break;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("Некорректный ввод!");
                                            break;
                                        }
                                }
                            } while (!exit);
                            exit = false;
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("\nВы выбрали ручное формирование массива.");
                            length = GetLength();
                            Console.WriteLine("Введите элементы массива (каждый с новой строки): ");
                            array = CreateArrayUser(length);
                            Console.WriteLine("Массив сформирован.\n");

                            do
                            {
                                PrintMenu(3); // печать меню с функциями для изменения массива
                                choice = GetNum("Введите число - пункт из меню: ");

                                switch (choice)
                                {
                                    case 1:
                                        {
                                            Console.WriteLine("Вы выбрали печать массива.\n");

                                            if (length == 0)
                                            {
                                                Console.WriteLine("Массив пустой!\n");
                                                break;
                                            }

                                            Console.Write("Ваш массив: ");
                                            PrintArray(array, array.Length);
                                            Console.WriteLine("\n");
                                            break;
                                        }
                                    case 2:
                                        {
                                            Console.WriteLine("\nВы выбрали удаление элемента массива.");
                                            int delete;
                                            
                                            if (length == 0)
                                            {
                                                Console.WriteLine("Массив пустой!\n");
                                                break;
                                            }
                                                
                                            do
                                            {
                                                delete = GetNum("Введите номер элемента который нужно удалить: ") - 1;
                                                if (delete < 0 || delete >= length)
                                                {
                                                    Console.WriteLine($"Ошибка: введите число от 1 до {length}!");
                                                }
                                            } while (delete < 0 || delete >= length);
                                            
                                            int item = array[delete];
                                            (array, length) = DeleteUnit(array, length, delete);
                                            Console.WriteLine($"Элемент {item} удален.\n");
                                            break;
                                        }
                                    case 3:
                                        {
                                            int k = 0, n = 0; // k - количество элементов, n - номер элемента
                                            Console.WriteLine("\nВы выбрали добавление элементов в массив.");

                                            do
                                            {
                                                k = GetNum("Введите количество элементов: ");
                                                if (k < 0)
                                                {
                                                    Console.WriteLine("Количество элементов должно быть положительным!");
                                                    continue;
                                                }
                                                n = GetNum("Введите номер элемента после которого нужно добавить элементы: ");
                                                if (n <= 0 || n >= length)
                                                {
                                                    Console.WriteLine("Неверно введен номер элемента!");
                                                    continue;
                                                }
                                            } while (k < 0 || n <= 0 || n >= length);

                                            if (k == 0)
                                            {
                                                Console.WriteLine("Элементы добавлены.\n");
                                                break;
                                            }

                                            (array, length) = AddElements(array, length, n, k);
                                            Console.WriteLine("Элементы добавлены.\n");
                                            break;
                                        }
                                    case 4:
                                        {
                                            int m;
                                            Console.WriteLine("\nВы выбрали сдвиг массива.");

                                            do
                                            {
                                                m = GetNum("Введите на сколько элементов нужно сдвинуть массив: "); // ввод количества сдвигов

                                                if (m < 0)
                                                {
                                                    Console.WriteLine("Количество элементов должно быть неотрицателным!");
                                                    continue;
                                                }
                                            } while (m < 0);

                                            if (m == 0)
                                            {
                                                Console.WriteLine("Элементы сдвинуты.\n");
                                                break;
                                            }

                                            ShiftArray(array, length, m);
                                            Console.WriteLine("Элементы сдвинуты.\n");
                                            break;
                                        }
                                    case 5:
                                        {
                                            Console.WriteLine("\nВы выбрали поиск элемента.");
                                            int target = GetNum("Введите значение элемента для поиска: ");
                                            SearchUnit(array, target);
                                            break;
                                        }
                                    case 6:
                                        {
                                            Console.WriteLine("\nВы выбрали сортировку массива простым включением");
                                            DoInsertionSort(array, length);
                                            Console.WriteLine("Массив отсортирован.\n");
                                            break;
                                        }
                                    case 7:
                                        {
                                            Console.WriteLine("\nВы выбрали бинарный поиск элемента.");
                                            DoQuickSortHoar(array); // сортировка массива методом Хоара
                                            int target = GetNum("Введите значение элемента для поиска: ");
                                            DoBinarySearch(array, length, target);
                                            break;
                                        }
                                    case 8:
                                        {
                                            Console.WriteLine("\nВы выбрали сортировку массива методом Хоара.");
                                            DoQuickSortHoar(array);
                                            Console.WriteLine("Массив отсортирован.\n");
                                            break;
                                        }
                                    case 9:
                                        {
                                            Console.WriteLine("\nВы выбрали сортировку массива методом Шелла.");
                                            DoShellSort(array, length);
                                            Console.WriteLine("Массив отсортирован.\n");
                                            break;
                                        }
                                    case 10:
                                        {
                                            exit = true;
                                            break;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("Некорректный ввод!\n");
                                            break;
                                        }
                                }
                            } while (!exit);
                            exit = false;
                            break;
                        }
                    case 3:
                        {
                            exit = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Некорректный ввод!\n");
                            break;
                        }
                }
            } while (!exit);
            Console.WriteLine("\nРабота программы завершена.");
        }
    }
}



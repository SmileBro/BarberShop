using System;
using System.Collections.Generic;
using System.Text;

namespace BarberShop
{
    class Cuts
    {
        private List<Cut> cuts;
        private string path = "Cuts.txt";
        private string backup_path = "CutsBackup.txt";
        public Cuts()//Получение всех стрижек
        {
            List<string> list_of_items = Input.FromFile(path);
            List<Cut> list_of_cuts = new List<Cut>();
            if (list_of_items.Count == 0)
            {
                Console.WriteLine($"\nФайл {path} пустой\n");
            }
            else
            {
                try
                {

                    foreach (var w in list_of_items)
                    {
                        if (w != null && !String.IsNullOrEmpty(w))
                        {
                            string[] temp = w.Split();
                            Cut cut = new Cut(temp[0], temp[1], Int32.Parse(temp[2]));
                            list_of_cuts.Add(cut);
                        }
                    }
                    cuts = list_of_cuts;
                }
                catch(Exception e)
                {
                    Console.WriteLine($"\nНеправильные значения в файле {path}\n");
                    Console.WriteLine(e.Message);
                }
            }
        }
        public void ToStr()//Вывод стрижек на консоль
        {

            string s = "\nСписок стрижек: \n";
            int i = 0;
            if (cuts == null||cuts.Count == 0)
            {
                Console.WriteLine($"\nФайл {path} пустой\n");
            }
            else
            {
                foreach (var cut in cuts)
                {
                    if (cut != null)
                        s += $"{i} {cut.ToStr()}\n";
                    i++;
                }
                Console.WriteLine(s);
            }

        }
        public string Str()//Возвращает строку, содержащую все стрижки
        {
            string s = String.Empty;
            if (cuts!=null)
            for (int i = 0; i < cuts.Count; i++)
            {
                if (cuts[i] != null)
                {
                    if (i != cuts.Count - 1)
                    {
                        s += $"{cuts[i].ToStr()}\n";
                    }
                    else
                    {
                        s += $"{cuts[i].ToStr()}";
                    }
                }

            }
            return s;
        }
        public Cut GetCut(int i)//Получает стрижки по индексу
        {
            if (cuts[i] != null)
            {
                return cuts[i];
            }
            else
            {
                
                throw (new Exception($"Отсутствует стрижка с индексом {i}"));
                
            }
        }
        public Cut GetCutByName(string name)//Получает стрижку по названию
        {
            if (cuts!= null)
            foreach (var cut in cuts)
            {
                if (cut != null)
                {
                    if(cut.Name == name)
                    return cut;
                }
            }
            
            throw (new Exception($"Отсутствует стрижка с именем {name}"));
            
        }
        public void Add()//Добавление в пустую ячейку или создание новой ячейки
        {

            try
            {
                List<string> list_of_items = Input.FromFile("NewCut.txt");
                Console.WriteLine("---------------");
                foreach (var w in list_of_items)
                    Console.WriteLine(w);
                Console.WriteLine("---------------");
                Console.WriteLine("Вы хотите добавить эти стрижки?");
                Console.WriteLine("Да  -   1     Нет   -   0");
                string a = Console.ReadLine();
                int flag = 0;
                Int32.TryParse(a, out flag);
                switch (flag)
                {

                    case 1:
                        List<Cut> list_of_new_cuts = new List<Cut>();
                        foreach (var w in list_of_items)
                        {
                            string[] temp = w.Split();
                            Cut cut = new Cut(temp[0], temp[1], Int32.Parse(temp[2]));
                            list_of_new_cuts.Add(cut);
                        }
                        
                        foreach (var cl in list_of_new_cuts)
                        {
                            int i = 0;
                            if (cuts == null)
                            {
                                cuts = new List<Cut>();
                                cuts.Add(cl);
                            }
                            else
                            {
                                for (i = 0; i < cuts.Count; i++)
                                    if (cuts[i] == null)
                                {
                                    cuts[i] = cl;
                                    break;
                                }
                                cuts.Add(cl);
                            }
                            
                            
                            Console.WriteLine($"\nСтрижка {cl.ToStr()} добавлена под индексом {i}");
                        }



                        break;
                    case 0: break;
                    default:
                        Console.WriteLine("Неправильный ввод параметра");
                        this.Add();

                        break;
                }
                Output.Backup(path, backup_path);
                Output.InFile(path, this.Str());
            }
            catch(Exception e)
            {
                Console.WriteLine($"\nНеправильные значения в файле NewCut.txt\n");
                Console.WriteLine(e.Message);
            }
        }
        public void Del(int i)//Удаление стрижки по индексу
        {
            try
            {
                Console.WriteLine($"Стрижка {this.GetCut(i).ToStr()} удалена под индексом {i}");
                cuts[i] = null;
                Output.Backup(path, backup_path);
                Output.InFile(path, this.Str());
            }
            catch
            {
                Console.WriteLine("Ошибка при удалении стрижки");
            }
        }
        public void SetPrice()//Установка новых цен на стрижки
        {
            try {
                List<string> list_of_items = Input.FromFile("NewCutPrices.txt");
                
                foreach (var item in list_of_items)
                {
                    string[] temp = item.Split();
                    Cut cut = this.GetCutByName(temp[0]);
                    cut.Price = Int32.Parse(temp[1]);
                }
                Output.Backup(path, backup_path);
                Output.Backup("CutsPrices.txt", "CutsPricesBackup.txt");
                Output.InFile("CutsPrices.txt", String.Join("\n", list_of_items));
                Output.InFile(path, this.Str());
                Console.WriteLine("\nЦены на стрижки были изменены\n");
            }
            catch
            {
                Console.WriteLine("Неправльные значения в файле NewCutPrices.txt");
            }
        }
        public void Dynamic()//Выводит изменение цен на стрижки на консоль
        {
            List<string> list_of_items = Input.FromFile("CutsPrices.txt");
            Console.WriteLine("--------------");
            foreach (var line in list_of_items)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("--------------\n");
        }
        public void Menu()
        {
            Console.WriteLine("--------------");
            Console.WriteLine("1. Вывести всю информацию о стрижках");
            Console.WriteLine("2. Получить информацию о стрижке по названию");
            Console.WriteLine("3. Добавить стрижки");
            Console.WriteLine("4. Удалить стрижку");
            Console.WriteLine("5. Установить новую цену на стрижки");
            Console.WriteLine("6. Посмотреть изменение цен на стрижки");
            Console.WriteLine("7. Отменить последнее изменение цен на стрижки");
            Console.WriteLine("8. Назад");
            Console.WriteLine("--------------");
            Console.WriteLine("\nВыбирете действие: ");
            string InStr = Console.ReadLine();
            int Action = 0;
            if (Int32.TryParse(InStr, out Action))
            {
                switch (Action)
                {
                    case 1:
                        this.ToStr();
                        break;
                    case 2:
                        Console.WriteLine("Введите название:");
                        string name = Console.ReadLine();
                        try
                        {
                            Cut cut = this.GetCutByName(name);
                            if (cut != null)
                                Console.WriteLine(cut.ToStr());
                        }
                        catch (Exception e){
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 3:
                        this.Add();
                        break;
                    case 4:
                        this.ToStr();
                        if (cuts != null)
                        {
                            Console.WriteLine("\nВведите индекс стрижки, которую хотите удалить:");
                            string In = Console.ReadLine();
                            int Index = 0;
                            if (Int32.TryParse(In, out Index))
                            {
                                this.Del(Index);
                            }
                            else
                            {
                                Console.WriteLine("Неверный индекс");
                            }
                        }
                        break;
                    case 5:
                        this.SetPrice();
                        break;
                    case 6:
                        this.Dynamic();
                        break;
                    case 7:
                        Console.WriteLine("\nВы хотите отменить изменение цены стрижки или изменение списка стрижек?");
                        Console.WriteLine("Изменение цены - 1 ");
                        Console.WriteLine("Изменение списка - 2");
                        Console.WriteLine("Назад - 0");
                        string InS = Console.ReadLine();
                        int Act = 0;
                        if (Int32.TryParse(InStr, out Act))
                        {
                            switch (Act)
                            {
                                case 2:
                                    Output.Backup(backup_path, path);
                                    Console.WriteLine("\nПоследнее действие отменено\n");
                                    break;
                                case 1:
                                    Output.Backup("CutsPricesBackup.txt", "CutsPrices.txt");
                                    Console.WriteLine("\nПоследнее действие отменено\n");
                                    break;
                                default:
                                    break;
                        }
                        }
                        break;
                    case 8:
                        break;
                    default:
                        Console.WriteLine("Неверный ввод номера действия");
                        this.Menu();
                        break;

                }
                Console.WriteLine();
            }
        }
    }
}

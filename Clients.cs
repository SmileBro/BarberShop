using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BarberShop
{
    class Clients
    {
        private List<Client> clients;
        private string path = "Clients.txt";
        private string backup_path = "ClientsBackup.txt";
        public Clients()//Получение всех данных о клиентах
        {
            List<string> list_of_items = Input.FromFile(path);
            List<Client> list_of_clients = new List<Client>();
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
                            Client client = new Client(temp[0], temp[1], temp[2], temp[3], Int32.Parse(temp[4]));
                            list_of_clients.Add(client);
                        }
                    }
                    clients = list_of_clients;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"\nСтрока имеет неправильный формат в файле {path}\n");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine($"\nСтрока имеет неправильный формат или в ней отсутствуют нужные данные в файле {path}\n");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"\nНеправильные значения в файле {path}\n");
                    Console.WriteLine(ex.Message);
                }
            }

        }
        public Client GetClient(int i)//Получает клиента по индексу
        {
            if (clients[i] != null)
            {
                return clients[i];
            }
            else
            {
                Console.WriteLine($"Отсутствует клиент с индексом {i}");
                return new Client();
            }
        }
        public Client GetClientByName(string s)//Получает клиента по ФИО
        {
            string[] temp = s.Split();
            if(clients != null)
            foreach (var client in clients)
            {
                if (client != null)
                {
                    if (client.LastName == temp[0] && client.Name == temp[1] && client.MiddleName == temp[2])
                        return client;
                }
            }
            Console.WriteLine($"Отсутствует клиент: {s}");
            return new Client();
        }
        public void ToStr()//Вывод клиентов на консоль
        {

            string s = "\nСписок клиентов: \n";
            int i = 0;
            if (clients == null || clients.Count == 0)
            {
                Console.WriteLine($"\nФайл {path} пустой\n");
            }
            else
            {
                foreach (var client in clients)
                {
                    if (client != null)
                        s += $"{i} {client.ToStr()}\n";
                    i++;
                }
                Console.WriteLine(s);
            }

        }
        public string Str()//Возвращает строку, содержащую всех клиентов
        {
            string s = String.Empty;
            if(clients!=null)
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i] != null)
                {
                    if (i != clients.Count - 1)
                    {
                        s += $"{clients[i].ToStr()}\n";
                    }
                    else
                    {
                        s += $"{clients[i].ToStr()}";
                    }
                }

            }
            return s;
        }
        public void Add()//Добавление в пустую ячейку или создание новой ячейки
        {

            try
            {
                List<string> list_of_items = Input.FromFile("NewClient.txt");
                Console.WriteLine("---------------");
                foreach (var w in list_of_items)
                    Console.WriteLine(w);
                Console.WriteLine("---------------");
                Console.WriteLine("Вы хотите добавить этих клиентов?");
                Console.WriteLine("Да  -   1     Нет   -   0");
                string a = Console.ReadLine();
                int flag = 0;
                Int32.TryParse(a, out flag);
                switch (flag)
                {

                    case 1:
                        List<Client> list_of_new_clients = new List<Client>();
                        foreach (var w in list_of_items)
                        {
                            string[] temp = w.Split();
                            Client client = new Client(temp[0], temp[1], temp[2], temp[3], 0);
                            list_of_new_clients.Add(client);
                        }
                        
                        foreach (var cl in list_of_new_clients)
                        {


                            int i = 0;
                            if (clients == null)
                            {
                                clients = new List<Client>();
                                clients.Add(cl);
                            }
                            else
                            {
                                for (i = 0; i < clients.Count; i++)
                                if (clients[i] == null)
                                {
                                    clients[i] = cl;
                                    break;
                                }
                                clients.Add(cl);
                            }
                            
                            
                            Console.WriteLine($"\nКлиент {cl.ToStr()} добавлен под индексом {i}");
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
                Console.WriteLine($"\nНеправильные значения в файле NewClient.txt\n");
                Console.WriteLine(e.Message);
            }
        }
        public void Del(int i)//Удаление клиента по индексу
        {
            Console.WriteLine($"Клиент {this.GetClient(i).ToStr()} удален под индексом {i}");
            clients[i] = null;
            Output.Backup(path, backup_path);
            Output.InFile(path, this.Str());
        }
        public void Menu()//Основные действия с клиентами
        {
            Console.WriteLine("--------------");
            Console.WriteLine("1. Вывести всю информацию о клиентах");
            Console.WriteLine("2. Получить информацию о клиенте по ФИО");
            Console.WriteLine("3. Добавить клиентов");
            Console.WriteLine("4. Удалить клиента");
            Console.WriteLine("5. Отменить последнее действие с клиентами");
            Console.WriteLine("6. Назад");
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
                        Console.WriteLine("Введите ФИО:");
                        string FIO = Console.ReadLine();
                        try
                        {
                            Client client = this.GetClientByName(FIO);
                            if (client != null)
                                Console.WriteLine(client.ToStr());
                        }
                        catch (Exception e){Console.WriteLine(e.Message); }
                        break;
                    case 3:
                        this.Add();
                        break;
                    case 4:
                        this.ToStr();
                        if (clients != null) 
                        {
                            Console.WriteLine("\nВведите индекс клиента, которого хотите удалить:");
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
                        Output.Backup(backup_path, path);
                        Console.WriteLine("\nПоследнее действие отменено\n");
                        break;
                    case 6:
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

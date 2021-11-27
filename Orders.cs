using System;
using System.Collections.Generic;
using System.IO;

namespace BarberShop
{
    class Orders
    {
        private List<Order> orders;
        private string path = "Orders.txt";
        private string backup_path = "OrdersBackup.txt";
        public Orders()//Получение всех данных о заказах
        {
            List<string> list_of_items = Input.FromFile(path);
            List<Order> list_of_orders = new List<Order>();
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
                        string[] temp = w.Split();
                        string s = $"{temp[1]} {temp[2]} {temp[3]}";
                        Cuts cuts = new Cuts();
                        Clients clients = new Clients();
                        Order order = new Order(cuts.GetCutByName(temp[0]), clients.GetClientByName(s), temp[4], temp[5]);
                        list_of_orders.Add(order);
                    }
                    orders = list_of_orders;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\nНеправильные значения в файле {path}\n");
                    Console.WriteLine(e.Message);
                }
            }

        }
        public void ToStr()//Вывод всех заказов на консоль
        {

            string s = "\nСписок заказов: \n";
            int i = 0;
            if (orders == null || orders.Count == 0)
            {
                Console.WriteLine($"\nФайл {path} пустой\n");
            }
            else
            {
                foreach (var order in orders)
                {
                    if (order != null)
                        s += $"{i} {order.ToStr()}\n";
                    i++;
                }
                Console.WriteLine(s);
            }

        }
        public void ToStrByFilial(string filial)//Вывод всех заказов из определенного филиала на консоль
        {

            string s = $"\nСписок заказов в филиале {filial}: \n";
            int i = 0;
            if (orders!=null)
            foreach (var order in orders)
            {
                if (order != null && order.Filial == filial)
                    s += $"{i} {order.ToStr()}\n";
                i++;
            }
            Console.WriteLine(s);

        }
        public string Str()//Возвращает строку, содержащую все заказы
        {
            string s = String.Empty;
            if (orders!=null)
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i] != null)
                {
                    if (i != orders.Count - 1)
                    {
                        s += $"{orders[i].ToStr()}\n";
                    }
                    else
                    {
                        s += $"{orders[i].ToStr()}";
                    }
                }

            }
            return s;
        }
        public string ToShortStr()//Возвращает строку, содержащую все заказы c сокращенными данными о клиентах
        {
            string s = String.Empty;
            if (orders!=null)
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i] != null)
                {
                    if (i != orders.Count - 1)
                    {
                        s += $"{orders[i].ToShortStr()}\n";
                    }
                    else
                    {
                        s += $"{orders[i].ToShortStr()}";
                    }
                }

            }
            return s;
        }
        public void Add(Clients clients)//Добавление в пустую ячейку или создание новой ячейки
        {

            try
            {
                List<string> list_of_items = Input.FromFile("NewOrder.txt");
                Console.WriteLine("---------------");
                foreach (var w in list_of_items)
                    Console.WriteLine(w);
                Console.WriteLine("---------------");
                Console.WriteLine("Вы хотите добавить эти заказы?");
                Console.WriteLine("Да  -   1     Нет   -   0");
                string a = Console.ReadLine();
                int flag = 0;
                Int32.TryParse(a, out flag);
                switch (flag)
                {

                    case 1:
                        List<Order> list_of_new_orders = new List<Order>();
                        
                        foreach (var w in list_of_items)
                        {
                            string[] temp = w.Split();
                            string s = temp[1] +" "+ temp[2] +" "+ temp[3];

                            Cut cut = new Cuts().GetCutByName(temp[0]);
                            //Получаем копию клиента, чтобы увеличить количество стрижек, не изменяя количество в исходном списке
                            Client client = new Client(new Clients().GetClientByName(s));
                            client.IncCutsCount();
                            
                            Order order = new Order(cut, client, temp[4], temp[5]);
                            list_of_new_orders.Add(order);
                        }
                        
                        foreach (var cl in list_of_new_orders)
                        {
                            int i = 0;
                            if (orders == null)
                            {
                                orders = new List<Order>();
                                orders.Add(cl);
                            }
                            else
                            {
                                for (i = 0; i < orders.Count; i++)
                                    if (orders[i] == null)
                                {
                                    orders[i] = cl;
                                    break;
                                }
                                orders.Add(cl);
                            }
                            
                            //Расчет стоимости заказа
                            double price = cl.Price();
                            
                            //Изменение данных о клиентах
                            string s = cl.Client.LastName+" "+ cl.Client.Name+" " + cl.Client.MiddleName;
                            clients.GetClientByName(s).IncCutsCount();
                            Console.WriteLine($"\nЗаказ {cl.ToShortStr()} добавлен под индексом {i}. Стоимость:{price}");
                        }
                        break;
                    case 0: break;
                    default:
                        Console.WriteLine("Неправильный ввод параметра");
                        this.Add(clients);

                        break;
                }
                Output.Backup(path, backup_path);
                Output.InFile(path, this.ToShortStr());
                Output.Backup("Clients.txt", "ClientsBackup.txt");
                Output.InFile("Clients.txt", clients.Str());
            }
            catch(Exception e)
            {
                Console.WriteLine($"\nНеправильные значения в файле NewOrder.txt\n");
                Console.WriteLine(e.Message);
            }
        }
        public Order GetOrder(int i)//Получает заказ по индексу
        {
            if (orders[i] != null)
            {
                return orders[i];
            }
            else
            {
                
                throw (new Exception($"Отсутствует заказ с индексом {i}"));

            }
        }
        public void Del(int i,Clients clients)//Удаление заказа по индексу
        {
            try
            {
                Order order = new Order(this.GetOrder(i));
                orders[i] = null;
                Output.Backup(path, backup_path);
                Output.InFile(path, this.Str());
                string s = order.Client.LastName + " " + order.Client.Name + " " + order.Client.MiddleName;
                clients.GetClientByName(s).DecCutsCount();
                Output.Backup("Clients.txt", "ClientsBackup.txt");
                Output.InFile("Clients.txt", clients.Str());
                Console.WriteLine($"Заказ {order.ToStr()} удален под индексом {i}");
                
            }
            catch
            {
                Console.WriteLine("Ошибка при удалении заказа");
            }
        }
        public List<string> Filials()//Возвращает список всех филиалов
        {
            try
            {
                List<string> filials = new List<string>();
                if (orders!=null)
                foreach (var order in orders)
                {
                    if (order != null && !filials.Contains(order.Filial))
                    {
                        filials.Add(order.Filial);
                    }
                }
                return filials;
            }
            catch
            {
                
                throw new Exception($"Неправильные значения на местах филиалов в {path}");
            }
        }
        public double TotalSum()
        {
            double summa = 0;
            if (orders != null)
            foreach (var w in orders)
                {
                    
                    if (w != null)
                        summa += w.Price();
                }
            return summa;
        }
        public void Menu(Clients clients)
        {
            Console.WriteLine("--------------");
            Console.WriteLine("1. Вывести всю информацию о заказах");
            Console.WriteLine("2. Подсчитать выручку за день");
            Console.WriteLine("3. Получить информацию о заказах по филиалу");
            Console.WriteLine("4. Добавить заказ(ы)");
            Console.WriteLine("5. Удалить заказ");
            Console.WriteLine("6. Посмотреть все названия филиалов");
            Console.WriteLine("7. Отменить последнее действие с заказами");
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
                        Console.WriteLine($"\nПолная выручка составила: {this.TotalSum()}");
                        break;
                    case 3:
                        Console.WriteLine("Введите название филиала:");
                        string name = Console.ReadLine();
                        List<string> names_of_filials = this.Filials();
                        if (names_of_filials.Contains(name))
                        {
                            this.ToStrByFilial(name);
                        }
                        else
                        {
                            Console.WriteLine("Неправильное название филиала");
                            this.Menu(clients);
                        }
                        break;
                    case 4:
                        this.Add(clients);
                        break;
                    case 5:
                        this.ToStr();
                        if (orders != null)
                        {
                            Console.WriteLine("\nВведите индекс заказа, который хотите удалить:");
                            string In = Console.ReadLine();
                            int Index = 0;
                            if (Int32.TryParse(In, out Index))
                            {
                                this.Del(Index, clients);
                            }
                            else
                            {
                                Console.WriteLine("Неверный индекс");
                            }
                        }
                        break;
                    case 6:
                        List<string> filials = this.Filials();
                        if (filials == null) {
                            Console.WriteLine("Список филиалов отсутствует\n");
                        }
                        else
                        {
                            Console.WriteLine("Список филиалов: ");
                            Console.WriteLine(String.Join("\n", filials));
                        }
                        break;
                    case 7:
                        Output.Backup(backup_path, path);
                        Output.Backup("ClientsBackup.txt", "Clients.txt");
                        break;
                    case 8:
                        break;
                  
                    default:
                        Console.WriteLine("Неверный ввод номера действия");
                        this.Menu(clients);
                        break;

                }
                Console.WriteLine();
            }
        }
    }

}

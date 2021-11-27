using System;

namespace BarberShop
{
    class Program
    {
        static void Main()
        {
            bool Running = true;
            while (Running)
            {
                //Инициализация основных данных
                Clients clients = new Clients();
                Cuts cuts = new Cuts();
                Orders orders = new Orders();

                Console.WriteLine("--------------");
                Console.WriteLine("1. Выполнить и добавить заказ");
                Console.WriteLine("2. Управление данными о клиентах");
                Console.WriteLine("3. Управление данными о стрижках");
                Console.WriteLine("4. Управление данными о заказах");
                Console.WriteLine("5. Закрыть программу");
                Console.WriteLine("--------------");
                Console.WriteLine("\nВыбирете действие: ");
                string InStr = Console.ReadLine();
                int Action = 0;
                if (Int32.TryParse(InStr,out Action)){
                    switch (Action)
                    {
                        case 1:
                            orders.Add(clients);
                            break;
                        case 2:
                            clients.Menu();
                            break;
                        case 3:
                            cuts.Menu();
                            break;
                        case 4:
                            orders.Menu(clients);
                            break;
                        case 5:
                            Running = false;
                            break;
                        default:
                            Console.WriteLine("\nНеверная цифра действия\n");
                            break;
                    }
                }
            }
            
        }
    }
}

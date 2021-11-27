using System;
using System.Collections.Generic;
using System.Text;

namespace BarberShop
{
    class Order
    {
        Cut cut;
        public Cut Cut { get { return cut; } }
        Client client;
        public Client Client { get { return client; } }
        string date;
        public string Date { get { return date; } }
        private string filial;
        public string Filial { get { return filial; } }

        public Order(Cut c, Client cl, string d,string filial)
        {
            cut = c;
            client = cl;
            DateTime dateTime = new DateTime();
            if (DateTime.TryParse(d, out dateTime))
            {
                date = d;
            }
            else
            {
                throw new Exception("Неправильное значение даты");
            }
            this.filial = filial;
        }
        public Order(Order o):this(o.Cut,o.Client,o.Date,o.Filial)
        {}
        

        public string ToStr()
        {
            return $"{this.cut.Name} {this.client.ToStr()} {this.date} {this.filial}";
        }
        public string ToShortStr()
        {
            return $"{this.cut.Name} {this.client.LastName} {this.client.Name} {this.client.MiddleName} {this.date} {this.filial}";
        }
        public double Price()
        {
            double price = (this.Client.IsRegular() ? (Convert.ToDouble(this.Cut.Price) - Convert.ToDouble(this.Cut.Price) / 100.0 * 3.0) : this.Cut.Price);
            return price;
        }
    }
}

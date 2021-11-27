using System;
using System.Collections.Generic;
using System.Text;

namespace BarberShop
{
    class Cut
    {
        string name;
        public string Name { get {return name; } }
        string gender;
        public string Gender { get { return gender; } }
        int price;
        public int Price { 
            get 
            { 
                return price; 
            }
            set
            {
                price = value;
            }
        }
        public Cut(string n, string g, int p)
        {
            int d = 0 ;
            if (!(Int32.TryParse(n, out d)))
            {
                name = n;
            }
            else
            {
                throw (new Exception("Неправильное значение названия"));
            }
            if (g == "m" || g == "f")
            {
                gender = g;
            }
            else
            {
                throw (new Exception("Неправильное значение пола стрижки"));
            }
            price = p;
        }
        public Cut() : this(String.Empty, String.Empty, 0) { }

        public string ToStr()
        {
            return $"{this.name} {this.gender} {this.price}";
        }
    }
}

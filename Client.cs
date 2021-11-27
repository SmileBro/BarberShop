using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BarberShop
{
    class Client
    {
        
        string last_name;
        public string LastName { get { return last_name; } }
        string name;
        public string Name { get { return name; } }
        string middle_name;
        public string MiddleName { get { return middle_name; } }
        string gender;
        public string Gender { get { return gender; } }
        int regular;
        public int Regular { get { return regular; } }

        public Client(string l, string n, string m, string g, int r)
        {
            if (String.IsNullOrEmpty(l))
                throw (new Exception("Неправильные входные данные о клиенте"));
            if (String.IsNullOrEmpty(n))
                throw (new Exception("Неправильные входные данные о клиенте"));
            if (String.IsNullOrEmpty(m))
                throw (new Exception("Неправильные входные данные о клиенте"));
            if (String.IsNullOrEmpty(g))
                throw (new Exception("Неправильные входные данные о клиенте"));
            double d = 0;
            if (!(Double.TryParse(l, out d)))
            {
                last_name = l;
            }
            else
            {
                throw (new Exception("Неправильное значение фамилии"));
            }
            if (!(Double.TryParse(n, out d)))
            {
                name = n;
            }
            else
            {
                throw (new Exception("Неправильное значение имени"));
            }
            if (!(Double.TryParse(m, out d)))
            {
                middle_name = m;
            }
            else
            {
                throw (new Exception("Неправильное значение отчества"));
            }
            if (g=="m" || g=="f") {
                gender = g;
            }
            else 
            {
                throw (new Exception("Неправильное значение пола"));
            }
            regular = r;
        }
        public Client() : this(String.Empty, String.Empty, String.Empty, String.Empty, 0) { }
        public Client(Client client)
        {
            this.last_name = client.LastName;
            this.name = client.Name;
            this.middle_name = client.MiddleName;
            this.gender = client.Gender;
            this.regular = client.Regular;
        }

        public string ToStr()
        {
            return $"{this.last_name} {this.name} {this.middle_name} {this.gender} {this.regular}";
        }
        public bool IsRegular()
        {
            return regular >= 5;
        }
        public void IncCutsCount()//Увеличивает количество стрижек
        {
            this.regular++;
        }
        public void DecCutsCount()//Уменьшает количество стрижек
        {
            this.regular--;
        }



    }
}

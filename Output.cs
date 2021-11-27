using System;
using System.IO;

namespace BarberShop
{
    class Output
    {
        public static void InFile(string path,string text)//Вывод в фаил path текста text
        {
            StreamWriter sw = new StreamWriter(path);
            if (String.IsNullOrEmpty(text)) {
                sw.Close();
                File.Create(path).Close();
            }
            else
            {
                sw.WriteLine(text);
            }
            sw.Close();
        }
        public static void Backup(string path, string backup_path)
        {
            
            File.Copy(path, backup_path, true);
            
        }
    }
}

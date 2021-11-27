using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BarberShop
{
    class Input
    {
        public static List<string> FromFile(string path)//Ввод из файла
        {
            String line;
            try
            {
                //Передаем путь к файлу и его имя в конструктор StreamReader
                StreamReader sr = new StreamReader(path);

                //Читаем первую строку текста
                line = sr.ReadLine();
                List<string> lines = new List<string>();
                //Продолжаем чтение пока не достигним последней строки в файле
                while (line != null)
                {
                    lines.Add(line);
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                return lines;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"\nОтсутствует фаил {path} \n");
                return new List<string>();
            }
            catch
            {

                Console.WriteLine($"\nНеправильные данные в файле {path} \n");

                return new List<string>();
            }

        }
        
    }
}

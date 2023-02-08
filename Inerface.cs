using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMag
{
    public class Inerface
    {
        public static void PrintInterface(ModelWorker user)
        {
            Console.WriteLine(user.atribute);
            Console.SetCursorPosition(50, 0);
            Console.WriteLine($"Добро пожаловать, {user.name}");
            Console.SetCursorPosition(85, 0);
            Console.WriteLine($"Роль: Admin");
            Console.WriteLine("______________________________________________________________________________________________________________________________________________");
            string startupPath = Directory.GetCurrentDirectory();
            int len = startupPath.Length - 17;
            string json = startupPath.Substring(0, len) + "\\Tables.json";
            List<Table> con = Converter.Des<List<Table>>(json);
            for (int i = 2; i < con.Count + 2; i++)
            {
                Console.SetCursorPosition(5, i);
                Console.WriteLine($"ID {con[i - 2].id}");
                Console.SetCursorPosition(15, i);
                Console.WriteLine($"{con[i - 2].login}");
                Console.SetCursorPosition(35, i);
                Console.WriteLine($"{con[i - 2].password}");
                Console.SetCursorPosition(60, i);
                Console.WriteLine($"{con[i - 2].role}");
            }


            for (int i = 2; i < 12; i++)
            {
                Console.SetCursorPosition(85, i);
                Console.WriteLine("|");
            }
            if (user.atribute != 4)
            {
                Console.SetCursorPosition(95, 2);
                Console.WriteLine("F1 - добавить запись");
                Console.SetCursorPosition(95, 3);
                Console.WriteLine("F2 - найти запись");
                Console.SetCursorPosition(95, 4);
                Console.WriteLine("F3 - удалить запись");
                Console.SetCursorPosition(95, 5);
                Console.WriteLine("F4 - прочитать запись");
            }

        }
    }
}

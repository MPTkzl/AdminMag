using AdminMag;
using System;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;

namespace AdminMag
{
    public class Program
    {
        public static int user;
        static void Main(string[] argrs)
        {
            while (true)
            {
                string startupPath = Directory.GetCurrentDirectory();
                int len = startupPath.Length - 17;

                string jsonUs = startupPath.Substring(0, len) + "\\Tables.json";
                List<Table> convert = Converter.Des<List<Table>>(jsonUs);
                string json = startupPath.Substring(0, len) + "\\Data.json";
                List<ModelWorker> con = Converter.Des<List<ModelWorker>>(json);

                List<(string, string)> logins = new List<(string, string)>();
                for (int i = 0; i < con.Count; i++)
                {
                    logins.Add((con[i].name, con[i].password));
                }

                int ind = Aut(logins);
                ModelWorker worker = new ModelWorker();
                if (ind != -1)
                {
                    worker = con[ind];
                }
                else
                {
                    foreach (ModelWorker i in con)
                    {
                        if (i.atribute == -1)
                        {
                            worker = i;
                        }
                    }
                }



                Console.Clear();
                Admin admin = new Admin(worker, convert);
                admin.Interface();
                break;
            }

        }
        public static (string, string, bool) PasswordInput(string name, List<(string, string)> workers)
        {
            string inpt = string.Empty;
            while (!workers.Contains((name, inpt)))
            {
                Console.Write("Введите пароль: ");
                inpt = string.Empty;
                while (true)
                {
                    var key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Enter) break;

                    Console.Write("*");
                    inpt += key.KeyChar;
                }

                if (!workers.Contains((name, inpt)))
                {
                    Console.WriteLine("No");
                    break;
                }
                Console.WriteLine();
            }
            if (!workers.Contains((name, inpt)))
            {
                return (name, inpt, false);
            }
            else if (workers.Contains((name, inpt)))
            {
                return (name, inpt, true);
                Console.ReadKey();
            }

            else
            {
                Console.WriteLine("Bred kakoito");
                return (name, inpt, false);
            }
        }

        public static int Aut(List<(string, string)> workers)
        {
            while (true)
            {
                Console.WriteLine("Введите имя");
                string name = Console.ReadLine();

                var password = PasswordInput(name, workers);
                if (password.Item3 == false)
                {
                    return -1;

                }
                else if (password.Item3 == true)
                {
                    foreach (var worker in workers)
                    {
                        Console.WriteLine(worker);
                    }


                    return workers.IndexOf((password.Item1, password.Item2));
                }
            }


        }
    }
}
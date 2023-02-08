using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMag
{
    internal class Admin
    {
        ModelWorker admin = new ModelWorker();

        List<Table> allUsers = new List<Table>();
        internal enum Post
        {
            F1 = ConsoleKey.F1,
            F2 = ConsoleKey.F2,
            F3 = ConsoleKey.F3,
            F4 = ConsoleKey.F4,
            Enter = ConsoleKey.Enter,
            UpArrow = ConsoleKey.UpArrow,
            DownArrow = ConsoleKey.DownArrow,
            Esc = ConsoleKey.Escape

        }
        public Admin(ModelWorker worker, List<Table> allUsers)
        {
            admin = worker;
            this.allUsers = allUsers;
        }
        public void Interface()
        {
            int pose = 2;
            int max = allUsers.Count() + 1;
            while (true)
            {
                //Усечение строки до файла с юзерами
                string startupPath = Directory.GetCurrentDirectory();
                int len = startupPath.Length - 17;
                string json = startupPath.Substring(0, len) + "\\Tables.json";
                List<Table> con = Converter.Des<List<Table>>(json);
                max = con.Count() + 1;
                Console.Clear();
                Console.WriteLine(pose);
                Console.SetCursorPosition(0, pose);
                Console.WriteLine("->");

                Inerface.PrintInterface(admin);

                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == (ConsoleKey)Post.F1)
                {
                    Console.Clear();
                    Create();
                }
                else if (key.Key == (ConsoleKey)Post.F2)
                {
                    Console.Clear();
                    Search();
                }

                else if (key.Key == (ConsoleKey)Post.F3)
                {
                    Console.Clear();
                    Delete();
                }
                else if (key.Key == (ConsoleKey)Post.F4)
                {
                    Console.Clear();
                    Read(pose);
                }
                else if (key.Key == (ConsoleKey)Post.UpArrow)
                {
                    if (pose <= 2)
                    {
                        pose += max - 2;
                    }
                    else
                    {
                        pose--;
                    }

                }
                else if (key.Key == (ConsoleKey)Post.DownArrow)
                {
                    if (pose >= max)
                    {
                        pose -= max - 2;
                    }
                    else
                    {
                        pose++;
                    }

                }
                else if (key.Key == (ConsoleKey)Post.Enter)
                {
                    Console.Clear();
                    Table user = allUsers[pose - 2];
                    Update(user.id);
                }
                else if (key.Key == (ConsoleKey)Post.Esc)
                {
                    Console.Clear();
                    break;
                }


            }




        }
        public void Create()
        {
            try
            {
                string startupPath = Directory.GetCurrentDirectory();
                int len = startupPath.Length - 17;
                string json = startupPath.Substring(0, len) + "\\Tables.json";
                List<Table> con = Converter.Des<List<Table>>(json);

                Console.WriteLine("Введите логин");
                string login = Console.ReadLine();
                Console.WriteLine("Введите пароль польвателя");
                string password = Console.ReadLine();
                Console.WriteLine("Введите роль польвателя");
                int role = Convert.ToInt32(Console.ReadLine());

                int id = con[con.Count - 1].id + 1;

                Table newUser = new Table();
                newUser.id = id;
                newUser.login = login;
                newUser.password = password;
                newUser.role = role;

                con.Add(newUser);

                ;
                Converter.Ser<List<Table>>(con, json);
            }catch (Exception)
            {
                
            }
            

        }

        public void Delete()
        {
            try
            {

                string startupPath = Directory.GetCurrentDirectory();
                int len = startupPath.Length - 17;
                string json = startupPath.Substring(0, len) + "\\Tables.json";
                List<Table> con = Converter.Des<List<Table>>(json);
                List<int> ids = new List<int>();

                foreach (Table user in con)
                {
                    ids.Add(user.id);
                }


                Console.WriteLine("Введите id пользователя, которого хотите удалить");
                int id = Convert.ToInt32(Console.ReadLine());

                if (ids.Contains(id))
                {
                    Table user = con[ids.IndexOf(id)];
                    con.Remove(user);


                    Converter.Ser<List<Table>>(con, json);

                }
                else
                {
                    Console.WriteLine("Такого пользователя нету, нажмите любую клавишу, что бы выйти");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            catch (Exception)
            {

            }
            


        }

        public void Read(int id)
        {
            List<int> ids = new List<int>();

            foreach (Table i in allUsers)
            {
                ids.Add(i.id);
            }
            Table user = allUsers[ids.IndexOf((id))];
            Console.WriteLine(user.id);
            Console.WriteLine(user.login);
            Console.WriteLine(user.role);
            Console.WriteLine(user.password);
            Console.WriteLine();

            Console.WriteLine("Нажмите на любую кнопку что бы выйти");
            Console.ReadKey();


        }

        public void Search()
        {
            Console.WriteLine("Введите ID");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите логин");
            string login = Console.ReadLine();
            Console.WriteLine("Введите пароль польвателя");
            string password = Console.ReadLine();
            Console.WriteLine("Введите роль польвателя");
            int role = Convert.ToInt32(Console.ReadLine());

            Table user = new Table();
            user.id = id;
            user.login = login;
            user.password = password;
            user.role = role;


            List<int> ids = new List<int>();

            foreach (Table i in allUsers)
            {
                ids.Add(i.id);
            }

            if (ids.Contains(id))
            {
                Console.Clear();
                Console.WriteLine(user.id);
                Console.WriteLine(user.login);
                Console.WriteLine(user.role);
                Console.WriteLine(user.password);
                Console.WriteLine();

                Console.WriteLine("Нажмите на любую кнопку что бы выйти");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Такого пользователя нету, нажмите любую клавишу, что бы выйти");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public void Update(int userUpdate)
        {

            string startupPath = Directory.GetCurrentDirectory();
            int len = startupPath.Length - 17;
            string json = startupPath.Substring(0, len) + "\\Tables.json";
            List<Table> con = Converter.Des<List<Table>>(json);

            List<int> ids = new List<int>();

            foreach (Table i in allUsers)
            {
                ids.Add(i.id);
            }

            Console.WriteLine("Введите новый логин");
            string login = Console.ReadLine();
            Console.WriteLine("Введите новый пароль польвателя");
            string password = Console.ReadLine();
            Console.WriteLine("Введите новую роль польвателя");
            int role = Convert.ToInt32(Console.ReadLine());

            Table user = con[ids.IndexOf((userUpdate))];
            allUsers.Remove(user);
            user.login = login;
            user.password = password;
            user.role = role;

            allUsers.Insert(userUpdate, user);
            Converter.Ser<List<Table>>(con, json);


        }
    }
}

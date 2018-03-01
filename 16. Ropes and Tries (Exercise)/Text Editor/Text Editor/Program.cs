using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Editor
{
    class Program
    {
        static void Main(string[] args)
        {
            TextEditor editor = new TextEditor();
            string input = Console.ReadLine();
            while (input != "end")
            {
                try
                {
                    string[] inputs = input.Split();
                    if (inputs[0] == "login")
                    {
                        string username = inputs[1];
                        editor.Login(username);
                    }
                    else if (inputs[0] == "logout")
                    {
                        string username = inputs[1];
                        if (editor.IsLogged(username))
                        {
                            editor.Logout(username);
                        }
                    }
                    else if (inputs[0] == "users")
                    {
                        if (inputs.Length == 1)
                        {
                            Console.WriteLine(String.Join("\n", editor.Users()));
                        }
                        else
                        {
                            Console.WriteLine(String.Join("\n", editor.Users(inputs[1])));
                        }
                    }
                    else
                    {
                        string username = inputs[0];
                        string command = inputs[1];
                        if (!editor.IsLogged(username))
                        {
                            input = Console.ReadLine();
                            continue;
                        }
                        if (command == "insert")
                        {
                            int index = int.Parse(inputs[2]);
                            string str = String.Join(" ", inputs.Skip(3)).Trim('"');
                            editor.Insert(username, index, str);
                        }
                        else if (command == "prepend")
                        {
                            string str = String.Join(" ", inputs.Skip(2)).Trim('"');
                            editor.Prepend(username, str);
                        }
                        else if (command == "substring")
                        {
                            int index = int.Parse(inputs[2]);
                            int length = int.Parse(inputs[3]);
                            editor.Substring(username, index, length);
                        }
                        else if (command == "delete")
                        {
                            int index = int.Parse(inputs[2]);
                            int length = int.Parse(inputs[3]);
                            editor.Delete(username, index, length);
                        }
                        else if (command == "clear")
                        {
                            editor.Clear(username); 
                        }
                        else if (command == "length")
                        {
                            Console.WriteLine(editor.Length(username));
                        }
                        else if (command == "print")
                        {
                            Console.WriteLine(editor.Print(username));
                        }
                        else if (command == "undo")
                        {
                            editor.Undo(username);
                        }
                    }
                }
                catch (Exception)
                { }
                input = Console.ReadLine();
            }
        }
    }
}

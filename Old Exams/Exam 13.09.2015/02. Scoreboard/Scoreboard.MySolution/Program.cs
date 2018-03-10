using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

namespace Scoreboard.MySolution
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> users = new Dictionary<string, string>();
            Dictionary<string, string> games = new Dictionary<string, string>();
            OrderedDictionary<string, OrderedBag<UserScorePair>> scoreboard = 
                new OrderedDictionary<string, OrderedBag<UserScorePair>>((x, y) => String.CompareOrdinal(x, y));

            //can do normal dict above and this for prefix - slightly slower but also works in judge
            //OrderedSet<string> gamesByPrefix = new OrderedSet<string>((x, y) => String.CompareOrdinal(x, y)); 

            string input = Console.ReadLine();
            while (input != "End")
            {
                string[] inputs = input.Split(' ');
                string command = inputs[0];
                if (command == "RegisterUser")
                {
                    string username = inputs[1];
                    string password = inputs[2];
                    if (!users.ContainsKey(username))
                    {
                        users.Add(username, password);
                        Console.WriteLine("User registered");
                    }
                    else
                    {
                        Console.WriteLine("Duplicated user");
                    }
                }
                else if (command == "RegisterGame")
                {
                    string game = inputs[1];
                    string password = inputs[2];
                    if (!games.ContainsKey(game))
                    {
                        games.Add(game, password);
                        scoreboard.Add(game, new OrderedBag<UserScorePair>());
                        Console.WriteLine("Game registered");
                    }
                    else
                    {
                        Console.WriteLine("Duplicated game");
                    }
                }
                else if (command == "AddScore")
                {
                    string username = inputs[1];
                    string userPassword = inputs[2];
                    string game = inputs[3];
                    string gamePassword = inputs[4];
                    int score = int.Parse(inputs[5]);
                    if (users.ContainsKey(username) && users[username] == userPassword &&
                        games.ContainsKey(game) && games[game] == gamePassword)
                    {
                        UserScorePair pair = new UserScorePair(username, score);
                        var scores = scoreboard[game];
                        scores.Add(pair);
                        //if (scores.Count > 10)   //possible optimisation for large sizes, for these tests it's slower
                        //{
                        //    scores.RemoveLast();
                        //}
                        Console.WriteLine("Score added");
                    }
                    else
                    {
                        Console.WriteLine("Cannot add score");
                    }
                }
                else if (command == "ShowScoreboard")
                {
                    string game = inputs[1];
                    StringBuilder result = new StringBuilder();
                    if (games.ContainsKey(game) && scoreboard[game].Count > 0)
                    {
                        int counter = 1;
                        foreach (var pair in scoreboard[game].Take(10))
                        {
                            result.AppendFormat("#{0} {1} {2}{3}", counter, pair.Username, pair.Score, Environment.NewLine);
                            counter++;
                        }
                        Console.WriteLine(result.ToString().TrimEnd());
                    }
                    else if (!games.ContainsKey(game))
                    {
                        Console.WriteLine("Game not found");
                    }
                    else
                    {
                        Console.WriteLine("No score");
                    }
                }
                else if (command == "ListGamesByPrefix")
                {
                    string prefix = inputs[1];
                    string max = prefix + 'z';
                    var results = scoreboard.Range(prefix, true, max, true).Take(10).Select(x => x.Key);
                    string result = String.Join(", ", results);
                    Console.WriteLine(result != String.Empty ? result : "No matches");
                }
                else if (command == "DeleteGame")
                {
                    string game = inputs[1];
                    string password = inputs[2];
                    if (games.ContainsKey(game) && games[game] == password)
                    {
                        games.Remove(game);
                        scoreboard.Remove(game);
                        Console.WriteLine("Game deleted");
                    }
                    else
                    {
                        Console.WriteLine("Cannot delete game");
                    }
                }
                input = Console.ReadLine();
            }
        }
    }

    class UserScorePair : IComparable<UserScorePair>
    {
        public string Username { get; set; }
        public int Score { get; set; }

        public UserScorePair(string username, int score)
        {
            this.Username = username;
            this.Score = score;
        }

        public int CompareTo(UserScorePair other)
        {
            if (this.Score != other.Score)
            {
                return other.Score.CompareTo(this.Score);
            }
            return this.Username.CompareTo(other.Username);
        }
    }
}

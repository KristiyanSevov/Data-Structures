using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Text_Editor
{
    class TextEditor : ITextEditor
    {
        private HashSet<string> loggedUsers = new HashSet<string>();
        private Trie<BigList<char>> users = new Trie<BigList<char>>();
        private Dictionary<string, Stack<BigList<char>>> cache = 
            new Dictionary<string, Stack<BigList<char>>>();
       
        public void Clear(string username)
        {
            BigList<char> str = users.GetValue(username);
            cache[username].Push(new BigList<char>(str));
            str.Clear();
        }

        public void Delete(string username, int startIndex, int length)
        {
            BigList<char> str = users.GetValue(username);
            cache[username].Push(new BigList<char>(str));
            str.RemoveRange(startIndex, length);
        }

        public void Insert(string username, int index, string str)
        {
            BigList<char> user_str = users.GetValue(username);
            cache[username].Push(new BigList<char>(user_str));
            user_str.InsertRange(index, str);
        }

        public int Length(string username)
        {
            return String.Join("", users.GetValue(username)).Length;
        }

        public void Login(string username)
        {
            if (loggedUsers.Contains(username))
            {
                this.Clear(username);
            }
            else
            {
                loggedUsers.Add(username);
                cache.Add(username, new Stack<BigList<char>>());
                users.Insert(username, new BigList<char>());
            }
        }

        public void Logout(string username)
        {
            loggedUsers.Remove(username);
            cache.Remove(username);
        }

        public void Prepend(string username, string str)
        {
            BigList<char> user_str = users.GetValue(username);
            cache[username].Push(new BigList<char>(user_str));
            user_str.InsertRange(0, str);
        }

        public string Print(string username)
        {
            return String.Join("", users.GetValue(username));
        }

        public void Substring(string username, int startIndex, int length)
        {
            BigList<char> user_str = users.GetValue(username);
            cache[username].Push(new BigList<char>(user_str));
            BigList<char> newList = new BigList<char>(user_str.Range(startIndex, length));
            users.Insert(username, newList);
        }

        public void Undo(string username)
        {
            BigList<char> newList = cache[username].Pop();
            users.Insert(username, newList);
        }

        public IEnumerable<string> Users(string prefix = "")
        {
            return users.GetByPrefix(prefix).Where(x => loggedUsers.Contains(x));
        }

        public bool IsLogged(string username)
        {
            return loggedUsers.Contains(username);
        }
    }
}

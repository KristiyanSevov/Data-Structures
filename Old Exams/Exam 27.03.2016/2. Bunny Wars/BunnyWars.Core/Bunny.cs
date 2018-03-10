namespace BunnyWars.Core
{
    using System;
    using System.Linq;

    public class Bunny : IComparable
    {
        public Bunny(string name, int team, int roomId)
        {
            this.Name = name;
            this.Team = team;
            this.RoomId = roomId;
            this.Health = 100;
        }

        public int RoomId { get; set; }

        public string Name { get; private set; }

        public int Health { get; set; }

        public int Score { get; set; }

        public int Team { get; private set; }

        public int CompareTo(object obj)
        {
            Bunny other = (Bunny)obj;
            string x = this.Name;
            string y = other.Name;
            if (x.Length >= y.Length)
            {
                int lenDiff = x.Length - y.Length;
                for (int i = x.Length - 1; i >= lenDiff; i--)
                {
                    if (x[i] > y[i - lenDiff])
                    {
                        return 1;
                    }
                    if (x[i] < y[i - lenDiff])
                    {
                        return -1;
                    }
                }
                return this.Name.Length.CompareTo(other.Name.Length);
            }
            else
            {
                int lenDiff = y.Length - x.Length;
                for (int i = y.Length - 1; i >= lenDiff; i--)
                {
                    if (y[i] > x[i - lenDiff])
                    {
                        return -1;
                    }
                    if (y[i] < x[i - lenDiff])
                    {
                        return 1;
                    }
                }
                return this.Name.Length.CompareTo(other.Name.Length);
            }


            ////slower
            //int compare = String.Compare(String.Join("", this.Name.Reverse()), String.Join("", other.Name.Reverse()), StringComparison.Ordinal);
            //if (compare == 0)
            //{
            //    return this.Name.Length.CompareTo(other.Name.Length);
            //}
            //return compare;
        }
    }
}

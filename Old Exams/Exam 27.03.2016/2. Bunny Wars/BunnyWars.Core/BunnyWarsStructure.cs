namespace BunnyWars.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class BunnyWarsStructure : IBunnyWarsStructure
    {
        private OrderedDictionary<int, HashSet<Bunny>[]> rooms = new OrderedDictionary<int, HashSet<Bunny>[]>();
        private Dictionary<int, OrderedSet<Bunny>> teams = new Dictionary<int, OrderedSet<Bunny>>();
        private Dictionary<string, Bunny> bunnies = new Dictionary<string, Bunny>(); 
        private OrderedSet<Bunny> bunniesBySuffix = new OrderedSet<Bunny>();

        public int BunnyCount { get { return this.bunnies.Count; } }

        public int RoomCount { get { return this.rooms.Count; } }

        public void AddRoom(int roomId)
        {
            if (rooms.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }
            rooms.Add(roomId, new HashSet<Bunny>[]{new HashSet<Bunny>(),  
            new HashSet<Bunny>(), new HashSet<Bunny>(), new HashSet<Bunny>(), new HashSet<Bunny>()}); 
        }

        public void AddBunny(string name, int team, int roomId)
        {
            if (team < 0 || team > 4)
            {
                throw new IndexOutOfRangeException();
            }
            if (!rooms.ContainsKey(roomId) || bunnies.ContainsKey(name))
            {
                throw new ArgumentException();
            }
            Bunny bunny = new Bunny(name, team, roomId);
            bunnies.Add(name, bunny);
            bunniesBySuffix.Add(bunny);
            if (!teams.ContainsKey(team))
            {
                teams.Add(team, new OrderedSet<Bunny>((x, y) => y.Name.CompareTo(x.Name)));
            }
            teams[team].Add(bunny);
            rooms[roomId][team].Add(bunny);
        }

        public void Remove(int roomId)
        {
            if (!rooms.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }
            foreach (var team in rooms[roomId])
            {
                foreach (var bunny in team)
                {
                    teams[bunny.Team].Remove(bunny);
                    bunnies.Remove(bunny.Name);
                    bunniesBySuffix.Remove(bunny);
                }
            }
            rooms.Remove(roomId);
        }

        public void Next(string bunnyName)
        {
            if (!bunnies.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }
            Bunny bunny = bunnies[bunnyName];
            int roomId = bunny.RoomId;
            var nextRooms = rooms.RangeFrom(roomId, false);
            int nextRoomId;
            if (nextRooms.Count == 0)
            {
                nextRoomId = rooms.First().Key;
            }
            else
            {
                nextRoomId = nextRooms.First().Key;
            }
            rooms[roomId][bunny.Team].Remove(bunny);
            rooms[nextRoomId][bunny.Team].Add(bunny);
            bunny.RoomId = nextRoomId;
        }

        public void Previous(string bunnyName)
        {
            if (!bunnies.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }
            Bunny bunny = bunnies[bunnyName];
            int roomId = bunny.RoomId;
            var previousRooms = rooms.RangeTo(roomId, false).Reversed();
            int prevRoomId;
            if (previousRooms.Count == 0)
            {
                prevRoomId = rooms.Reversed().First().Key;
            }
            else
            {
                prevRoomId = previousRooms.First().Key;
            }
            rooms[roomId][bunny.Team].Remove(bunny);
            rooms[prevRoomId][bunny.Team].Add(bunny);
            bunny.RoomId = prevRoomId;
        }

        public void Detonate(string bunnyName)
        {
            if (!bunnies.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }
            Bunny detonated = bunnies[bunnyName];
            int roomId = detonated.RoomId;
            for (int i = 0; i < rooms[roomId].Length; i++)
            {
                if (i == detonated.Team)
                {
                    continue;
                }
                HashSet<Bunny> buns = rooms[roomId][i];
                HashSet<Bunny> toRemove = new HashSet<Bunny>();
                foreach (var bun in buns)
                {
                    if (bun.Team != detonated.Team)
                    {
                        bun.Health -= 30;
                        if (bun.Health <= 0)
                        {
                            string name = bun.Name;
                            toRemove.Add(bun);
                            teams[bun.Team].Remove(bun);
                            bunnies.Remove(name);
                            bunniesBySuffix.Remove(bun);
                            detonated.Score++;
                        }
                    }
                }
                buns.ExceptWith(toRemove);
            }
        }

        public IEnumerable<Bunny> ListBunniesByTeam(int team)
        {
            if (team < 0 || team > 4)
            {
                throw new IndexOutOfRangeException();
            }
            return teams[team];
        }


        public IEnumerable<Bunny> ListBunniesBySuffix(string suffix)
        {
            if (suffix == string.Empty)
            {
                return bunniesBySuffix;
            }
            return bunniesBySuffix.Range(new Bunny((char)char.MinValue + suffix, 0, 0), true, new Bunny((char)char.MaxValue + suffix, 0, 0), true);
        }
    }
}

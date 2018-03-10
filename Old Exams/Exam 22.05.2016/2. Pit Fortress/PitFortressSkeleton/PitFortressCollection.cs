namespace PitFortress
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PitFortress.Classes;
    using PitFortress.Interfaces;
    using Wintellect.PowerCollections;

    public class PitFortressCollection : IPitFortress
    {
        private Dictionary<string, Player> players;
        private OrderedBag<Minion> minions;
        private HashSet<Mine> mines;
        int mineCounter = 1;
        int minionsCounter = 1;

        public PitFortressCollection()
        {
            this.mineCounter = 1;
            this.minionsCounter = 1;
            this.players = new Dictionary<string, Player>();
            this.minions = new OrderedBag<Minion>();
            this.mines = new HashSet<Mine>();
        }

        public int PlayersCount { get { return this.players.Count; } }

        public int MinionsCount { get { return this.minions.Count; } }

        public int MinesCount { get { return this.mines.Count; } }

        public void AddPlayer(string name, int mineRadius)
        {
            if (players.ContainsKey(name) || mineRadius < 0)
            {
                throw new ArgumentException();
            }
            Player player = new Player(name, mineRadius);
            players.Add(name, player);
        }

        public void AddMinion(int xCoordinate)
        {
            if (xCoordinate < 0 || xCoordinate > 1000000)
            {
                throw new ArgumentException();
            }
            Minion minion = new Minion(xCoordinate, this.minionsCounter);
            this.minionsCounter++;
            this.minions.Add(minion);
        }

        public void SetMine(string playerName, int xCoordinate, int delay, int damage)
        {
            if (!players.ContainsKey(playerName) || xCoordinate < 0 || xCoordinate > 1000000 ||
                delay < 1 || delay > 10000 || damage < 0 || damage > 100)
            {
                throw new ArgumentException();
            }
            Player player = this.players[playerName];
            Mine mine = new Mine(this.mineCounter, delay, damage, xCoordinate, player);
            this.mines.Add(mine);
            this.mineCounter++;
        }

        public IEnumerable<Minion> ReportMinions()
        {
            return this.minions;
        }

        public IEnumerable<Player> Top3PlayersByScore()
        {
            if (players.Count < 3)
            {
                throw new ArgumentException();
            }
            return this.players.Values.OrderByDescending(x => x.Score).ThenByDescending(x => x.Name).Take(3);
        }

        public IEnumerable<Player> Min3PlayersByScore()
        {
            if (players.Count < 3)
            {
                throw new ArgumentException();
            }
            return this.players.Values.OrderBy(x => x.Score).ThenBy(x => x.Name).Take(3);
        }

        public IEnumerable<Mine> GetMines()
        {
            return this.mines.OrderBy(x => x.Delay).ThenBy(x => x.Id);
        }

        public void PlayTurn()
        {
            HashSet<Mine> toDelete = new HashSet<Mine>();
            foreach (var mine in this.mines)
            {
                mine.Delay--;
                if (mine.Delay == 0)
                {
                    this.ExplodeMine(mine);
                    toDelete.Add(mine);
                }
            }
            this.mines.ExceptWith(toDelete);
        }

        private void ExplodeMine(Mine mine)
        {
            int radius = mine.Player.Radius;
            int min = mine.XCoordinate - radius;
            int max = mine.XCoordinate + radius;
            List<Minion> toDelete = new List<Minion>();
            foreach (var minion in minions.Range(new Minion(min, 1), true, new Minion(max, int.MaxValue), true))
            {
                minion.Health -= mine.Damage;
                if (minion.Health <= 0)
                {
                    toDelete.Add(minion);
                    mine.Player.Score++;
                }
            }
            foreach (var minion in toDelete)
            {
                minions.Remove(minion);
            }
        }
    }
}

namespace PitFortress.Classes
{
    using PitFortress.Interfaces;

    public class Player : IPlayer
    {
        public int CompareTo(Player other)
        {
            if (this.Score != other.Score)
            {
                return this.Score.CompareTo(other.Score);
            }
            else
            {
                return this.Name.CompareTo(other.Name);
            }
        }

        public string Name { get; private set; }

        public int Radius { get; private set; }

        public int Score { get; set; }

        public Player(string name, int radius)
        {
            Name = name;
            Radius = radius;
            this.Score = 0;
        }
    }
}

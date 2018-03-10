namespace PitFortress.Classes
{
    using PitFortress.Interfaces;

    public class Minion : IMinion
    {
        public int CompareTo(Minion other)
        {
            if (this.XCoordinate != other.XCoordinate)
            {
                return this.XCoordinate.CompareTo(other.XCoordinate);
            }
            else
            {
                return this.Id.CompareTo(other.Id);
            }
        }

        public int Id { get; private set; }

        public int XCoordinate { get; private set; }

        public int Health { get; set; }

        public Minion(int xCoordinate, int id)
        {
            this.XCoordinate = xCoordinate;
            this.Health = 100;
            this.Id = id;
        }
    }
}

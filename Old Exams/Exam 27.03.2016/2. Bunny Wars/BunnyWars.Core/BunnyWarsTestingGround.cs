namespace BunnyWars.Core
{
    class BunnyWarsTestingGround
    {
        static void Main(string[] args)
        {
            var x = new BunnyWarsStructure();
            x.AddRoom(1);
            x.AddBunny("pesho", 1, 1);
            x.ListBunniesByTeam(-2);
        }
    }
}

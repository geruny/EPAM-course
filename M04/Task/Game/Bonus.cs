namespace Game
{
    internal abstract class Bonus : IGameConsole
    {
        public string NameBonus { get; protected set; }
        public int Points { get; protected set; }

        protected Bonus(string nameBonus, int points)
        {
            NameBonus = nameBonus;
            Points = points;
        }

        public virtual void GivePoints()
        {
            IGameConsole.Print($"Give {Points} points");
        }
    }
}

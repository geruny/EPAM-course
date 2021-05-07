namespace Game
{
    internal class Player : Character
    {
        public override string Name { get; }
        public override int Damage { get; } = 3;

        public Player(int health, string name) : base(health)
        {
            Name = name;
        }

        public override void Attack()
        {
            IGameConsole.Print("Piu Piu");
        }

        public void TakeBonus()
        {
        }
    }
}

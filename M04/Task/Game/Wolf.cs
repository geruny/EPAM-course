namespace Game
{
    internal class Wolf : Monster
    {
        public override string Name { get; } = "Angry Wolf";
        public override int Damage { get; } = 3;

        public Wolf(int health, int points) : base(health, points)
        {
        }

        public override void Attack()
        {
            IGameConsole.Print("Ggrrrrrr!");
        }

    }
}

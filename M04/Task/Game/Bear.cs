namespace Game
{
    internal class Bear : Monster
    {
        public override string Name { get; } = "Angry Bear";
        public override int Damage { get; } = 5;

        public Bear(int health, int points) : base(health, points)
        {
        }

        public override void Attack()
        {
            IGameConsole.Print("Bbbbrruueee!");
        }
    }
}

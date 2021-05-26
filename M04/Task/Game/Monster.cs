namespace Game
{
    internal abstract class Monster : Character
    {
        private int _points;

        protected Monster(int health, int points) : base(health)
        {
            _points = points;
        }
    }
}

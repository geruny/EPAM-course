namespace Game
{
    internal abstract class Character : IGameConsole
    {
        public int Health { get; protected set; }
        public abstract string Name { get; }
        public abstract int Damage { get; }

        protected Character(int health)
        {
            Health = health;
        }

        public virtual void Run()
        {
        }

        public abstract void Attack();

        public void Die()
        {
        }
    }
}

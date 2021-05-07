namespace Game
{
    internal abstract class Obstacle
    {
        protected int Damage = 1;
        public abstract int Width { get; protected set; }
    }
}

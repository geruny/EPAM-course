using System.Collections.Generic;

namespace Game
{
    internal class Game : IGameConsole
    {
        private static Field _field;
        private static List<Monster> _monsters = new();
        private static List<Obstacle> _obstacles = new();
        private static List<Bonus> _bonuses = new();
        private static Player _player;

        public void Start(int width, int height)
        {
            _field = new Field(width, height);

            _player = new Player(30, "Vasya");

            _monsters.Add(new Bear(20, 15));
            _monsters.Add(new Wolf(10, 7));

            _obstacles.Add(new Stone());
            _obstacles.Add(new Tree());

            _bonuses.Add(new Apple("Super Apple", 2));
            _bonuses.Add(new Banana("Long Banana", 5));
            _bonuses.Add(new Cherry("Magic Cherry", 10));
        }

        public void Pause()
        {
            IGameConsole.Print("Paused");
        }

        public void Win()
        {
            IGameConsole.Print("U win!");
        }

        public void GameOver()
        {
            _monsters.Clear();
            _obstacles.Clear();
            _bonuses.Clear();
        }
    }
}

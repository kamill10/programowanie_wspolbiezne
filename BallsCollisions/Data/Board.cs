using System.Collections.ObjectModel;


namespace Data
{
    public class Board
    {
        private const int V = 450;
        private const int V1 = 750;
        public static int _boardHeight = V;
        public static int _boardWidth = V1;
        private ObservableCollection<Balls> _balls = new();
        
        public Board()
        {

        }
        public int BoardHeight
        {
            get => _boardHeight;
        }
        public int BoardWidth
        {
            get => _boardWidth;
        }
        public ObservableCollection<Balls> Balls
        {
            get => _balls;
        }
        public void GenerateBalls(int amount, float radious, float mass,float v)
        {
            Random random = new Random();
            for (int i = 0; i < amount; i++)
            {
                Balls ball = new(v,radious,mass)
                {
                    Position = new System.Numerics.Vector2(random.Next(10, BoardWidth - 15), random.Next(10, BoardHeight - 15)),
                    Valocity = new System.Numerics.Vector2((float)0.003, (float)0.003)
                };
                _balls.Add(ball);
            }

        }
    }
}

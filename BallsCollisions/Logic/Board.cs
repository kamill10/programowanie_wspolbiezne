using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Board
    {
        public static int _boardHeight = 450;
        public static int _boardWidth = 750;
        private ObservableCollection<Balls> _balls = new ObservableCollection<Balls>();
        private int _ballsnumber;

        public Board(int ballsnumber)
        {
            _ballsnumber = ballsnumber;
        }
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
        public void GenerateBalls(int amount )
        {
            Random random = new Random();
            for(int i =0; i < amount; i++)
            {
                Balls ball = new Balls(10);
                ball.Position = new System.Numerics.Vector2(random.Next(10, BoardWidth - 15), random.Next(10,BoardHeight - 15));
                ball.Valocity = new System.Numerics.Vector2((float) 0.003, (float)0.003);
                _balls.Add(ball);
            }

        }
    }
}

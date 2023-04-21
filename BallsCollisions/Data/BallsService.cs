using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace Data
{
    public  class BallsService 
    {
        public  static bool IsBallsCollides(Balls ballOne, Balls ballTwo)
        {
            Vector2 centerOne = ballOne.Position + (Vector2.One * ballOne.Radious / 2) + ballOne.Valocity * (16 / 1000f);
            Vector2 centerTwo = ballTwo.Position + (Vector2.One * ballTwo.Radious / 2) + ballTwo.Valocity * (16 / 1000f);

            float distance = Vector2.Distance(centerOne, centerTwo);
            float radiusSum = (ballOne.Radious + ballTwo.Radious) / 2f;

            return distance <= radiusSum;
        }

        public static void BoundFromWall(Balls ball)
        {
            if (ball.Position.X < ball.Radious - 15 || ball.Position.X > Board._boardWidth - ball.Radious)
            {
                ball.Valocity = new Vector2(-ball.Valocity.X, ball.Valocity.Y);
                ball.X += ball.Valocity.X;
            }
            if (ball.Position.Y < ball.Radious - 15 || ball.Position.Y > Board._boardHeight - ball.Radious)
            {
                ball.Valocity = new Vector2(ball.Valocity.X, -ball.Valocity.Y);
                ball.Y += ball.Valocity.Y;
            }
        }

    }
}

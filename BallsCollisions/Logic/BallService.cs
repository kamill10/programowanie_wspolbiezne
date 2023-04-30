using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using Data;
using Microsoft.VisualBasic;

namespace Logic
{
    public  class BallService
    {

        public static Balls CheckCollision(Balls ball, IEnumerable<Balls> ballsList)
        {
            foreach (Balls ballTwo in ballsList)
            {
                if (ReferenceEquals(ball, ballTwo))
                {
                    continue;
                }

                if (IsBallsCollides(ball, ballTwo))
                {
                    return ballTwo;
                }
            }

            return null;
        }

        private static bool IsBallsCollides(Balls ballOne, Balls ballTwo)
        {


            Vector2 centerOne = ballOne.Position + (Vector2.One * ballOne.Radious / 2) + ballOne.Valocity * (16 / 1000f);
        Vector2 centerTwo = ballTwo.Position + (Vector2.One * ballTwo.Radious / 2) + ballTwo.Valocity * (16 / 1000f);

        float distance = Vector2.Distance(centerOne, centerTwo);
        float radiusSum = (ballOne.Radious + ballTwo.Radious) / 2f;

        return distance <= radiusSum;  

            
        }

        private static   void HandleColide(Balls ballOne, Balls ballTwo)
        {
            /* float ballOnenewSpeedX = (ballOne.Mass - ballTwo.Mass) * ballOne.Valocity.X / (ballOne.Mass + ballTwo.Mass) + 2 * ballTwo.Mass * ballTwo.Valocity.X / (ballOne.Mass + ballTwo.Mass);
             float ballOnenewSpeedY = (ballOne.Mass - ballTwo.Mass) * ballOne.Valocity.X / (ballOne.Mass + ballTwo.Mass) + 2 * ballTwo.Mass * ballTwo.Valocity.Y / (ballOne.Mass + ballTwo.Mass);
             float ballTwonewSpeedX = (ballTwo.Mass - ballOne.Mass) * ballTwo.Valocity.X / (ballOne.Mass + ballTwo.Mass) + 2 * ballOne.Mass * ballOne.Valocity.X / (ballOne.Mass + ballTwo.Mass);
             float ballTwonewSpeedY = (ballTwo.Mass - ballOne.Mass) * ballTwo.Valocity.X / (ballOne.Mass + ballTwo.Mass) + 2 * ballOne.Mass * ballOne.Valocity.Y / (ballOne.Mass + ballTwo.Mass);
             ballOne.Valocity = new Vector2(ballOnenewSpeedX, ballOnenewSpeedY);
             ballTwo.Valocity = new Vector2(ballTwonewSpeedX, ballTwonewSpeedY); */

            Vector2 ballOneVelocity = ballOne.Valocity;
            Vector2 ballTwoVelocity = ballTwo.Valocity;
            Vector2 collisionNormal = Vector2.Normalize(ballTwo.Position - ballOne.Position);

            float ballOneInitialVelocityAlongNormal = Vector2.Dot(ballOneVelocity, collisionNormal);
            float ballTwoInitialVelocityAlongNormal = Vector2.Dot(ballTwoVelocity, collisionNormal);

            float ballOneInitialVelocityAlongTangent = Vector2.Dot(ballOneVelocity, GetPerpendicularVector(collisionNormal));
            float ballTwoInitialVelocityAlongTangent = Vector2.Dot(ballTwoVelocity, GetPerpendicularVector(collisionNormal));

            float ballOneFinalVelocityAlongNormal = (ballOneInitialVelocityAlongNormal * (ballOne.Mass - ballTwo.Mass) + 2 * ballTwo.Mass * ballTwoInitialVelocityAlongNormal) / (ballOne.Mass + ballTwo.Mass);
            float ballTwoFinalVelocityAlongNormal = (ballTwoInitialVelocityAlongNormal * (ballTwo.Mass - ballOne.Mass) + 2 * ballOne.Mass * ballOneInitialVelocityAlongNormal) / (ballOne.Mass + ballTwo.Mass);

            Vector2 ballOneFinalVelocity = ballOneFinalVelocityAlongNormal * collisionNormal + ballOneInitialVelocityAlongTangent * GetPerpendicularVector(collisionNormal);
            Vector2 ballTwoFinalVelocity = ballTwoFinalVelocityAlongNormal * collisionNormal + ballTwoInitialVelocityAlongTangent * GetPerpendicularVector(collisionNormal);

            ballOne.Valocity = ballOneFinalVelocity;
            ballTwo.Valocity = ballTwoFinalVelocity;

        }

        private static Vector2 GetPerpendicularVector(Vector2 vector)
        {
            return new Vector2(-vector.Y, vector.X);
        }

        public static void Collide(Balls ball, ObservableCollection<Balls> balls)
        {
            Balls colided = CheckCollision(ball, balls);
            if (colided != null)
            {
                    HandleColide(colided, ball);            
            }
           if (colided != null)
            {
                ball.Position+= new Vector2(2*ball.Valocity.X * ball.Speed, 2*ball.Valocity.Y * ball.Speed);
            }  
            

        }
}


}


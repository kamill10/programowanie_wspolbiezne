using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Data;

namespace Logic
{
    public  class BallService
    {

        public static Balls CheckCollisions(Balls ball, IEnumerable<Balls> ballsList)
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

        public static void HandleColide(Balls ballOne, Balls ballTwo)
        {
            
            Vector2 centerOne = ballOne.Position + (Vector2.One * ballOne.Radious / 2);
            Vector2 centerTwo = ballTwo.Position + (Vector2.One * ballTwo.Radious / 2);

            Vector2 unitNormalVector = Vector2.Normalize(centerTwo - centerOne);
            Vector2 unitTangentVector = new Vector2(-unitNormalVector.Y, unitNormalVector.X);

            float velocityOneNormal = Vector2.Dot(unitNormalVector, ballOne.Valocity);
            float velocityOneTangent = Vector2.Dot(unitTangentVector, ballOne.Valocity);
            float velocityTwoNormal = Vector2.Dot(unitNormalVector, ballTwo.Valocity);
            float velocityTwoTangent = Vector2.Dot(unitTangentVector, ballTwo.Valocity);

            float newNormalVelocityOne = (velocityOneNormal * (ballOne.Mass - ballTwo.Mass) + 2 * ballTwo.Mass * velocityTwoNormal) / (ballOne.Mass + ballTwo.Mass);
            float newNormalVelocityTwo = (velocityTwoNormal * (ballTwo.Mass - ballOne.Mass) + 2 * ballOne.Mass * velocityOneNormal) / (ballOne.Mass + ballTwo.Mass);

            Vector2 newVelocityOne = Vector2.Multiply(unitNormalVector, newNormalVelocityOne) + Vector2.Multiply(unitTangentVector, velocityOneTangent);
            Vector2 newVelocityTwo = Vector2.Multiply(unitNormalVector, newNormalVelocityTwo) + Vector2.Multiply(unitTangentVector, velocityTwoTangent);

            ballOne.Valocity = newVelocityOne;
            ballTwo.Valocity = newVelocityTwo;

            //ballOne.Speed = (((ballOne.Mass - ballTwo.Mass) / (ballOne.Mass + ballTwo.Mass) * ballOne.Speed) + (2 * ballTwo.Mass / (ballOne.Mass + ballTwo.Mass) * ballTwo.Speed));
            //ballTwo.Speed = (((ballTwo.Mass - ballOne.Mass) / (ballOne.Mass + ballTwo.Mass) * ballTwo.Speed) + (2 * ballOne.Mass / (ballOne.Mass + ballTwo.Mass) * ballOne.Speed));
        }

      
    }
}

﻿using System;
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
            float ballOnenewSpeedX = (ballOne.Mass - ballTwo.Mass) * ballOne.Valocity.X / (ballOne.Mass + ballTwo.Mass) + 2 * ballTwo.Mass * ballTwo.Valocity.X / (ballOne.Mass + ballTwo.Mass);
            float ballOnenewSpeedY = (ballOne.Mass - ballTwo.Mass) * ballOne.Valocity.X / (ballOne.Mass + ballTwo.Mass) + 2 * ballTwo.Mass * ballTwo.Valocity.Y / (ballOne.Mass + ballTwo.Mass);
            float ballTwonewSpeedX = (ballTwo.Mass - ballOne.Mass) * ballTwo.Valocity.X / (ballOne.Mass + ballTwo.Mass) + 2 * ballOne.Mass * ballOne.Valocity.X / (ballOne.Mass + ballTwo.Mass);
            float ballTwonewSpeedY = (ballTwo.Mass - ballOne.Mass) * ballTwo.Valocity.X / (ballOne.Mass + ballTwo.Mass) + 2 * ballOne.Mass * ballOne.Valocity.Y / (ballOne.Mass + ballTwo.Mass);
            ballOne.Valocity = new Vector2(ballOnenewSpeedX, ballOnenewSpeedY);
            ballTwo.Valocity = new Vector2(ballTwonewSpeedX, ballTwonewSpeedY);

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
                ball.Position+= new Vector2(ball.Valocity.X * ball.Speed, ball.Valocity.Y * ball.Speed);
                colided.Position += new Vector2(colided.Valocity.X * colided.Speed, colided.Valocity.Y * colided.Speed);
            }

        }
}

    public static class Constants
    {
        public const float RestitutionCoefficient = 0.8f;
    }

}


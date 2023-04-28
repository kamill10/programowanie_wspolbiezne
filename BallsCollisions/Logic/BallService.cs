﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using Data;

namespace Logic
{
    public  class BallService
    {

        private static Balls CheckCollisions(Balls ball, IEnumerable<Balls> ballsList)
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

        private static void HandleColide(Balls ballOne, Balls ballTwo)
        {

            throw new Exception("ds");

        
            }

        public static void Collide(Balls ball,ObservableCollection<Balls>balls)
        {
            Balls colided = CheckCollisions(ball, balls);
            if (colided != null)
            {
                HandleColide(colided, ball);

            }
        }

      
    }
}

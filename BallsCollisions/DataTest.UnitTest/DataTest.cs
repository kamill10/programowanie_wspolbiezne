using Data;
using System.Numerics;

namespace DataTest.UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {


        }

        [Test]
        public void BoardTest()
        {
            Board board = new();
            board.GenerateBalls(10,3,4,5);
            Assert.That(board.Balls, Has.Count.EqualTo(10));
        }

        [Test]
        public void BallContructorTest()
        {
            Vector2 v = new(1, 2);
            int radius = 5;
            Balls ball = new(v, radius);
            Assert.Multiple(() =>
            {
                Assert.That(ball.Radious, Is.EqualTo(radius));
                Assert.That(ball.Position, Is.EqualTo(v));
            });
        }

        [Test]
        public void PositionChangedTest()
        {
            Balls ball = new(1500, 4, 5)
            {
                Valocity = new Vector2((float)0.003, (float)0.003),
                Position = new Vector2(50, 50)
            };
            ball.ChangePosition();
            Assert.AreNotEqual(50, ball.Position.X);
            Assert.AreNotEqual(50, ball.Position.Y);
            Assert.AreNotEqual(750, ball.Position.X);
            Assert.AreNotEqual(450, ball.Position.Y);
            Assert.That(new Vector2((float)0.003, (float)0.003), Is.EqualTo(ball.Valocity));
            
        }
    }
}
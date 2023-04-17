using Data;
using Logic;
using System.Numerics;

namespace LogicTest.UnitTest
{
    public class Tests
    {
        LogicAbstractApi api;

        [SetUp]
        public void Setup()
        {
            api = LogicAbstractApi.CreateLogicAPI(DataAbstractApi.CreateDataApi());

        }

        [Test]
        public void BoardTest()
        {
            Board board = new();
            board.GenerateBalls(10);
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
            Balls ball = new()
            {
                Valocity = new Vector2((float)0.003, (float)0.003),
                Position = new Vector2(50, 50)
            };
            ball.ChangePosition();
            Vector2 vector = new((float)(50 + 1500 * 0.003), (float)(50 + 1500 * 0.003));
            Assert.That(ball.Position, Is.EqualTo(vector));
        }



        [Test]
        public void logicApiTest()
        {
            Board board = new();
            board.GenerateBalls(10);
            api.TaskRun(board);
            api.TaskStop(board);
            Assert.That(board.Balls, Is.Empty);

        }

    }
}
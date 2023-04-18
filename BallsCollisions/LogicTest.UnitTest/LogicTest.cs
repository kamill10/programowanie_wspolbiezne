using Data;
using Logic;
using System.Numerics;

namespace LogicTest.UnitTest
{
    public class Tests
    {
        LogicAbstractApi api ;

        [SetUp]
        public void Setup()
        {
            api = LogicAbstractApi.CreateLogicAPI(DataAbstractApi.CreateDataApi());

        }

        [Test] 
        public void BoardTest()
        {
            Board board = new Board();
            board.GenerateBalls(10);
            Assert.True(board.Balls.Count == 10);
        }

        [Test]
        public void BallContructorTest()
        {
            Vector2 v = new Vector2(1, 2);
            int radius = 5;
            Balls ball = new Balls(v, radius);

            Assert.AreEqual(radius, ball.Radious);
            Assert.AreEqual(v, ball.Position);

        }

        [Test]
        public void PositionChangedTest()
        {
            Balls ball = new Balls();
            ball.Valocity = new Vector2((float)0.003, (float)0.003);
            ball.Position = new Vector2(50,50);
            ball.ChangePosition();
            Vector2 vector = new Vector2((float)(50 + 1500 * 0.003), (float)(50 + 1500 * 0.003));
            Assert.That(ball.Position, Is.EqualTo(vector));
        }

       

        [Test]
        public void logicApiTest()
        {
            Board board = new Board();
            board.GenerateBalls(10);
            api.TaskRun(board);
            api.TaskStop(board);
            Assert.True(board.Balls.Count == 0);

        }  
        
    }
}
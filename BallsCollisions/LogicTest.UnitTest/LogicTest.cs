using Data;
using Logic;
using System.Numerics;

namespace LogicTest.UnitTest
{
    public class Tests
    {
        LogicAbstractApi api;
        DataAbstractApi data;

        [SetUp]
        public void Setup()
        {
            data = DataAbstractApi.CreateDataApi(5, 4, 3);
            data.generateBalls(10);
            api = LogicAbstractApi.CreateLogicAPI(data);
        }


        [Test]
        public void CreateBallsTest()
        {
            int _amount = 10;
            int _radius = 25;
            api.TaskRun();
            //Console.WriteLine(api.getBalls().Count);
            //Assert.That(_amount, Is.EqualTo(api.getBalls().Count));

            foreach (Balls ball in data.getBalls())
            {
                Assert.IsTrue(ball.Position.X >= 1);
                Assert.IsTrue(ball.Position.X <= 750);
                Assert.IsTrue(ball.Position.Y >= 1);
                Assert.IsTrue(ball.Position.Y <= 450);
            }

        }

        [Test]
        public void DeleteBallsTest()
        {
            api.TaskStop();
            //Assert.AreEqual(0, api.getBalls().Count);
        }



    }
}
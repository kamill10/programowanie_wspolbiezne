using Logic;
using System.Numerics;

namespace LogicTest.UnitTest
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
            Board board = new Board();
            board.GenerateBalls(10);
            Assert.True(board.Balls.Count == 10);

        }
        public void Balls_Move()
        {
            // Arrange
            var obj = new Balls();
            obj.Position = new Vector2(50, 50);
            obj.Valocity = new Vector2(1, 1);
            obj.Speed = 10;
            obj.Radious = 10;
            Assert.True(obj.X == 50);
            Assert.True(obj.Y == 50);
            Assert.True(obj.Valocity == new Vector2(1, 1));
            Assert.True(obj.Position == new Vector2(50, 50));
            Assert.True(obj.Speed == 10);            // Act
            obj.ChangePosition();
            // Assert
            Assert.AreEqual(new Vector2(60, 60), obj.Position); // position should be updated
            //Check bound from the wall
            obj.Position = new Vector2(741, 451);
            obj.Valocity = new Vector2(1, 1);
            obj.ChangePosition();
            Assert.AreEqual(new Vector2(731, 431), obj.Position); // position should be updated
            Assert.AreEqual(new Vector2(-1, -1), obj.Valocity); // position should be updated

        }
    }
}
using Solution1;

namespace CalculatorTest
{
    public class Tests
    {
        private int result1;
      
        Calculator calculator = new Calculator();

        [SetUp]
        public void Setup()
        {
            result1 = calculator.pov(2, 3);

        }

        [Test]

        public void TestPov() => Assert.AreEqual(8, result1);
    }
}
using Solution1;

namespace CalculatorTest
{
    public class Tests
    {
        private int result;
        Calculator calculator = new Solution1.Calculator();

        [SetUp]
        public void Setup()
        {
             result = calculator.add(2, 3);
        }

        [Test]

        public void Test1() => Assert.AreEqual(5, result);
    }
}
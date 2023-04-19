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
            api = LogicAbstractApi.CreateLogicAPI(DataAbstractApi.CreateDataApi(5,4));

        }

       
    }
}
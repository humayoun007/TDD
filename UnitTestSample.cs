using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace EFFIK.Tests
{
    

    
    public class UnitTestSample
    {
        private ITestOutputHelper output;

        public UnitTestSample(ITestOutputHelper output)
        {
            this.output = output;
        }


        [Fact]
        public void PassingTest()
        {
            output.WriteLine("Expected {0}, Actual is: {1}", 4, Add(2, 2));
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            output.WriteLine("Expected {0}, Actual is: {1}", 5, Add(2, 2));
            Assert.Equal(5, Add(2, 2));
        }

        int Add(int x,int y)
        {
            return x + y;
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(6)]
        public void MyFirstTheroy(int value)
        {
            output.WriteLine($"value is {value}, Is odd: {IsOdd(value)}");
            Assert.True(IsOdd(value));
        }

        bool IsOdd(int value)
        {
            return value % 2 == 1;
        }

        public static IEnumerable<object[]> TestData => new object[][] { 
          new object[] {42},
          new object[] {21.12},
          new object[] { DateTime.Now},
          new object[] {null}
        };

        [Theory]
        [MemberData(nameof(TestData))]
        public void UnrunTestRepro(object data)
        {
            output.WriteLine($"value of data is: {data}");
            Assert.NotNull(data);
        }
    }
}

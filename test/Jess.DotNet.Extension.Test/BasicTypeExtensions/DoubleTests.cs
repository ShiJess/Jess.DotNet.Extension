using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Jess.DotNet.Extension;

namespace Jess.DotNet.Extension.Test
{
    public class DoubleTests
    {
        [Theory]
        [InlineData(1, 2, 3, false)]
        [InlineData(2.5, 2, 3, true)]
        [InlineData(4, 2, 3, false)]
        [InlineData(2, 2, 3, false)]
        [InlineData(3, 2, 3, false)]
        public void DoubleBetween(double current, double begin, double end, bool result)
        {
            Assert.Equal(result, current.Between(begin, end));
        }

        [Theory]
        [InlineData(1, 2, 3, false)]
        [InlineData(2.5, 2, 3, true)]
        [InlineData(4, 2, 3, false)]
        [InlineData(2, 2, 3, true)]
        [InlineData(3, 2, 3, true)]
        public void DoubleBetweenInclude(double current, double begin, double end, bool result)
        {
            Assert.Equal(result, current.BetweenInclude(begin, end));
        }

    }
}

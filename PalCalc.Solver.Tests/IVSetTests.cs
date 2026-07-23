namespace PalCalc.Solver.Tests
{
    [TestClass]
    public class IVSetTests
    {
        [TestMethod]
        public void CompareQuality_PrefersHighestAverageThenHighestMinimum()
        {
            var lowerAverage = new IV_Set(
                new IV_Value(false, 90, 90),
                new IV_Value(false, 90, 90),
                new IV_Value(false, 90, 90)
            );
            var higherAverage = lowerAverage with { Defense = new IV_Value(false, 100, 100) };
            var widerRange = higherAverage with { Defense = new IV_Value(false, 90, 100) };

            Assert.IsTrue(higherAverage.CompareQualityTo(lowerAverage) > 0);
            Assert.IsTrue(higherAverage.CompareQualityTo(widerRange) > 0);
        }
    }
}

namespace PalCalc.Solver.Tests
{
    [TestClass]
    public class IVSetTests
    {
        [TestMethod]
        public void CompareIVs_DistinguishesAverageFromPotential()
        {
            var consistent = new IV_Set(
                new IV_Value(false, 63, 73),
                new IV_Value(false, 88, 89),
                new IV_Value(false, 98, 99)
            );
            var highPotential = new IV_Set(
                new IV_Value(false, 30, 98),
                new IV_Value(false, 31, 97),
                new IV_Value(false, 31, 91)
            );

            Assert.IsTrue(consistent.CompareAverageTo(highPotential) > 0);
            Assert.IsTrue(highPotential.ComparePotentialTo(consistent) > 0);
        }
    }
}

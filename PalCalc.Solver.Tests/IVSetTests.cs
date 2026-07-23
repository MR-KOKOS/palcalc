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

        [TestMethod]
        public void PotentialDominance_KeepsComplementaryIVs()
        {
            var balanced = new IV_Set(
                new IV_Value(false, 80, 80),
                new IV_Value(false, 91, 91),
                new IV_Value(false, 49, 49)
            );
            var highDefense = new IV_Set(
                new IV_Value(false, 41, 41),
                new IV_Value(false, 28, 28),
                new IV_Value(false, 100, 100)
            );

            Assert.IsFalse(balanced.PotentiallyDominates(highDefense));
            Assert.IsFalse(highDefense.PotentiallyDominates(balanced));
        }
    }
}

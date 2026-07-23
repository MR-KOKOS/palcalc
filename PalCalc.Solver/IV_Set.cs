using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalCalc.Solver
{
    public readonly record struct IV_Set(IV_Value HP, IV_Value Attack, IV_Value Defense)
    {
        public int TotalMax => HP.Max + Attack.Max + Defense.Max;
        public int TotalMin => HP.Min + Attack.Min + Defense.Min;
        public int AverageScore => TotalMin + TotalMax;

        public int ComparePotentialTo(IV_Set other)
        {
            var maxComparison = TotalMax.CompareTo(other.TotalMax);
            return maxComparison != 0 ? maxComparison : TotalMin.CompareTo(other.TotalMin);
        }

        public int CompareAverageTo(IV_Set other)
        {
            var averageComparison = AverageScore.CompareTo(other.AverageScore);
            return averageComparison != 0 ? averageComparison : TotalMin.CompareTo(other.TotalMin);
        }

        public int CompareQualityTo(IV_Set other) => ComparePotentialTo(other);

        public bool PotentiallyDominates(IV_Set other) =>
            HP.Max >= other.HP.Max &&
            Attack.Max >= other.Attack.Max &&
            Defense.Max >= other.Defense.Max;
    }
}

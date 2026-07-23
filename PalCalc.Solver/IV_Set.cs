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

        public int CompareQualityTo(IV_Set other)
        {
            var maxComparison = TotalMax.CompareTo(other.TotalMax);
            return maxComparison != 0 ? maxComparison : TotalMin.CompareTo(other.TotalMin);
        }
    }
}

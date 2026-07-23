using PalCalc.Solver.PalReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalCalc.Solver.ResultPruning
{
    public class AggregatePruning : IResultPruning
    {
        List<IResultPruning> contents;

        public AggregatePruning(CancellationToken token, IEnumerable<IResultPruning> contents) : base(token)
        {
            this.contents = contents.ToList();
        }

        public override IEnumerable<IPalReference> Apply(IEnumerable<IPalReference> results, CachedResultData cachedData) =>
            contents.Aggregate(results, (r, p) => p.Apply(r, cachedData));
    }

    public class UnionPruning(CancellationToken token, params IResultPruning[] contents) : IResultPruning(token)
    {
        public override IEnumerable<IPalReference> Apply(IEnumerable<IPalReference> results, CachedResultData cachedData)
        {
            var source = results.ToList();
            return contents.SelectMany(p => p.Apply(source, cachedData)).Distinct();
        }
    }
}

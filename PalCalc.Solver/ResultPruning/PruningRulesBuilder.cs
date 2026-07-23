using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalCalc.Solver.ResultPruning
{
    public class PruningRulesBuilder
    {
        public Func<CancellationToken, IEnumerable<IResultPruning>> Build { get; }

        public Func<CancellationToken, IResultPruning> BuildAggregate => t => new AggregatePruning(t, Build(t));

        public PruningRulesBuilder(Func<CancellationToken, IEnumerable<IResultPruning>> build)
        {
            Build = build;
        }

        public PruningRulesBuilder WithRule(Func<CancellationToken, IResultPruning> ruleBuilder) =>
            new PruningRulesBuilder(
                t => Build(t).Append(ruleBuilder(t))
            );

        public PruningRulesBuilder PrioritizeIVs(bool average, bool potential) =>
            new PruningRulesBuilder(token =>
            {
                List<IResultPruning> RulesFor(bool prioritizeAverage, bool prioritizePotential)
                {
                    var rules = Build(token).Where(r => r is not OptimalIVsPruning).ToList();
                    rules.Insert(
                        rules.FindIndex(r => r is MinimumEffortPruning) + 1,
                        new OptimalIVsPruning(token, maxIvDifference: 0, prioritizeAverage, prioritizePotential)
                    );
                    return rules;
                }

                if (average && potential)
                    return [
                        new UnionPruning(
                            token,
                            new AggregatePruning(token, RulesFor(true, false)),
                            new AggregatePruning(token, RulesFor(false, true))
                        )
                    ];

                return RulesFor(average, potential);
            });

        public static readonly PruningRulesBuilder Default = new(
            token => new List<IResultPruning>
                {
                    new MinimumEffortPruning(token),
                    new MinimumBreedingStepsPruning(token),
                    new OptimalIVsPruning(token, maxIvDifference: 10),
                    new MinimumCostPruning(token),
                    new PreferredLocationPruning(token),
                    new MinimumReusePruning(token),
                    new MinimumWildPalsPruning(token),
                    new MinimumReferencedPlayersPruning(token),
                    new VariedResultsPruning(token, maxSimilarityPercent: 0.1f),
                    new ResultLimitPruning(token, maxResults: 3),
                }
        );
    }
}

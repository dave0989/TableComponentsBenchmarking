namespace Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Core.Data;

    using NUnit.Framework;

    [TestFixture]
    public abstract class BenchmarkFixtureBase<T> where T : SecurityRecordWithoutINotifyPropertyChanged, new()
    {
        private static readonly int[] scenarios = new[] { 10, 100, 1000, 10000, 100000, 1000000 };

        private const int NumberOfTrials = 100;

        private Dictionary<int, SecurityRepository<T>> securityRepository;

        [TestFixtureSetUp]
        protected void Init()
        {
            this.securityRepository = new Dictionary<int, SecurityRepository<T>>();

            foreach (var scenario in scenarios)
            {
                this.securityRepository.Add(scenario, new SecurityRepository<T>(scenario));
            }
        }

        protected void Tester(Action<IEnumerable> assignDataSource, Action resetDataSource)
        {
            Console.WriteLine("Scenario, CollectionType, AverageTime, MemoryBeforeExecution, MemoryAfterExecution");
            foreach (var scenario in scenarios)
            {
                var repository = this.securityRepository[scenario];
                foreach (var sourceCollection in repository.DataSources())
                {
                    IEnumerable collection = sourceCollection;
                    long totalMemoryBeforeExecution = GC.GetTotalMemory(false) / 1024;

                    double averageTime = 0;
                    for (var i = 0; i < NumberOfTrials; ++i)
                    {
                        var startTime = DateTime.Now;

                        assignDataSource(collection);

                        var endTime = DateTime.Now;

                        var elapsedTime = (endTime - startTime).TotalMilliseconds;
                        averageTime += elapsedTime;

                        resetDataSource();
                    }

                    long totalMemoryAfterExecutionAndGarbageCollection = GC.GetTotalMemory(true) / 1024;

                    Console.WriteLine("{0}, {1}, {2}, {3},{4}", scenario, collection.GetType(), averageTime / NumberOfTrials, totalMemoryBeforeExecution, totalMemoryAfterExecutionAndGarbageCollection);
                }
            }
        }
    }
}
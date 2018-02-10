using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Example
    {
        //I want to find the integer that better appoximate PI * 100
        const float GOAL = (float)Math.PI * 100;

        static void Main(string[] args)
        {
            EvolveInt settings = new EvolveInt();
            GeneticAlgorithm<int> algo = new GeneticAlgorithm<int>(settings);

            int fittest = algo.Fittest();
            while (Math.Abs(GOAL - fittest) > 0.5)
            {
                algo.Step();

                if(algo.Fittest() != fittest)
                {
                    fittest = algo.Fittest();
                    Console.WriteLine(fittest + " -> " + settings.Fitness(fittest));
                }
            }

            Console.WriteLine(fittest);
            Console.Read();
        }

        private class EvolveInt : GeneticAlgorithmSettings<int>
        {
            public override float MutationRate { get => 0.001f; set => MutationRate = value; }
            public override uint PopulationSize { get => 50; protected set => PopulationSize = value; }
            public override uint NumberOfParentsForCrossover { get => 2; protected set => NumberOfParentsForCrossover = value; }

            private Random r = new Random();

            public override int Crossover(params int[] t)
            {
                return (t[0] + t[1]) / 2;
            }

            public override float Fitness(int t)
            {
                return 1.0f / (Math.Abs(GOAL - t) + 1f);
            }

            public override int Mutate(int t)
            {
                int amount = r.Next(1, 20);
                if (r.NextDouble() < 0.5f)
                    return t + amount;
                return t - amount;

            }

            public override int RandomElement()
            {
                return r.Next(1, 10000);
            }
        }
    }
}

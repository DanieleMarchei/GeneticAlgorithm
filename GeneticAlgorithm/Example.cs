using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Example
    {
        //I want to find the integer that bettere appoximate PI * 100
        const float GOAL = (float)Math.PI * 100;

        static void Main(string[] args)
        {
            EvolveInt settings = new EvolveInt();
            GeneticAlgorithm<int> algo = new GeneticAlgorithm<int>(settings);

            int fittest = algo.Fittest();
            while (Math.Abs(fittest - GOAL) > 0.5)
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
            private Random r = new Random();

            public EvolveInt()
            {
                MutationRate = 0.001f;
                PopulationSize = 50;
                NumberOfParentsForCrossover = 2;
            }

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

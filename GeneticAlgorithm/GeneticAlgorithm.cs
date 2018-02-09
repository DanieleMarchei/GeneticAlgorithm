using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public class GeneticAlgorithm<T>
    {
        private GeneticAlgorithmSettings<T> Settings;
        private List<T> Population;
        private Random r;

        public GeneticAlgorithm(GeneticAlgorithmSettings<T> settings)
        {
            if (settings.MutationRate < 0 || settings.MutationRate > 1)
                throw new ArgumentException("Mutation rate must be between 0 and 1");

            Settings = settings;
            Population = new List<T>();
            r = new Random();
            RandomPopulation();
        }

        public T Fittest()
        {
            T max = Population[0];
            foreach (T t in Population)
            {
                if (Settings.Fitness(t) > Settings.Fitness(max))
                    max = t;
            }
            return max;
        }

        public void Step()
        {
            float fitnessSum = Population.Sum(t => Settings.Fitness(t));
            List<float> Probabilities = new List<float>();
            foreach (T t in Population)
            {
                Probabilities.Add(Settings.Fitness(t) / fitnessSum);
            }

            List<T> newPopulation = new List<T>();
            for (int i = 0; i < Settings.PopulationSize; i++)
            {
                T[] parents = RandomParents(Probabilities);
                T child = Settings.Crossover(parents);
                if (r.NextDouble() < Settings.MutationRate)
                {
                    child = Settings.Mutate(child);
                }
                newPopulation.Add(child);
            }

            Population = newPopulation;
        }

        private T[] RandomParents(List<float> Probabilities)
        {
            T[] parents = new T[Settings.NumberOfParentsForCrossover];
            for (int i = 0; i < Settings.NumberOfParentsForCrossover; i++)
            {
                bool found = false;
                while (!found)
                {
                    double prob = r.NextDouble();
                    for (int j = 0; j < Settings.PopulationSize; j++)
                    {
                        if (prob < Probabilities[j])
                        {
                            parents[i] = Population[j];
                            found = true;
                            break;
                        }
                    } 
                }
            }
            return parents;
        }

        private void RandomPopulation()
        {
            for (int i = 0; i < Settings.PopulationSize; i++)
            {
                Population.Add(Settings.RandomElement());
            }
        }
    }
}

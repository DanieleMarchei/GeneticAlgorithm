﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public abstract class GeneticAlgorithmSettings<T>
    {
        public float MutationRate { get; set; }

        public uint PopulationSize { get; protected set; }

        public uint NumberOfParentsForCrossover { get; protected set; }

        public abstract float Fitness(T t);

        public abstract T RandomElement();

        public abstract T Crossover(params T[] t);

        public abstract T Mutate(T t);
    }
}

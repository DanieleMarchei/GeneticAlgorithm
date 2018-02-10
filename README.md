# GeneticAlgorithm
An easy-to-use C# library for implementing a genetic algorithm.
What you need to do is create a setting class that overrides GeneticAlgorithmSettings's methods.

```cs
public class MySettings : GeneticAlgorithmSettings<T>
{
  public override float MutationRate { get => 0.001f; set => MutationRate = value; }
  public override uint PopulationSize { get => 50; protected set => PopulationSize = value; }
  public override uint NumberOfParentsForCrossover { get => 2; protected set => NumberOfParentsForCrossover = value; }
   
  public override T Crossover(params T[] t) { ... }
  public override float Fitness(T t) { ... }
  public override T Mutate(T t) { ... }
  public override T RandomElement() { ... }
}
```
T can be any object you want.

Than you can use the settings like this to run the genetic algorithm.
```cs
GeneticAlgorithmSettings<T> settings = new MySettings();
GeneticAlgorithm<T> algo = new GeneticAlgorithm<T>(settings);

while(condition)
{
  algo.Step();
}

Console.Write(algo.Fittest());
```
## Docs
### GeneticAlgorithmSettings
| Attribute                       | Description        |
| ------------------------------- | ------------------ |
|float MutationRate|Set the probability of a mutation. This is the only attribute editable outside the class.|
|int PopulationSize|Set the initial population size. This attribute is not editable outside the class.|
|int NumberOfParentsForCrossover|Set the numbers of random parents that a crossovers needs. This attribute is not editable outside the class.|

| Method                          | Description        |
| ------------------------------- | ------------------ |
|T Crossover(T[] t)|Specify how to perform the crossover. Parameter array t has to be lenght equal to NumberOfParentsForCrossover.|
|float Fitness(T t)|Specify how to calculate the fitness function for an element t.|
|T Mutate(T t)|Specify how to perform the mutation of an element t.|
|T RandomElement()|Specify how to generate a random element t.|

### GeneticAlgorithm

| Method                          | Description        |
| ------------------------------- | ------------------ |
|T Fittest()|Returns the fittest element in the current population.|
|void Step()|Performs a new generation.|

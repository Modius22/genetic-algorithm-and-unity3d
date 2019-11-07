# genetic-algorithm-and-unity3d

This project serves to facilitate the introduction of genetic algorithms in the game development environment Unity3d.
The necessary scripts for the implementation of genetic algorithms were selected in the src folder.

In the folder prototyp_pathfinding there is an example implementation of genetic algorithms in the context of pathfinding.

## Resources:

The basis for the implementation of the pathfindig prototype was the project of https://github.com/hobogalaxy/EvOLuTIoN. But I only use Level 2 for the prototype.

## src/
In the src folder you will find the following files:

* Player.cs
* Population.cs
* NaturalSelection.cs
* ArtificalSelection.cs
* Recombination.cs
* Replacement.cs

The Players.cs must be adapted for other projects. Here you define the action possibilities of an individual.


The Population.cs is used to control the generations. Here the following three variables are of particular importance:

* selection = 1;
* replacement = 1;
* recombination = 1;

With these, the algorithms and procedures used can be controlled. These variables can also be set via the Unity frontend. The values are to be selected as follows:

        Selection:
            1: RoulettePrinciple
            2: StochasticUniversalSampling
        Replacement:
            1: GeneralReplacement
            2: PrincipleOfTheElites
            3: WeakElitism
        Recombination:    
            1: OnePointCrossover
            2: TwoPointCrossover

The remaining files contain the relevant algorithms

## German Blog Post Serie

[Grundlagen Genetische Algorithmen](https://www.modius-techblog.de/data-science/grundlagen-genetische-algorithmen/)
[Genetische Algorithmen - Selektion](https://www.modius-techblog.de/data-science/genetische-algorithmen-selektion/)

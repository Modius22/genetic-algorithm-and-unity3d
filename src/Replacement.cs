using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
    In these classes, individuals from the old population are replaced with new individuals.
 */

using System;
    

public class Replacement : MonoBehaviour{

    GameObject pop = null;

    public Recombination recom;
    public Population population;
    public ArtificalSelection art;

    void Start(){
        population = GameObject.Find("GameMaster").gameObject.GetComponent<Population>();
        recom = GameObject.Find("GameMaster").gameObject.GetComponent<Recombination>();
        art = GameObject.Find("GameMaster").gameObject.GetComponent<ArtificalSelection>();
    }

    public void GeneralReplacement(int recombination, int brainsize){

        /*
        1 = OnePointCrossover
        2 = TwoPointCrossover
        3 = TemplateCrossover
        4 = UniformCrossover
        5 = SchuffleCrossover
        defautl = Debug.Break()
        */        
        switch (recombination)
        {
            case 1:
                for (int i = 0; i < population.playerNum; i += 2){
                    recom.OnePointCrossover(population.Players[i], population.Players[i+1], brainsize);
                    population.Mutate(population.Players[i]);
                    population.Mutate(population.Players[i+1]);
                }  
                break;
            case 2:

                 for (int i = 0; i < population.playerNum; i += 2){
                    recom.TwoPointCrossover(population.Players[i], population.Players[i+1], brainsize);
                    population.Mutate(population.Players[i]);
                    population.Mutate(population.Players[i+1]);
                }  
                      
                break;
        }

    }

    public void PrincipleOfTheElites(int elite, int recombination, int brainsize){
        int bestOf = 99;
        elite = elite -1;

        //copy Champion
        CopyBrain(population.Players[0], population.champion);

        for (int i = 1; i < elite; i += 1){
            CopyBrain(population.Players[i], art.sorted[bestOf]);
            bestOf = bestOf - 1;
        }

        elite = elite +1;

        switch (recombination)
        {
            case 1:
                for (int i = elite; i < population.playerNum; i += 2){
                    recom.OnePointCrossover(population.Players[i], population.Players[i+1], brainsize);
                    population.Mutate(population.Players[i]);
                    population.Mutate(population.Players[i+1]);
                }  
                break;
            case 2:

                 for (int i = elite; i < population.playerNum; i += 2){
                    recom.TwoPointCrossover(population.Players[i], population.Players[i+1], brainsize);
                    population.Mutate(population.Players[i]);
                    population.Mutate(population.Players[i+1]);
                }  
                      
                break;
        }

    }

    public void WeakElitism(int elite, int recombination, int brainsize){
                int bestOf = 99;
        elite = elite -1;

        //copy Champion
        CopyBrain(population.Players[0], population.champion);

        for (int i = 1; i < elite; i += 1){
            CopyBrain(population.Players[i], art.sorted[bestOf]);
            population.Mutate(population.Players[i]);
            bestOf = bestOf - 1;
        }

        elite = elite +1;

        switch (recombination)
        {
            case 1:
                for (int i = elite; i < population.playerNum; i += 2){
                    recom.OnePointCrossover(population.Players[i], population.Players[i+1], brainsize);
                    population.Mutate(population.Players[i]);
                    population.Mutate(population.Players[i+1]);
                }  
                break;
            case 2:

                 for (int i = elite; i < population.playerNum; i += 2){
                    recom.TwoPointCrossover(population.Players[i], population.Players[i+1], brainsize);
                    population.Mutate(population.Players[i]);
                    population.Mutate(population.Players[i+1]);
                }  
                      
                break;
        }

    }

    void DeleteNLatestSchema(){}

    public void CopyBrain(GameObject P1, GameObject P2)
    {
        for( int i = 0; i < Player.brainSize; i++)
        {
            P1.GetComponent<Player>().brain[i][0] = P2.GetComponent<Player>().brain[i][0];
            P1.GetComponent<Player>().brain[i][1] = P2.GetComponent<Player>().brain[i][1];
            P1.GetComponent<Player>().brain[i][2] = P2.GetComponent<Player>().brain[i][2];
        }
    }


}


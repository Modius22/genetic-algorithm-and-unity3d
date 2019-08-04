using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
  In this class the individuals are assigned probabilities for the selection.
 */
public class NaturalSelection : MonoBehaviour{

  public Population population;

  void Start(){
    population = GameObject.Find("GameMaster").gameObject.GetComponent<Population>();

  }

  // must refractor to controlGA
  /*public void OwnSelection()
  {
    population.SetChampion();

    population.CalculateFitness();
    population.CalculateFitnessSum();

    population.CopyBrain(population.Players[0], population.champion);    //Champion is always reborn in next generation unchanged

    for (int i = 1; i < population.playerNum; i++)
    {
        GameObject parent = population.SelectParent();
        population.CopyBrain(population.Players[i], parent);
        population.Mutate(population.Players[i]);
    }
    
  } */

  public void FitnessProportional(){
    for (int i = 0; i < population.playerNum; i++){
      population.Players[i].GetComponent<Player>().rating = population.Players[i].GetComponent<Player>().fitness;
    }
  }

}


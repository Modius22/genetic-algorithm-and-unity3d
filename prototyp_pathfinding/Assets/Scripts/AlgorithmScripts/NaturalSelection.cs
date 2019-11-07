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

  public void FitnessProportional(){
    for (int i = 0; i < population.playerNum; i++){
      population.Players[i].GetComponent<Player>().rating = population.Players[i].GetComponent<Player>().fitness;
    }
  }

}


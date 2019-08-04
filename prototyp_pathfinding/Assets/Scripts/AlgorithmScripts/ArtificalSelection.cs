  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 using System.Linq;


/*
  In this class the individuals are selected for crossover and transferred to a mating pool.
 */
public class ArtificalSelection : MonoBehaviour{

  public Population population;
  public GameObject[] sorted ;


  void Start(){
  population = GameObject.Find("GameMaster").gameObject.GetComponent<Population>();

  }

  public void RoulettePrinciple(int num){
    float fitSum = population.fitnessSum;

    for (int i = 0; i < population.playerNum; i++){
      population.Players[i].GetComponent<Player>().relativeFitness = population.Players[i].GetComponent<Player>().rating / population.fitnessSum;
    }
    sorted = population.Players.ToList().OrderBy(relativeFitness => relativeFitness.GetComponent<Player>().relativeFitness).ToList().ToArray();

    population.MartingPool = new GameObject [num];


    int p = 0;
    while (p < num){
        float r = Random.Range (0.0000f, 1.00000f)/4;
        int x = 0;
        while (x < population.playerNum){

          
          if (r < sorted[x].GetComponent<Player>().relativeFitness){

            population.MartingPool[p] = sorted[x];

            p++;
            break;
          }
          x++;
        }
     
    }
  }


  public void StochasticUniversalSampling(int num){

    for (int k = 0; k < population.playerNum; k++){
      population.Players[k].GetComponent<Player>().relativeFitness = population.Players[k].GetComponent<Player>().rating;
    }
    sorted = population.Players.ToList().OrderBy(relativeFitness => relativeFitness.GetComponent<Player>().relativeFitness).ToList().ToArray();

    float fitSum = population.fitnessSum;
    float pointDistance = fitSum / num;
    float r = Random.Range (0.0000f, sorted[0].GetComponent<Player>().relativeFitness) * pointDistance;
  

    population.MartingPool = new GameObject [num];

    int i = 0;
    float sum = sorted[i].GetComponent<Player>().relativeFitness;

    for (int j = 0; j < num; j++){
      float pointer = r + j * pointDistance;

      if (sum >= pointer){
        population.MartingPool[j] = sorted[i];
      } else {
        for(++i; i <  population.playerNum;i++ ){
          sum += sorted[i].GetComponent<Player>().relativeFitness;
          if( sum >= pointer){
              population.MartingPool[j] = sorted[i];
              break;
          }
        }
      }

    } 
  }

}
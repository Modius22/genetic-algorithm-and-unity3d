using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
  Composition of new individuals from parents included in the marting pool
 */

public class Recombination : MonoBehaviour{

  public Population population;

  void Start(){
  population = GameObject.Find("GameMaster").gameObject.GetComponent<Population>();
  }
  public void OnePointCrossover(GameObject P1 , GameObject P2, int brainsize){

    int r1 = Random.Range (0, population.MartingPool.Length );
    int r2 = Random.Range (0, population.MartingPool.Length );
    int r3 = Random.Range (0, brainsize); 
    
    for( int i = 0; i < brainsize; i++)
    {
      
      if (i < r3){
        //Child 1
        P1.GetComponent<Player>().brain[i][0] = population.MartingPool[r1].GetComponent<Player>().brain[i][0];
        P1.GetComponent<Player>().brain[i][1] = population.MartingPool[r1].GetComponent<Player>().brain[i][1];
        P1.GetComponent<Player>().brain[i][2] = population.MartingPool[r1].GetComponent<Player>().brain[i][2];

        //Child 2
        P2.GetComponent<Player>().brain[i][0] = population.MartingPool[r2].GetComponent<Player>().brain[i][0];
        P2.GetComponent<Player>().brain[i][1] = population.MartingPool[r2].GetComponent<Player>().brain[i][1];
        P2.GetComponent<Player>().brain[i][2] = population.MartingPool[r2].GetComponent<Player>().brain[i][2];
      }
      if (i >= r3){
        //Child 1
        P1.GetComponent<Player>().brain[i][0] = population.MartingPool[r2].GetComponent<Player>().brain[i][0];
        P1.GetComponent<Player>().brain[i][1] = population.MartingPool[r2].GetComponent<Player>().brain[i][1];
        P1.GetComponent<Player>().brain[i][2] = population.MartingPool[r2].GetComponent<Player>().brain[i][2];

        //Child 2
        P2.GetComponent<Player>().brain[i][0] = population.MartingPool[r1].GetComponent<Player>().brain[i][0];
        P2.GetComponent<Player>().brain[i][1] = population.MartingPool[r1].GetComponent<Player>().brain[i][1];
        P2.GetComponent<Player>().brain[i][2] = population.MartingPool[r1].GetComponent<Player>().brain[i][2];
      }
       
    }
  }

  public void TwoPointCrossover(GameObject P1 , GameObject P2, int brainsize){
    int r1 = Random.Range (0, population.MartingPool.Length ); //MartingPool Gene 1
    int r2 = Random.Range (0, population.MartingPool.Length ); //MartingPool Gene 2
    int r3 = Random.Range (0, brainsize); //Split Point 1
    int r4 = Random.Range (0, brainsize); //Split Point 2

    if (r3 > r4){
      int r = r3;
      r3 = r4;
      r4 = r;
    }

    for( int i = 0; i < brainsize; i++){
      if (r3 < i && i < r4){
        //Child 1
        P1.GetComponent<Player>().brain[i][0] = population.MartingPool[r2].GetComponent<Player>().brain[i][0];
        P1.GetComponent<Player>().brain[i][1] = population.MartingPool[r2].GetComponent<Player>().brain[i][1];
        P1.GetComponent<Player>().brain[i][2] = population.MartingPool[r2].GetComponent<Player>().brain[i][2];

        //Child 2
        P2.GetComponent<Player>().brain[i][0] = population.MartingPool[r1].GetComponent<Player>().brain[i][0];
        P2.GetComponent<Player>().brain[i][1] = population.MartingPool[r1].GetComponent<Player>().brain[i][1];
        P2.GetComponent<Player>().brain[i][2] = population.MartingPool[r1].GetComponent<Player>().brain[i][2];
      } else {
        P1.GetComponent<Player>().brain[i][0] = population.MartingPool[r1].GetComponent<Player>().brain[i][0];
        P1.GetComponent<Player>().brain[i][1] = population.MartingPool[r1].GetComponent<Player>().brain[i][1];
        P1.GetComponent<Player>().brain[i][2] = population.MartingPool[r1].GetComponent<Player>().brain[i][2];

        //Child 2
        P2.GetComponent<Player>().brain[i][0] = population.MartingPool[r2].GetComponent<Player>().brain[i][0];
        P2.GetComponent<Player>().brain[i][1] = population.MartingPool[r2].GetComponent<Player>().brain[i][1];
        P2.GetComponent<Player>().brain[i][2] = population.MartingPool[r2].GetComponent<Player>().brain[i][2];
      }
    }
  }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Population : MonoBehaviour{

    public GameObject player;
    public GameObject goal;
    public GameObject champion;    //player with best fitness score

    //public static int playerNum;    //number of players
    public int playerNum;    //number of players

    public GameObject[] Players;
    public GameObject[] MartingPool;
    public int MartingPoolSize = 40;


    private Vector3 spawn = new Vector3(68.5f, 1.50f, -15.3f);



    public float fitnessSum;
    private float mutationRate = 0.02f; //0.02 means only 2% of movement vectors will be modified for each new player
    private int minStep = Player.brainSize;  //minimum of steps taken to reach the goal
    public int generation = 0;

    private bool jumpingEnabled;
    private bool noWinnerBefore = true;
    private long k = 0; //update counter

    GameObject pop = null;
    public NaturalSelection naturalSel;
    public ArtificalSelection artSel;

    public Recombination recom;
    public Replacement replace;
    /*
    
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
    
     */

    public int selection = 1;
    public int replacement = 1;
    public int recombination = 1;
    public int survivors = 20;

    // Use this for initialization
    void Start(){
        pop = GameObject.Find("GameMaster");
        naturalSel = pop.GetComponent<NaturalSelection>();
        artSel = pop.GetComponent<ArtificalSelection>();
        recom = pop.GetComponent<Recombination>();
        replace = pop.GetComponent<Replacement>();

        playerNum = PauseMenu.playerNum; //number of players is taken from other script because it enables changing this value in game by user
        Players = new GameObject[playerNum];

        jumpingEnabled = PauseMenu.jumpingEnabled;  //jumping can be enabled through game menu

        SpawnPlayers();

    }

    // Unity method for physics update
    void FixedUpdate(){

        if(k % 5 == 0)  //update only once per 5 physics updates
        {
            k = 0;
            if (ReachedTheGoal())
            {
                if(noWinnerBefore)
                {
                    print("Success!!! The Winner was born in " + generation + " generation!");
                    noWinnerBefore = false;
                }
            }      

            if(AllDead())   //if everyone is dead their score is evaluated, the champion is colored green and game pauses for 1 second
            {     
                controlGA();
                champion.GetComponent<Player>().SetAsChampion();                   
                StartCoroutine(PauseAndRespawn()); // respawning needs to be embedded inside coroutine coz pause doesn't work otherwise :/                   
            }
            else
            {
                for(int i = 0; i < playerNum; i++)
                {
                    if (Players[i].GetComponent<Player>().i > minStep)
                    {
                        Players[i].GetComponent<Player>().Die(); //player is killed if it has already taken more steps then the best player that reached the goal (that way they stil learn to optimise their moves to reach the goal faster)
                    }
                    else if (Players[i].GetComponent<Rigidbody>().velocity.magnitude < Player.maxSpeed)  //movement vector is applied only if player hasn't crossed max speed limit 
                    {
                        Players[i].GetComponent<Player>().MovePlayer();
                    }
                }
            }
        }
        k++;
    }   


    //function for the control of the ga setup 
    public void controlGA(){

        SetChampion();

        CalculateFitness();
        CalculateFitnessSum();

        naturalSel.FitnessProportional();

        switch (selection)
        {
            case 1:
                artSel.RoulettePrinciple(MartingPoolSize);
                break;
            case 2:
                artSel.StochasticUniversalSampling(MartingPoolSize);
                break;
        }

        switch (replacement)
        {
            case 1:
                replace.GeneralReplacement(recombination, Player.brainSize);
                break;
            case 2:
                replace.PrincipleOfTheElites(survivors,recombination, Player.brainSize);
                break;
            case 3:
                replace.WeakElitism(survivors,recombination, Player.brainSize);
                break;
        }
    }

    //Respawn new genereation
    IEnumerator PauseAndRespawn()
    {
        enabled = false;    //turn off update function
        yield return new WaitForSeconds(1.0f);  //pause
        enabled = true;     //turn on update function

        RespawnAll();
        generation++;

        if (generation % 5 == 0) IncreaseLifespan();   //every 5 generations expand their lifetime
    }

    //check complete population dead
    bool AllDead()
    {
        for (int i = 0; i < playerNum; i++)
        {
            if (Players[i].GetComponent<Player>().dead == false) return false;
        }
        return true;
    }


    bool ReachedTheGoal()   //returns true if any player reached the goal
    {
        bool rechedTheGoal = false;
        for (int i = 0; i < playerNum; i++)
        {
            if (Players[i].GetComponent<Player>().reachedTheGoal == true)
            {
                if (Players[i].GetComponent<Player>().i < minStep)
                {
                    minStep = Players[i].GetComponent<Player>().i;
                }            
                rechedTheGoal = true;
            }
        }

        if(rechedTheGoal) return true;
        else return false;
    }


    void RespawnAll()
    {
        for (int i = 0; i < playerNum; i++)
        {
            Players[i].GetComponent<Player>().Respawn();
        }

        Players[0].GetComponent<Player>().SetAsChampion();    //makes the best player from previous generation to be green
    }


    public void SetChampion()
    {
        float best_score = Vector3.Distance(Players[0].transform.position, goal.transform.position);
        champion = Players[0];

        for (int i = 1; i < playerNum; i++)
        {
            float DistanceToGoal = Vector3.Distance(Players[i].transform.position, goal.transform.position);
            if (DistanceToGoal < best_score)
            {
                best_score = DistanceToGoal;
                champion = Players[i];
            }
        }
    }


    void IncreaseLifespan()
    {
        for (int i = 0; i < playerNum; i++)
        {
            Players[i].GetComponent<Player>().lifespan += 5;
        }
    }


    void SpawnPlayers()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        for (int i = 0; i < playerNum; i++)
        {
            //Spawn player
            GameObject player_x;

            player_x = Instantiate(player, spawn, Quaternion.identity) as GameObject;
      
            

            //Generate name for player
            string[] name_tmp = { "player", (i + 1).ToString() };
            name = string.Join("", name_tmp, 0, 2);
            player_x.name = name;

            //Assign player to array
            Players[i] = player_x;

            //Calculate distance from spawn to goal
            Players[i].GetComponent<Player>().distToGoalFromSpawn = Vector3.Distance(Players[i].transform.position, goal.transform.position);

        }
    }

    //create fitness per individual
    public void CalculateFitness()
    {
        for (int i = 0; i < playerNum; i++)
        {
            float DistanceToGoal = Vector3.Distance(Players[i].transform.position, goal.transform.position);

            if(Players[i].GetComponent<Player>().reachedTheGoal)
            {
                int step = Players[i].GetComponent<Player>().i;
                float distToGoalFromSpawn = Players[i].GetComponent<Player>().distToGoalFromSpawn;
                Players[i].GetComponent<Player>().fitness = 1.0f / 24 + distToGoalFromSpawn * 100 / (step * step);
            }
            else
            {
                Players[i].GetComponent<Player>().fitness = 10.0f / (DistanceToGoal * DistanceToGoal * DistanceToGoal * DistanceToGoal);
            }
        }
       // Debug.Break();
    }

    //creat fitness per population
    public void CalculateFitnessSum()
    {
        fitnessSum = 0;
        for (int i = 0; i < playerNum; i++)
            fitnessSum += Players[i].GetComponent<Player>().fitness;
        //Debug.Log(fitnessSum);
    }


    public void Mutate(GameObject PlayerX)
    {    
        for (int i = 0; i < PlayerX.GetComponent<Player>().lifespan; i++)
        {
            float rand = Random.Range(0.0f, 1.0f);
            if (rand < mutationRate)
            {
                PlayerX.GetComponent<Player>().brain[i] = new Vector3(Random.Range(10, -11), 0, Random.Range(10, -11));
                Debug.Log("Mutate");
                //they also get y axis force if jumping is enabled
                if(jumpingEnabled)
                {
                    PlayerX.GetComponent<Player>().brain[i][1] = Random.Range(10, -11);
                }
            }
        }
    }

}

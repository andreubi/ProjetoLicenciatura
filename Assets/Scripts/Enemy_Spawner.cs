using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Enemy_Spawner : MonoBehaviour
{

    public List<SpawnableEnemy> enemies = new List<SpawnableEnemy>();
    public int currWave;
    private int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public Transform[] spawnLocation;
    public int spawnIndex;

    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;


    public List<GameObject> spawnedEnemies = new List<GameObject>(); //keep track of already spawned enemies
    public static event Action<GameObject> OnEnemySpawned;
    public static event Action<int> OnWaveUpdate;


    // Start is called before the first frame update
    void Start()
    {
        GenerateWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnTimer <= 0)
        {
            //spawn an enemy
            if (enemiesToSpawn.Count > 0)
            {
                if (enemiesToSpawn[0].layer == 9) // Flying Layer
                {
                    //Spawn in Flying spawners (spawners in array positions 3 and 4, therefore Random.Range between 3(inclusive) and 5(exclusive)
                    GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], spawnLocation[Random.Range(3, 5)].position, Quaternion.identity); // spawn first enemy in our list
                    OnEnemySpawned?.Invoke(enemy); // send a "warning" to gamemanager to keep track of enemies spawned for counting.
                    enemiesToSpawn.RemoveAt(0); // and remove it
                    spawnedEnemies.Add(enemy); // add to array of spawned enemies to keep track of them
                    spawnTimer = spawnInterval; // reset the spawntimer, so it starts counting down again until we can spawn next enemy

                    /*
                    // Fixing algorithm issues here
                    if (spawnIndex + 1 <= spawnLocation.Length - 1)
                    {
                        spawnIndex++;
                    }
                    else
                    {
                        spawnIndex = 0;
                    }
                    */
                }
                if (enemiesToSpawn[0].layer == 7) // Ground Layer
                {
                    //Spawn in Flying spawners (spawners in array positions 0,1 and 2, therefore Random.Range between 0(inclusive) and 3(exclusive)
                    GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], spawnLocation[Random.Range(0, 3)].position, Quaternion.identity); // spawn first enemy in our list
                    OnEnemySpawned?.Invoke(enemy);  // send a "warning" to gamemanager to keep track of enemies spawned for counting.
                    enemiesToSpawn.RemoveAt(0); // and remove it
                    spawnedEnemies.Add(enemy); // add to array of spawned enemies to keep track of them
                    spawnTimer = spawnInterval; // reset the spawntimer, so it starts counting down again until we can spawn next enemy
                    // Fixing algorithm issues here
                    /*
                    if (spawnIndex + 1 <= spawnLocation.Length - 1)
                    {
                        spawnIndex++;
                    }
                    else
                    {
                        spawnIndex = 0;
                    }
                    */

                }
                // The previous if cases are there because there are 2 different types of enemies that require different spawn points
                // Ground enemies can't spawn in the flying layer and in the air, and Flying enemies can't spawn in the Ground layer and on the ground.
            }
            else
            {
                waveTimer = 0; // if no enemies remain, end wave
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime; // ticking the timer with each frame
            waveTimer -= Time.fixedDeltaTime; // ticking the timer with each frame
        }

        if (waveTimer <= 0 && spawnedEnemies.Count <= 0) //if wave is over
        {
            currWave++; // next wave!
            waveDuration = 15 * currWave; // update duration to tweak difficulty
            if (waveDuration > 60) // max duration is 60 cuz why not
                waveDuration = 60;
            OnWaveUpdate?.Invoke(currWave); // send a warning to gamemanager that a new wave started
            GenerateWave(); //generate new wave
        }
    }

    public void GenerateWave()
    {
        waveValue = currWave * 10; // wavevalue is how many "coins" the monster shop has
        GenerateEnemies();

        spawnInterval = waveDuration / enemiesToSpawn.Count; // gives a fixed time between each enemies
        waveTimer = waveDuration; // wave duration is read only
    }

    public void GenerateEnemies()
    {
        // Create a temporary list of enemies to generate
        // 
        // in a loop grab a random enemy 
        // see if we can afford it
        // if we can, add it to our list, and deduct the cost.

        // repeat... 

        //  -> if we have no points left, leave the loop

        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0 || generatedEnemies.Count < 50)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

}

[System.Serializable]
public class SpawnableEnemy
{
    public GameObject enemyPrefab;
    public int cost;
}
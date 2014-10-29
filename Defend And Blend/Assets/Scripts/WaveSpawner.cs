using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour {

    public bool isSpawning;
    public GameObject[] enemies;
    public Defendable target;
    private int totalEnemies;

    public Vector3 spawnValues;
    public GameObject StrawBerry;

    public Text currentWaveText;

    //How many enemies should have one wave?
    public int waveCount = 3;
    public int currentWave = 1;
    //Wait time after a wave is finished
    public float waitBetweenWaves;
    //Seconds between the spawn of next enemy in wave.
    public int timeBetweenNextEnemy = 2;

    public int RandomIndex;

	// Use this for initialization
	void Start () 
    {
        totalEnemies = enemies.Length;

        setCurrentWave();
        StartCoroutine(SpawnWaves());
	}

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(0);
        
        
        while (true)
        {
            Debug.Log("Wave #" + currentWave + "started");
            
            //Check if the max number of enemies is already spawned
            //If not, keep looping till the max enemies / wave are spawned
            for (int i = 0; i < waveCount; i++)
            {
                // Getting a random int from 0 to total enemies.
                // This will help me getting a random enemy each wave.
                RandomIndex = Random.Range(0, totalEnemies);

                // Setting up the spawnpositions of the spawnable.
                Vector3 spawnPosition = new Vector3(spawnValues.x, spawnValues.y, spawnValues.z);
                // Setting up the rotation of the spawnable (is needed for 'Instantiate')
                // What it does I'll actually need to find it out.
                Quaternion spawnRotation = Quaternion.identity;

                // Cloning a random prefab at the correct position with a rotation?
                GameObject clone = Instantiate(enemies[RandomIndex], spawnPosition, spawnRotation) as GameObject;

                // Make the monster go to the target
                Monster monster = clone.GetComponent<Monster>();//target
                monster.target = target;

                // Waiting a few (2) seconds, to prefend monsters will spawn on each others.
                yield return new WaitForSeconds(timeBetweenNextEnemy);
                Debug.Log("Waiting #" + timeBetweenNextEnemy + "sec to deploy next fruit");
            }
            //The wave is finished, waiting (x) seconds for next wave.
            Debug.Log("Waiting for the next wave");
            yield return new WaitForSeconds(waitBetweenWaves);
           
            // Current wave = current wave + 1.
            // Will count 'currentwave' each time + 1
            currentWave += 1;
            setCurrentWave();

        }
    }

	// Update is called once per frame
	void Update () 
    {
	    
	}

    void setCurrentWave()
    {
        currentWaveText.text = "Current Wave: " + currentWave;
    }
}

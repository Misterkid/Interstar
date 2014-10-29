using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour {

    public bool isSpawning;
    public GameObject[] enemies;
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

	// Use this for initialization
	void Start () 
    {
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
            for (int i = 0; i < waveCount; i++)
            {
    
                
             
                Vector3 spawnPosition = new Vector3(spawnValues.x, spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(StrawBerry, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(timeBetweenNextEnemy);
                Debug.Log("Waiting #" + timeBetweenNextEnemy + "sec to deploy next fruit");
            }
            Debug.Log("Waiting for the next wave");
            yield return new WaitForSeconds(waitBetweenWaves);
           
            
            currentWave += 1;
            setCurrentWave();

            Debug.Log(currentWave);
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

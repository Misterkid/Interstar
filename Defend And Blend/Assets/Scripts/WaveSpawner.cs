using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour {

    public bool isSpawning;
    public GameObject[] enemies;
    public Vector3 spawnValues;
    public GameObject StrawBerry;

    //How many enemies should have one wave?
    public int waveCount = 3;
    public int currentWave = 1;

    //Wait time after a wave is finished
    public float waitBetweenWaves = 15;
    
    //Seconds between the spawn of next enemy in wave.
    public float timeBetweenNextEnemy = 1;

	// Use this for initialization
	void Start () 
    {
        Debug.Log("Starting with Wave:" + currentWave);
        StartCoroutine(SpawnWaves());
	}

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(0);
        Debug.Log("################################");
        
        while (true)
        {
            Debug.Log("Wave #" + currentWave + "started");
            //Check if the max number of enemies is already spawned
            for (int i = 0; i < waveCount; i++)
            {
                
                Debug.Log("################################");
                
                Vector3 spawnPosition = new Vector3(Random.Range(10, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(StrawBerry, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(timeBetweenNextEnemy);
                Debug.Log("Waiting #" + timeBetweenNextEnemy + "sec to deploy next fruit");
            }
            Debug.Log("Waiting #" + waitBetweenWaves + "seconds for the next wave");
            yield return new WaitForSeconds(waitBetweenWaves);
            Debug.Log("Waiting finished, preparing next wave!");
            Debug.Log("################################");
            currentWave += 1;
        }
    }

	// Update is called once per frame
	void Update () 
    {
	    
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawnerTwo : MonoBehaviour 
{
// <-- Starting with declaring the vars. --> //
// Creating variables that are needed to create the wave system.
    // Making an Array of GameObjects with the Enemies within.
    public GameObject[] monsters;
    // Making an variable of the target they will attack.
    public Defendable target;
    // Making a Vector3 for the spawnPosition of the enemies.
    public Vector3 spawnValues;
    // Declaring an integer that says what the current wave (number) is.
    public int currentWave;
    // Making a variable that is holding the current wave text.
    public Text currentWaveText;
    // Declaring in an integer how many enemies the wave should contain.
    public int enemiesInWave;
    //Wait time after a wave is finished
    public float waitBetweenWaves;
    //Seconds between the spawn of next enemy in wave.
    public int timeBetweenNextEnemy = 2;
    
    //public Wave references to the class Wave.
    // 
    public Wave[] waves;
// <-- Ending with declaring the vars. --> //

    // Use this for initialization
	void Start () 
    {
        // The player will start at Wave 1.
        currentWave = 0;
        // In the first wave there will be <2> enemies.
        enemiesInWave = 2;
       
        
        setCurrentWave();
        StartCoroutine(SpawnWaves());
	}

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(0);
        
        while (true)
        {       
            //Check if the max number of enemies is already spawned
            //If not, keep looping till the max enemies / wave are spawned
            for (int MN = 0; MN < waves[currentWave].monsters.Length; MN++)
            {
               
                // Setting up the spawnpositions of the spawnable.
                Vector3 spawnPosition = new Vector3(spawnValues.x, spawnValues.y, spawnValues.z);
                // Setting up the rotation of the spawnable (is needed for 'Instantiate')
                // What it does I'll actually need to find it out.
                Quaternion spawnRotation = Quaternion.identity;
                //currentWave = waves.Length;

                Debug.Log(monsters[waves[currentWave].monsters[MN]]);
                GameObject clone = Instantiate(monsters[waves[currentWave].monsters[MN]], spawnPosition, spawnRotation) as GameObject;

                // Make the monster go to the target
                Monster monster = clone.GetComponent<Monster>();//target
                monster.target = target;

                // Waiting a few (2) seconds, to prefend monsters will spawn on each others.
                yield return new WaitForSeconds(timeBetweenNextEnemy);
                
            }         
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
        if (currentWaveText != null)
        currentWaveText.text = "Current Wave: " + currentWave;
    }
}

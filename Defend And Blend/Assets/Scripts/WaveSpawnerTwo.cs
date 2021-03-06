﻿using UnityEngine;
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
    //Seconds between the spawn of next enemy in wave.s
    private float timeBetweenNextEnemy = 1.5F;
    //Seconds between the spawn of next enemy in wave.s
    private float timeBetweenNextWave = 1;
    
    //public Wave references to the class Wave.
    // 
    public Wave[] waves;

    public List<GameObject> SpawnedMonsters = new List<GameObject>();
// <-- Ending with declaring the vars. --> //

    // Use this for initialization
	void Start () 
    {
        //Config
        DataLoader.LoadWaves();
        waves = ConfigData.waveDatas.ToArray();
        DataLoader.LoadMonsters();
        DataLoader.LoadConfig();
        timeBetweenNextEnemy = ConfigData.gameConfig.timeBetweenSpawns;
        waitBetweenWaves = ConfigData.gameConfig.timeBetweenWaves;
        //End config
        // The player will start at Wave 1.
        currentWave = 0;
        // In the first wave there will be <2> enemies.
        enemiesInWave = 2;
        setCurrentWave();
        
       
        StartCoroutine(SpawnWaves());

        if (monsters.Length <= 0)
            Debug.LogError("You forgot the monsters!");
	}
    IEnumerator waitForNextEnemy()
    {
        Debug.Log("Waiting "+ timeBetweenNextEnemy +"s for deploying enemy");
        yield return new WaitForSeconds(timeBetweenNextEnemy);
    }
    IEnumerator waitForNextWave()
    {
        Debug.Log("Waiting " + timeBetweenNextWave + "s for deploying wave");
        yield return new WaitForSeconds(timeBetweenNextWave);
    }
    IEnumerator SpawnWaves()
    {
        Debug.Log("SPAWNWAVES STARTED.");
        //This yield is used for the time between the first and the second enemy.
        yield return new WaitForSeconds(3.5F);
        
        // On every spawn, check wheter the game is on pause or not.
        if (GameValues.ISPAUSED == false)
        {

            while (true)//Mark check this out!
            {

                //Check if the max number of enemies is already spawned
                //If not, keep looping till the max enemies / wave are spawned
                for (int MN = 0; MN < waves[currentWave].monsters.Length; MN++)
                {
                    if (!GameValues.ISPAUSED)
                    {
                        // Setting up the spawnpositions of the spawnable.
                        Vector3 spawnPosition = new Vector3(spawnValues.x, spawnValues.y, spawnValues.z);
                        // Setting up the rotation of the spawnable (is needed for 'Instantiate')
                        // What it does I'll actually need to find it out.
                        Quaternion spawnRotation = Quaternion.identity;
                        //currentWave = waves.Length;

                        //Debug.Log(monsters[waves[currentWave].monsters[MN]]);
                        GameObject clone = Instantiate(monsters[waves[currentWave].monsters[MN]], spawnPosition, spawnRotation) as GameObject;
                        SpawnedMonsters.Add(clone);
                        // Make the monster go to the target
                        Monster monster = clone.GetComponent<Monster>();
                        // monster.
                        monster.target = target;//target
                        monster.speed = ConfigData.monsterDatas[waves[currentWave].monsters[MN]].speed;
                        monster.damage = ConfigData.monsterDatas[waves[currentWave].monsters[MN]].damage;
                        monster.minPressure = ConfigData.monsterDatas[waves[currentWave].monsters[MN]].minSueezePower;
                        monster.maxPressure = ConfigData.monsterDatas[waves[currentWave].monsters[MN]].maxSqueezePower;
                        monster.fruitSize = ConfigData.monsterDatas[waves[currentWave].monsters[MN]].fruitSize;
                        // Waiting a few (2) seconds, to prefend monsters will spawn on each others.
                    }
                    else
                    {
                        MN = MN - 1;
                    }

                    yield return new WaitForSeconds(timeBetweenNextEnemy);

                }
                yield return new WaitForSeconds(waitBetweenWaves);

                // Current wave = current wave + 1.
                // Will count 'currentwave' each time + 1
                currentWave += 1;
                GameValues.CURRENTWAVE = currentWave;
                //Last wave! Repeat!
                if (currentWave == waves.Length)
                    currentWave = waves.Length - 1;

                setCurrentWave();

            }
        }

        //The Comment below ain't working. Still working on it though.
        if (GameValues.ISPAUSED == true) { Debug.Log("I Can't Spawn right now, we're on pause sorry comrad!"); }  // GAME IS ON PAUSE! DON'T SPAWN
    }


  


	// Update is called once per frame
	void Update () 
    {
	    
	}

    void setCurrentWave()
    {
        if (currentWaveText != null)
            currentWaveText.text = "Wave # " + (currentWave + 1);
    }
}

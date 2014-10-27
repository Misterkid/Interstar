using UnityEngine;
using System.Collections;

public class WaveSystem : MonoBehaviour 
{
    public GameObject Tomato;
    public Vector3 spawnValues;
	
    // Use this for initialization
	void Start () 
    {
        spawnWaves();
	}
	
	// Update is called once per frame
	void Update () 
    {
        spawnWaves();
	}

    void spawnWaves()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity; 
        Instantiate(Tomato, spawnPosition, spawnRotation);
    }
}

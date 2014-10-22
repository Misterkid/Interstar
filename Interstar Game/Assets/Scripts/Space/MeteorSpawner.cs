using UnityEngine;
using System.Collections;
using System.Timers;
public class MeteorSpawner : MonoBehaviour 
{
    public GameObject meteorPrefab;
	// Use this for initialization
	void Start () 
    {
        SpawnMeteor();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    private void SpawnMeteor()
    {
        GameObject clone = GameObject.Instantiate(meteorPrefab, transform.position, transform.rotation) as GameObject;
        
        
    }
}

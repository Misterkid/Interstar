using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BlenderCatch : MonoBehaviour 
{
    private List<GameObject> monsters = new List<GameObject>();
	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Input.GetButton("Fire1"))
        {
            for(int i = 0; i < monsters.Count; i++)
            {
                Monster monster = monsters[i].GetComponent<Monster>();
                monster.Die();
                GameValues.SCORE++;
            }
            monsters = new List<GameObject>();
        }
	}
    void OnCollisionEnter(Collision other)
    {
        Monster monster = other.collider.GetComponent<Monster>();
        if (monster != null)
        {
            if (!monster.isInholding)
            {
                GameValues.SCORE++;
                monster.isInBlender = true;
                monsters.Add(monster.gameObject);
                //We are gone!
                WaveSpawnerTwo waveSpawner = FindObjectOfType<WaveSpawnerTwo>();
                waveSpawner.SpawnedMonsters.Remove(monster.gameObject);
                //monster.Die();
            }
        }
    }
}

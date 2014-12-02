using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BlenderCatch : MonoBehaviour 
{
    public float CamShakeTime = 0.5f;
    private float shakeTime = 0;
    private List<GameObject> monsters = new List<GameObject>();
    private Animator cameraAnimator;
    public Animator drawerAnimator;
	// Use this for initialization
	void Start () 
    {
        cameraAnimator = Camera.main.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (GameValues.ISPAUSED)
            return;

	    if(Input.GetButton("Fire1"))
        {
            for(int i = 0; i < monsters.Count; i++)
            {
                Monster monster = monsters[i].GetComponent<Monster>();
                monster.Die();
                GameValues.SCORE++;
            }
            cameraAnimator.SetTrigger("Shake");
            //Debug.Log(drawerAnimator);
            //drawerAnimator.animation.Play(true);
            drawerAnimator.SetTrigger("Shake");
            //drawerAnimator.SetBool("isPlaying", true);
            shakeTime = Time.time + CamShakeTime;
            monsters = new List<GameObject>();
        }
        if (Time.time >= shakeTime)
        {
            cameraAnimator.SetTrigger("StopShake");
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

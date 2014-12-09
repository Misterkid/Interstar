using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BlenderCatch : MonoBehaviour 
{
    public float CamShakeTime = 1f;
    private float shakeTime = 0;
    private List<GameObject> monsters = new List<GameObject>();
    private Animator cameraAnimator;
    public Animator drawerAnimator;
    public Animator smoothAnimator;

    public GameObject SmoothObject;
    public bool isBlending = false;
	// Use this for initialization
	void Start () 
    {
        cameraAnimator = Camera.main.GetComponent<Animator>();
        //SmoothObject.SetActive(false);
        
	}
	
	// Update is called once per frame
	void Update () 
    {
       
        if (GameValues.ISPAUSED)
            return;
        
	    if (Input.GetButtonUp ("Fire1") ) 
        {
            for(int i = 0; i < monsters.Count; i++)
            {

                Monster monster = monsters[i].GetComponent<Monster>();
                //points
                /*
                if(monster is Strawberry)
                {
                    //for(int m = 1; m < )
                }
                 * */

                Debug.Log(monster);


                monster.Die();
                GameValues.SCORE++;
                shakeTime = Time.time +( CamShakeTime + 1000);
            }
            SmoothObject.SetActive(true);
            // Made an function when its blending. 
            Blend();
            isBlending = true;

           
        }
        if (Time.time >= shakeTime && isBlending)
        {
            cameraAnimator.SetTrigger("StopShake");
            //
            isBlending = false;
            SmoothObject.SetActive(false);
            Debug.Log("False!");
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

    public void Blend()
    {
        smoothAnimator.SetTrigger("Smoothy");
        cameraAnimator.SetTrigger("Shake");
        drawerAnimator.SetTrigger("Shake");
        monsters = new List<GameObject>();        
    }
}

    
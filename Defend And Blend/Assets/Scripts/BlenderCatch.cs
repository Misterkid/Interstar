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

    public  GameObject SmoothObject;
    public bool isBlending = false;


 
	// Use this for initialization
	void Start () 
    {
        cameraAnimator = Camera.main.GetComponent<Animator>();
        SmoothObject.active = false;
       
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Debug.Log(SmoothObject.active);
        
        //Debug.Log("isBlending" + isBlending);
        if (GameValues.ISPAUSED)
            return;
        
	    if (Input.GetButtonUp ("Fire1") ) 
        {
            isBlending = true;

            if(SmoothObject.active == false)
            {
                SmoothObject.active = true;
                smoothAnimator.SetBool("SmoothAnimation", true);
            }
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

                //Debug.Log(monster);

                
                monster.Die();
                GameValues.SCORE++;
                shakeTime = Time.time +( CamShakeTime + 1000);
            }
            Debug.Log(smoothAnimator.IsInTransition(0));
            // Made an function when its blending. 
            // isBlending = true;
            Blend();
            //Debug.Log(shakeTime);
           
           
        }
        if (Time.time >= shakeTime && isBlending)
        {
            cameraAnimator.SetTrigger("StopShake");

            //smoothAnimator.SetBool("SmoothAnimation") = false;
            

            //
            isBlending = false;
            //AfterBlend();
           // 
            //Debug.Log("False!");
        }
        //SmoothObject.SetActive(false);
        if (isBlending == false)
        {
            
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
        SmoothObject.active = true;
        smoothAnimator.SetBool("SmoothAnimation", true);
        cameraAnimator.SetTrigger("Shake");
        drawerAnimator.SetTrigger("Shake");
        monsters = new List<GameObject>();
    }


    public void AfterBlend()
    {
        smoothAnimator.SetBool("SmoothAnimation", false);
        SmoothObject.SetActive(false);
    }
}

    
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
    public Animator spiceJarsAnimator;

    public  GameObject SmoothObject;
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
        //Debug.Log(SmoothObject.active);
        //Debug.Log("isBlending" + isBlending);
        if (GameValues.ISPAUSED)
            return;
        
	    if (Input.GetButtonUp ("Fire1") && !isBlending) 
        {
            for(int i = 0; i < monsters.Count; i++)
            {

                Monster monster = monsters[i].GetComponent<Monster>();
                monster.Die();
                GameValues.SCORE++;
            }
            //Debug.Log(smoothAnimator.IsInTransition(0));
            // Made an function when its blending. 
            // isBlending = true;
            Blend();
            shakeTime = Time.time + CamShakeTime;
            isBlending = true;
        }
        if (Time.time >= shakeTime && isBlending)
        {
            //Debug.Log("Hello");
            StopBlend();
            //cameraAnimator.SetTrigger("StopShake");
            isBlending = false;
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

    private void Blend()
    {
        //SmoothObject.active = true;
        if (SmoothObject.activeSelf == false)
        {
            //SmoothObject.active = true;
            SmoothObject.SetActive(true);
            smoothAnimator.SetTrigger("Smooth");
        }
        cameraAnimator.SetTrigger("Shake");
        drawerAnimator.SetTrigger("Shake");
        spiceJarsAnimator.SetTrigger("Shake");
        monsters = new List<GameObject>();
    }
    private void StopBlend()
    {
        if (SmoothObject.activeSelf == true)
        {
            SmoothObject.SetActive(false);
            //SmoothObject.active = false;
            smoothAnimator.SetTrigger("StopSmooth");
        }
        cameraAnimator.SetTrigger("StopShake");
        spiceJarsAnimator.SetTrigger("StopShake");
    }
}
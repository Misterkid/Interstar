using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

    public int maxSmoothPoints = 60;
    public int smoothPoints;
    public Text smoothyText;

    public AudioClip blendSound;

    public int currentPointsInBlender = 0;
	// Use this for initialization
	void Start () 
    {
        cameraAnimator = Camera.main.GetComponent<Animator>();
        //SmoothObject.SetActive(false);
        smoothyText.text = "0";
	}
	
	// Update is called once per frame
	void Update () 
    {
        Debug.Log(GameValues.BlenderFilledPoints);
        
        
        //Debug.Log(SmoothObject.active);
        //Debug.Log("isBlending" + isBlending);
        if (GameValues.ISPAUSED)
            return;
        
	    if (Input.GetButtonUp ("Fire1") && !isBlending && monsters.Count > 0) 
        {
           // int smoothPoints = 0;
            for(int i = 0; i < monsters.Count; i++)
            {
                Monster monster = monsters[i].GetComponent<Monster>();
                monster.Die();
               // GameValues.SCORE++;
            }
            //
            //Debug.Log(smoothAnimator.IsInTransition(0));
            // Made an function when its blending. 
            // isBlending = true;
            Blend();
            shakeTime = Time.time + CamShakeTime;
            isBlending = true;
            SoundManager.Instance.PlaySound(blendSound, transform.position, SoundManager.SoundTypes.EFFECT);
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
            if (!monster.isInholding /*&& smoothPoints < 60*/)
            {
                GameValues.SCORE++;
                monster.isInBlender = true;
                smoothPoints += monster.fruitSize;
                Debug.Log(smoothPoints);
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
        if (smoothPoints > 20 && smoothPoints < 40)
        {
            GameValues.SMOOTHYPOINTS += (smoothPoints * 2);
        }
        else if (smoothPoints > 40)
        {
            GameValues.SMOOTHYPOINTS += (smoothPoints * 4);
        }
        else
        {
            GameValues.SMOOTHYPOINTS += smoothPoints;
        }
        GameValues.SCORE += smoothPoints;
        GameValues.SMOOTHYPOINTS += smoothPoints;
        smoothyText.text = GameValues.SMOOTHYPOINTS.ToString();
        smoothPoints = 0;

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
            smoothAnimator.SetTrigger("StopSmooth");
            SmoothObject.SetActive(false);
            //SmoothObject.active = false;
        }
        cameraAnimator.SetTrigger("StopShake");
        spiceJarsAnimator.SetTrigger("StopShake");
    }
}
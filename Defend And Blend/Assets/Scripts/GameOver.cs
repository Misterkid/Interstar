using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour 
{

	// Use this for initialization
    public Animator gameOverAnimator;
    private bool isGameOver = false;
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(isGameOver)
        {
            if(Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space))
            {
                Application.LoadLevel(0);
            }
        }
	}
    public void DieGame()
    {
        if (gameOverAnimator != null)
        {
            gameOverAnimator.gameObject.SetActive(true);
            gameOverAnimator.SetTrigger("gameover");
        }
        //GameOver
        WaveSpawnerTwo waveSpawnerTwo = GameObject.FindObjectOfType<WaveSpawnerTwo>();
        Destroy(waveSpawnerTwo);
        waveSpawnerTwo = null;

        HelpingHand helpingHand = GameObject.FindObjectOfType<HelpingHand>();
        Destroy(helpingHand);
        helpingHand = null;

        Defendable blender = GameObject.FindObjectOfType<Defendable>();
        Destroy(blender.gameObject);
        blender = null;

        Monster[] monsters = GameObject.FindObjectsOfType<Monster>();
        for (int i = 0; i < monsters.Length; i++)
        {
            Destroy(monsters[i].gameObject);
        }
        monsters = null;
        isGameOver = true;
    }
}

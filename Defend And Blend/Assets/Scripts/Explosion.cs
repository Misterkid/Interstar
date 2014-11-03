using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour 
{
    public AudioClip explosionClip;
    private ParticleSystem particleSystem;
    private float explosionEnd;
	// Use this for initialization
	void Start () 
    {
        SoundManager.PlaySound(explosionClip, transform.position, SoundManager.SoundTypes.EFFECT);
        particleSystem = this.GetComponent<ParticleSystem>();
        explosionEnd = Time.time + particleSystem.time;
	}
	
	// Update is called once per frame
	void Update () 
    {
            if (Time.time >= explosionEnd)
            {
                Destroy(gameObject);
            }
	}
}

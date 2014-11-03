using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour 
{
    public AudioClip explosionClip;
    private ParticleSystem particleSystem;
	// Use this for initialization
	void Start () 
    {
        if (explosionClip != null)
            SoundManager.PlaySound(explosionClip, transform.position, SoundManager.SoundTypes.EFFECT);

        particleSystem = this.GetComponent<ParticleSystem>();

        //particleSystem.startColor = 
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(!particleSystem.IsAlive())
        {
            Destroy(gameObject);
        }
	}
}

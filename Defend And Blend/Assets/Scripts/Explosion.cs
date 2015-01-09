using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour 
{
    public AudioClip explosionClip;
    private Light pointLight;
	// Use this for initialization
	void Start () 
    {

        //particleSystem.startColor = 
	}
    public void explode(Color color)
    {
        if (explosionClip != null)
            SoundManager.Instance.PlaySound(explosionClip, transform.position, SoundManager.SoundTypes.EFFECT);


        ParticleSystem[] particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < particleSystems.Length; i++ )
        {
            particleSystems[i].startColor = color;
        }
        particleSystem.startColor = color;
        particleSystem.Play();
    }
	
	// Update is called once per frame
	void Update () 
    {
       // particleSystem.startColor = Mathf.Lerp()
        if (!particleSystem.IsAlive())
        {
            Destroy(gameObject);
        }
	}
}

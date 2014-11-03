using UnityEngine;
using System.Collections;
using System.Timers;

/* 
 * Author: Eduard Meivogel
 * Website: https://www.facebook.com/EddyMeivogelProjects
 * Creation Year: 2014
 *
 *
 */
/// <summary>
/// NamedAudioSource
/// This is a class gives a type to audio, so we can change the volume for each kind of audio type
/// </summary> 
public class NamedAudioSource : MonoBehaviour
{
    /*
    public AudioClip audioClip;
    public SoundManager.SoundTypes audioType = SoundManager.SoundTypes.EFFECT;
    */
   // private GameObject gameObject;
    public SoundManager.SoundTypes _type;
    public AudioSource audioSource;
    private bool isAlwaysThere = false;
    private bool destroy = false;
    /*
    public SoundManager.SoundTypes type
    {
        get
        {
            return this.type;
        }
    }
     */
    private void Start()
    {
        if (audioSource != null)
        {
            isAlwaysThere = true;
            PlaySound(audioSource.clip, audioSource.transform.position, _type, audioSource.loop);
        }
    }
    public void PlaySound(AudioClip audioClip,Vector3 position, SoundManager.SoundTypes audioType,bool loop = false)
    {
        /*
        GameObject gameObject = new GameObject("NamedAudioSource");
        gameObject.AddComponent<Transform>();
        */
        gameObject.transform.position = position;
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        else
        {
            isAlwaysThere = true;
            loop = audioSource.loop;
        }
        if (audioSource.clip == null)
            audioSource.clip = audioClip;

        _type = audioType;
        switch(audioType)
        {
            case SoundManager.SoundTypes.MUSIC:
                    audioSource.volume = SoundManager.MUSIC_VOLUME;
                break;
            case SoundManager.SoundTypes.EFFECT:
                    audioSource.volume = SoundManager.EFFECT_VOLUME;
                break;
            case SoundManager.SoundTypes.VOICE:
                    audioSource.volume = SoundManager.VOICE_VOLUME;
                break;
            case SoundManager.SoundTypes.AMBIENT:
                audioSource.volume = SoundManager.AMBIENT_VOLUME;
                break;
            default:
                    audioSource.volume = 1;
                break;
        }
        audioSource.loop = loop;
        if(!loop && !isAlwaysThere)
        {
            Timer endTimer = new Timer(Mathf.Ceil(audioClip.length * 1000) );
            endTimer.Elapsed += EndTimer_Elapsed;
            endTimer.Start();
            //Debug.Log(audioClip.length + ":" + endTimer.Interval);
        }
        audioSource.Play();

    }

    void EndTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        Timer timer = sender as Timer;
        timer.Stop();
        timer.Elapsed -= EndTimer_Elapsed;
        timer.Dispose();
        timer = null;
       // Destroy(this.gameObject);
        destroy = true;
        //throw new System.NotImplementedException();
    }
    void Update()
    {
        if(destroy)
        {
            Destroy(this.gameObject);
        }
    }
}
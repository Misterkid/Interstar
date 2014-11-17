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
/// This is a class gives a type to audio, so we can change the volume for each kind of audio type.
/// </summary> 
[RequireComponent(typeof(AudioSource))]
public class NamedAudioSource : MonoBehaviour
{
    public SoundManager.SoundTypes _type;
    public AudioSource audioSource;
    private bool isAlwaysThere = false;
    private bool destroy = false;
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
        audioSource.volume = SoundManager.Instance.soundValues[audioType];

        audioSource.loop = loop;
        if(!loop && !isAlwaysThere)
        {
            Timer endTimer = new Timer(Mathf.Ceil(audioClip.length * 1000) );
            endTimer.Elapsed += EndTimer_Elapsed;
            endTimer.Start();
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
            SoundManager.Instance.RemoveSound(this);
            Destroy(this.gameObject);
        }
    }
}
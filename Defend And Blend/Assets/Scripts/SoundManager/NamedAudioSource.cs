using UnityEngine;
using System.Collections;
//using System.Timers;

/* 
 * Author: Eduard Meivogel
 * Website: https://www.facebook.com/EddyMeivogelProjects
 * Creation Year: 2014
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
    private float soundTimerEnd = 0;
    private void Start()
    {
        if (audioSource != null)
        {
            isAlwaysThere = true;
            PlaySound(audioSource.clip, audioSource.transform.position, _type, audioSource.loop);
        }
    }
    void Update()
    {
        if (Time.time >= soundTimerEnd)
        {
            SoundManager.Instance.RemoveSound(this);
            Destroy(this.gameObject);
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
            soundTimerEnd = Time.time + audioClip.length;//Mathf.Ceil(audioClip.length * 1000);

        }
        audioSource.Play();
    }
}
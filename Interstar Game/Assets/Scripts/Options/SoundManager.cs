using UnityEngine;
using System.Collections;

/* 
 * Author: Eduard Meivogel
 * Website: https://www.facebook.com/EddyMeivogelProjects
 * Creation Year: 2014
 *
 *
 */
public class SoundManager
{
    private static float music_volume;
    public static float MUSIC_VOLUME
    {
        set
        {
            if (music_volume != value)
            {
                music_volume = value;
                if (music_volume < 0)
                    music_volume = 0;
                if (music_volume > 1)
                    music_volume = 1;

                PlayerPrefs.SetFloat("e_sm_music", music_volume);
                ChangeVolume(SoundTypes.MUSIC);
            }
        }
        get
        {
            return music_volume;
        }
    }
    private static float effect_volume;
    public static float EFFECT_VOLUME
    {
        set
        {
            if (effect_volume != value)
            {
                effect_volume = value;
                if (effect_volume < 0)
                    effect_volume = 0;
                if (effect_volume > 1)
                    effect_volume = 1;

                PlayerPrefs.SetFloat("e_sm_effect", effect_volume);
                ChangeVolume(SoundTypes.EFFECT);
            }
        }
        get
        {
            return effect_volume;
        }
    }
    private static float voice_volume;
    public static float VOICE_VOLUME
    {
        set
        {
            if (voice_volume != value)
            {
                voice_volume = value;
                if (voice_volume < 0)
                    voice_volume = 0;
                if (voice_volume > 1)
                    voice_volume = 1;

                PlayerPrefs.SetFloat("e_sm_voice", voice_volume);
                ChangeVolume(SoundTypes.VOICE);
            }
        }
        get
        {
            return voice_volume;
        }
    }

    private static float ambient_volume;
    public static float AMBIENT_VOLUME
    {
        set
        {
            if (ambient_volume != value)
            {
                ambient_volume = value;
                if (ambient_volume < 0)
                    ambient_volume = 0;
                if (ambient_volume > 1)
                    ambient_volume = 1;

                PlayerPrefs.SetFloat("e_sm_ambient", ambient_volume);
                ChangeVolume(SoundTypes.AMBIENT);
            }
        }
        get
        {
            return ambient_volume;
        }
    }
    public enum SoundTypes
    {
        MUSIC,
        EFFECT,
        VOICE,
        AMBIENT
    }
    public static void SoundManagerInit()
    {
        if(PlayerPrefs.HasKey("e_sm_music"))
        {
            MUSIC_VOLUME = PlayerPrefs.GetFloat("e_sm_music");
        }
        else
        {
            PlayerPrefs.SetFloat("e_sm_music", 1);
            MUSIC_VOLUME = 1;
            Debug.Log("e_sm_music has been added to PlayerPrefs");
        }

        if(PlayerPrefs.HasKey("e_sm_effect"))
        {
            EFFECT_VOLUME = PlayerPrefs.GetFloat("e_sm_effect");
        }
        else
        {
            PlayerPrefs.SetFloat("e_sm_effect", 1);
            EFFECT_VOLUME = 1;
            Debug.Log("e_sm_effect has been added to PlayerPrefs");
        }

        if(PlayerPrefs.HasKey("e_sm_voice"))
        {
            VOICE_VOLUME = PlayerPrefs.GetFloat("e_sm_voice");
        }
        else
        {
            
            PlayerPrefs.SetFloat("e_sm_voice", 1);
            VOICE_VOLUME = 1;
            Debug.Log("e_sm_voice has been added to PlayerPrefs");
        }

        if (PlayerPrefs.HasKey("e_sm_ambient"))
        {
            AMBIENT_VOLUME = PlayerPrefs.GetFloat("e_sm_ambient");
        }
        else
        {

            PlayerPrefs.SetFloat("e_sm_ambient", 1);
            AMBIENT_VOLUME = 1;
            Debug.Log("e_sm_ambient has been added to PlayerPrefs");
        }
    }
    public static void PlaySound(AudioClip audioClip,Vector3 position,SoundTypes soundType,bool loop = false,Transform parent = null)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.position = position;
        if (parent != null)
            gameObject.transform.parent = parent;
        NamedAudioSource namedAudioSource = gameObject.AddComponent<NamedAudioSource>();
        namedAudioSource.PlaySound(audioClip, position, soundType,loop);
    }
    private static void ChangeVolume(SoundManager.SoundTypes type)
    {
        NamedAudioSource[] audioSources = GameObject.FindObjectsOfType<NamedAudioSource>() as NamedAudioSource[];
        for(int i = 0; i < audioSources.Length; i++)
        {
            if(type == audioSources[i]._type)
            {
                switch(type)
                {
                    case SoundManager.SoundTypes.MUSIC:
                            audioSources[i].audioSource.volume = SoundManager.MUSIC_VOLUME;
                        break;
                    case SoundManager.SoundTypes.EFFECT:
                            audioSources[i].audioSource.volume = SoundManager.EFFECT_VOLUME;
                        break;
                    case SoundManager.SoundTypes.VOICE:
                            audioSources[i].audioSource.volume = SoundManager.VOICE_VOLUME;
                        break;

                    case SoundManager.SoundTypes.AMBIENT:
                        audioSources[i].audioSource.volume = SoundManager.AMBIENT_VOLUME;
                        break;
                    default:
                            audioSources[i].audioSource.volume = 1;
                        break;
                }
            }
        }
    }
}
﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/* 
 * Author: Eduard Meivogel
 * Website: https://www.facebook.com/EddyMeivogelProjects
 * Creation Year: 2014
 */
/// <summary>
/// SoundManager
/// This is a class to change the volume for each "channel" or to play a sound.
/// </summary> 
public sealed class SoundManager
{
    static private SoundManager _instance;// = new SoundManager();
    #region Private Variables
    private List<NamedAudioSource> namedAudioSources = new List<NamedAudioSource>();//NamedAudioSource[] audioSources;
    #endregion
    #region Public Variables
    /// <summary>
    /// A dictionary with sounds by soundtypes.
    /// </summary> 
    public Dictionary<SoundTypes, float> soundValues = new Dictionary<SoundTypes, float>();
    /// <summary>
    /// Diffrent kind of soundtypes !
    /// </summary> 
    public enum SoundTypes
    {
        MUSIC,
        EFFECT,
        VOICE,
        AMBIENT
    }
    #endregion
    public static SoundManager Instance
    {
        get
        {
            return _instance = _instance ?? new SoundManager();
        }
    }
    SoundManager()
    {
        if (!PlayerPrefs.HasKey("e_sm_music"))//Is there a music key?
        {
            PlayerPrefs.SetFloat("e_sm_music", 1);//Create one and set to 1
            Debug.Log("e_sm_music has been added to PlayerPrefs");
        }
        soundValues.Add(SoundTypes.MUSIC, PlayerPrefs.GetFloat("e_sm_music"));//add it to dictionary
        //Repeat
        if (!PlayerPrefs.HasKey("e_sm_effect"))
        {
            PlayerPrefs.SetFloat("e_sm_effect", 1);
            Debug.Log("e_sm_effect has been added to PlayerPrefs");
        }
        soundValues.Add(SoundTypes.EFFECT, PlayerPrefs.GetFloat("e_sm_effect"));

        if (!PlayerPrefs.HasKey("e_sm_voice"))
        {
            PlayerPrefs.SetFloat("e_sm_voice", 1);
            Debug.Log("e_sm_voice has been added to PlayerPrefs");
        }
        soundValues.Add(SoundTypes.VOICE, PlayerPrefs.GetFloat("e_sm_voice"));

        if (!PlayerPrefs.HasKey("e_sm_ambient"))
        {
            PlayerPrefs.SetFloat("e_sm_ambient", 1);
            Debug.Log("e_sm_ambient has been added to PlayerPrefs");
        }
        soundValues.Add(SoundTypes.AMBIENT, PlayerPrefs.GetFloat("e_sm_ambient"));
    }
    #region Public Methods
    /// <summary>
    /// Play a sound at position with a soundtype. Can it loop? and does it need a parent?
    /// </summary> 
    public void PlaySound(AudioClip audioClip,Vector3 position,SoundTypes soundType,bool loop = false,Transform parent = null)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.position = position;
        if (parent != null)
            gameObject.transform.parent = parent;

        NamedAudioSource namedAudioSource = gameObject.AddComponent<NamedAudioSource>();
        namedAudioSource.PlaySound(audioClip, position, soundType,loop);
        namedAudioSources.Add(namedAudioSource);
    }
    /// <summary>
    /// Remove the sound from the list! :)
    /// </summary>
    public void RemoveSound(NamedAudioSource namedAudioSource)
    {
        namedAudioSources.Remove(namedAudioSource);
    }
    public void AddSound(NamedAudioSource namedAudioSource)
    {
        namedAudioSources.Add(namedAudioSource);
    }
    /// <summary>
    /// Change the Volume of a sound type.
    /// Use SoundManager.Instance.soundValues[SoundManager.SoundTypes.TYPE]
    /// </summary>
    public void ChangeVolume(float volume,SoundManager.SoundTypes type)
    {
        soundValues[type] = volume;
        List<NamedAudioSource> destroyedSources = new List<NamedAudioSource>();
        for(int i = 0; i < namedAudioSources.Count; i++)
        {
            if(namedAudioSources[i]._type == type)
            {
                if (namedAudioSources[i] != null)
                    namedAudioSources[i].audioSource.volume = soundValues[type];
                else
                    destroyedSources.Add(namedAudioSources[i]);//queue for deletion
            }
        }
        for (int d = 0; d < destroyedSources.Count; d++)
        {
            namedAudioSources.Remove(destroyedSources[d]);//EXTERMINATE! EXTERMINATE!
        }
        destroyedSources = null;

        PlayerPrefs.SetFloat("e_sm_music", soundValues[SoundTypes.MUSIC]);//Create one and set to 1
        PlayerPrefs.SetFloat("e_sm_effect", soundValues[SoundTypes.EFFECT]);
        PlayerPrefs.SetFloat("e_sm_voice", soundValues[SoundTypes.VOICE]);
        PlayerPrefs.SetFloat("e_sm_ambient", soundValues[SoundTypes.AMBIENT]);
    }
    #endregion
}
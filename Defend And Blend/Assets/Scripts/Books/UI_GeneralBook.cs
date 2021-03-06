﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI_GeneralBook : MonoBehaviour
{
    public GameObject rp_StartGame;
    public GameObject rp_Options;
    public GameObject rp_Highscores;
    public GameObject rp_Credits;
    
    public HelpingHand theHand;

    public Slider musicSlider;
    public Slider effectSlider;
    public Slider ambientSlider;
    //public Animator cameraAnimator;
    protected virtual void Start()
    {
        if (musicSlider != null)
            musicSlider.value = SoundManager.Instance.soundValues[SoundManager.SoundTypes.MUSIC];
        if (effectSlider != null)
            effectSlider.value = SoundManager.Instance.soundValues[SoundManager.SoundTypes.EFFECT];
        if (ambientSlider != null)
            ambientSlider.value = SoundManager.Instance.soundValues[SoundManager.SoundTypes.AMBIENT];
    }
    public void setMusicVolume(float vol)
    {
        //backgroundMusic.volume = vol;
        //SoundManager.Instance.PlaySound()
        SoundManager.Instance.ChangeVolume(vol, SoundManager.SoundTypes.MUSIC);

    }

    public void setSoundEffectsVolume(float vol)
    {
        SoundManager.Instance.ChangeVolume(vol, SoundManager.SoundTypes.EFFECT);
    }
    public void setAmbientVolume(float vol)
    {
        SoundManager.Instance.ChangeVolume(vol, SoundManager.SoundTypes.AMBIENT);
    }
    /*
    public void setAutoXMovement(bool autoX)
    {
        theHand.AutoMoveX = autoX;

        Debug.Log("X-as goes automaticly? :" + theHand.AutoMoveX);
    }
    public void setAutoYMovement(bool autoY)
    {
        theHand.AutoMoveY = autoY;
        Debug.Log("Y-as goes automaticly? :" + theHand.AutoMoveY);
    }
    public void setAutoGrab(bool autoGrab)
    {
        theHand.AutoGrab = autoGrab;
        Debug.Log("Grabbing Automaticly? :" + theHand.AutoGrab);
    }
    */
    protected void turnOffAllRightPages()
    {
        if (rp_StartGame != null)
        {
            rp_StartGame.SetActive(false);
        }
        if (rp_Options != null)
        {
            rp_Options.SetActive(false);
        }
        if (rp_Credits != null)
        {
            rp_Credits.SetActive(false);
        }
        if (rp_Highscores != null)
        {
            rp_Highscores.SetActive(false);
        }
    }
    protected void openOptions()
    {
        //First we need to turn off the other pages.
        turnOffAllRightPages();
        //BookAnimator.SetTrigger("turnPage_anim");
        if (rp_Options.active == true)
        {
            rp_Options.SetActive(false);
        }
        else if (rp_Options.active == false)
        {
            rp_Options.SetActive(true);

        }
    }

    
}

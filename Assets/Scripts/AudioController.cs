using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioSource MusicSFX;
    public AudioSource MusicSFX2;
    public AudioSource MusicSFX3;
    public AudioSource SoundSFX;

    public Sprite MusicOn;
    public Sprite MusicOff;
    public Sprite SoundOn;
    public Sprite SoundOff;

    public Image MusicButton;
    public Image SoundButton;



    public void OnClickMusicButton()
    {
        if (MusicSFX.volume >= 0.1f)
        {
            MusicSFX.volume = 0;
            MusicButton.GetComponent<Image>().sprite = MusicOff;

        }
        else
        {
            MusicSFX.volume = 0.5f;
            MusicButton.GetComponent<Image>().sprite = MusicOn;
        }
    }



    public void OnClickSoundButton()
    {
        if (SoundSFX.volume >= 0.1f)
        {
           SoundSFX.volume = 0;
            SoundButton.GetComponent<Image>().sprite = SoundOff;

        }
        else
        {
            SoundSFX.volume = 0.5f;
            SoundButton.GetComponent<Image>().sprite = SoundOn;
        }
    }





}

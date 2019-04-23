using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenuIG : MonoBehaviour
{
    public Slider slider;
    public AudioMixer audioMixer;
    public Text VolumePercentage;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Volume");
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume",volume);
        audioMixer.SetFloat("Volume",PlayerPrefs.GetFloat("Volume",volume));
    }

    public void FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    private void Update() 
    {
        VolumePercentageUpdate();
    }


    public void VolumePercentageUpdate()
    {
        
        VolumePercentage.text = Mathf.RoundToInt( PlayerPrefs.GetFloat("Volume") *100/80+100) + "%";
    }
  
}

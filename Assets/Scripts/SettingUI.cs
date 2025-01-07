using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class SettingUI : MonoBehaviour
{
    public Scrollbar volumeSlider;
    public Scrollbar musicSlider;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        musicSlider.onValueChanged.AddListener(SetGlobalMusic);
    }

    public void SetGlobalVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void SetGlobalMusic(float value)
    {
        AudioListener.volume = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    [Header("사운드 등록")]
    [SerializeField] Sound[] bgmSounds;


    [Header("사운드 플레이어")]
    [SerializeField] AudioSource bgmPlayer;

    private float Sound_Background = 0.2f;
    private float Sound_Effect = 0.2f;

    // option panal
    // public GameObject _optionpanel;
    // public GameObject _optionpanelfrommain;


    void Start()
    {
        // playBGM1();
    }
    public void playBGM1()
    {
        bgmPlayer.clip = bgmSounds[0].clip;
        bgmPlayer.Play();
    }

    public void playBGM2()
    {
        bgmPlayer.clip = bgmSounds[1].clip;
        bgmPlayer.Play();
    }

    public void setBackgroundSound(Slider slider)
    {
        Sound_Background = slider.value;
    }

    public void setEffectSound(Slider slider)
    {
        Sound_Effect = slider.value;
    }

    public float getBackgroundSound()
    {
        return Sound_Background;
    }

    public float getEffectSound()
    {
        return Sound_Effect;
    }

}

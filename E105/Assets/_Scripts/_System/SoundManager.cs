using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sound{
    public string soundName;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    [Header("사운드 등록")]
    [SerializeField] Sound[] bgmSounds;


    [Header("사운드 플레이어")]
    [SerializeField] AudioSource bgmPlayer;


    private float Sound_Background = 20;
    private float Sound_Effect = 20;

    // option panal
    public GameObject _optionpanel;
    public GameObject _optionpanelfrommain;


    void Start()
    {
        playBGM();
    }
    public void playBGM()
    {
        bgmPlayer.clip = bgmSounds[0].clip;
        bgmPlayer.Play();

    }

    public void setBackgroundSound(Slider slider)
    {
        Sound_Background = slider.value;
        _optionpanel.transform.Find("BackgroundSlider").gameObject.GetComponent<Slider>().value = Sound_Background;
        _optionpanelfrommain.transform.Find("BackgroundSlider").gameObject.GetComponent<Slider>().value = Sound_Background;
        bgmPlayer.volume = Sound_Background;
    }

    public void setEffectSound(Slider slider)
    {
        Sound_Effect = slider.value;
        _optionpanel.transform.Find("EffectSlider").gameObject.GetComponent<Slider>().value = Sound_Effect;
        _optionpanelfrommain.transform.Find("EffectSlider").gameObject.GetComponent<Slider>().value = Sound_Effect;
    }

    // Start is called before the first frame update
    // void Start()
    // {
    //     Debug.Log(_optionpanel);
    //     _optionpanel.transform.Find("BackgroundSlider").gameObject.GetComponent<Slider>().value = Sound_Background;
    //     _optionpanelfrommain.transform.Find("BackgroundSlider").gameObject.GetComponent<Slider>().value = Sound_Background;
    //     _optionpanel.transform.Find("EffectSlider").gameObject.GetComponent<Slider>().value = Sound_Effect;
    //     _optionpanelfrommain.transform.Find("EffectSlider").gameObject.GetComponent<Slider>().value = Sound_Effect;

    // }

    // Update is called once per frame
    void Update()
    {

    }
}

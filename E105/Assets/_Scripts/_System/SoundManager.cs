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

    [SerializeField] private float Sound_Background = 0.2f;
    [SerializeField] private float Sound_Effect = 0.2f;

    private UIManager _UIManager;

    // option panal
    // public GameObject _optionpanel;
    // public GameObject _optionpanelfrommain;
    void Awake() {
        Sound_Background = 0.2f;
    }

    void Start()
    {
        _UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        
        playMainTheme();
    }

    public void FunctionCall(int num){
        switch(num){
            case 0:
                playBGM1();
                break;
            case 1:
                playRandom();
                break;
        }
    }

    public void playBGM1()  //정화
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = bgmSounds[0].clip;
        bgmPlayer.Play();
    }

    public void playBGM2()  //황폐화
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = bgmSounds[1].clip;
        bgmPlayer.Play();
    }

    public void playBGM3()  //숲
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = bgmSounds[2].clip;
        bgmPlayer.Play();
    }

    public void playBGM4()  //제단
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = bgmSounds[3].clip;
        bgmPlayer.Play();
    }

    public void playRandom()  //음악가가 랜덤음악 재생
    {
        bgmPlayer.Stop();
        int random = Random.Range(0, 2);

        switch (random)
        {
            case 0:
                bgmPlayer.clip = bgmSounds[4].clip;
                break;
            case 1:
                bgmPlayer.clip = bgmSounds[5].clip;
                break;
        }

        bgmPlayer.Play();

        _UIManager.BGMChanger();
    }

    public void playMainTheme()  // 메인 화면
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = bgmSounds[6].clip;
        bgmPlayer.Play();
    }

    public void setBackgroundSound(Slider volume)
    {
        Sound_Background = volume.value;
        bgmPlayer.volume = Sound_Background;
    }

    public void setEffectSound(Slider volume)
    {
        Sound_Effect = volume.value;
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

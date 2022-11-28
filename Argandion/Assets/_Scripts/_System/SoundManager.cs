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
    public AudioSource effectSoundPlayer;
    public AudioClip getItem;

    // public AudioClip drinking;
    // public AudioClip eating;
    public AudioClip cooking;
    public AudioClip fishing;
    public AudioClip farming;
    public AudioClip watering;
    public AudioClip alter;

    private bool canplay = true;
    public AudioSource playerSoundPlayer;
    public AudioClip moving;
    public AudioClip axing;
    public AudioClip picking;

    void Awake() {
        Sound_Background = 0.4f;
        bgmPlayer.volume = Sound_Background;
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

    public void playEffectSound(string action)
    {
        switch(action) {
            case "GETITEM":
                effectSoundPlayer.clip = getItem;
                break;
            // case "DRINKING":
            //     effectSoundPlayer.clip = drinking;
            //     break;
            // case "EATING":
            //     effectSoundPlayer.clip = eating;
            //     break;
            case "AXING":
                effectSoundPlayer.clip = axing;
                break;
            case "PICKING":
                effectSoundPlayer.clip = picking;
                break;
            case "COOKING":
                effectSoundPlayer.clip = cooking;
                break;
            case "FISHING":
                effectSoundPlayer.clip = fishing;
                break;
            case "FARMING":
                effectSoundPlayer.clip = farming;
                break;
            case "WATERING":
                effectSoundPlayer.clip = watering;
                break;
            case "ALTER":
                effectSoundPlayer.clip = alter;
                break;
        }
        effectSoundPlayer.Play();
    }

    public void playerEffectSound(string action)
    {
        if(canplay){
            switch(action) {
                case "MOVING":
                    playerSoundPlayer.clip = moving;
                    canplay = false;
                    Invoke("CanPlayTrue", 0.4f);
                    break;
                case "RUNNING":
                    playerSoundPlayer.clip = moving;
                    canplay = false;
                    Invoke("CanPlayTrue", 0.2f);
                    break;
                case "AXING":
                    playerSoundPlayer.clip = axing;
                    break;
            }
            playerSoundPlayer.Play();
        }
    }

    private void CanPlayTrue() {
        canplay = true;
    }

}

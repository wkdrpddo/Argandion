using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public SystemManager systemManager;
    public BuffManager _buffManager;
    public ParticleSystem _snow;
    public ParticleSystem _rain;

    private float[] perfect = {1.0f, 99.0f};
    private int[] perfectIdx = {7, 0};
    private float[] spring = {1.0f, 6.0f, 13.0f, 13.0f, 67.0f};
    private int[] springIdx = {7, 6, 2, 1, 0};
    private float[] summer = {1.0f, 4.0f, 6.0f, 8.0f, 10.0f, 14.0f, 57.0f};
    private int[] summerIdx = {7, 6, 5, 3, 2, 1, 0};
    private float[] summerAfter5 = {1.0f, 4.0f, 8.0f, 10.0f, 20.0f, 57.0f};
    private int[] summerAfter5Idx = {7, 6, 3, 2, 1, 0};
    private float[] fall = {1.0f, 4.0f, 10.0f, 12.0f, 13.0f, 60.0f};
    private int[] fallIdx = {7, 5, 1, 6, 2, 0};
    private float[] fallAfter5 = {1.0f, 12.0f, 13.0f, 14.0f, 60.0f};
    private int[] fallAfter5Idx = {7, 6, 2, 1, 0};
    private float[] winter = {1.0f, 9.0f, 11.0f, 14.0f, 65.0f};
    private int[] winterIdx = {7, 4, 1, 2, 0};
    private float[] winterAfter4 = {1.0f, 14.0f, 20.0f, 65.0f};
    private int[] winterAfter4Idx = {7, 2, 1, 0};

    private bool after4 = false; // 폭설 후
    private bool while4 = false; // 폭설 중 (폭설 - 폭설 - 비를 구현하기 위한 변수)
    private int how4 = 0; // 폭설 몇일째인지 (폭설 - 폭설 - 비를 구현하기 위한 변수)

    private bool after5 = false; // 태풍 후
    private bool while5 = false; // 태풍 중 (태풍-태풍-비를 구현하기 위한 변수)
    private int how5 = 0; // 태풍 몇일째인지 (태풍-태풍-비를 구현하기 위한 변수)

    private void Start() {
        _buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        SetYearEvent(); // 나중에 변경/삭제 해야함
        _snow = GameObject.Find("Main Camera").transform.GetChild(0).GetComponent<ParticleSystem>();
        _rain = GameObject.Find("Main Camera").transform.GetChild(1).GetComponent<ParticleSystem>();
    }
    private int _season;

    public void SetWeather(int season)
    {
        _season = season;
        if (_buffManager.blueSpirit) { // 파랑색 정령버프가 있으면 무조건 비가 온다.
            systemManager._weather = 1;
            _buffManager.blueSpirit = false;
            playFX(1);
            return ;
        }

        float randomPerfect = Random.value * 100.0f;
        if (randomPerfect <= 1.0f) {
            systemManager._weather = 7;
            if (while4){
                if (how4 == 2) {
                    while4 = false;
                    after4 = true;
                } else {
                    how4 +=1;
                }
            }

            if (while5){
                if (how5 == 2) {
                    while5 = false;
                    after5 = true;
                } else {
                    how5 +=1;
                }
            }
            return ;
        }

        if (season == 0) { // 봄은 무난하다
            SetSpring();
        } else if (season == 1) { // 여름의 경우
            if( !while5 ){ // 태풍중이 아니라면 날씨를 정해준다
                SetSummer();
            } else if ( how5 < 2) { // 태풍 둘쨋날 (how5 == 1) 이면 여전히 날씨는 태풍
                systemManager._weather = 5;
                how5 += 1;
                playFX(1);
                return ;
            } else { // 태풍 마지막날엔 비가 내리고, 태풍이 끝나고 , 태풍후 변수를 true로 바꿔준다.
                systemManager._weather = 1;
                while5 = false;
                after5 = true;
                playFX(1);
                return ;
            }
        } else if (season == 2) { // 가을의 경우
            if( !while5 ){ // 태풍중이 아니라면 날씨를 정해준다
                SetFall();
            } else if ( how5 < 2) { // 태풍 둘쨋날 (how5 == 1) 이면 여전히 날씨는 태풍
                systemManager._weather = 5;
                how5 += 1;
                playFX(1);
                return ;
            } else { // 태풍 마지막날엔 비가 내리고, 태풍이 끝나고 , 태풍 후 변수를 true로 바꿔준다.
                systemManager._weather = 1;
                while5 = false;
                after5 = true;
                playFX(1);
                return ;
            }
        } else if (season == 3) { // 겨울의 경우
            if( !while4 ){ // 폭설 중이 아니라면 날씨를 정해준다.
                SetWinter();
            } else if ( how4 < 2) { // 폭설 둘쨋날 (how4 == 1) 이면 여전히 날씨는 폭설
                systemManager._weather = 4;
                how4 += 1;
                playFX(1);
                return ;
            } else { // 폭설 마지막날엔 비가 내리고, 폭설이 끝나고, 폭설 후 변수를 true로 바꿔준다.
                systemManager._weather = 1;
                while4 = false;
                after4 = true;
                playFX(1);
                return ;
            }
        }
    }

    private void SetSpring()
    {
        float randomValue = Random.value * 100.0f;

        float cumulative = 0.0f;
        for (int i = 0; i < spring.Length; i++){
            cumulative += spring[i];
            if(randomValue <= cumulative) {
                systemManager._weather = springIdx[i];
                playFX(springIdx[i]);
                return ;
            }
        }
    }

    private void SetSummer()
    {   

        if (_buffManager.inRainy) { // 장마 시즌일경우 비로 고정
            systemManager._weather = 1;
            return ;
        }

        float randomValue = Random.value * 100.0f;

        float cumulative = 0.0f;
        if (after5) {
            for (int i = 0; i < summerAfter5.Length; i ++) {
                cumulative += summerAfter5[i];
                if(randomValue <= cumulative) {
                    systemManager._weather = summerAfter5Idx[i];
                    playFX(summerAfter5Idx[i]);
                    return ;
                }
            }
        } else {
            for (int i = 0; i< summer.Length; i++) {
                cumulative += summer[i];
                if(randomValue <= cumulative) {
                    systemManager._weather = summerIdx[i];
                    if (summerIdx[i] == 5) {
                        while5 = true;
                        how5 += 1;
                    }
                    playFX(summerIdx[i]);
                    return ;
                }
            }
        }
    }

    private void SetFall()
    {
        float randomValue = Random.value * 100.0f;

        float cumulative = 0.0f;
        if (after5) {
            for (int i = 0; i < fallAfter5.Length; i ++) {
                cumulative += fallAfter5[i];
                if(randomValue <= cumulative) {
                    systemManager._weather = fallAfter5Idx[i];
                    playFX(fallAfter5Idx[i]);
                    return ;
                }
            }
        } else {
            for (int i = 0; i< fall.Length; i++) {
                cumulative += fall[i];
                if(randomValue <= cumulative) {
                    systemManager._weather = fallIdx[i];
                    if (fallIdx[i] == 5) {
                        while5 = true;
                        how5 += 1;
                    }
                    playFX(fallIdx[i]);
                    return ;
                }
            }
        }
    }

    private void SetWinter()
    {
        float randomValue = Random.value * 100.0f;

        float cumulative = 0.0f;
        if (after4) {
            for (int i = 0; i < winterAfter4.Length; i ++) {
                cumulative += winterAfter4[i];
                if(randomValue <= cumulative) {
                    systemManager._weather = winterAfter4Idx[i];
                    playFX(winterAfter4Idx[i]);
                    return ;
                }
            }
        } else {
            for (int i = 0; i< winter.Length; i++) {
                cumulative += winter[i];
                if(randomValue <= cumulative) {
                    systemManager._weather = winterIdx[i];
                    if (winterIdx[i] == 4) {
                        while4 = true;
                        how4 += 1;
                    }
                    playFX(winterIdx[i]);
                    return ;
                }
            }
        }
    }

    private void SetYearEvent()
    {   
        // 변수 초기화
        after4 = false;
        while4 = false; 
        how4 = 0; 
        after5 = false; 
        while5 = false; 
        how5 = 0;
        
        // 장마, 한파 날짜 정해주기
        _buffManager.rainyMonth = Random.Range(3,5);
        if (_buffManager.rainyMonth == 2) {
            _buffManager.rainyDay = Random.Range(1,29);
        } else {
            _buffManager.rainyDay = Random.Range(1,23);
        }
        _buffManager.coldWaveMonth = Random.Range(7,9);
        if (_buffManager.coldWaveMonth == 6) {
            _buffManager.coldWaveDay = Random.Range(1,29);
        } else {
            _buffManager.coldWaveDay = Random.Range(1,16);
        }
    }

    private void playFX(int index)
    {
        switch(index)
        {
            case 0:
                _snow.Stop();
                _rain.Stop();
                break;
            case 1:
                if (_season == 3)
                {
                    _snow.Play();
                    _rain.Stop();
                }
                else
                {
                    _rain.Play();
                    _snow.Stop();
                }
                break;
            case 2:
                _rain.Stop();
                _snow.Stop();
                break;
            case 3:
                _rain.Stop();
                _snow.Stop();
                break;
            case 4:
                _rain.Stop();
                _snow.Play();
                break;
            case 5:
                _rain.Play();
                _snow.Stop();
                break;
            case 6:
                _rain.Stop();
                _snow.Stop();
                break;
            case 7:
                _rain.Stop();
                _snow.Stop();
                break;
        }
    }
}

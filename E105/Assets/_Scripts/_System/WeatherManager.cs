using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public SystemManager systemManager;

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

    private int rainyMonth = 0;
    private int rainyDay = 0;
    private int coldWindMonth = 0;
    private int coldWindDay = 0;

    public void SetWeather(int season)
    {
        if (season == 0) {
            SetSpring();
        } else if (season == 1) {
            if( !while5 ){
                SetSummer();
            } else if ( how5 < 2) {
                systemManager._weather = 5;
                how5 += 1;
                return ;
            } else {
                systemManager._weather = 1;
                while5 = false;
                return ;
            }
        } else if (season == 2) {
            if( !while5 ){
                SetFall();
            } else if ( how5 < 2) {
                systemManager._weather = 5;
                how5 += 1;
                return ;
            } else {
                systemManager._weather = 1;
                while5 = false;
                return ;
            }
        } else if (season == 3) {
            if( !while4 ){
                SetWinter();
            } else if ( how4 < 2) {
                systemManager._weather = 4;
                how4 += 1;
                return ;
            } else {
                systemManager._weather = 1;
                while4 = false;
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
                return ;
            }
        }
    }

    private void SetSummer()
    {
        float randomValue = Random.value * 100.0f;

        float cumulative = 0.0f;
        if (after5) {
            for (int i = 0; i < summerAfter5.Length; i ++) {
                cumulative += summerAfter5[i];
                if(randomValue <= cumulative) {
                    systemManager._weather = summerAfter5Idx[i];
                    return ;
                }
            }
        } else {
            for (int i = 0; i< summer.Length; i++) {
                cumulative += summer[i];
                if(randomValue <= cumulative) {
                    systemManager._weather = summerIdx[i];
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
                    return ;
                }
            }
        } else {
            for (int i = 0; i< fall.Length; i++) {
                cumulative += fall[i];
                if(randomValue <= cumulative) {
                    systemManager._weather = fallIdx[i];
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
                    return ;
                }
            }
        } else {
            for (int i = 0; i< winter.Length; i++) {
                cumulative += winter[i];
                if(randomValue <= cumulative) {
                    systemManager._weather = winterIdx[i];
                    return ;
                }
            }
        }
    }

    private void SetYearEvent()
    {

    }
}

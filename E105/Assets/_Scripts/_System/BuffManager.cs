using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public SystemManager _systemManager;

    // 꽃 버프 관련
    public int _flowerBuffTargetMonth = 0; // 버프가 끝나는 날의 달
    public int _flowerBuffTargetDay = 0; // 버프가 끝나는 날의 날
    public bool _isFlowerBuffActived = false; // 버프 진행 중
    // 버프 종류
    public bool whiteSpirit = false;
    public bool whitePray = false;
    public bool orangePray = false;
    public bool blueSpirit = false;
    public bool bluePray = false;
    public bool redSpirit = false;
    public bool redPray = false;
    public bool pinkSpirit = false;
    public bool pinkPray = false;
    public bool yellowSpirit = false;
    public bool yellowPray = false;
    public bool skySpirit = false;
    public bool skyPray = false;

    // 장마, 한파 관련
    public int coldWaveMonth;
    public int coldWaveDay;
    public bool inColdWave = false;
    public int rainyMonth;
    public int rainyDay;
    public bool inRainy = false;
    public int _endMonth;
    public int _endDay;

    private void Start()
    {
        _systemManager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
    }

    public void DayEnd()
    {
        if (_flowerBuffTargetMonth == _systemManager._month && _flowerBuffTargetDay == _systemManager._day)
        {
            FlowerBuffEnd();
        }

        if (_endMonth == _systemManager._month && _endDay == _systemManager._day)
        {
            inColdWave = false;
            inRainy = false;
            _endMonth = 0;
            _endDay = 0;
        }

        if (coldWaveMonth == _systemManager._month && coldWaveDay == _systemManager._day)
        {
            inColdWave = true;
            _endMonth = coldWaveMonth;
            _endDay = coldWaveDay + 14;
            if (_endDay > 28)
            {
                _endDay -= 28;
                _endMonth += 1;
            }
        }

        if (rainyMonth == _systemManager._month && rainyDay == _systemManager._day)
        {
            inRainy = true;
            _endMonth = rainyMonth;
            _endDay = rainyDay + 7;
            if (_endDay > 28)
            {
                _endDay -= 28;
                _endMonth += 1;
            }
        }
    }

    public void FlowerBuffEnd()
    {
        Debug.Log("버프끗");
        _flowerBuffTargetMonth = 0;
        _flowerBuffTargetDay = 0;
        _isFlowerBuffActived = false;
        whiteSpirit = false;
        whitePray = false;
        orangePray = false;
        bluePray = false;
        redSpirit = false;
        redPray = false;
        pinkSpirit = false;
        pinkPray = false;
        yellowSpirit = false;
        yellowPray = false;
        skySpirit = false;
        skyPray = false;
    }
}

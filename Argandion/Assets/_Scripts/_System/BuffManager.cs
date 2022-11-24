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
    public bool _isPrayBuffActived = false; // 제단 버프 진행 중
    // 버프 종류
    public bool whiteSpirit = false;
    public bool whitePray = false;
    public bool orangeSpirit = false;
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
    public bool isColdWaveIcons = false;
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
            FlowerBuffEnd(20);
        }

        if (_endMonth == _systemManager._month && _endDay == _systemManager._day)
        {
            // 한파 종료시 아이콘 제거
            if(inColdWave)
            {
                inColdWave = false;
                // _systemManager._EventPanel.inactiveIcon(58);
                if (isColdWaveIcons) {
                    _systemManager.callInactiveIcon(58);
                    isColdWaveIcons = false;
                }
            }
            if(inRainy)
            {
                inRainy = false;
            }
            _endMonth = 0;
            _endDay = 0;
        }

        if (coldWaveMonth == _systemManager._month && coldWaveDay == _systemManager._day)
        {
            inColdWave = true;
            _systemManager._EventPanel.activeIcon(58);
            isColdWaveIcons = true;
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

        if (redPray && isColdWaveIcons)
        {
            _systemManager._EventPanel.inactiveIcon(58);
            isColdWaveIcons = false;
        }
        if (!redPray && !isColdWaveIcons && inColdWave)
        {
            _systemManager._EventPanel.activeIcon(58);
            isColdWaveIcons = true;
        }
    }

    public void FlowerBuffEnd(int index)
    {
        _flowerBuffTargetMonth = 0;
        _flowerBuffTargetDay = 0;
        _isFlowerBuffActived = false;
        _isPrayBuffActived = false;
        if (index != 11 && whiteSpirit)
        {
            whiteSpirit = false;
            _systemManager.callInactiveIcon(11);
        }
        if (index != 1 && whitePray)
        {
            whitePray = false;
            _systemManager.callInactiveIcon(1);
        }
        if (index != 12 && orangeSpirit)
        {
            orangeSpirit = false;
            _systemManager.callInactiveIcon(12);
        }
        if (index != 2 && orangePray)
        {
            orangePray = false;
            _systemManager.callInactiveIcon(2);
        }
        if (index != 13 && blueSpirit)
        {
            bluePray = false;
            _systemManager.callInactiveIcon(13);
        }
        if (index != 3 && bluePray)
        {
            bluePray = false;
            _systemManager.callInactiveIcon(3);
        }
        if (index != 14 && redSpirit)
        {
            redSpirit = false;
            _systemManager.callInactiveIcon(14);
        }
        if (index != 4 && redPray)
        {
            redPray = false;
            _systemManager.callInactiveIcon(4);
            if (inColdWave && !isColdWaveIcons)
            {
                isColdWaveIcons = true;
                _systemManager.callActiveIcon(58);
            }
        }
        if (index != 15 && pinkSpirit)
        {
            pinkSpirit = false;
            _systemManager.callInactiveIcon(15);
        }
        if (index != 5 && pinkPray)
        {
            pinkPray = false;
            _systemManager.callInactiveIcon(5);
        }
        if (index != 16 && yellowSpirit)
        {
            yellowSpirit = false;
            _systemManager.callInactiveIcon(16);
        }
        if (index != 6 && yellowPray)
        {
            yellowPray = false;
            _systemManager.callInactiveIcon(6);
        }
        if (index != 17 && skySpirit)
        {
            skySpirit = false;
            _systemManager.callInactiveIcon(17);
        }
        if (index != 7 && skyPray)
        {
            skyPray = false;
            _systemManager.callInactiveIcon(7);
        }
        // whiteSpirit = false;
        // whitePray = false;
        // orangePray = false;
        // bluePray = false;
        // redSpirit = false;
        // redPray = false;
        // pinkSpirit = false;
        // pinkPray = false;
        // yellowSpirit = false;
        // yellowPray = false;
        // skySpirit = false;
        // skyPray = false;
    }
}

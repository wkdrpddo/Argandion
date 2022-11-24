using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EventCodeToIndex;
using FoodCodeToEventCode;

public class EventManager : MonoBehaviour
{
    // 이벤트 패널
    public EventPanel _eventPanel;

    public BuffManager _buffManager;
    public UIManager _uiManager;
    // public GameObject _event;

    // 테스트
    // public bool _eat;
    // private float time;
    // public int _eatItemCode;

    public void setting()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        _eventPanel = _uiManager._eventpanel.GetComponent<EventPanel>();
        // setDescriptObj();
        // callAllPanel();
    }

    // 테스트 코드
    // void Update()
    // {
    //     time += Time.deltaTime;
    //     if (_eat)
    //     {
    //         Debug.Log("=====================================");
    //         Debug.Log("아이템 코드: " + _eatItemCode);
    //         startFoodBuff(_eatItemCode);
    //         _eat = false;
    //     }
    // }

    // 안 씀
    // // 음식 효과 발생 => 로드할 때는 안 부름
    // private void startFoodBuff(int itemCode)
    // {
    //     Debug.Log("음식 먹기 콜 시간: " + time);
    //     /* 음식 버프 콜 조건
    //         => 우유, 빵, 샌드위치 중복 허용 X
    //     */
    //     if (itemCode == 110 || itemCode == 120 || itemCode == 131)
    //     { // 우유, 빵, 샌드위치
    //         if (!_foodBuff[itemCode - 100])
    //         { // 이미 버프 실행 중이면
    //             Debug.Log("음식 안 먹은 거랑 같음");
    //             return;
    //         }
    //     }
    //     _food.UseFood(itemCode);
    //     if (itemCode == 120 || itemCode == 110 || itemCode == 123 || itemCode == 125 || itemCode == 126 || itemCode == 131 || itemCode == 128 || itemCode == 129 || itemCode == 130)
    //     {
    //         // 패널 콜
    //         createFoodIcon(itemCode);
    //     }

    //     foodBuffChecking();

    // }

    // // 테스트 코드
    // private void foodBuffChecking()
    // {
    //     Debug.Log("버프 체킹");
    //     for (int i = 0; i < 7; i++)
    //     {
    //         Debug.Log("i: " + i + ", 버프:" + _foodBuff[i]);
    //     }
    // }

    // // 이미 버프 실행 중인 경우에는 call X
    // private void createFoodIcon(int itemCode)
    // {
    //     int eventCode = FoodToEventCodeArray.arr[itemCode];
    //     // 패널 call
    //     _eventPanel.activeIcon(eventCode);
    //     // 상태 변경
    //     if ((eventCode != 100 && _foodBuff[0]) || (eventCode != 101 && _foodBuff[1]) || (eventCode != 105 && _foodBuff[5]))
    //     {// 직전 빵, 우유, 샌드위치
    //         destroyFoodIcon(itemCode);
    //     }
    //     _foodBuff[eventCode - 100] = true;

    // }

    // public void destroyFoodIcon(int itemCode)
    // {
    //     int eventCode = FoodToEventCodeArray.arr[itemCode];
    //     // 패널 call
    //     _eventPanel.inactiveIcon(eventCode);
    //     // 상태 변경
    //     _foodBuff[eventCode - 100] = false;
    // }


}

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

    // 음식 
    public Food _food;

    public BuffManager _buffManager;
    public UIManager _uiManager;
    public GameObject _event;

    // 테스트
    public bool _test;
    public bool _test1;
    public bool _test2;


    // void Start()
    // {
    //     _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    //     _buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
    //     _event = _uiManager._eventAnnounce;
    //     Debug.Log(_event);
    //     Debug.Log(_uiManager._eventpanel);
    //     _eventPanel = _uiManager._eventpanel.GetComponent<EventPanel>();
    //     _food = gameObject.GetComponent<Food>();
    //     setDescriptObj();
    //     // callAllPanel();
    // }

    public void setting()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        _eventPanel = _uiManager._eventpanel.GetComponent<EventPanel>();
        _food = gameObject.GetComponent<Food>();
        // setDescriptObj();
        // callAllPanel();
    }

    void Update()
    {
        if (_test)
        {
            // startFoodBuff(128);
            createPanel(1);
            createPanel(50);
            createPanel(100);
            _test = false;
        }
        if (_test1)
        {
            destroyPanel(1);
            _test1 = false;

        }
        if (_test2)
        {
            destroyPanel(50);
            _test2 = false;
        }
    }

    // 테스트용 코드
    private void callAllPanel()
    {


    }



    // 음식 효과 발생 => 로드할 때는 안 부름
    public void startFoodBuff(int itemCode)
    {
        // _food.UseFood(itemCode);
        // 버프 체크(빵, 우유, 샌드위치) 

        // 패널 콜
        // createPanel(FoodToEventCodeArray.arr[itemCode]);
    }
    // 음식 효과 해제 => panel 콜만
    public void endFoodBuff(int itemCode)
    {
        // destroyPanel(FoodToEventCodeArray.arr[itemCode]);
    }

    public void createPanel(int eventCode)
    {
        // Debug.Log("크리에이트 콜");
        // DescriptObj descObj = iconData[EventIndexArray.arr[eventCode]];
        // Debug.Log(descObj.Name);
        // _eventPanel.setEventIcon(eventCode, descObj);
    }
    public void destroyPanel(int eventCode)
    {
        // _eventPanel.delEventIcon(eventCode);
        // _eventPanel.deActiveEventIcon(eventCode);
    }

}

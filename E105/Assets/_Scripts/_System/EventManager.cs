using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EventCodeToIndex;
using FoodCodeToEventCode;

[System.Serializable]
public class DescriptObj
{
    public int Id;
    public string Name;
    public string Description;
}

public class EventManager : MonoBehaviour
{
    // 이벤트 패널
    public EventPanel _eventPanel;
    private DescriptObj[] iconData;

    // 음식 
    public Food _food;

    public BuffManager _buffManager;
    public UIManager _uiManager;
    public GameObject _event;

    // 테스트
    public bool _test;


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
        setDescriptObj();
        // callAllPanel();
    }

    void Update()
    {
        if (_test)
        {
            startFoodBuff(128);
            _test = false;
        }
    }

    // 테스트용 코드
    private void callAllPanel()
    {
        createPanelTest(1);
        createPanelTest(2);
        createPanelTest(3);
        createPanelTest(4);
        createPanelTest(5);
        createPanelTest(6);
        createPanelTest(7);
        createPanelTest(11);
        createPanelTest(12);
        createPanelTest(13);
        createPanelTest(14);
        createPanelTest(15);
        createPanelTest(16);
        createPanelTest(17);
        createPanelTest(50);
        createPanelTest(51);
        createPanelTest(52);
        createPanelTest(53);
        createPanelTest(54);
        createPanelTest(55);
        createPanelTest(56);
        createPanelTest(100);
        createPanelTest(101);
        createPanelTest(102);
        createPanelTest(103);
        createPanelTest(104);
        createPanelTest(105);
        createPanelTest(106);

    }

    // Json 부르는 코드 (배열로)
    private void setDescriptObj()
    {
        string jsonInputString = Application.dataPath + "/Data/Json/EventPanelDescription.json";
        string jsonString = File.ReadAllText(jsonInputString);
        iconData = JsonHelper.FromJson<DescriptObj>(jsonString);
    }

    // 음식 효과 발생 => 로드할 때는 안 부름
    public void startFoodBuff(int itemCode)
    {
        _food.UseFood(itemCode);
        // 버프 체크(빵, 우유, 샌드위치) 
        if (!_buffManager.eatSandwich)
        {

        }
        // 패널 콜
        createPanel(FoodToEventCodeArray.arr[itemCode]);
    }
    // 음식 효과 해제 => panel 콜만
    public void endFoodBuff(int itemCode)
    {
        destroyPanel(FoodToEventCodeArray.arr[itemCode]);
    }

    public void createPanel(int eventCode)
    {
        // Debug.Log("크리에이트 콜");
        DescriptObj descObj = iconData[EventIndexArray.arr[eventCode]];
        // Debug.Log(descObj.Name);
        _eventPanel.setEventIcon(eventCode, descObj);
    }
    public void destroyPanel(int eventCode)
    {
        _eventPanel.delEventIcon(eventCode);
    }

}

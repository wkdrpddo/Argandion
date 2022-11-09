using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EventCodeToIndex;

[System.Serializable]
public class DescriptObj
{
    public int Id;
    public string Name;
    public string Description;
}

public class EventManager : MonoBehaviour
{
    public EventPanel _eventPanel;
    private DescriptObj[] iconData;

    void Start()
    {
        _eventPanel = GameObject.Find("EventPanel").GetComponent<EventPanel>();
        setDescriptObj();
        callAllPanel();
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
        // Debug.Log(jsonInputString);

        string jsonString = File.ReadAllText(jsonInputString);

        // Debug.Log(jsonString);
        iconData = JsonHelper.FromJson<DescriptObj>(jsonString);

        Debug.Log(iconData.Length);
        // Debug.Log(iconData);

        // for (int i = 0; i < iconData.Length; i++)
        // {
        //     // Debug.Log(iconData[i]);
        //     descriptObj descObj = iconData[i];
        //     // Debug.Log(iconData[i].Name);
        // }
    }

    public void createPanelTest(int imgNum)
    {
        Debug.Log("크리에이트 콜");
        DescriptObj descObj = iconData[EventIndexArray.arr[imgNum]];
        Debug.Log(descObj.Name);
        _eventPanel.setEventIcon(imgNum, descObj);
        // if (descObj != null)
        // {
        //     _eventPanel.setEventIcon(imgNum, descObj);
        // }

    }

    // Id를 배열 인덱스로 만들어야 하나? => 지금 배열 크기가 안 커서 반복해도 될 것 같긴 한데
    public DescriptObj findByID(int imgNum)
    {
        return iconData[EventIndexArray.arr[imgNum]];
    }


}

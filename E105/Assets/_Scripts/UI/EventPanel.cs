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
// 이벤트 관련 코드
public class EventPanel : MonoBehaviour
{
    public GameObject _eventIconPrefab; // 미리 끌어서 넣기
    public GameObject _otherPanel; // 미리 끌어서 넣기
    public GameObject _foodPanel; // 미리 끌어서 넣기

    // public bool _onSpiritBuff;

    private GameObject[] _iconObject;
    private DescriptObj[] _descData;

    // 아이콘 활성화 개수인데 이건 스크립트 위치 바뀔 수 있음
    private byte _foodIconACnt;
    private byte _otherIconACnt;

    private Dictionary<int, Sprite> Dic = new Dictionary<int, Sprite>(); // 아이콘 이미지

    // 테스트용 코드 시작 =======================================================================
    // public bool _setTest;
    // public bool _delTest;
    // public int _testNum;




    // 테스트용 업데이트
    // void Update()
    // {
    //     if (_setTest)
    //     {
    //         // activeIcon(3);
    //         _setTest = false;
    //     }
    //     if (_delTest)
    //     {
    //         // inactiveIcon(1);
    //         // inactiveIcon(56);
    //         // inactiveIcon(50);
    //         _delTest = false;
    //     }
    // }

    // 테스트 코드
    // public void arrayLog()
    // {
    //     for (int i = 0; i < _iconObject.Length; i++)
    //     {
    //         Debug.Log(_iconObject[i]);
    //     }
    //     activeIcon(1);
    //     activeIcon(100);
    //     activeIcon(56);
    //     activeIcon(50);

    //     // activeIcon(3);
    //     // _panelObject[EventIndexArray.arr[0]].SetActive(true);
    //     // _panelObject[EventIndexArray.arr[100]].SetActive(true);
    // }

    // 테스트용 코드 끝 =======================================================================

    // 패널 생성하기
    public void setting()
    {
        Debug.Log("이벤트 패널 세팅 콜");
        setDescriptObj();
        createAllPanel();
    }

    // 아이콘 가져오기
    private Sprite getIconImg(int eventCode)
    {
        if (Dic.ContainsKey(eventCode))
        {
            return Dic[eventCode];
        }
        Sprite img = Resources.Load<Sprite>("EventIcon/" + eventCode);
        Dic.Add(eventCode, img);
        return img;
    }

    // Json 부르는 코드 (배열로)
    private void setDescriptObj()
    {
        string jsonInputString = Application.dataPath + "/Data/Json/EventPanelDescription.json";
        string jsonString = File.ReadAllText(jsonInputString);
        _descData = JsonHelper.FromJson<DescriptObj>(jsonString);
    }

    // 처음 패널 전부 생성
    public void createAllPanel()
    {
        // 패널 배열 생성
        _iconObject = new GameObject[_descData.Length];
        // 패널 배열에 패널 만들어 넣기
        for (int i = 0; i < _iconObject.Length; i++)
        {
            int eventCode = _descData[i].Id;
            // 아이콘 생성
            GameObject icon;
            if (eventCode >= 100)
            { // 음식
                icon = Instantiate(_eventIconPrefab, _foodPanel.transform);
            }
            else
            { // 날씨, 정령
                icon = Instantiate(_eventIconPrefab, _otherPanel.transform);
            }
            icon.name = eventCode.ToString() + "Icon";
            // 아이콘 이미지 변경
            Sprite iconImg = getIconImg(eventCode);
            icon.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = iconImg;
            // 아이콘 번호 부여
            icon.GetComponent<EventIcon>()._iconNum = eventCode;
            // 아이콘 hover 정보 부여
            icon.GetComponent<EventIcon>()._descriptObj = _descData[i];
            // 비활성화
            icon.SetActive(false);
            // 배열에 panel 넣기
            _iconObject[i] = icon;
        }
    }

    public void activeIcon(int eventCode)
    {
        GameObject icon = _iconObject[EventIndexArray.arr[eventCode]];
        icon.SetActive(true);
        if (eventCode >= 100)
        { // 음식
            _foodIconACnt++;
            icon.transform.SetAsLastSibling(); // 맨 뒤로
        }
        else if (eventCode >= 50)
        { // 날씨
            _otherIconACnt++;
            icon.transform.SetAsLastSibling(); // 맨 뒤로
        }
        else
        { // 정령
            _otherIconACnt++;
            icon.transform.SetAsFirstSibling(); // 맨 앞으로
        }
        activePanel();
    }

    public void inactiveIcon(int eventCode)
    {
        _iconObject[EventIndexArray.arr[eventCode]].SetActive(false);
        if (eventCode >= 100)
        { // 음식
            _foodIconACnt--;
        }
        else
        { // 날씨, 정령
            _otherIconACnt--;
        }
        activePanel();
    }

    // panel 활성화
    private void activePanel()
    {
        if (_otherIconACnt == 0)
        {
            _otherPanel.SetActive(false);
        }
        else
        {
            _otherPanel.SetActive(true);
        }
        if (_foodIconACnt == 0)
        {
            _foodPanel.SetActive(false);
        }
        else
        {
            _foodPanel.SetActive(true);
        }

    }

}
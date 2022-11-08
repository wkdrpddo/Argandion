using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 이벤트 관련 코드
public class EventPanel : MonoBehaviour
{
    // public LinkedList<GameObject> eventList = new LinkedList<GameObject>();
    // public GameObject[] eventList;  // 현재 이벤트 리스트
    // private int eventCnt = 0;        // 현재 이벤트 갯수
    // public GameObject questButton;      // 퀘스트 발생 아이콘
    // // public GameObject rainIcon;
    // private Quaternion rotateZero = Quaternion.Euler(new Vector3(0, 0, 0));     // 회전값 기본값 세팅

    public GameObject _eventIconPrefab; // 미리 끌어서 넣기
    public GameObject _otherPanel; // 미리 끌어서 넣기
    public GameObject _foodPanel; // 미리 끌어서 넣기
    // public GameObject _spiritPanel; // 미리 끌어서 넣기
    // public GameObject _foodPanel; // 미리 끌어서 넣기

    // public EventIcon _eventIcon;

    private bool _onSpiritBuff;

    private Dictionary<int, Sprite> Dic = new Dictionary<int, Sprite>(); // 아이콘 이미지


    // 테스트용 코드
    public bool _setTest;
    public bool _delTest;
    void Start()
    {
        // setEventIcon(1);
        // setEventIcon(2);
        // setEventIcon(3);
        // setEventIcon(4);
        // setEventIcon(5);
        // setEventIcon(6);
        // setEventIcon(7);
        // setEventIcon(50);
        // setEventIcon(51);
        // setEventIcon(52);
        // setEventIcon(53);
        // setEventIcon(54);
        // setEventIcon(55);
        // setEventIcon(56);
        setEventIcon(100);
        setEventIcon(101);
        setEventIcon(102);
        setEventIcon(103);
        setEventIcon(104);
        setEventIcon(105);
        setEventIcon(106);
        // setEventIcon(1);
        // setEventIcon(2);
        // setEventIcon(3);
        // setEventIcon(4);
        // setEventIcon(5);
        // setEventIcon(6);
        // setEventIcon(7);

        // setEventRain();
        // setEventRain();
        // setEventRain();
        // setEventRain();
        // setEventRain();
        // setEventRain();
        // setEventRain();
        // setEventRain();
        // setEventRain();
        // setQuest();
    }

    // Update is called once per frame
    void Update()
    {
        if (_setTest)
        {
            setEventIcon(1);
            _setTest = false;
        }
        if (_delTest)
        {
            delEventIcon(1);
            _delTest = false;
        }
    }

    // 비 - EventRain
    // 화창 - EventClear
    // 폭염 - EventHeatWave
    // 폭설 - EventHeavySnow
    // 태풍 - EventHurricane
    // 풍년 - EventGookcrops
    // 완벽 - EventClear

    // panel 활성화
    private void activePanel()
    {
        int fCnt = _foodPanel.transform.childCount;
        // int wCnt = _weatherPanel.transform.childCount;
        // int wCnt = _weatherPanel.transform.childCount;
        int oCnt = _otherPanel.transform.childCount;
        // Debug.Log("oCnt: " + oCnt);
        // Debug.Log("fCnt: " + fCnt);
        if (oCnt == 0)
        {
            _otherPanel.SetActive(false);
        }
        else
        {
            _otherPanel.SetActive(true);
        }
        if (fCnt == 0)
        {
            _foodPanel.SetActive(false);
        }
        else
        {
            _foodPanel.SetActive(true);
        }

    }

    // 아이콘 생성 및 이미지 변경
    public void setEventIcon(int imgNum)
    {
        // Debug.Log("셋 이벤트 콜");
        // 아이콘 생성
        GameObject icon;
        if (imgNum >= 100)
        { // 음식
            // Debug.Log("음식");
            icon = Instantiate(_eventIconPrefab, _foodPanel.transform);
            // icon = Instantiate(_eventIconPrefab, _foodPanel.transform);
        }
        else if (imgNum >= 50)
        { // 날씨
            // Debug.Log("날씨");
            icon = Instantiate(_eventIconPrefab, _otherPanel.transform);
        }
        else
        { // 정령
            if (!_onSpiritBuff)
            {
                // Debug.Log("정령");
                icon = Instantiate(_eventIconPrefab, _otherPanel.transform);
                icon.transform.SetAsFirstSibling(); // 맨 앞으로
                _onSpiritBuff = true;
            }
            else
            {
                return;
            }
        }
        // Debug.Log(icon);
        // 아이콘 이름 변경
        icon.name = imgNum.ToString() + "Icon";
        // Debug.Log(icon.name);
        // 아이콘 이미지 변경
        Sprite iconImg = getIconImg(imgNum);
        icon.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = iconImg;
        // 아이콘 번호 부여
        icon.GetComponent<EventIcon>()._iconNum = imgNum;
        // Debug.Log(icon.GetComponent<EventIcon>()._iconNum);
        // panel 부분 활성화/비활성화
        activePanel();

    }

    public Sprite getIconImg(int imgNum)
    {
        // Debug.Log("겟 아이콘 이미지");
        if (Dic.ContainsKey(imgNum))
        {
            return Dic[imgNum];
        }
        Sprite img = Resources.Load<Sprite>("EventIcon/" + imgNum);
        Dic.Add(imgNum, img);
        return img;
    }

    // 이벤트 오브젝트 삭제 함수
    public void delEventIcon(int imgNum)
    {
        GameObject delObj = GameObject.Find(imgNum.ToString() + "Icon"); // 삭제할 아이콘
        // Debug.Log(delObj);
        Destroy(delObj); // 삭제

        if (delObj.transform.parent.gameObject.transform.childCount == 1)
        { // 라인 삭제
            delObj.transform.parent.gameObject.SetActive(false);
        }
        // Debug.Log(_otherPanel.transform.childCount);

        if (imgNum >= 1 && imgNum <= 7)
        { // 정령이면 버프 체크
            _onSpiritBuff = false;
        }
        // Debug.Log(_onSpiritBuff);

        // if (imgNum >= 100)
        // { // 음식
        //     icon = Instantiate(_eventIcon, _foodPanel.transform);
        // }
        // else if (imgNum >= 50)
        // { // 날씨
        //     icon = Instantiate(_eventIcon, _weatherPanel.transform);
        // }
        // else if (imgNum >= 1)
        // { // 정령
        //     icon = Instantiate(_eventIcon, _spiritPanel.transform);
        // }



        // // int removeIdx = child.GetComponent<UIEventIdx>().idx - 1;

        // Destroy(child);

        // // 이벤트 리스트 shifting 작업
        // for (int i = removeIdx; i < eventCnt - 1; i++)
        // {
        //     Vector3 posit = new Vector3(i * 30 + 10, -10, 0);

        //     GameObject questBtnInit = eventList[i + 1];

        //     RectTransform questBtnRect = questBtnInit.GetComponent<RectTransform>();
        //     questBtnRect.SetLocalPositionAndRotation(posit, rotateZero);

        //     questBtnInit.GetComponent<UIEventIdx>().idx = i + 1;

        //     eventList[i] = questBtnInit;
        // }
        // eventCnt--;
    }

    // public void setEventRain()
    // {
    //     GameObject rainInit = Instantiate(rainIcon, this.transform);
    // }

    // 퀘스트 발생 이벤트
    // public void setQuest()
    // {
    //     Vector3 posit = new Vector3(eventCnt * 30 + 10, -10, 0);

    //     GameObject questBtnInit = Instantiate(questButton, this.transform);     // 프리펩 객체 생성

    //     // 게임 오브젝트의 Rect값 추출 및 LocalPosition setting
    //     RectTransform questBtnRect = questBtnInit.GetComponent<RectTransform>();
    //     questBtnRect.SetLocalPositionAndRotation(posit, rotateZero);

    //     // 이벤트 Cnt 증가 및 게임 오브젝트의 idx 할당
    //     eventCnt++;
    //     questBtnInit.GetComponent<UIEventIdx>().idx = eventCnt;

    //     // 현재 리스트를 갱신
    //     copyList();

    //     // 새로운 게임 오브젝트 리스트에 추가
    //     eventList[eventCnt - 1] = questBtnInit;
    // }







    // // 이벤트 오브젝트 삭제 함수 : 클릭 시, 이벤트 시간 종료 시 호출
    // public void deleteObject(GameObject child)
    // {
    //     // int removeIdx = child.GetComponent<UIEventIdx>().idx - 1;

    //     Destroy(child);

    //     // 이벤트 리스트 shifting 작업
    //     for (int i = removeIdx; i < eventCnt - 1; i++)
    //     {
    //         Vector3 posit = new Vector3(i * 30 + 10, -10, 0);

    //         GameObject questBtnInit = eventList[i + 1];

    //         RectTransform questBtnRect = questBtnInit.GetComponent<RectTransform>();
    //         questBtnRect.SetLocalPositionAndRotation(posit, rotateZero);

    //         questBtnInit.GetComponent<UIEventIdx>().idx = i + 1;

    //         eventList[i] = questBtnInit;
    //     }
    //     eventCnt--;
    // }

    // 이벤트 리스트 갱신을 위한 복사 함수
    // private void copyList()
    // {
    //     GameObject[] newList = new GameObject[eventCnt];
    //     for (int i = 0; i < eventCnt - 1; i++)
    //     {
    //         newList[i] = eventList[i];
    //     }

    //     eventList = new GameObject[eventCnt];
    //     newList.CopyTo(eventList, 0);
    // }
}

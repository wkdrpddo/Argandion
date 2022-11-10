using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 이벤트 관련 코드
public class EventPanel : MonoBehaviour
{
    public GameObject _eventIconPrefab; // 미리 끌어서 넣기
    public GameObject _otherPanel; // 미리 끌어서 넣기
    public GameObject _foodPanel; // 미리 끌어서 넣기

    // public bool _onSpiritBuff;

    private Dictionary<int, Sprite> Dic = new Dictionary<int, Sprite>(); // 아이콘 이미지

    // 테스트용 코드 시작 =======================================================================
    public bool _setTest;
    public bool _delTest;
    public int _testNum;

    void Start()
    {
        // setEventIcon(1);
        // setEventIcon(50);
        // setEventIcon(100);
        // setEventIcon(101);
        // setEventIcon(102);
    }

    // 테스트용 업데이트
    void Update()
    {
        if (_setTest)
        {
            // setEventIcon(_testNum);
            _setTest = false;
        }
        if (_delTest)
        {
            delEventIcon(_testNum);
            _delTest = false;
        }
    }

    // 테스트용 코드 끝 =======================================================================

    // 아이콘 가져오기
    public Sprite getIconImg(int eventCode)
    {
        if (Dic.ContainsKey(eventCode))
        {
            return Dic[eventCode];
        }
        Sprite img = Resources.Load<Sprite>("EventIcon/" + eventCode);
        Dic.Add(eventCode, img);
        return img;
    }

    // panel 활성화
    private void activePanel()
    {
        int fCnt = _foodPanel.transform.childCount;
        int oCnt = _otherPanel.transform.childCount;
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
    // 정령 버프 중이면 나중에 들어온 걸로 교체
    // 계절이랑 음식은 시간 안 띄우니까 나중에 들어온 거 무시 => delete 함수 콜할 때 시간 리셋만 해주면 될 듯
    public void setEventIcon(int eventCode, DescriptObj descObj)
    {
        // 아이콘 생성
        GameObject icon;
        if (eventCode >= 100)
        { // 음식
            if (GameObject.Find(eventCode.ToString() + "Icon") == null)
            {
                icon = Instantiate(_eventIconPrefab, _foodPanel.transform);
            }
            else
            {
                return;
            }
        }
        else if (eventCode >= 50)
        { // 날씨
            if (GameObject.Find(eventCode.ToString() + "Icon") == null)
            {
                icon = Instantiate(_eventIconPrefab, _otherPanel.transform);
            }
            else
            {
                return;
            }
        }
        else
        { // 정령
            // if (_onSpiritBuff)
            // { // 버프 실행 중이면
            //     Destroy(_otherPanel.transform.GetChild(0).gameObject);
            // }
            icon = Instantiate(_eventIconPrefab, _otherPanel.transform);
            icon.transform.SetAsFirstSibling(); // 맨 앞으로
            // _onSpiritBuff = true;
        }
        // 아이콘 이름 변경
        icon.name = eventCode.ToString() + "Icon";
        // 아이콘 이미지 변경
        Sprite iconImg = getIconImg(eventCode);
        icon.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = iconImg;
        // 아이콘 번호 부여
        icon.GetComponent<EventIcon>()._iconNum = eventCode;
        // 아이콘 hover 정보 부여
        icon.GetComponent<EventIcon>()._descriptObj = descObj;
        // panel 부분 활성화/비활성화
        activePanel();
    }

    // 이벤트 오브젝트 삭제 함수
    public void delEventIcon(int eventCode)
    {
        GameObject delObj = GameObject.Find(eventCode.ToString() + "Icon"); // 삭제할 아이콘
        if (delObj == null)
        {
            return;
        }
        else
        {
            Destroy(delObj); // 삭제

            if (delObj.transform.parent.gameObject.transform.childCount == 1)
            { // 라인 삭제
                delObj.transform.parent.gameObject.SetActive(false);
            }
        }


        // if (imgNum >= 1 && imgNum <= 7)
        // { // 정령이면 버프 체크
        //     _onSpiritBuff = false;
        // }
    }
}

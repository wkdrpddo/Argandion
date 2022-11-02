using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    // public LinkedList<GameObject> eventList = new LinkedList<GameObject>();
    public GameObject[] eventList;  // 현재 이벤트 리스트
    private int eventCnt = 0;        // 현재 이벤트 갯수
    public GameObject questButton;      // 퀘스트 발생 아이콘
    public GameObject rainIcon;
    private Quaternion rotateZero = Quaternion.Euler(new Vector3(0, 0, 0));     // 회전값 기본값 세팅

    // 퀘스트 발생 이벤트
    public void setQuest()
    {
        Vector3 posit = new Vector3(eventCnt * 30 + 10, -10, 0);

        GameObject questBtnInit = Instantiate(questButton, this.transform);     // 프리펩 객체 생성

        // 게임 오브젝트의 Rect값 추출 및 LocalPosition setting
        RectTransform questBtnRect = questBtnInit.GetComponent<RectTransform>();
        questBtnRect.SetLocalPositionAndRotation(posit, rotateZero);

        // 이벤트 Cnt 증가 및 게임 오브젝트의 idx 할당
        eventCnt++;
        questBtnInit.GetComponent<UIEventIdx>().idx = eventCnt;

        // 현재 리스트를 갱신
        copyList();

        // 새로운 게임 오브젝트 리스트에 추가
        eventList[eventCnt - 1] = questBtnInit;
    }

    public void setEventRain()
    {
        Vector3 posit = new Vector3(eventCnt * 30 + 10, -10, 0);

        GameObject rainInit = Instantiate(rainIcon, this.transform);     // 프리펩 객체 생성

        // 게임 오브젝트의 Rect값 추출 및 LocalPosition setting
        RectTransform rainRect = rainInit.GetComponent<RectTransform>();
        rainRect.SetLocalPositionAndRotation(posit, rotateZero);

        // 이벤트 Cnt 증가 및 게임 오브젝트의 idx 할당
        eventCnt++;
        rainInit.GetComponent<UIEventIdx>().idx = eventCnt;

        // 현재 리스트를 갱신
        copyList();

        // 새로운 게임 오브젝트 리스트에 추가
        eventList[eventCnt - 1] = rainInit;
    }

    // Start is called before the first frame update
    void Start()
    {
        setEventRain();
        setEventRain();
        setEventRain();
        setQuest();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 이벤트 오브젝트 삭제 함수 : 클릭 시, 이벤트 시간 종료 시 호출
    public void deleteObject(GameObject child)
    {
        int removeIdx = child.GetComponent<UIEventIdx>().idx - 1;

        Destroy(child);

        // 이벤트 리스트 shifting 작업
        for (int i = removeIdx; i < eventCnt - 1; i++)
        {
            Vector3 posit = new Vector3(i * 30 + 10, -10, 0);

            GameObject questBtnInit = eventList[i + 1];

            RectTransform questBtnRect = questBtnInit.GetComponent<RectTransform>();
            questBtnRect.SetLocalPositionAndRotation(posit, rotateZero);

            questBtnInit.GetComponent<UIEventIdx>().idx = i + 1;

            eventList[i] = questBtnInit;
        }
        eventCnt--;
    }

    // 이벤트 리스트 갱신을 위한 복사 함수
    private void copyList()
    {
        GameObject[] newList = new GameObject[eventCnt];
        for (int i = 0; i < eventCnt - 1; i++)
        {
            newList[i] = eventList[i];
        }

        eventList = new GameObject[eventCnt];
        newList.CopyTo(eventList, 0);
    }
}

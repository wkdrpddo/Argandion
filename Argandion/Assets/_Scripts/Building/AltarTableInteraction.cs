using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarTableInteraction : MonoBehaviour
{
    private UIManager _uiManager;
    // public int _sectorNum;
    private PrayBuff _prayBuff;
    // private BuffManager _buffManager;

    void Start()
    {
        _prayBuff = GameObject.Find("BuffManager").GetComponent<PrayBuff>();
        // _buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void Interaction(int itemCode, int quickslotNum)
    {
        Debug.Log("인터렉션 호출");
        int prePray = _prayBuff.flowerIdx; // 직전 버프 번호
        Debug.Log("prePray "+prePray);
        // int prayDay = _prayBuff.prayDay; // 직전 버프 횟수
        // bool isActived = _buffManager._isPrayBuffActived; // 제사 버프 진행 중
        Debug.Log("todayPray " + _prayBuff.todayPray);
        // 손에 뭐 들고 있는지 확인해서
        // 조건에 맞게 UI 호출
        if(_prayBuff.todayPray){
            Debug.Log("들어옴");
            _uiManager.OnResultNotificationPanel("오늘은 이미 꽃을 바쳤습니다.");
            return;
        }
        _uiManager.prayToAltar(prePray, itemCode, quickslotNum);
        
        // switch (itemCode)
        // {
        //     case 50: // 분홍
        //         _uiManager.prayToAltar(prePray, itemCode);
        //         break;
        //     case 51: // 주황
        //         break;
        //     case 52: // 빨강
        //         break;
        //     case 53: // 하늘
        //         break;
        //     case 54: // 파랑
        //         break;
        //     case 55: // 노랑
        //         break;
        //     case 56: // 흰색
        //         break;
        // }
    }
}

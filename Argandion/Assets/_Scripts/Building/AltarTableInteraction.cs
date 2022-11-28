using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarTableInteraction : MonoBehaviour
{
    private UIManager _uiManager;
    private PrayBuff _prayBuff;

    void Start()
    {
        _prayBuff = GameObject.Find("BuffManager").GetComponent<PrayBuff>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void Interaction(int itemCode, int quickslotNum)
    {
        int prePray = _prayBuff.flowerIdx; // 직전 버프 번호
        if(_prayBuff.todayPray){
            _uiManager.OnResultNotificationPanel("오늘은 이미 꽃을 바쳤습니다.");
            return;
        }
        _uiManager.prayToAltar(prePray, itemCode, quickslotNum);
    }
}

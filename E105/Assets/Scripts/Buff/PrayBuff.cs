using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayBuff : MonoBehaviour
{
    public SystemManager systemManager;
    public BuffManager buffManager;
    public Inventory _theInventory;
    
    public int tempDay;
    public bool todayPray = false;
    public int flowerIdx = 0;
    public int prayDay = 0;

    void Start() {
        tempDay = systemManager._day;
    }

    void Update() {
        if (tempDay != systemManager._day) {
            NewDay();
            tempDay = systemManager._day;
        }
    }

    public void Pray(ItemObject item) {
        if (!todayPray) {
            if ((flowerIdx == 0 || flowerIdx == item.ItemCode)) {
                flowerIdx = item.ItemCode;
                todayPray = true;
                if (prayDay < 2) {
                    _theInventory.StoreItem(0,-1);
                    prayDay +=1;
                } else {
                    if(_theInventory.slots[0].itemCount >= 2){
                        _theInventory.StoreItem(0, -2);
                        prayDay +=1;
                    } else {
                        Debug.Log("꽃이 부족합니다.");
                    }
                }
                if (prayDay == 3) {
                    Reincarnation();
                }
            } else {
                Debug.Log("다른 제사버프가 활성화중입니다.");
            }
        } else {
            Debug.Log("오늘은 이미 기도를 드렸습니다.");
        }
    }

    public void NewDay() {
        if ( !todayPray ) {
            prayDay = 0;
            flowerIdx = 0;
        } else {
            todayPray = false;
        }
    }

    public void Reincarnation() {
        Debug.Log("버프 시작");
    }

    public void WhitePray(){
        buffManager.whitePray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8) {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 하얀색제단버프");
    }

    public void OrangePray(){
        buffManager.orangePray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8) {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 주황색제단버프");
    }
    
    public void BluePray(){
        buffManager.bluePray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8) {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 파랑색제단버프");
    }
    
    public void RedPray(){
        buffManager.redPray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8) {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 빨강색제단버프");
    }
    
    public void PinkPray(){
        buffManager.pinkPray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8) {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 분홍색제단버프");
    }
    
    public void YellowPray(){
        buffManager.yellowPray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8) {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 노랑색제단버프");
    }
    
    public void SkyPray(){
        buffManager.skyPray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8) {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 하늘색제단버프");
    }
    
}
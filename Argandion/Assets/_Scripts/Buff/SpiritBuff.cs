using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritBuff : MonoBehaviour
{   
    public SystemManager systemManager;
    public GameObject _buffManagerObject;
    private BuffManager buffManager;
    public GameObject[] buildings;

    private void Start() {
        _buffManagerObject = GameObject.Find("BuffManager");
        buffManager = _buffManagerObject.GetComponent<BuffManager>();
    }

    public void Spirit(ItemObject item) {
        if (buffManager._isFlowerBuffActived == false) {
            if (item.ItemCode == 50) {
                PinkSpirit();
                systemManager.callActiveIcon(15);
            } else if (item.ItemCode == 51) {
                OrangeSpirit();
                systemManager.callActiveIcon(12);
            } else if (item.ItemCode == 52) {
                RedSpirit();
                systemManager.callActiveIcon(14);
            } else if (item.ItemCode == 53) {
                SkySpirit();
                systemManager.callActiveIcon(17);
            } else if (item.ItemCode == 54) {
                BlueSpirit();
                systemManager.callActiveIcon(13);
            } else if (item.ItemCode == 55) {
                YellowSpirit();
                systemManager.callActiveIcon(16);
            } else if (item.ItemCode == 56) {
                WhiteSpirit();
                systemManager.callActiveIcon(11);
            } else {
                // Debug.Log("꽃이 아님");
            }
        } else {
            // Debug.Log("다른 꽃 버프가 활성 중");
        }
    }

    public void WhiteSpirit(){
        buffManager._isFlowerBuffActived = true;
        buffManager.whiteSpirit = true;
        buffManager._flowerBuffTargetDay = systemManager._day + 3;
        buffManager._flowerBuffTargetMonth = systemManager._month;
        if (buffManager._flowerBuffTargetDay > 28) {
            buffManager._flowerBuffTargetDay -= 28;
            buffManager._flowerBuffTargetMonth += + 1;
            if (buffManager._flowerBuffTargetMonth > 8) {
                buffManager._flowerBuffTargetMonth -= 8;
            }
        }
        // Debug.Log("나는 하얀색정령버프");
    }

    public void OrangeSpirit(){
        buffManager._isFlowerBuffActived = true;
        buffManager.orangeSpirit = true;
        buffManager._flowerBuffTargetDay = systemManager._day + 1;
        buffManager._flowerBuffTargetMonth = systemManager._month;
        if (buffManager._flowerBuffTargetDay > 28) {
            buffManager._flowerBuffTargetDay -= 28;
            buffManager._flowerBuffTargetMonth += + 1;
            if (buffManager._flowerBuffTargetMonth > 8) {
                buffManager._flowerBuffTargetMonth -= 8;
            }
        }
        // Debug.Log("나는 주황색정령버프");
    }
    
    public void BlueSpirit(){
        buffManager._isFlowerBuffActived = true;
        buffManager.blueSpirit = true;
        buffManager._flowerBuffTargetDay = systemManager._day + 1;
        buffManager._flowerBuffTargetMonth = systemManager._month;
        if (buffManager._flowerBuffTargetDay > 28) {
            buffManager._flowerBuffTargetDay -= 28;
            buffManager._flowerBuffTargetMonth += + 1;
            if (buffManager._flowerBuffTargetMonth > 8) {
                buffManager._flowerBuffTargetMonth -= 8;
            }
        }
        // Debug.Log("나는 파랑색정령버프");
    }
    
    public void RedSpirit(){
        buffManager._isFlowerBuffActived = true;
        buffManager.redSpirit = true;
        buffManager._flowerBuffTargetDay = systemManager._day + 3;
        buffManager._flowerBuffTargetMonth = systemManager._month;
        if (buffManager._flowerBuffTargetDay > 28) {
            buffManager._flowerBuffTargetDay -= 28;
            buffManager._flowerBuffTargetMonth += + 1;
            if (buffManager._flowerBuffTargetMonth > 8) {
                buffManager._flowerBuffTargetMonth -= 8;
            }
        }
        // Debug.Log("나는 빨강색정령버프");
    }
    
    public void PinkSpirit(){
        buffManager._isFlowerBuffActived = true;
        buffManager.pinkSpirit = true;
        buffManager._flowerBuffTargetDay = systemManager._day + 3;
        buffManager._flowerBuffTargetMonth = systemManager._month;
        if (buffManager._flowerBuffTargetDay > 28) {
            buffManager._flowerBuffTargetDay -= 28;
            buffManager._flowerBuffTargetMonth += + 1;
            if (buffManager._flowerBuffTargetMonth > 8) {
                buffManager._flowerBuffTargetMonth -= 8;
            }
        }
        // Debug.Log("나는 분홍색정령버프");
    }
    
    public void YellowSpirit(){
        buffManager._isFlowerBuffActived = true;
        buffManager.yellowSpirit = true;
        buffManager._flowerBuffTargetDay = systemManager._day + 3;
        buffManager._flowerBuffTargetMonth = systemManager._month;
        if (buffManager._flowerBuffTargetDay > 28) {
            buffManager._flowerBuffTargetDay -= 28;
            buffManager._flowerBuffTargetMonth += + 1;
            if (buffManager._flowerBuffTargetMonth > 8) {
                buffManager._flowerBuffTargetMonth -= 8;
            }
        }
        // Debug.Log("나는 노랑색정령버프");
    }
    
    public void SkySpirit(){
        buffManager._isFlowerBuffActived = true;
        buffManager.skySpirit = true;
        buffManager._flowerBuffTargetDay = systemManager._day + 3;
        buffManager._flowerBuffTargetMonth = systemManager._month;
        if (buffManager._flowerBuffTargetDay > 28) {
            buffManager._flowerBuffTargetDay -= 28;
            buffManager._flowerBuffTargetMonth += + 1;
            if (buffManager._flowerBuffTargetMonth > 8) {
                buffManager._flowerBuffTargetMonth -= 8;
            }
        }
        // Debug.Log("나는 하늘색정령버프");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritBuff : MonoBehaviour
{   
    public SystemManager systemManager;
    public BuffManager buffManager;

    public void Spirit(ItemObject item) {
        if (item.ItemCode == 21) {
            WhiteSpirit();
        }
    }

    public void WhiteSpirit(){
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
        Debug.Log("나는 하얀색정령버프");
    }

    public void OrangeSpirit(){
        Debug.Log("나는 주황색정령버프");
    }
    
    public void BlueSpirit(){
        Debug.Log("나는 파랑색정령버프");
    }
    
    public void RedSpirit(){
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
        Debug.Log("나는 빨강색정령버프");
    }
    
    public void PinkSpirit(){
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
        Debug.Log("나는 분홍색정령버프");
    }
    
    public void YellowSpirit(){
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
        Debug.Log("나는 노랑색정령버프");
    }
    
    public void SkySpirit(){
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
        Debug.Log("나는 하늘색정령버프");
    }
    
}
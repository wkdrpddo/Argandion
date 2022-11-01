using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayBuff : MonoBehaviour
{
    public SystemManager systemManager;
    public BuffManager buffManager;

    public void Spirit(ItemObject item) {
        if (item.ItemCode == 21) {
            WhitePray();
        }
    }

    public void WhitePray(){
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8) {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 하얀색정령버프");
    }

    public void OrangePray(){
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8) {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 주황색정령버프");
    }
    
    public void BluePray(){
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8) {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 파랑색정령버프");
    }
    
    public void RedPray(){
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8) {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 빨강색정령버프");
    }
    
    public void PinkPray(){
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8) {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 분홍색정령버프");
    }
    
    public void YellowPray(){
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8) {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 노랑색정령버프");
    }
    
    public void SkyPray(){
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8) {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 하늘색정령버프");
    }
    
}
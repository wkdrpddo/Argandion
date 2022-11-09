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
            } else if (item.ItemCode == 51) {
                OrangeSpirit();
            } else if (item.ItemCode == 52) {
                RedSpirit();
            } else if (item.ItemCode == 53) {
                SkySpirit();
            } else if (item.ItemCode == 54) {
                BlueSpirit();
            } else if (item.ItemCode == 55) {
                YellowSpirit();
            } else if (item.ItemCode == 56) {
                WhiteSpirit();
            } else {
                Debug.Log("꽃이 아니야!");
            }
        } else {
            Debug.Log("다른 꽃 버프가 활성중 입니다!");
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
        Debug.Log("나는 하얀색정령버프");
    }

    public void OrangeSpirit(){
        // buildings = GameObject.FindGameObjectsWithLayer(10)
        Debug.Log("나는 주황색정령버프");
    }
    
    public void BlueSpirit(){
        buffManager.blueSpirit = true;
        Debug.Log("나는 파랑색정령버프");
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
        Debug.Log("나는 빨강색정령버프");
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
        Debug.Log("나는 분홍색정령버프");
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
        Debug.Log("나는 노랑색정령버프");
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
        Debug.Log("나는 하늘색정령버프");
    }
    
}
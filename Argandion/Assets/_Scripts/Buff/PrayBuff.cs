using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayBuff : MonoBehaviour
{
    public SystemManager systemManager;
    private BuffManager buffManager;
    public Inventory _theInventory;
    private Altar _altar;

    public Item _item;

    public int tempDay;
    public int tempMonth;
    public bool todayPray = false;
    public int flowerIdx = 0;
    public int prayDay = 0;

    public bool testCode;

    void Start()
    {
        tempDay = systemManager._day;
        buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        _altar = GameObject.Find("Altar").GetComponent<Altar>();
    }

    public void DayStart()
    {
        if (tempDay != systemManager._day)
        {
            NewDay();
            tempDay = systemManager._day;
        }

        if (buffManager._flowerBuffTargetMonth == systemManager._month && buffManager._flowerBuffTargetDay == systemManager._day)
        {
            buffManager.FlowerBuffEnd(20); // 버프 끝
            _altar.buffEnd();
        }
    }

    public void Pray(int itemCode)
    {
        if (!todayPray)
        {
            todayPray = true;
            if (flowerIdx != itemCode)
            {
                flowerIdx = itemCode;
                prayDay = 1;
            }
            else
            {
                if (prayDay <= 2)
                {
                    prayDay += 1;
                }
                if (prayDay == 3)
                {
                    Reincarnation();
                }
            }
        }
    }

    public void NewDay()
    {
        if (!todayPray)
        { // 제사 연속으로 안 하면 리셋
            prayDay = 0;
            flowerIdx = 0;
        }
        else
        { 
            todayPray = false;
        }
        if(prayDay==3){
            prayDay=0;
        }
    }

    public void Reincarnation()
    {
        if (flowerIdx == 50)
        {
            PinkPray();
            systemManager.callActiveIcon(5);
        }
        else if (flowerIdx == 51)
        {
            OrangePray();
            systemManager.callActiveIcon(2);
        }
        else if (flowerIdx == 52)
        {
            RedPray();
            systemManager.callActiveIcon(4);
            if (buffManager.isColdWaveIcons)
            {
                systemManager.callInactiveIcon(58);
            };
        }
        else if (flowerIdx == 53)
        {
            SkyPray();
            systemManager.callActiveIcon(7);
        }
        else if (flowerIdx == 54)
        {
            BluePray();
            systemManager.callActiveIcon(3);
        }
        else if (flowerIdx == 55)
        {
            YellowPray();
            systemManager.callActiveIcon(6);
        }
        else if (flowerIdx == 56)
        {
            WhitePray();
            systemManager.callActiveIcon(1);
        }
    }

    public void WhitePray()
    {
        buffManager.FlowerBuffEnd(1);
        buffManager._isFlowerBuffActived = true;
        buffManager._isPrayBuffActived = true;
        buffManager.whitePray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8)
        {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        // Debug.Log("나는 하얀색제단버프");
    }

    public void OrangePray()
    {
        buffManager.FlowerBuffEnd(2);
        buffManager._isFlowerBuffActived = true;
        buffManager._isPrayBuffActived = true;
        buffManager.orangePray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8)
        {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        // Debug.Log("나는 주황색제단버프");
    }

    public void BluePray()
    {
        buffManager.FlowerBuffEnd(3);
        buffManager._isFlowerBuffActived = true;
        buffManager._isPrayBuffActived = true;
        buffManager.bluePray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8)
        {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        // Debug.Log("나는 파랑색제단버프");
    }

    public void RedPray()
    {
        buffManager.FlowerBuffEnd(4);
        buffManager._isFlowerBuffActived = true;
        buffManager._isPrayBuffActived = true;
        buffManager.redPray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8)
        {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        // Debug.Log("나는 빨강색제단버프");
    }

    public void PinkPray()
    {
        buffManager.FlowerBuffEnd(5);
        buffManager._isFlowerBuffActived = true;
        buffManager._isPrayBuffActived = true;
        buffManager.pinkPray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8)
        {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        // Debug.Log("나는 분홍색제단버프");
    }

    public void YellowPray()
    {
        buffManager.FlowerBuffEnd(6);
        buffManager._isFlowerBuffActived = true;
        buffManager._isPrayBuffActived = true;
        buffManager.yellowPray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8)
        {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        // Debug.Log("나는 노랑색제단버프");
    }

    public void SkyPray()
    {
        buffManager.FlowerBuffEnd(7);
        buffManager._isFlowerBuffActived = true;
        buffManager._isPrayBuffActived = true;
        buffManager.skyPray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8)
        {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        // Debug.Log("나는 하늘색제단버프");
    }

}
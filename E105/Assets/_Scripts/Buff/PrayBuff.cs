using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayBuff : MonoBehaviour
{
    public SystemManager systemManager;
    private BuffManager buffManager;
    public Inventory _theInventory;

    public int tempDay;
    public int tempMonth;
    public bool todayPray = false;
    public int flowerIdx = 0;
    public int prayDay = 0;

    void Start()
    {
        tempDay = systemManager._day;
        buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
    }

    void Update()
    {
        if (tempDay != systemManager._day)
        {
            NewDay();
            tempDay = systemManager._day;
        }

        if (buffManager._flowerBuffTargetMonth == systemManager._month && buffManager._flowerBuffTargetDay == systemManager._day)
        {
            buffManager.FlowerBuffEnd(); // 버프 끝
        }
    }

    public void Pray(ItemObject item)
    {
        if (buffManager._isFlowerBuffActived)
        {
            Debug.Log("이미 다른버프가 활성화 중 입니다.");
            return;
        }

        if (item.Category != "꽃")
        {
            Debug.Log("꽃이 아닙니다!");
            return;
        }

        if (!todayPray)
        {
            if ((flowerIdx == 0 || flowerIdx == item.ItemCode))
            {
                flowerIdx = item.ItemCode;
                todayPray = true;
                if (prayDay < 2)
                {
                    _theInventory.StoreItem(0, -1);
                    prayDay += 1;
                }
                else
                {
                    if (_theInventory.slots[0].itemCount >= 2)
                    {
                        _theInventory.StoreItem(0, -2);
                        prayDay += 1;
                    }
                    else
                    {
                        Debug.Log("꽃이 부족합니다.");
                    }
                }
                if (prayDay == 3)
                {
                    Reincarnation();
                }
            }
            else
            {
                Debug.Log("다른 제사버프가 활성화중입니다.");
            }
        }
        else
        {
            Debug.Log("오늘은 이미 기도를 드렸습니다.");
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
        { // ??
            todayPray = false;
        }
    }

    public void Reincarnation()
    {
        if (flowerIdx == 50)
        {
            PinkPray();
        }
        else if (flowerIdx == 51)
        {
            OrangePray();
        }
        else if (flowerIdx == 52)
        {
            RedPray();
        }
        else if (flowerIdx == 53)
        {
            SkyPray();
        }
        else if (flowerIdx == 54)
        {
            BluePray();
        }
        else if (flowerIdx == 55)
        {
            YellowPray();
        }
        else if (flowerIdx == 56)
        {
            WhitePray();
        }
    }

    public void WhitePray()
    {
        buffManager._isFlowerBuffActived = true;
        buffManager.whitePray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8)
        {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 하얀색제단버프");
    }

    public void OrangePray()
    {
        buffManager._isFlowerBuffActived = true;
        buffManager.orangePray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8)
        {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 주황색제단버프");
    }

    public void BluePray()
    {
        buffManager._isFlowerBuffActived = true;
        buffManager.bluePray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8)
        {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 파랑색제단버프");
    }

    public void RedPray()
    {
        buffManager._isFlowerBuffActived = true;
        buffManager.redPray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8)
        {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 빨강색제단버프");
    }

    public void PinkPray()
    {
        buffManager._isFlowerBuffActived = true;
        buffManager.pinkPray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8)
        {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 분홍색제단버프");
    }

    public void YellowPray()
    {
        buffManager._isFlowerBuffActived = true;
        buffManager.yellowPray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8)
        {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 노랑색제단버프");
    }

    public void SkyPray()
    {
        buffManager._isFlowerBuffActived = true;
        buffManager.skyPray = true;
        buffManager._flowerBuffTargetDay = systemManager._day;
        buffManager._flowerBuffTargetMonth = systemManager._month + 1;
        if (buffManager._flowerBuffTargetMonth > 8)
        {
            buffManager._flowerBuffTargetMonth -= 8;
        }
        Debug.Log("나는 하늘색제단버프");
    }

}
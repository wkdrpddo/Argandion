using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Ranch : MonoBehaviour
{
    public SystemManager system;
    public int temp;
    public GameObject _buffManagerObject;
    private BuffManager _buff;
    public Inventory theInventory;
    public GameObject item;
    private UIManager ui;

    public int cows = 0;
    public int chicks = 0;
    public int sheeps = 0;
    private int point = 0;
    private int maxPoint = 80;

    public int milk = 0;
    public int eggs = 0;
    public int wool = 0;

    private int[] milkPerDay = { 0, 0, 0, 1, 1 };
    private int[] eggsPerDay = { 0, 0, 1, 1, 2 };
    private int[] woolPerDay = { 0, 1, 2, 3, 4 };

    void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();

        temp = system._day;
        _buffManagerObject = GameObject.Find("BuffManager");
        _buff = _buffManagerObject.GetComponent<BuffManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (temp != system._day)
        {
            NewDay();
            temp = system._day;
        }
    }

    void NewDay()
    {
        for (int i = 0; i < cows; i++)
        {
            if (_buff.yellowPray || _buff.yellowSpirit)
            {
                int milkToday = milkPerDay[Random.Range(0, 5)];
                float milkFloat = (float)milkToday;
                milkFloat *= 1.4f;
                milk += Mathf.RoundToInt(milkFloat);
            }
            else
            {
                milk += milkPerDay[Random.Range(0, 5)];
            }
        }

        for (int i = 0; i < chicks; i++)
        {
            if (_buff.yellowPray || _buff.yellowSpirit)
            {
                int eggsToday = eggsPerDay[Random.Range(0, 5)];
                float eggsFloat = (float)eggsToday;
                eggsFloat *= 1.4f;
                eggs += Mathf.RoundToInt(eggsFloat);
            }
            else
            {
                eggs += eggsPerDay[Random.Range(0, 5)];
            }
        }

        for (int i = 0; i < sheeps; i++)
        {
            if (_buff.yellowPray || _buff.yellowSpirit)
            {
                int woolToday = woolPerDay[Random.Range(0, 5)];
                float woolFloat = (float)woolToday;
                woolFloat *= 1.4f;
                wool += Mathf.RoundToInt(woolFloat);
            }
            else
            {
                wool += woolPerDay[Random.Range(0, 5)];
            }
        }
    }

    public void GetMilk(int howMany)
    {
        if (milk >= howMany)
        {
            milk -= howMany;
            theInventory.AcquireItem(item.GetComponent<Item>().FindItem(110), howMany);
        }
    }

    public void GetWool(int howMany)
    {
        if (wool >= howMany)
        {
            wool -= howMany;
            theInventory.AcquireItem(item.GetComponent<Item>().FindItem(505), howMany);
        }
    }

    public void GetEggs(int howMany)
    {
        if (eggs >= howMany)
        {
            eggs -= howMany;
            theInventory.AcquireItem(item.GetComponent<Item>().FindItem(111), howMany);
        }
    }

    public void BuyCow()
    {
        int tempPoint = point + 4;
        if (ui.getPlayerGold() < 1000)
        {
            ui.OnResultNotificationPanel("소지금이 부족합니다.");
        }
        else if (tempPoint > maxPoint)
        {
            ui.OnResultNotificationPanel("농장의 수용량이 부족합니다.");
        }
        else
        {
            cows++;
            ui.addPlayerGold(-1000);
            point = tempPoint;
        }
    }

    public void BuyChick()
    {
        int tempPoint = point + 3;
        if (ui.getPlayerGold() < 650)
        {
            ui.OnResultNotificationPanel("소지금이 부족합니다.");
        }
        else if (tempPoint > maxPoint)
        {
            ui.OnResultNotificationPanel("농장의 수용량이 부족합니다.");
        }
        else
        {
            chicks++;
            ui.addPlayerGold(-650);
            point = tempPoint;
        }
    }

    public void BuySheep()
    {
        int tempPoint = point + 2;
        if (ui.getPlayerGold() < 350)
        {
            ui.OnResultNotificationPanel("소지금이 부족합니다.");
        }
        else if (tempPoint > maxPoint)
        {
            ui.OnResultNotificationPanel("농장의 수용량이 부족합니다.");
        }
        else
        {
            sheeps++;
            ui.addPlayerGold(-350);
            point = tempPoint;
        }
    }

    public void SellCow(int howMany)
    {
        if (cows >= howMany)
        {
            cows -= howMany;
            ui.addPlayerGold((howMany * 500));
            point -= 4 * howMany;
        }
    }

    public void SellChick(int howMany)
    {
        if (chicks >= howMany)
        {
            chicks -= howMany;
            ui.addPlayerGold((howMany * 325));
            point -= 3 * howMany;
        }
    }

    public void SellSheep(int howMany)
    {
        if (sheeps >= howMany)
        {
            sheeps -= howMany;
            ui.addPlayerGold((howMany * 175));
            point -= 2 * howMany;
        }
    }
}
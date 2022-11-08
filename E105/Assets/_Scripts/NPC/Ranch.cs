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

    void GetMilk(int howMany)
    {
        if (milk >= howMany)
        {
            milk -= howMany;
            theInventory.AcquireItem(item.GetComponent<Item>().FindItem(110), howMany);
        }
    }

    void GetWool(int howMany)
    {
        if (wool >= howMany)
        {
            wool -= howMany;
            theInventory.AcquireItem(item.GetComponent<Item>().FindItem(505), howMany);
        }
    }

    void GetEggs(int howMany)
    {
        if (eggs >= howMany)
        {
            eggs -= howMany;
            theInventory.AcquireItem(item.GetComponent<Item>().FindItem(111), howMany);
        }
    }

    void BuyCow(int howMany)
    {
        int tempPoint = point + (4 * howMany);
        if (ui.getPlayerGold() >= (howMany * 1000) && tempPoint <= maxPoint)
        {
            cows += howMany;
            ui.addPlayerGold(-1 * (howMany * 1000));
            point = tempPoint;
        }
    }

    void BuyChick(int howMany)
    {
        int tempPoint = point + (3 * howMany);
        if (ui.getPlayerGold() >= (howMany * 650) && tempPoint <= maxPoint)
        {
            cows += howMany;
            ui.addPlayerGold(-1 * (howMany * 650));
            point = tempPoint;
        }
    }

    void BuySheep(int howMany)
    {
        int tempPoint = point + (2 * howMany);
        if (ui.getPlayerGold() >= (howMany * 350) && tempPoint <= maxPoint)
        {
            cows += howMany;
            ui.addPlayerGold(-1 * (howMany * 350));
            point = tempPoint;
        }
    }

    void SellCow(int howMany)
    {
        if (cows >= howMany)
        {
            cows -= howMany;
            ui.addPlayerGold((howMany * 500));
            point -= 4 * howMany;
        }
    }

    void SellChick(int howMany)
    {
        if (chicks >= howMany)
        {
            chicks -= howMany;
            ui.addPlayerGold((howMany * 325));
            point -= 3 * howMany;
        }
    }

    void SellSheep(int howMany)
    {
        if (sheeps >= howMany)
        {
            sheeps -= howMany;
            ui.addPlayerGold((howMany * 175));
            point -= 2 * howMany;
        }
    }
}
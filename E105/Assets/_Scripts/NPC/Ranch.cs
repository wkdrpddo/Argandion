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

    public int cows = 0;
    public int chicks = 0;
    public int sheeps = 0;
    private int point = 0;
    private int maxPoint = 80;

    public int milk = 0;
    public int eggs = 0;
    public int wool = 0;

    private int[] milkPerDay = {0,0,0,1,1};
    private int[] eggsPerDay = {0,0,1,1,2};
    private int[] woolPerDay = {0,1,2,3,4};

    void Start()
    {
        temp = system._day;
        _buffManagerObject = GameObject.Find("BuffManager");
        _buff = _buffManagerObject.GetComponent<BuffManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (temp != system._day) {
            NewDay();
            temp = system._day;
        }
    }

    void NewDay()
    {
        for (int i = 0; i < cows; i ++) {
            milk += milkPerDay[Random.Range(0,5)];
        }

        for (int i = 0; i < chicks; i++) {
            eggs += eggsPerDay[Random.Range(0,5)];
        }

        for (int i = 0; i < sheeps; i++) {
            wool += woolPerDay[Random.Range(0,5)];
        }
    }
    
    void GetMilk(int howMany)
    {
        if (milk >= howMany) {
            milk -= howMany;
            theInventory.AcquireItem(item.GetComponent<Item>().FindItem(110),howMany);
        }
    }

    void GetWool(int howMany)
    {
        if (wool >= howMany) {
            wool -= howMany;
            theInventory.AcquireItem(item.GetComponent<Item>().FindItem(505),howMany);
        }
    }

    void GetEggs(int howMany)
    {
        if (eggs >= howMany) {
            eggs -= howMany;
            theInventory.AcquireItem(item.GetComponent<Item>().FindItem(111),howMany);
        }
    }

    void BuyCow(int howMany)
    {
        int tempPoint = point + (4*howMany);
        if (theInventory.gold >= (howMany * 1000) && tempPoint <= maxPoint) {
            cows += howMany;
            theInventory.gold -= (howMany *1000);
            point = tempPoint;
        }
    }

    void BuyChick(int howMany)
    {
        int tempPoint = point + (3*howMany);
        if (theInventory.gold >= (howMany * 650) && tempPoint <= maxPoint) {
            cows += howMany;
            theInventory.gold -= (howMany * 650);
            point = tempPoint;
        }
    }

    void BuySheep(int howMany)
    {
        int tempPoint = point + (2*howMany);
        if (theInventory.gold >= (howMany * 350) && tempPoint <= maxPoint) {
            cows += howMany;
            theInventory.gold -= (howMany * 350);
            point = tempPoint;
        }
    }

    void SellCow(int howMany)
    {
        if (cows >= howMany) {
            cows -= howMany;
            theInventory.gold += (howMany * 500);
            point -= 4 * howMany;
        }
    }

    void SellChick(int howMany)
    {
        if (chicks >= howMany) {
            chicks -= howMany;
            theInventory.gold += (howMany * 325);
            point -= 3 * howMany;
        }
    }

    void SellSheep(int howMany)
    {
        if (sheeps >= howMany) {
            sheeps -= howMany;
            theInventory.gold += (howMany * 175);
            point -= 2 * howMany;
        }
    }
}
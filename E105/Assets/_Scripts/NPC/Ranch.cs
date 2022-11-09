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


    public GameObject droppedMilk;
    public GameObject droppedEgg;
    public GameObject droppedWool;

    public GameObject cowPrefab;
    public GameObject chickenPrefab;
    public GameObject sheepPrefab;

    private List<GameObject> cowPrefabs = new List<GameObject>();
    private List<GameObject> chickenPrefabs = new List<GameObject>();
    private List<GameObject> sheepPrefabs = new List<GameObject>();


    void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        system = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        _buffManagerObject = GameObject.Find("BuffManager");
        _buff = _buffManagerObject.GetComponent<BuffManager>();

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

    public int getPoint()
    {
        return point;
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

        for (int i = 0; i < milk; i++)
        {
            Instantiate(droppedMilk, getRandonPoint(), Quaternion.identity);
        }
        for (int i = 0; i < eggs; i++)
        {
            Instantiate(droppedEgg, getRandonPoint(), Quaternion.identity);
        }
        for (int i = 0; i < wool; i++)
        {
            Instantiate(droppedWool, getRandonPoint(), Quaternion.identity);
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
            // cowPrefab 랜덤으로 생성하고 리스트에 저장하기
            GameObject addData = Instantiate(cowPrefab, getRandonPoint(), Quaternion.identity);
            cowPrefabs.Add(addData);

            cows++;
            ui.addPlayerGold(-1000);
            point = tempPoint;
        }

        // 목장 중앙에서 랜덤 offset 으로 소 생성
        // Instantiate(cowPrefab, new Vector3(0, 2, 0), Quaternion.identity);
        // 얘를 관리해야 함.. 어떻게?
        // 배열을 두기
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
            GameObject addData = Instantiate(chickenPrefab, getRandonPoint(), Quaternion.identity);
            chickenPrefabs.Add(addData);

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
            Debug.Log("소지금 부족 오류");
            ui.OnResultNotificationPanel("소지금이 부족합니다.");
        }
        else if (tempPoint > maxPoint)
        {
            Debug.Log("수용량 부족 오류");
            ui.OnResultNotificationPanel("농장의 수용량이 부족합니다.");
        }
        else
        {
            Debug.Log("정상 동작");
            GameObject addData = Instantiate(sheepPrefab, getRandonPoint(), Quaternion.identity);
            sheepPrefabs.Add(addData);

            sheeps++;
            ui.addPlayerGold(-350);
            point = tempPoint;
        }
    }

    public void SellCow(int howMany)
    {
        if (cows >= howMany)
        {
            // cowPrefab 삭제하고 리스트에서 삭제하기
            for (int i = howMany - 1; i >= cows; i--)
            {
                Destroy(cowPrefabs[i], 0.5f);
            }

            cows -= howMany;
            ui.addPlayerGold((howMany * 500));
            point -= 4 * howMany;
        }

        // destory cow prefabs

    }

    public void SellChick(int howMany)
    {
        if (chicks >= howMany)
        {

            for (int i = howMany - 1; i >= chicks; i--)
            {
                Destroy(chickenPrefabs[i], 0.5f);
            }

            chicks -= howMany;
            ui.addPlayerGold((howMany * 325));
            point -= 3 * howMany;
        }
    }

    public void SellSheep(int howMany)
    {
        if (sheeps >= howMany)
        {

            for (int i = howMany - 1; i >= sheeps; i--)
            {
                Destroy(sheepPrefabs[i], 0.5f);
            }

            sheeps -= howMany;
            ui.addPlayerGold((howMany * 175));
            point -= 2 * howMany;
        }
    }

    Vector3 getRandonPoint()
    {
        Vector3 point = new Vector3();
        while (true)
        {
            point = new Vector3(Random.Range(210, 275), 2, Random.Range(150, 190));
            if (isValid(point))
                return point;
        }
    }

    bool isValid(Vector3 point)
    {
        // 재단사 집 범위에 해당하는 경우
        if (point.x < 224 && point.z >= 150)
            return false;
        return true;
    }

}

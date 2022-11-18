using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class CombHunter : MonoBehaviour
{
    public GameObject go_SlotsParent;
    public Inventory theInventory;
    public CombRecipe[] myData;
    public GameObject item;

    // private UIManager ui;

    private int[] myItems = new int[25];
    private int[] howItems = new int[25];
    private int[] canMake = new int[4];

    private Slot[] slots;

    void Awake()
    {
        // ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        // slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/CombHunter.json");
        var combDatas = JsonHelper.FromJson<CombRecipe>(jsonString);
        myData = combDatas;
    }

    public void Hello()
    {
        slots = UIManager._uimanagerInstance.getInventorySlots();

        myItems = new int[25];
        howItems = new int[25];
        canMake = new int[4];

        foreach (Slot slot in slots)
        {
            if (slot.itemCount > 0)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (myItems[i] == 0 || myItems[i] == slot.item.ItemCode)
                    {
                        myItems[i] = slot.item.ItemCode;
                        howItems[i] += slot.itemCount;
                        break;
                    }
                }
            }
        }

        int canMakeIdx = 0;
        foreach (CombRecipe rec in myData)
        {
            int canMakeNum = CanMake(rec, myItems, howItems);
            canMake[canMakeIdx] = canMakeNum;
            if (canMakeNum > 0)
                Debug.Log(UIManager._uimanagerInstance.findItem(rec.Result).Name + "는(은) " + CanMake(rec, myItems, howItems) + "개 만들 수 있습니다.");
            else
                Debug.Log(UIManager._uimanagerInstance.findItem(rec.Result).Name + "는(은) 만들 수 없습니다 T.T");

            canMakeIdx += 1;
        }
    }

    public int[] getCanMakeList()
    {
        return canMake;
    }

    public void Trade(int idx, int howMany)
    {
        if (canMake[idx] > 0 && canMake[idx] >= howMany)
        {
            UIManager._uimanagerInstance.acquireItem(UIManager._uimanagerInstance.findItem(myData[idx].Result), howMany);
            UIManager._uimanagerInstance.reductItem(UIManager._uimanagerInstance.findItem(myData[idx].Material1), -myData[idx].Cost1 * howMany);
            if (myData[idx].Material2 > 0)
                UIManager._uimanagerInstance.reductItem(UIManager._uimanagerInstance.findItem(myData[idx].Material2), -myData[idx].Cost2 * howMany);
            if (myData[idx].Material3 > 0)
                UIManager._uimanagerInstance.reductItem(UIManager._uimanagerInstance.findItem(myData[idx].Material3), -myData[idx].Cost3 * howMany);
            if (myData[idx].Material4 > 0)
                UIManager._uimanagerInstance.reductItem(UIManager._uimanagerInstance.findItem(myData[idx].Material4), -myData[idx].Cost4 * howMany);
            if (myData[idx].Material5 > 0)
                UIManager._uimanagerInstance.reductItem(UIManager._uimanagerInstance.findItem(myData[idx].Material5), -myData[idx].Cost5 * howMany);
            if (myData[idx].Material6 > 0)
                UIManager._uimanagerInstance.reductItem(UIManager._uimanagerInstance.findItem(myData[idx].Material6), -myData[idx].Cost6 * howMany);
            // canMake[idx] -= howMany;
            Hello();
        }
    }

    int CanMake(CombRecipe rec, int[] myItems, int[] howItems)
    {
        int howM1 = 0;
        int howM2 = 0;
        int howM3 = 0;
        int howM4 = 0;
        int howM5 = 0;
        int howM6 = 0;

        for (int i = 0; i < 25; i++)
        {
            if (myItems[i] == rec.Material1)
            {
                howM1 = howItems[i] / rec.Cost1;
                if (howM1 == 0)
                {
                    return 0;
                }
                break;
            }
        }

        if (rec.Material2 > 0)
        {
            for (int i = 0; i < 25; i++)
            {
                if (myItems[i] == rec.Material2)
                {
                    howM2 = howItems[i] / rec.Cost2;
                    if (howM2 == 0)
                    {
                        return 0;
                    }
                    break;
                }
            }
        }
        else
        {
            howM2 = 9999;
        }

        if (rec.Material3 > 0)
        {
            for (int i = 0; i < 25; i++)
            {
                if (myItems[i] == rec.Material3)
                {
                    howM3 = howItems[i] / rec.Cost3;
                    if (howM3 == 0)
                    {
                        return 0;
                    }
                    break;
                }
            }
        }
        else
        {
            howM3 = 9999;
        }

        if (rec.Material4 > 0)
        {
            for (int i = 0; i < 25; i++)
            {
                if (myItems[i] == rec.Material4)
                {
                    howM4 = howItems[i] / rec.Cost4;
                    if (howM4 == 0)
                    {
                        return 0;
                    }
                    break;
                }
            }
        }
        else
        {
            howM4 = 9999;
        }

        if (rec.Material5 > 0)
        {
            for (int i = 0; i < 25; i++)
            {
                if (myItems[i] == rec.Material5)
                {
                    howM5 = howItems[i] / rec.Cost5;
                    if (howM5 == 0)
                    {
                        return 0;
                    }
                    break;
                }
            }
        }
        else
        {
            howM5 = 9999;
        }

        if (rec.Material6 > 0)
        {
            for (int i = 0; i < 25; i++)
            {
                if (myItems[i] == rec.Material6)
                {
                    howM6 = howItems[i] / rec.Cost6;
                    if (howM6 == 0)
                    {
                        return 0;
                    }
                    break;
                }
            }
        }
        else
        {
            howM6 = 9999;
        }

        int[] howM = { howM1, howM2, howM3, howM4, howM5, howM6 };

        return FindMin(howM);
    }

    int FindMin(int[] howM)
    {
        int min = howM[0];
        for (int idx = 0; idx < howM.Length; idx++)
        {
            if (min > howM[idx])
            {
                min = howM[idx];
            }
        }

        return min;
    }
}

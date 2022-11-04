using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class CombRecipe
{
    public int Result;
    public int Material1;
    public int Cost1;
    public int Material2;
    public int Cost2;
    public int Material3;
    public int Cost3;
    public int Material4;
    public int Cost4;
    public int Material5;
    public int Cost5;
    public int Material6;
    public int Cost6;
}

[System.Serializable]
public class CombRecipes
{
    public List<CombRecipe> Combs;
}

public class CombCarpentor : MonoBehaviour
{   
    public GameObject go_SlotsParent;
    public Inventory theInventory;
    public CombRecipe[] myData;
    public GameObject item;

    private int[] myItems = new int[25];
    private int[] howItems = new int[25];
    private int[] canMake = new int[8];

    private Slot[] slots;

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        string jsonString = File.ReadAllText(Application.dataPath + "/Scripts/CombCarpentor.json");
        var combDatas = JsonHelper.FromJson<CombRecipe>(jsonString);
        myData = combDatas;
    }

    public void Hello()
    {
        myItems = new int[25];
        howItems = new int[25];
        canMake = new int[8];
        
        foreach(Slot slot in slots) {
            if (slot.itemCount >0) {
                for (int i=0; i < 12; i++) {
                    if (myItems[i] == 0 || myItems[i] == slot.item.ItemCode){
                        myItems[i] = slot.item.ItemCode;
                        howItems[i] += slot.itemCount;
                        break;
                    }
                }
            }
        }

        int canMakeIdx = 0;
        foreach(CombRecipe rec in myData) {
            int canMakeNum = CanMake(rec, myItems, howItems);
            canMake[canMakeIdx] = canMakeNum;
            if (canMakeNum > 0)
                Debug.Log(item.GetComponent<Item>().FindName(rec.Result) + "는(은) " +CanMake(rec, myItems, howItems) + "개 만들 수 있습니다.");
            else
                Debug.Log(item.GetComponent<Item>().FindName(rec.Result) + "는(은) 만들 수 없습니다 T.T");
            
            canMakeIdx +=1;
        }
    }

    public void Trade(int idx, int howMany)
    {   
        if (canMake[idx] > 0 && canMake[idx] >= howMany )
        {
            theInventory.AcquireItem(item.GetComponent<Item>().FindItem(myData[idx].Result),howMany);
            theInventory.AcquireItem(item.GetComponent<Item>().FindItem(myData[idx].Material1), -myData[idx].Cost1 * howMany);
            if (myData[idx].Material2 > 0)
                theInventory.AcquireItem(item.GetComponent<Item>().FindItem(myData[idx].Material2), -myData[idx].Cost2 * howMany);
            if (myData[idx].Material3 > 0)
                theInventory.AcquireItem(item.GetComponent<Item>().FindItem(myData[idx].Material3), -myData[idx].Cost3 * howMany);
            if (myData[idx].Material4 > 0)
                theInventory.AcquireItem(item.GetComponent<Item>().FindItem(myData[idx].Material4), -myData[idx].Cost4 * howMany);
            if (myData[idx].Material5 > 0)
                theInventory.AcquireItem(item.GetComponent<Item>().FindItem(myData[idx].Material5), -myData[idx].Cost5 * howMany);
            if (myData[idx].Material6 > 0)
                theInventory.AcquireItem(item.GetComponent<Item>().FindItem(myData[idx].Material6), -myData[idx].Cost6 * howMany);
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

        for (int i=0; i<25; i++) {
            if (myItems[i] == rec.Material1) {
                howM1 = howItems[i] / rec.Cost1;
                if (howM1 == 0) {
                    return 0;
                }
                break;
            }
        }

        if ( rec.Material2 > 0 ){
            for (int i=0; i<25; i++) {
                if (myItems[i] == rec.Material2) {
                    howM2 = howItems[i] / rec.Cost2;
                    if (howM2 == 0) {
                        return 0;
                    }
                    break;
                }
            }
        } else {
            howM2 = 9999;
        }

        if ( rec.Material3 > 0 ){
            for (int i=0; i<25; i++) {
                if (myItems[i] == rec.Material3) {
                    howM3 = howItems[i] / rec.Cost3;
                    if (howM3 == 0) {
                        return 0;
                    }
                    break;
                }
            }
        } else {
            howM3 = 9999;
        }

        if ( rec.Material4 > 0 ){
            for (int i=0; i<25; i++) {
                if (myItems[i] == rec.Material4) {
                    howM4 = howItems[i] / rec.Cost4;
                    if (howM4 == 0) {
                        return 0;
                    }
                    break;
                }
            }
        } else {
            howM4 = 9999;
        }

        if ( rec.Material5 > 0 ){
            for (int i=0; i<25; i++) {
                if (myItems[i] == rec.Material5) {
                    howM5 = howItems[i] / rec.Cost5;
                    if (howM5 == 0) {
                        return 0;
                    }
                    break;
                }
            }
        } else {
            howM5 = 9999;
        }

        if ( rec.Material6 > 0 ){
            for (int i=0; i<25; i++) {
                if (myItems[i] == rec.Material6) {
                    howM6 = howItems[i] / rec.Cost6;
                    if (howM6 == 0) {
                        return 0;
                    }
                    break;
                }
            }
        } else {
            howM6 = 9999;
        }

        int[] howM = {howM1, howM2, howM3, howM4, howM5, howM6};
        
        return FindMin(howM);
    }

    int FindMin(int[] howM)
    {   
        int min = howM[0];
        for (int idx = 0; idx < howM.Length; idx ++) {
            if (min > howM[idx]) {
                min = howM[idx];
            }
        }

        return min;
    }
}

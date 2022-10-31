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
}

[System.Serializable]
public class CombRecipes
{
    public List<CombRecipe> Combs;
}

public class CombCarpentor : MonoBehaviour
{   
    public GameObject go_SlotsParent;
    public CombRecipe[] myData;
    public GameObject item;

    private int[] myItems = new int[25];
    private int[] howItems = new int[25];

    private Slot[] slots;

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        string jsonString = File.ReadAllText(Application.dataPath + "/Scripts/CombinationTable.json");
        var combDatas = JsonHelper.FromJson<CombRecipe>(jsonString);
        myData = combDatas;
        // Debug.Log(item.GetComponent<Item>().FindName(4));
    }

    public void Hello()
    {
        myItems = new int[25];
        howItems = new int[25];
        
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

        foreach(CombRecipe rec in myData) {
            
        }
    }

    int CanMake(CombRecipe rec, int[] myItems, int[] howItems)
    {
        int howM1 = 0;
        int howM2 = 0;

        for (int i=0; i<12; i++) {
            if (myItems[i] == rec.Material1) {
                howM1 = myItems[i] / rec.Cost1;
                if (howM1 == 0) {
                    return 0;
                }
                break;
            }
        }

        for (int i=0; i<12; i++) {
            if (myItems[i] == rec.Material2) {
                howM2 = myItems[i] / rec.Cost2;
                if (howM2 == 0) {
                    return 0;
                }
                break;
            }
        }

        if( howM1 < howM2  ) {
            return howM1;
        } else {
            return howM2;
        }
    }
}

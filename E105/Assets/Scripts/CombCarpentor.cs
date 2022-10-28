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
    public CombRecipe[] mydata;

    private int[] myItems = new int[25];
    private int[] howItems = new int[25];

    private Slot[] slots;

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        string jsonString = File.ReadAllText(Application.dataPath + "/Scripts/CombinationTable.json");
        var combDatas = JsonHelper.FromJson<CombRecipe>(jsonString);
        mydata = combDatas;
    }

    public void Hello()
    {
        myItems = new int[25];
        howItems = new int[25];
        
        foreach(Slot slot in slots) {
            if (slot.itemCount >0) {
                for (int i=0; i < 25; i++) {
                    if (myItems[i] == 0 || myItems[i] == slot.item.ItemCode){
                        myItems[i] = slot.item.ItemCode;
                        howItems[i] += slot.itemCount;
                        break;
                    }
                }
            }
        }

        Debug.Log(myItems[0]);
        Debug.Log(howItems[0]);
    }

    // void canMake()
    // {
    //     foreach(CombRecipe combData in mydata) {

    //     }
    // }
}

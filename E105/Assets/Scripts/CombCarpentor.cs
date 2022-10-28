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
        foreach(Slot slot in slots) {
            if (slot.itemCount >0) {
                Debug.Log(slot.item.Name +"은" + slot.itemCount + "개 있당~");
            }
        }
    }

    // void canMake()
    // {
    //     foreach(CombRecipe combData in mydata) {

    //     }
    // }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class FoodObject
{
    public int ItemCode;
    public int Health;
    public int Stamina;
}

[System.Serializable]
public class FoodData
{
    public List<FoodObject> Food;
}

public class Food : MonoBehaviour
{
    public PlayerSystem player;

    public void UseFood(int itemCode)
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/FoodTable.json");
        var itemData = JsonHelper.FromJson<FoodObject>(jsonString);
        for (int idx = 0 ; idx < itemData.Length ; idx++ ) {
            if (itemData[idx].ItemCode == itemCode) {
                player.changeHealth((-1)*itemData[idx].Health);
                player.changeEnergy((-1)*itemData[idx].Stamina);
                break;
            }
        }
    }
}

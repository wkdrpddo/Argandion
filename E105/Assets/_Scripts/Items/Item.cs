using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ItemCodeToIndex;

[System.Serializable]
public class ItemObject
{
    public int ItemCode;
    public string Name;
    public string Category;
    public string Desc;
    public int SellCost;
}

[System.Serializable]
public class ItemData
{
    public List<ItemObject> Item;
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return UnityEngine.JsonUtility.ToJson(wrapper);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

public class Item : MonoBehaviour
{
    public int itemCode;
    public ItemObject itemObject;

    void Start()
    {
        // Debug.Log("test");
        // Debug.Log(itemCode);
        // Debug.Log(ItemIndexArray.arr[itemCode]);
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/ItemTable2.json");
        var itemData = JsonHelper.FromJson<ItemObject>(jsonString);
        itemObject = itemData[ItemIndexArray.arr[itemCode]];
        // Debug.Log("----");
        // Debug.Log(itemObject.Name);
        // Debug.Log("----");
    }

    public string FindName(int idx)
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/ItemTable2.json");
        var itemData = JsonHelper.FromJson<ItemObject>(jsonString);
        itemObject = itemData[ItemIndexArray.arr[idx]];
        return itemObject.Name;
    }

    public ItemObject FindItem(int idx)
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/ItemTable2.json");
        var itemData = JsonHelper.FromJson<ItemObject>(jsonString);
        itemObject = itemData[ItemIndexArray.arr[idx]];
        return itemObject;
    }

    public ItemObject FindItemToSeq(int idx)
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/ItemTable2.json");
        var itemData = JsonHelper.FromJson<ItemObject>(jsonString);
        return itemData[idx];
    }
}
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
    public Sprite ItemImage;
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
        return  UnityEngine.JsonUtility.ToJson(wrapper);
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
        Debug.Log("test");
        Debug.Log(itemCode);
        Debug.Log(ItemIndexArray.arr[itemCode]);
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/ItemTable2.json");
        var itemData = JsonHelper.FromJson<ItemObject>(jsonString);
        itemObject = itemData[ItemIndexArray.arr[itemCode]];
        LoadImage(itemCode);
        Debug.Log("----");
        Debug.Log(itemObject.Name);
        Debug.Log("----");
    }

    private void LoadImage(int idx)
    {
        byte[] byteTexture = System.IO.File.ReadAllBytes("C:/Users/SSAFY/Desktop/E1058/S07P31E105/E105/Assets/Data/Image/37002.png");
        Texture2D texture = new Texture2D(0,0);
        texture.LoadImage(byteTexture);

        Rect rect = new Rect(0,0, texture.width, texture.height);
        itemObject.ItemImage = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
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
}
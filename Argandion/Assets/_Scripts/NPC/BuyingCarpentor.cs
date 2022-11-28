using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Product
{
    public int Result;
    public int Cost;
}

[System.Serializable]
public class Products
{
    public List<Product> Pros;
}

public class BuyingCarpentor : MonoBehaviour
{
    public Product[] myData;
    public GameObject item;
    private UIManager ui;

    void Awake()
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/BuyingCarpentor.json");
        var buyDatas = JsonHelper.FromJson<Product>(jsonString);
        myData = buyDatas;
    }

    public void Buy(int idx, int howMany = 1)
    {
        if (UIManager._uimanagerInstance.getPlayerGold() >= myData[idx].Cost * howMany)
        {
            UIManager._uimanagerInstance.acquireItem(UIManager._uimanagerInstance.findItem(myData[idx].Result), howMany);
            UIManager._uimanagerInstance.addPlayerGold(myData[idx].Cost * howMany * -1);
        }
        else
        {
            UIManager._uimanagerInstance.OnResultNotificationPanel("잔액이 부족합니다.");
        }
    }
}
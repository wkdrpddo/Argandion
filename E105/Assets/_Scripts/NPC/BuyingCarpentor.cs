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
    public Inventory theInventory;
    public Product[] myData;
    public GameObject item;
    private UIManager ui;

    // Start is called before the first frame update
    void Start()
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/BuyingCarpentor.json");
        var buyDatas = JsonHelper.FromJson<Product>(jsonString);
        myData = buyDatas;

        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void Buy(int idx, int howMany = 1)
    {
        if (ui.getPlayerGold() > myData[idx].Cost * howMany)
        {
            theInventory.AcquireItem(item.GetComponent<Item>().FindItem(myData[idx].Result), howMany);
            ui.addPlayerGold(myData[idx].Cost * howMany * -1);
        }
        else
        {
            Debug.Log("돈없엉");
        }
    }
}
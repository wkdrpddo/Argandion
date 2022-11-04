using System.Collections; 
using System.Collections.Generic;
using System.IO;
using UnityEngine; 

public class BuyingSmith : MonoBehaviour
{
    public Inventory theInventory;
    public Product[] myData;
    public GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/BuyingSmith.json");
        var buyDatas = JsonHelper.FromJson<Product>(jsonString);
        myData = buyDatas;
    }

    public void Buy(int idx, int howMany = 1)
    {
        if (theInventory.gold > myData[idx].Cost * howMany) {
            theInventory.AcquireItem(item.GetComponent<Item>().FindItem(myData[idx].Result),howMany);
            theInventory.gold -= myData[idx].Cost * howMany;
        } else {
            Debug.Log("돈없엉");
        }
    }
}

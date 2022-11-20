using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BuyingSeed : MonoBehaviour
{
    public Product[] myData;
    public GameObject item;
    // private UIManager ui;

    // Start is called before the first frame update
    void Awake()
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/BuyingSeed.json");
        var buyDatas = JsonHelper.FromJson<Product>(jsonString);
        myData = buyDatas;

        // ui = GameObject.Find("UIManager").GetComponent<UIManager>();
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

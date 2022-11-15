using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BuyingSeed : MonoBehaviour
{
    public Product[] myData;
    public GameObject item;
    private UIManager ui;

    // Start is called before the first frame update
    void Start()
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/BuyingSeed.json");
        var buyDatas = JsonHelper.FromJson<Product>(jsonString);
        myData = buyDatas;

        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void Buy(int idx, int howMany = 1)
    {
        if (ui.getPlayerGold() >= myData[idx].Cost * howMany)
        {
            ui.acquireItem(ui.findItem(myData[idx].Result), howMany);
            ui.addPlayerGold(myData[idx].Cost * howMany * -1);
        }
        else
        {
            ui.OnResultNotificationPanel("잔액이 부족합니다.");
        }
    }
}

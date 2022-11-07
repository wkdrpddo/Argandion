using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TransactionPanel : MonoBehaviour
{
    /*
        1. 재단사 [포목점]
        2. 목수 [공방]
        3. 호수지기 [호수]
        4. 대장장이 [대장간]
        5. 목장지기
        6. 사냥꾼 [사냥꾼 오두막]
    */
    public GameObject storeItemCard;

    public string jsonStringBase;
    public string jsonInputString;
    public string jsonString;
    public ItemObject[] itemData;
    private ItemObject itemObject;

    void Start()
    {
        jsonStringBase = Application.dataPath + "/Data/Json";
    }

    public void OnPanel(int value)
    {
        gameObject.SetActive(true);
        setBuyPanelList(value);
    }

    private void setBuyPanelList(int value)
    {
        switch (value)
        {
            case 1:
                jsonInputString = "/BuyingDesigner.json";
                break;
            case 2:
                jsonInputString = jsonStringBase + "/BuyingCarpentor.json";
                break;
            case 3:
                jsonInputString = jsonStringBase + "/BuyingFisher.json";
                break;
            case 4:
                jsonInputString = jsonStringBase + "/BuyingSmith.json";
                break;
            case 5:
                // jsonInputString = jsonStringBase + "/BuyingDesigner.json";
                break;
            case 6:
                jsonInputString = jsonStringBase + "/BuyingHunter.json";
                break;
        }

        jsonString = File.ReadAllText(jsonStringBase + jsonInputString);
        itemData = JsonHelper.FromJson<ItemObject>(jsonString);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class buyingObject
{
    public int Result;
    public int Cost;
}

public class TransactionPanel : MonoBehaviour
{
    /*
        1. 재단사 [포목점]
        2. 목수 [공방]
        3. 호수지기 [호수]
        4. 대장장이 [대장간]
        6. 사냥꾼 [사냥꾼 오두막]
    */
    public GameObject storeItemCard;

    private GameObject ScrollContent;
    private UIManager ui;
    private Item _itemmanager;

    private string jsonStringBase;
    private string jsonInputString;
    private string jsonString;
    private buyingObject[] itemData;
    public buyingObject buyingObject;
    public ItemObject itemObject;

    void Start()
    {
        jsonStringBase = Application.dataPath + "/Data/Json";
        _itemmanager = GameObject.Find("ItemManager").GetComponent<Item>();
        ScrollContent = transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).gameObject;
        ui = gameObject.GetComponentInParent<UIManager>();
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
                jsonInputString = jsonStringBase + "/BuyingDesigner.json";
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
            case 6:
                jsonInputString = jsonStringBase + "/BuyingHunter.json";
                break;
        }

        jsonString = File.ReadAllText(jsonInputString);
        itemData = JsonHelper.FromJson<buyingObject>(jsonString);

        for (int i = 0; i < itemData.Length; i++)
        {
            buyingObject = itemData[i];
            itemObject = _itemmanager.FindItem(buyingObject.Result);

            GameObject productBtn = Instantiate(storeItemCard, ScrollContent.transform);
            RectTransform productBtnRect = productBtn.GetComponent<RectTransform>();

            Debug.Log(productBtnRect.anchoredPosition);
            productBtnRect.SetLocalPositionAndRotation(new Vector3(7.5f, -10 - i * 50, 0), ui.rotateZero);
            Debug.Log(productBtn.GetComponent<RectTransform>().position);

            productBtn.transform.GetChild(0).GetComponent<Image>().sprite = ui.getItemIcon(itemObject.ItemCode);
            productBtn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemObject.Name;
            productBtn.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = buyingObject.Cost.ToString();
            productBtn.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = itemObject.Desc;
        }
    }

    public void closeTransaction()
    {
        RectTransform[] selectObjectList = ScrollContent.GetComponentsInChildren<RectTransform>();
        for (int i = 1; i < selectObjectList.Length; i++)
        {
            if (selectObjectList[i] != ScrollContent.GetComponent<RectTransform>())
            {
                Destroy(selectObjectList[i].gameObject);
            }
        }

        gameObject.SetActive(false);
        ui.OnInventory(2);
    }
}

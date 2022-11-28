using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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

    [SerializeField] private GameObject ScrollContent;
    // [SerializeField] private UIManager ui;
    [SerializeField] private Item _itemmanager;

    private string jsonStringBase;
    private string jsonInputString;
    private string jsonString;
    private buyingObject[] itemData;
    public buyingObject buyingObject;
    public ItemObject itemObject;

    void Awake()
    {
        jsonStringBase = Application.dataPath + "/Data/Json";
        _itemmanager = GameObject.Find("ItemManager").GetComponent<Item>();
        ScrollContent = transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).gameObject;
        // ui = gameObject.GetComponentInParent<UIManager>();
    }

    public void handelPanel(int value)
    {
        if(value == -1) {
            return;
        }
        
        gameObject.SetActive(!gameObject.activeSelf);
        UIManager._uimanagerInstance.setIsOpenTransaction(gameObject.activeSelf);
        UIManager._uimanagerInstance.OnInventory(2);

        if (gameObject.activeSelf)
        {
            UIManager._uimanagerInstance.delayStopControllKeys();
            setBuyPanelList(value);
        }
        else
        {
            UIManager._uimanagerInstance.runControllKeys();
            UIManager._uimanagerInstance.conversationNPC = 0;
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

        handelPanel(-1);
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
            case 10:
                jsonInputString = jsonStringBase + "/BuyingSeed.json";
                break;
        }

        Debug.Log(jsonInputString);
        jsonString = File.ReadAllText(jsonInputString);
        itemData = JsonHelper.FromJson<buyingObject>(jsonString);

        for (int i = 0; i < itemData.Length; i++)
        {
            buyingObject = itemData[i];
            itemObject = _itemmanager.FindItem(buyingObject.Result);

            // Debug.Log("현재 아이템 데이터 : " + buyingObject.Result);

            GameObject productBtn = Instantiate(storeItemCard, ScrollContent.transform);

            productBtn.transform.GetChild(0).GetComponent<Image>().sprite = UIManager._uimanagerInstance.getItemIcon(itemObject.ItemCode);
            productBtn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemObject.Name;
            productBtn.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = buyingObject.Cost.ToString();
            productBtn.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = itemObject.Desc;

            int itemIdx = i;
            string name = itemObject.Name;
            int itemCode = itemObject.ItemCode;
            int buyCost = buyingObject.Cost;
            int pos = Array.IndexOf(multiBuyItemCode, buyingObject.Result);
            // Debug.Log(pos);
            if (pos == -1)
            {
                // Debug.Log("name : " + itemObject.Name + " | itemCode : " + itemObject.ItemCode);
                productBtn.GetComponent<Button>().onClick.AddListener(() => UIManager._uimanagerInstance.OnTransactionDoubleCheckPanel(name, value, itemIdx, itemCode));
            }
            else
            {
                productBtn.GetComponent<Button>().onClick.AddListener(() => UIManager._uimanagerInstance.OnTradeModal(name, itemCode.ToString(), 99, buyCost, 1, value, itemIdx));
            }
        }
    }

    private int[] multiBuyItemCode = new int[] { 4, 500, 501, 502, 503, 504, 212, 213, 214, 215, 216, 217, 218, 219 };
}

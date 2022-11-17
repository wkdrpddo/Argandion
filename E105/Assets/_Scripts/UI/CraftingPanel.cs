using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class combObject
{
    public int Result;
    public int Material1;
    public int Cost1;
    public int Material2;
    public int Cost2;
    public int Material3;
    public int Cost3;
    public int Material4;
    public int Cost4;
    public int Material5;
    public int Cost5;
    public int Material6;
    public int Cost6;
}

public class CraftingPanel : MonoBehaviour
{
    [SerializeField] private UIManager ui;
    public GameObject _craftItemButton;
    public GameObject _metarialsCard;
    [SerializeField] private GameObject _scrollcontent;
    [SerializeField] private GameObject _metarialsgrid;

    [SerializeField] private combObject[] itemData;
    [SerializeField] private int[] canMakeList;
    [SerializeField] private int index;
    [SerializeField] private int storeIdx;

    // Start is called before the first frame update
    void Start()
    {
        ui = gameObject.GetComponentInParent<UIManager>();
        _scrollcontent = transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).gameObject;
        _metarialsgrid = transform.GetChild(1).GetChild(1).gameObject;
    }

    public void handelPanel(int value)
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if (gameObject.activeSelf)
        {
            ui.stopControllKeys();
            ui.setIsOpenCraft(true);
            setCraftData(value);
            gameObject.transform.GetChild(1).GetChild(2).GetComponent<Button>().interactable = false;
        }
        else
        {
            ui.runControllKeys();
            ui.setIsOpenCraft(false);
            deleteMetarialList();
            deleteCraftList();
            gameObject.transform.GetChild(1).GetChild(2).GetComponent<Button>().interactable = true;
        }
    }

    public void onClickCraft()
    {
        bool isContainInventory;
        ItemObject craftItem = ui.findItem(itemData[index].Result);
        if (craftItem.Category == "장비" || craftItem.Category == "옷")
        {
            isContainInventory = ui.checkInventory(craftItem, 1);
        }
        else
        {
            isContainInventory = ui.checkInventory(craftItem, canMakeList[index]);
        }

        if (isContainInventory)
        {
            ui.OnTradeModal(craftItem.Name, craftItem.ItemCode.ToString(), canMakeList[index], -1, 2, storeIdx, index);
        }
        else
        {
            ui.OnResultNotificationPanel("인벤토리에 빈 공간이 없습니다.");
        }

        gameObject.transform.GetChild(1).GetChild(2).GetComponent<Button>().interactable = false;
    }

    private void setCraftData(int value)
    {
        storeIdx = value;
        deleteCraftList();

        string jsonInputString = Application.dataPath + "/Data/Json";
        switch (value)
        {
            case 1:
                jsonInputString += "/CombDesigner.json";
                gameObject.GetComponent<CombDesigner>().Hello();
                canMakeList = gameObject.GetComponent<CombDesigner>().getCanMakeList();
                break;
            case 2:
                jsonInputString += "/CombCarpentor.json";
                gameObject.GetComponent<CombCarpentor>().Hello();
                canMakeList = gameObject.GetComponent<CombCarpentor>().getCanMakeList();
                break;
            case 4:
                jsonInputString += "/CombSmith.json";
                gameObject.GetComponent<CombSmith>().Hello();
                canMakeList = gameObject.GetComponent<CombSmith>().getCanMakeList();
                break;
            case 6:
                jsonInputString += "/CombHunter.json";
                gameObject.GetComponent<CombHunter>().Hello();
                canMakeList = gameObject.GetComponent<CombHunter>().getCanMakeList();
                break;
        }

        string jsonString = File.ReadAllText(jsonInputString);
        itemData = JsonHelper.FromJson<combObject>(jsonString);

        for (int i = 0; i < itemData.Length; i++)
        {
            combObject combObj = itemData[i];
            ItemObject itemObject = ui.findItem(combObj.Result);

            GameObject craftBtn = Instantiate(_craftItemButton, _scrollcontent.transform);

            craftBtn.transform.GetChild(0).GetComponent<Image>().sprite = ui.getItemIcon(itemObject.ItemCode);
            craftBtn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemObject.Name;
            craftBtn.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = itemObject.Desc;

            int index = i;
            craftBtn.GetComponent<Button>().onClick.AddListener(() => setMetarialsList(index));
        }
    }

    private void setMetarialsList(int value)
    {
        if (canMakeList[value] <= 0)
        {
            gameObject.transform.GetChild(1).GetChild(2).GetComponent<Button>().interactable = false;
        }
        else
        {
            gameObject.transform.GetChild(1).GetChild(2).GetComponent<Button>().interactable = true;
        }

        index = value;

        deleteMetarialList();

        combObject combObj = itemData[value];
        if (combObj.Material1 == 0)
        {
            createNullCard();
        }
        else
        {
            createMetarialCard(combObj.Material1, combObj.Cost1);
        }

        if (combObj.Material2 == 0)
        {
            createNullCard();
        }
        else
        {
            createMetarialCard(combObj.Material2, combObj.Cost2);
        }
        if (combObj.Material3 == 0)
        {
            createNullCard();
        }
        else
        {
            createMetarialCard(combObj.Material3, combObj.Cost3);
        }
        if (combObj.Material4 == 0)
        {
            createNullCard();
        }
        else
        {
            createMetarialCard(combObj.Material4, combObj.Cost4);
        }
        if (combObj.Material5 == 0)
        {
            createNullCard();
        }
        else
        {
            createMetarialCard(combObj.Material5, combObj.Cost5);
        }
        if (combObj.Material6 == 0)
        {
            createNullCard();
        }
        else
        {
            createMetarialCard(combObj.Material6, combObj.Cost6);
        }
    }

    private void createMetarialCard(int material, int count)
    {
        GameObject metarialCard = Instantiate(_metarialsCard, _metarialsgrid.transform);
        ItemObject itemObj = ui.findItem(material);

        metarialCard.transform.GetChild(0).GetComponent<Image>().sprite = ui.getItemIcon(itemObj.ItemCode);
        metarialCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemObj.Name;
        metarialCard.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = count.ToString();
    }

    private void createNullCard()
    {
        GameObject metarialCard = Instantiate(_metarialsCard, _metarialsgrid.transform);
        metarialCard.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        metarialCard.transform.GetChild(0).gameObject.SetActive(false);
        metarialCard.transform.GetChild(1).gameObject.SetActive(false);
        metarialCard.transform.GetChild(2).gameObject.SetActive(false);
    }

    private void deleteMetarialList()
    {
        RectTransform[] metarialObjs = _metarialsgrid.GetComponentsInChildren<RectTransform>();
        for (int i = 1; i < metarialObjs.Length; i++)
        {
            if (metarialObjs[i] != _metarialsgrid.GetComponent<RectTransform>())
            {
                Destroy(metarialObjs[i].gameObject);
            }
        }
    }

    private void deleteCraftList()
    {
        RectTransform[] craftObjs = _scrollcontent.GetComponentsInChildren<RectTransform>();
        for (int i = 1; i < craftObjs.Length; i++)
        {
            if (craftObjs[i] != _scrollcontent.GetComponent<RectTransform>())
            {
                Destroy(craftObjs[i].gameObject);
            }
        }
    }

    public void syncCanMakeList()
    {
        switch (storeIdx)
        {
            case 1:
                canMakeList = gameObject.GetComponent<CombDesigner>().getCanMakeList();
                break;
            case 2:
                canMakeList = gameObject.GetComponent<CombCarpentor>().getCanMakeList();
                break;
            case 4:
                canMakeList = gameObject.GetComponent<CombSmith>().getCanMakeList();
                break;
            case 6:
                canMakeList = gameObject.GetComponent<CombHunter>().getCanMakeList();
                break;
        }

        deleteMetarialList();
    }
}

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
    private UIManager ui;
    public GameObject craftItemButton;
    private Item _itemmanager;
    private GameObject ScrollContent;

    public void closeWindow()
    {
        gameObject.SetActive(false);
        ui.runControllPlayer();
    }

    public void OnPanel(int value)
    {
        gameObject.SetActive(true);
        setCraftData(value);
    }

    private void setCraftData(int value)
    {
        string jsonInputString = Application.dataPath + "/Data/Json";
        switch (value)
        {
            case 1:
                jsonInputString += "/CombDesigner.json";
                break;
            case 2:
                jsonInputString += "/CombCarpentor.json";
                break;
            case 4:
                jsonInputString += "/CombSmith.json";
                break;
            case 6:
                jsonInputString += "/CombHunter.json";
                break;
        }

        string jsonString = File.ReadAllText(jsonInputString);
        combObject[] itemData = JsonHelper.FromJson<combObject>(jsonString);

        for (int i = 0; i < itemData.Length; i++)
        {
            combObject combObj = itemData[i];
            ItemObject itemObject = _itemmanager.FindItem(combObj.Result);

            GameObject craftBtn = Instantiate(craftItemButton, ScrollContent.transform);

            // craftBtn
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ui = gameObject.GetComponentInParent<UIManager>();
        _itemmanager = GameObject.Find("ItemManager").GetComponent<Item>();
        ScrollContent = transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

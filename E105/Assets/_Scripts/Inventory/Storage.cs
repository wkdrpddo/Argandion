using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ItemCodeToIndex;
using System.IO;

public class Storage : MonoBehaviour
{
    [SerializeField]
    private GameObject go_InventoryBase; // Inventory_Base 이미지
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot들의 부모인 Grid Setting 

    public Slot[] slots;  // 슬롯들 배열

    [SerializeField] private UIManager ui;

    void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();

        go_InventoryBase = transform.GetChild(0).gameObject;
        go_SlotsParent = go_InventoryBase.transform.GetChild(0).gameObject;
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();

        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/ItemTable2.json");
        ItemObject[] ogjs = JsonHelper.FromJson<ItemObject>(jsonString);

        for (int i = 0; i < 98; i++)
        {
            int idx = i;
            slots[i].setIdx(idx);
            slots[i].item = ogjs[i + 1];
        }
    }

    public int checkStorage(ItemObject _item, int _count)
    {
        int slotIndex = ItemIndexArray.arr[_item.ItemCode] - 1;
        return (slots[slotIndex].itemCount + _count) - 9999;
    }

    public void AcquireItem(ItemObject _item, int _count = 1)
    {
        int slotIndex = ItemIndexArray.arr[_item.ItemCode] - 1;
        slots[slotIndex].SetSlotCount(_count);

        if (slots[slotIndex].itemCount > 0)
        {
            slots[slotIndex].transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255);
            slots[slotIndex].transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            slots[slotIndex].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0);
            slots[slotIndex].transform.GetChild(1).gameObject.SetActive(false);
            slots[slotIndex].transform.GetChild(0).GetComponent<Image>().sprite = ui.getItemIcon(_item.ItemCode);
        }
    }
}

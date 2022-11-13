using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemCodeToIndex;

public class Storage : MonoBehaviour
{
    [SerializeField]
    private GameObject go_InventoryBase; // Inventory_Base 이미지
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot들의 부모인 Grid Setting 

    public Slot[] slots;  // 슬롯들 배열

    void Start()
    {
        go_InventoryBase = transform.GetChild(0).gameObject;
        go_SlotsParent = go_InventoryBase.transform.GetChild(0).gameObject;
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();

        for (int i = 0; i < 100; i++)
        {
            slots[i].setIdx(i);
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
    }
}

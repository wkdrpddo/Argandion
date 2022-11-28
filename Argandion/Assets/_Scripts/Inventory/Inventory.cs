using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject go_InventoryBase; // Inventory_Base 이미지
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot들의 부모인 Grid Setting 

    public Slot[] slots;  // 슬롯들 배열

    void Awake()
    {
        go_InventoryBase = transform.GetChild(0).gameObject;
        go_SlotsParent = go_InventoryBase.transform.GetChild(0).gameObject;
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();

        for (int i = 0; i < 25; i++)
        {
            slots[i].setIdx(i);
        }
    }

    public bool CheckInven(ItemObject _item, int _count = 1, bool _sec = false)
    {
        if (!_sec)
        {
            if (_item.Category != "장비" && _item.Category != "옷")
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].item != null)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
                    {
                        if (slots[i].item.Name == _item.Name && slots[i].itemCount < 99)
                        {
                            if (slots[i].itemCount + _count <= 99)
                            {
                                return true;
                            }
                            else
                            {
                                return CheckInven(_item, _count, true);
                            }
                        }
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemCount == 0)
            {
                return true;
            }

            if (i + 1 == slots.Length)
            {
                return false;
            }
        }
        return false;
    }

    public void AcquireItem(ItemObject _item, int _count = 1)
    {
        if (_item.Category != "장비" && _item.Category != "옷")
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
                {
                    if (slots[i].item.Name == _item.Name && slots[i].itemCount < 99)
                    {
                        if (slots[i].itemCount + _count <= 99)
                        {
                            slots[i].SetSlotCount(_count);
                            return;
                        }
                        else
                        {
                            int temp = slots[i].itemCount;
                            slots[i].SetSlotCount(99 - slots[i].itemCount);
                            AcquireItem(_item, _count + temp - 99);
                            return;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemCount == 0)
            {
                slots[i].AddItem(_item, _count);
                return;
            }

            if (i + 1 == slots.Length)
            {
                // Debug.Log("꽉찼음");
            }
        }
    }

    public void ReductItem(ItemObject _item, int _count = 1)
    {
        if (_item.Category != "장비" && _item.Category != "옷")
        {
            for (int i = slots.Length - 1; i >= 0; i--)
            {
                if (slots[i].item != null)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
                {
                    if (slots[i].item.Name == _item.Name && slots[i].itemCount <= 99)
                    {
                        if (slots[i].itemCount + _count <= 99)
                        {
                            slots[i].SetSlotCount(_count);
                            SyncBait(_item, slots[i].getSlotItemCount());
                            return;
                        }
                        else
                        {
                            int temp = slots[i].itemCount;
                            slots[i].SetSlotCount(99 - slots[i].itemCount);
                            AcquireItem(_item, _count + temp - 99);
                            return;
                        }
                    }
                }
            }
        }

        for (int i = slots.Length - 1; i >= 0; i--)
        {
            if (slots[i].itemCount == 0)
            {
                slots[i].AddItem(_item, _count);
                return;
            }

            if (i + 1 == slots.Length)
            {
                // Debug.Log("꽉찼음");
            }
        }
        
    }
    public void SyncBait(ItemObject _item, int _count){
        if (_item.ItemCode >= 502 && _item.ItemCode <= 504)
        {
            UIManager._uimanagerInstance.setPlayerQuickSlot(7, _item.ItemCode, _count);
            UIManager._uimanagerInstance._baseuipanel.transform.GetChild(3).GetChild(1).GetComponentInChildren<Slot>().AddItem(_item, _count);
        }
    }

    public int getLessBaitCount(int itemCode)
    {

        for (int i = slots.Length - 1; i >= 0; i--)
        {
            if (slots[i].item != null)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
            {
                if (slots[i].item.ItemCode == itemCode)
                {
                    return slots[i].itemCount;
                }
            }
        }

        return -1;
    }

    public ItemObject StoreItem(int idx, int _count = 1)
    {
        ItemObject returnedItem = slots[idx].item;
        if (_count != 0)
        {
            slots[idx].SetSlotCount(_count);
        }
        return returnedItem;
    }

    public void SellInventoryItem(int slotIdx, int count)
    {
        slots[slotIdx].SetSlotCount(count * -1);
    }

    public Slot[] getInventorySlots()
    {
        return slots;
    }
}

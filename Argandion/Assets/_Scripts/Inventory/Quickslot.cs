using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quickslot : MonoBehaviour
{
    [SerializeField]
    private GameObject go_InventoryBase; // Inventory_Base 이미지
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot들의 부모인 Grid Setting 

    public Slot[] slots;  // 슬롯들 배열
    // [SerializeField] private UIManager ui;

    void Awake()
    {
        go_InventoryBase = transform.GetChild(0).gameObject;
        go_SlotsParent = go_InventoryBase.transform.GetChild(0).gameObject;
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        // ui = GameObject.Find("UIManager").GetComponent<UIManager>();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].setIdx(i);
        }
    }

    // 인벤토리에 빈 공간이 있는지 확인
    public bool CheckInven(ItemObject _item, int _count = 1, bool _sec = false)
    {
        if (!_sec)
        {
            if (_item.Category != "장비")
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

    // 인벤토리에 아이템 추가
    public void AcquireItem(ItemObject _item, int _count = 1)
    {
        if (_item.Category != "장비")
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
                {
                    if (slots[i].item.Name == _item.Name && slots[i].itemCount < 99)
                    {
                        if (slots[i].itemCount + _count <= 99)
                        {
                            UIManager._uimanagerInstance.setPlayerQuickSlot(i, _item.ItemCode, slots[i].itemCount + _count);
                            slots[i].SetSlotCount(_count);

                            UIManager._uimanagerInstance.syncQuickSlot();
                            return;
                        }
                        else
                        {
                            int temp = slots[i].itemCount;
                            UIManager._uimanagerInstance.setPlayerQuickSlot(i, _item.ItemCode, 99);
                            slots[i].SetSlotCount(99 - slots[i].itemCount);
                            AcquireItem(_item, _count + temp - 99);

                            UIManager._uimanagerInstance.syncQuickSlot();
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
                UIManager._uimanagerInstance.setPlayerQuickSlot(i, _item.ItemCode, _count);
                slots[i].AddItem(_item, _count);

                UIManager._uimanagerInstance.syncQuickSlot();
                return;
            }

            if (i + 1 == slots.Length)
            {
                Debug.Log("꽉찼엉");
            }
        }
    }

    public void ReductItem(ItemObject _item, int _count = 1)
    {
        if (_item.Category != "장비")
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
                {
                    if (slots[i].item.Name == _item.Name && slots[i].itemCount <= 99)
                    {
                        if (slots[i].itemCount + _count <= 99)
                        {
                            slots[i].SetSlotCount(_count);

                            UIManager._uimanagerInstance.syncQuickSlot();
                            return;
                        }
                        else
                        {
                            int temp = slots[i].itemCount;
                            slots[i].SetSlotCount(99 - slots[i].itemCount);
                            AcquireItem(_item, _count + temp - 99);

                            UIManager._uimanagerInstance.syncQuickSlot();
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

                UIManager._uimanagerInstance.syncQuickSlot();
                return;
            }

            if (i + 1 == slots.Length)
            {
                Debug.Log("꽉찼엉");
            }
        }
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

    public void SellQuickslotItem(int slotIdx, int count)
    {
        if (slots[slotIdx].itemCount - count == 0)
        {
            UIManager._uimanagerInstance.setPlayerQuickSlot(slotIdx, 0, 0);
        }
        else
        {
            UIManager._uimanagerInstance.setPlayerQuickSlot(slotIdx, slots[slotIdx].item.ItemCode, slots[slotIdx].itemCount - count);
        }
        slots[slotIdx].SetSlotCount(count * -1);

        UIManager._uimanagerInstance.syncQuickSlot();
    }

    public Slot[] getInventorySlots()
    {
        return slots;
    }
}

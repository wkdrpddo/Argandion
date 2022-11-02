using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool invectoryActivated = false;  // 인벤토리 활성화 여부.

    [SerializeField]
    private GameObject go_InventoryBase; // Inventory_Base 이미지
    [SerializeField] 
    private GameObject go_SlotsParent;  // Slot들의 부모인 Grid Setting 

    public Slot[] slots;  // 슬롯들 배열

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    // void Update()
    // {
    //     TryOpenInventory();
    // }

    // private void TryOpenInventory()
    // {
    //     if(Input.GetKeyDown(KeyCode.I))
    //     {
    //         invectoryActivated = !invectoryActivated;

    //         if (invectoryActivated)
    //             OpenInventory();
    //         else
    //             CloseInventory();

    //     }
    // }

    // private void OpenInventory()
    // {
    //     go_InventoryBase.SetActive(true);
    // }

    // private void CloseInventory()
    // {
    //     go_InventoryBase.SetActive(false);
    // }

    public void AcquireItem(ItemObject _item, int _count = 1)
    {
        if(_item.Category != "장비")
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
                {
                    if (slots[i].item.Name == _item.Name && slots[i].itemCount < 99) {
                        if (slots[i].itemCount + _count <= 99 ) {
                            slots[i].SetSlotCount(_count);
                            return;
                        } else {
                            int temp = slots[i].itemCount;
                            slots[i].SetSlotCount( 99-slots[i].itemCount );
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

            if ( i + 1 == slots.Length) {
                Debug.Log("꽉찼엉");
            }
        }
    }

    public ItemObject StoreItem(int idx, int _count = 1)
    {   
        ItemObject returnedItem = slots[idx].item;
        if (_count != 0){
            slots[idx].SetSlotCount(_count);
        }
        return returnedItem;
    }
}

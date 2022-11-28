using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public static bool chestActivated = false;  // 상자 활성화 여부.

    [SerializeField]
    private GameObject go_ChestBase; // Chest_Base 이미지
    [SerializeField] 
    private GameObject go_ChestSlotsParent1;  // Slot들의 부모인 Grid Setting
    [SerializeField] 
    private GameObject go_ChestSlotsParent2;  // Slot들의 부모인 Grid Setting 
    [SerializeField] 
    private GameObject go_ChestSlotsParent3;  // Slot들의 부모인 Grid Setting 
    [SerializeField] 
    private GameObject go_ChestSlotsParent4;  // Slot들의 부모인 Grid Setting 
    [SerializeField] 
    private GameObject go_ChestSlotsParent5;  // Slot들의 부모인 Grid Setting 
    private ChestSlot[] slots;  // 슬롯들 배열

    void Start()
    {
        slots = go_ChestSlotsParent1.GetComponentsInChildren<ChestSlot>();
    }

    void Update()
    {
        Page1();
        Page2();
        Page3();
        Page4();
        Page5();
    }
    
    private void Page1()
    {   if(Input.GetKeyDown(KeyCode.Keypad1)) {
            slots = go_ChestSlotsParent1.GetComponentsInChildren<ChestSlot>();
        }
    }

    private void Page2()
    {
        if(Input.GetKeyDown(KeyCode.Keypad2)) {
            slots = go_ChestSlotsParent2.GetComponentsInChildren<ChestSlot>();
        }
    }

    private void Page3()
    {
        if(Input.GetKeyDown(KeyCode.Keypad3)) {
            slots = go_ChestSlotsParent3.GetComponentsInChildren<ChestSlot>();
        }
    }

    private void Page4()
    {
        if(Input.GetKeyDown(KeyCode.Keypad4)) {
            slots = go_ChestSlotsParent4.GetComponentsInChildren<ChestSlot>();
        }
    }

    private void Page5()
    {
        if(Input.GetKeyDown(KeyCode.Keypad5)) {
            slots = go_ChestSlotsParent5.GetComponentsInChildren<ChestSlot>();
        }
    }

    public void PutItem(ItemObject _item, int _count = 1) {
        for (int i = 0; i < slots.Length; i++ ) {
            if (slots[i].item != null) {
                if (slots[i].item.Name == _item.Name) {
                    slots[i].SetSlotCount(_count);
                    return;
                }
            }
        }

        for (int i = 0; i< slots.Length; i++) {
            if (slots[i].itemCount == 0) {
                slots[i].AddItem(_item, _count);
                return;
            }

            if (i+1 == slots.Length) {
                return;
            }
        }
    }

    public ItemObject TakeItem(int idx, int _count = 1)
    {
        slots[idx].SetSlotCount(_count);
        return slots[idx].item;
    }

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

            }
        }
    }
}

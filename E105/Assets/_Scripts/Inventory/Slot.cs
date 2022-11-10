using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int idx;
    public ItemObject item;
    public int itemCount;
    public Image itemImage;
    public Food foodManager;

    [SerializeField]
    private TextMeshProUGUI text_Count;
    // private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    private UIManager ui;

    void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        itemImage = gameObject.transform.GetChild(0).GetComponent<Image>();
        text_Count = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    // private void Update() {
    //     ItemUse();
    // }

    public void setIdx(int slotIdx)
    {
        idx = slotIdx;
    }

    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    public void AddItem(ItemObject _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        LoadImage(item.ItemCode);

        if (item.Category != "장비" && _item.Category != "옷")
        {
            // go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
            text_Count.gameObject.SetActive(true);
        }
        else
        {
            text_Count.text = "0";
            text_Count.gameObject.SetActive(false);
            // go_CountImage.SetActive(false);
        }

        SetColor(1);
    }

    private void LoadImage(int idx)
    {
        Sprite iconImg = ui.getItemIcon(idx);
        itemImage.sprite = iconImg;
    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        text_Count.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 좌클릭 시, 상점이 열려있는 경우
        if (eventData.button == PointerEventData.InputButton.Left && ui.getIsOpenTransaction() && itemCount != 0)
        {
            ui.OnTradeModal(item.Name, item.ItemCode.ToString(), itemCount, item.SellCost, 0, -1, idx);
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item.ItemCode == 0)
            {
                Debug.Log("아이템이 업ㅇ서용");
            }
            else
            {
                if (item.Category == "식량")
                {
                    foodManager.UseFood(item.ItemCode);
                    SetSlotCount(-1);
                }
            }
            // if (item != null)
            // {
            //     if(item.itemType == Item.ItemType.Equipment)
            //     {
            //         // 장착
            //         StartCoroutine(theWeaponManager.ChangeWeaponCoroutine(item.weaponType, item.itemName));
            //     }
            //     else
            //     {
            //         // 소비
            //         Debug.Log(item.itemName + " 을 사용했습니다.");
            //         SetSlotCount(-1);
            //     }
            // }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ui.getIsOpenTransaction() && itemCount != 0)
        {
            Vector3 vec3 = Input.mousePosition - new Vector3(2, -2, 0);
            string _text = item.Name + "\n가격 : " + item.SellCost;
            ui.onSlotOverModal(_text, vec3);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.offSlotOverModal();
    }
}

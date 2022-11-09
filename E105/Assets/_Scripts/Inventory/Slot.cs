using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public int idx;
    public ItemObject item;
    public int itemCount;
    public Image itemImage;
    public Food foodManager;

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    // private void Update() {
    //     ItemUse();
    // }

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

        if (item.Category != "장비")
        {
            // go_CountImage.SetActive(true);
            // text_Count.text = itemCount.ToString();
        }
        else
        {
            // text_Count.text = "0";
            // go_CountImage.SetActive(false);
        }

        SetColor(1);
    }

    private void LoadImage(int idx)
    {
        int sdfs = 37002;
        byte[] byteTexture = System.IO.File.ReadAllBytes(Application.dataPath + $"/Data/Image/{sdfs}.png");
        Texture2D texture = new Texture2D(0, 0);
        texture.LoadImage(byteTexture);

        Rect rect = new Rect(0, 0, texture.width, texture.height);
        itemImage.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        // text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        // text_Count.text = "0";
        // go_CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
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
}

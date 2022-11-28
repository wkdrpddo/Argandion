using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour
{
    public int idx;
    public ItemObject item;
    public int itemCount;
    public Image itemImage;

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

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

        if(item.Category != "장비")
        {
            // go_CountImage.SetActive(true);
            // text_Count.text = itemCount.ToString();
        }
        else
        {
            // text_Count.text = "0";
            // go_CountImage.SetActive(false);
        }

    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        if (itemCount <= 0)
            ClearSlot();
    }

    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
    }
}

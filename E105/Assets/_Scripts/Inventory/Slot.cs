using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
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
        LoadImage(item.ItemCode);

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

        SetColor(1);
    }

    private void LoadImage(int idx)
    {
        byte[] byteTexture = System.IO.File.ReadAllBytes("C:/Users/SSAFY/Desktop/E1058/S07P31E105/E105/Assets/Data/Image/37002.png");
        Texture2D texture = new Texture2D(0,0);
        texture.LoadImage(byteTexture);

        Rect rect = new Rect(0,0, texture.width, texture.height);
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
}

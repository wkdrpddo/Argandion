using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ItemCodeToIndex;

public class DroppedItem : MonoBehaviour
{
    private Rigidbody rigid;
    private Transform trans;
    private BoxCollider box;
    public int itemCode;
    public ItemObject itemObject;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();
        rigid.AddForce(Vector3.up * 1.5f, ForceMode.Impulse);
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/ItemTable2.json");
        var itemData = JsonHelper.FromJson<ItemObject>(jsonString);
        itemObject = itemData[ItemIndexArray.arr[itemCode]];


    }
}

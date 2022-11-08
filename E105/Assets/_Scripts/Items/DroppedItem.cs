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

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();
        rigid.AddForce(Vector3.up * 1.5f, ForceMode.Impulse);
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/ItemTable2.json");
        var itemData = JsonHelper.FromJson<ItemObject>(jsonString);
        itemObject = itemData[ItemIndexArray.arr[itemCode]];
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 100 * Time.deltaTime);
        if (trans.position.y < 0.1f)
        {
            trans.position = new Vector3(trans.position.x, 0.1f, trans.position.z);
        }
    }

}

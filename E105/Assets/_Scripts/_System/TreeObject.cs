using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : MonoBehaviour
{
    public int _days;
    public float _health;
    public bool _isFallen;
    public BuffManager _buff;
    public GameObject[] _droppedItem = new GameObject[3];

    public GameObject[] _trees = new GameObject[8];
    public Collider _box;
    public GameObject _itemParent;
    private SectorObject _sector;
    void Start()
    {
        _buff = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        _itemParent = GameObject.Find("Items");
        _sector = gameObject.transform.parent.gameObject.transform.parent.GetComponent<SectorObject>();
    }
    public void DayEnd()
    {
        _days += 1;
        if (!_isFallen && _days >= 4)
        {
            int rnd = Random.Range(0,1);
            Instantiate(_trees[rnd],new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+3f,gameObject.transform.position.z),new Quaternion());
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == 0)
        {
            Debug.Log("충돌 판정");
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            _box.gameObject.SetActive(true);
        }
    }

    public void Damaged(float damage)
    {
        _health -= damage;
        if (_health <=0 )
        {
            DestroyWood();
        }
    }

    private void DestroyWood()
    {
        Transform trans = gameObject.transform;
        _sector._tree_remain -= 1;
        Destroy(this.gameObject);
        int many = Random.Range(1,5);
        for (int i=0;i<many;i++) {
            Instantiate(_droppedItem[0], trans.position + new Vector3(Random.Range(-2f,2f),3.5f,Random.Range(-2f,2f)), trans.rotation, _itemParent.transform);
        }
        int many2 = Random.Range(2,4);
        int many3 = Random.Range(2,4);
        if ( _buff.orangePray ) {
            many2 += 1;
            many3 += 1;
        }
        for (int i=0;i<many2;i++) {
            Instantiate(_droppedItem[1], trans.position + new Vector3(Random.Range(-2f,2f),3.5f,Random.Range(-2f,2f)), trans.rotation, _itemParent.transform);
        }
        for (int i=0;i<many3;i++) {
            Instantiate(_droppedItem[2], trans.position + new Vector3(Random.Range(-2f,2f),3.5f,Random.Range(-2f,2f)), trans.rotation, _itemParent.transform);
        }
    }
}

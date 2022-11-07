using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : MonoBehaviour
{
    public int _days;
    public float _health;
    public bool _isFallen;

    public GameObject[] _trees = new GameObject[8];
    public Collider _box;
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
        Debug.Log(other.gameObject.layer);
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
        Destroy(this.gameObject);
    }
}

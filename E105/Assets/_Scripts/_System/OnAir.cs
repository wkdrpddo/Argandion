using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAir : MonoBehaviour
{
    public PlayerSystem _playerSystem;
    void Start()
    {
        _playerSystem = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
    }
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("플레이어 Enter");
        if (other.gameObject.layer==0 || other.gameObject.layer==10)
        {
            _playerSystem._onAir += 1;
        }
    }

    void OnCollisionExit(Collision other)
    {
        Debug.Log("플레이어 Exit");
        if (other.gameObject.layer==0 || other.gameObject.layer==10)
        {
            _playerSystem._onAir -= 1;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("플레이어 Enter!");
        if (other.gameObject.layer==0 || other.gameObject.layer==10)
        {
            Debug.Log("ww");
            _playerSystem._onAir += 1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("플레이어 Exit!");
        if (other.gameObject.layer==0 || other.gameObject.layer==10)
        {
            _playerSystem._onAir -= 1;
        }
    }
}

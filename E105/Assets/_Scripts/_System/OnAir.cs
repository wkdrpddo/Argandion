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
    // void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.layer == 0 || other.gameObject.layer == 10)
    //     {
    //         _playerSystem._onAir += 1;
    //     }
    // }

    // void OnCollisionExit(Collision other)
    // {
    //     if (other.gameObject.layer == 0 || other.gameObject.layer == 10)
    //     {
    //         _playerSystem._onAir -= 1;
    //     }
    // }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.layer == 0 || other.gameObject.layer == 10)
    //     {
    //         _playerSystem._onAir += 1;
    //     }
    // }

    void OnTriggerExit(Collider other)
    {
        _playerSystem._onAir = 0;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 0 || other.gameObject.layer == 10)
        {
            _playerSystem._onAir = 1;
        }
    }
}

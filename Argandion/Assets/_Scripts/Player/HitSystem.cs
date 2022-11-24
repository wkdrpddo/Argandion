using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSystem : MonoBehaviour
{
    public int type;
    public PlayerSystem _ps;
    public float damage;
    
    void Start()
    {
        _ps = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
    }
    // void OnTriggerEnter(Collider other)
    // {
    //     if (type==2 && other.gameObject.layer == 7)
    //     {
    //         Debug.Log("추웅도올");
    //         other.GetComponent<TreeObject>().Damaged(damage);
    //     }

    //     if (type==3 && other.gameObject.tag == "Ore")
    //     {
    //         other.GetComponent<OreObject>().Damaged(damage);
    //     }
    // }
}

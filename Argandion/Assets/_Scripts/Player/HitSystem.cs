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
}

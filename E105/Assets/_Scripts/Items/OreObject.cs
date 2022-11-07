using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreObject : MonoBehaviour
{
    public float _health;
    // Start is called before the first frame update
    public void DayEnd()
    {
        Destroy(gameObject);
    }

    public void Damaged(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

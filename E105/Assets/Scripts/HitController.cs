using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rabbit")
        {
            Debug.Log("토끼 충돌");
            other.transform.GetComponent<Rabbit>().Damage(1, transform.position);
        }
        else if (other.tag == "Deer")
        {
            Debug.Log("순록 충돌");
            other.transform.GetComponent<Deer>().Damage(1, transform.position);
        }
        else if (other.tag == "Wolf")
        {
            Debug.Log("늑대 충돌");
            other.transform.GetComponent<Wolf>().Damage(1);
        }
        else if (other.tag == "Bear")
        {
            Debug.Log("곰 충돌");
            other.transform.GetComponent<Bear>().Damage(1);
        }


    }
}

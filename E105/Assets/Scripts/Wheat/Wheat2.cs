using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat2 : MonoBehaviour
{
    float nextPhaseTime = 10f;
    float updateTime = 0.0f;
    bool onWet;
    public GameObject wheat3;
    public Transform wheat2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onWet) {
            if(updateTime > nextPhaseTime)
            {
                Instantiate(wheat3, wheat2.position, wheat2.rotation);
                Destroy(gameObject);
                Debug.Log("다 자랐다!");
            }
            else
            {
                updateTime += Time.deltaTime;
            }
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("wateredDirt"))
        {
            onWet = true;
        }
        else if (other.gameObject.CompareTag("dirt"))
        {
            onWet = false;
        }
    }
}

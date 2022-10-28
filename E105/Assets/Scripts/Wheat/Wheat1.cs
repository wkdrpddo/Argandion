using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat1 : MonoBehaviour
{
    float nextPhaseTime = 10f;
    float updateTime = 0.0f;
    bool onWet;
    public GameObject wheat2;
    public Transform wheat1;

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
                Instantiate(wheat2, wheat1.position, wheat1.rotation);
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

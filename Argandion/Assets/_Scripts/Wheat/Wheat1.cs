using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat1 : MonoBehaviour
{
    float nextPhaseDay = 2;
    float updateDay = 0;
    bool onWet;
    public GameObject wheat2;
    public Transform wheat1;
    public GameObject _buffManagerObject;
    private BuffManager _buffManager;

    void Start()
    {
        _buffManagerObject = GameObject.Find("BuffManager");
        _buffManager = _buffManagerObject.GetComponent<BuffManager>();
    }

    public void growUp()
    {
        if (onWet) {
            updateDay +=1;
            if (updateDay >= nextPhaseDay) {
                Instantiate(wheat2, wheat1.position, wheat1.rotation);
                Destroy(gameObject);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat2 : MonoBehaviour
{
    float nextPhaseDay = 2;
    float updateDay = 0;
    bool onWet;
    public GameObject wheat3;
    public Transform wheat2;
    public GameObject _buffManagerObject;
    private BuffManager _buffManager;

    // Start is called before the first frame update
    void Start()
    {
        _buffManagerObject = GameObject.Find("BuffManager");
        _buffManager = _buffManagerObject.GetComponent<BuffManager>();
    }

    // Update is called once per frame
    public void growUp()
    {
        if (onWet) {
            updateDay +=1;
            if (updateDay >= nextPhaseDay) {
                Instantiate(wheat3, wheat2.position, wheat2.rotation);
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

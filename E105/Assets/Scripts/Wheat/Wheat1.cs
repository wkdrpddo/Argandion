using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat1 : MonoBehaviour
{
    float nextPhaseTime = 3f;
    float updateTime = 0.0f;
    bool onWet;
    public GameObject wheat2;
    public Transform wheat1;
    public GameObject _buffManagerObject;
    private BuffManager _buffManager;

    // Start is called before the first frame update
    void Start()
    {
        _buffManagerObject = GameObject.Find("BuffManager");
        _buffManager = _buffManagerObject.GetComponent<BuffManager>();
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
                if (_buffManager.whiteSpirit) {
                    Debug.Log("버프 적용중!");
                }
                updateTime += Time.deltaTime * (_buffManager.whiteSpirit ? 1.2f : 1);
                Debug.Log(updateTime);
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

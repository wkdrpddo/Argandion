using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarBridgeFx : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        FxExit();
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        Invoke("FxExit",2f);
    }

    private void FxExit(){
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }
}

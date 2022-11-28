using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarBridgeFx : MonoBehaviour
{
    // 제단 다리 fx 조건에 맞게 끄고 켜는 스크립트

    // 위치 들어감
    void OnTriggerEnter(Collider other) {
        FxExit(); // 효과 끄기
        // 효과 켜기
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        Invoke("FxExit",2f); // 효과 끄기
    }

    // 위치 벗어남
    private void FxExit(){
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }
}

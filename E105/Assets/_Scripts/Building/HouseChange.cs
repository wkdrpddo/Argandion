using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseChange : MonoBehaviour
{
    private SystemManager _systemManager;

    void Start()
    {
        _systemManager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
    }

    // 발전도 변하면 call하기
    public void ChangeHouse()
    {
        int devLevel = _systemManager.getDevelopLevel();
        gameObject.transform.GetChild(devLevel - 2).gameObject.SetActive(false);
        gameObject.transform.GetChild(devLevel - 1).gameObject.SetActive(true);
    }
}

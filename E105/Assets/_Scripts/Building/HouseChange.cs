using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseChange : MonoBehaviour
{
    public SystemManager _systemManager;

    void Start()
    {
        _systemManager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
    }

    // ==================================================== 지울 위치 (시작) ==================================

    // 테스트용 변수
    public bool _devLevelUpTest;
    int tmp = 0;

    // 테스트용 update();
    void Update()
    {
        if (_devLevelUpTest)
        {
            Debug.Log("devLevelUp");
            _devLevelUpTest = false; // 테스트용 변수
            ChangeHouseTest(++tmp);
        }

    }

    // 발전도 변하면 call하기
    public void ChangeHouseTest(int devLevel)
    {
        gameObject.transform.GetChild(devLevel - 1).gameObject.SetActive(false);
        gameObject.transform.GetChild(devLevel).gameObject.SetActive(true);
    }

    // ==================================================== 지울 위치 (끝) ==================================


    // 발전도 변하면 call하기
    public void ChangeHouse()
    {
        int devLevel = _systemManager._development_level;
        gameObject.transform.GetChild(devLevel - 1).gameObject.SetActive(false);
        gameObject.transform.GetChild(devLevel).gameObject.SetActive(true);
    }
}

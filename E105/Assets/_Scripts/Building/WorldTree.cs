using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTree : MonoBehaviour
{
    private SystemManager _systemManager;
    private UIManager _uiManager;
    private PlayerSystem _playerSystem;

    [SerializeField] private GameObject[] _teleportOutside = new GameObject[2]; // 시작 전 넣어주기 // 텔포할 곳

    void Start()
    {
        _systemManager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _playerSystem = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
    }
    // 테스트 코드 ========================================================
    // public bool _go0;

    // private void Update() {
    //     if(_go0){
    //         doTeleport(0);
    //         _go0 = false;
    //     }
    // }
    // ==================================================================

    public void Interaction()
    {
        _uiManager.OnConversationPanel(9);
    }

    // '발전도' 변하면 call하기
    public void ChangeTreeLevel()
    {
        int devLevel = _systemManager.getDevelopLevel() - 1; // 변한 발전도
        int season = _systemManager._season; // 변한 계절
        gameObject.transform.GetChild(devLevel - 1).gameObject.SetActive(false);
        gameObject.transform.GetChild(devLevel).gameObject.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(devLevel).gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        gameObject.transform.GetChild(devLevel).gameObject.transform.GetChild(season).gameObject.SetActive(true);
    }

    // '계절' 변하면 call하기
    public void ChangeSeason()
    {
        int devLevel = _systemManager.getDevelopLevel() - 1; // 변한 발전도 
        int season = _systemManager._season; ; // 변한 계절
        Transform worldtree = gameObject.transform.GetChild(devLevel);
        if (season == 0)
        {
            worldtree.transform.GetChild(3).gameObject.SetActive(false);
            worldtree.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            worldtree.transform.GetChild(season - 1).gameObject.SetActive(false);
            worldtree.transform.GetChild(season).gameObject.SetActive(true);
        }

    }

    // 세계수 텔레포트 코드
    public void doTeleport(int num)
    {
        GameObject tp = _teleportOutside[num];
        tp.transform.GetChild(0).gameObject.SetActive(true);
        _playerSystem.transform.GetChild(0).gameObject.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        _playerSystem.transform.position = new Vector3(tp.transform.position.x, tp.transform.position.y, tp.transform.position.z);
        StartCoroutine("FxDelay");
    }

    // fx 딜레이
    IEnumerator FxDelay(){
        _playerSystem._canMove = false;
        // 사운드 변경
        yield return new WaitForSeconds(2.0f);
        _playerSystem._canMove = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAltar : MonoBehaviour
{
    private SoundManager _soundManager;
    private PlayerSystem _playerSystem;
    private UIManager _uiManager;
    private SectorObject _sector8; 
    [SerializeField] private GameObject _teleportUp; // 시작 전 넣어주기
    [SerializeField] private GameObject _teleportDown; // 시작 전 넣어주기
    [SerializeField] private GameObject _fxPtD;
    [SerializeField] private GameObject _fxPtU;

    void Start()
    {
        _playerSystem = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _sector8 = GameObject.Find("Sector8").GetComponent<SectorObject>();
        _fxPtD = _teleportDown.transform.GetChild(0).gameObject;
        _fxPtU = _teleportUp.transform.GetChild(0).gameObject;

    }

    // 테스트 코드
    public bool _test;

    private void Update() {
        if(_test){
            goUp();
            _test = false;
        }
    }


    // Enter 했을 때 fx 넣기?

    public void Interaction()
    {
        _uiManager.OnConversationPanel(11);
    }

    // 위로 올라갈 때 call
    public void goUp()
    {
        // 이동 fx
        fxD(true);
        _fxPtD.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
        // StartCoroutine("FxDelayGoUpDepart");
    }

    // 밑으로 내려갈 때 call
    private void goDown()
    {
        _playerSystem.transform.position = new Vector3(_teleportDown.transform.position.x, _teleportDown.transform.position.y, _teleportDown.transform.position.z);
        if(_sector8._purifier){
            _soundManager.playBGM1();
        }else{
            _soundManager.playBGM2();
        }
    }

    // fx 딜레이
    IEnumerator FxDelayGoUpDepart(){
        yield return new WaitForSeconds(2.0f);
        // Down쪽 fx 되돌리기
        fxD(false);
        // Up쪽 fx 하기
        fxU(true);
        // 이동
        _playerSystem.transform.position = new Vector3(_teleportUp.transform.position.x, _teleportUp.transform.position.y+1f, _teleportUp.transform.position.z);
        // _soundManager.playBGM4();
        
        StartCoroutine("FxDelayGoUpArrival");
    }

    IEnumerator FxDelayGoUpArrival(){
        _playerSystem._canMove = false;
        yield return new WaitForSeconds(3.0f);
        _playerSystem._canMove = true;
        // Up쪽 fx 되돌리기
        fxU(false);
    }

    private void fxD(bool b){
        _fxPtD.transform.GetChild(0).gameObject.SetActive(b);
        _fxPtD.transform.GetChild(1).gameObject.SetActive(b);
        _fxPtD.transform.GetChild(2).gameObject.SetActive(b);
    }
    private void fxU(bool b){
        _fxPtU.transform.GetChild(0).gameObject.SetActive(b);
        _fxPtU.transform.GetChild(1).gameObject.SetActive(b);
        _fxPtU.transform.GetChild(2).gameObject.SetActive(b);
    }



}

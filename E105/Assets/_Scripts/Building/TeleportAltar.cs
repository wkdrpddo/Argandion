using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAltar : MonoBehaviour
{
    private SoundManager _soundManager;
    private PlayerSystem _playerSystem;
    [SerializeField] private UIManager _uiManager;
    private SectorObject _sector8; 
    [SerializeField] private GameObject _teleportUp; // 시작 전 넣어주기
    [SerializeField] private GameObject _teleportDown; // 시작 전 넣어주기
    private GameObject _fxPtD;
    private GameObject _fxPtU;

    void Start()
    {
        _playerSystem = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _sector8 = GameObject.Find("Sector8").GetComponent<SectorObject>();
        _fxPtD = _teleportDown.transform.GetChild(0).gameObject;
        _fxPtU = _teleportUp.transform.GetChild(0).gameObject;

    }

    // 테스트 코드 ========================================================
    // public bool _goup;
    // public bool _godown;

    // private void Update() {
    //     if(_goup){
    //         goUp();
    //         _goup = false;
    //     }if(_godown){
    //         goDown();
    //         _godown = false;
    //     }
    // }
    // ==================================================================

    public void Interaction()
    {
        Debug.Log("인터렉션");
        // 게임오브젝트 == 으로 비교 가능?
        if(gameObject == _teleportDown){ // 올라가기
            Debug.Log("올라가기");
            _uiManager.OnConversationPanel(11);
        }else if(gameObject == _teleportUp){ // 내려가기
            Debug.Log("내려가기");
            _uiManager.OnConversationPanel(12);
        }
    }

    // 위로 올라갈 때 call
    public void goUp()
    {
        // 플레이어
        _playerSystem.transform.GetChild(0).gameObject.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        _playerSystem.transform.position = new Vector3(_teleportDown.transform.position.x, _playerSystem.transform.position.y, _teleportDown.transform.position.z);
        // 이동 fx
        fxD(true);
        StartCoroutine("FxDelayGoUp");
    }

    // 밑으로 내려갈 때 call
    public void goDown()
    {
        // 플레이어
        _playerSystem.transform.GetChild(0).gameObject.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        _playerSystem.transform.position = new Vector3(_teleportUp.transform.position.x, _playerSystem.transform.position.y, _teleportUp.transform.position.z);
        // 이동 fx
        fxU(true);
        StartCoroutine("FxDelayGoDown");
    }

    // Down -> Up fx 딜레이
    IEnumerator FxDelayGoUp(){
        yield return new WaitForSeconds(2.0f);
        // Up쪽 fx 하기
        _fxPtU.transform.GetChild(2).gameObject.SetActive(true);
        _playerSystem._canMove = false;
        yield return new WaitForSeconds(0.5f);
        // 이동
        Debug.Log(_teleportUp.transform.position.y);
        _playerSystem.transform.position = new Vector3(_teleportUp.transform.position.x, _teleportUp.transform.position.y, _teleportUp.transform.position.z);
        // 사운드
        // _soundManager.playBGM4();
        // fx 되돌리기
        yield return new WaitForSeconds(1.3f);
        _playerSystem._canMove = true;
        fxD(false);
        fxU(false);
    }

    // Up -> Down fx 딜레이
    IEnumerator FxDelayGoDown(){
        yield return new WaitForSeconds(2.0f);
        // Down쪽 fx 하기
        _fxPtD.transform.GetChild(2).gameObject.SetActive(true);
        _playerSystem._canMove = false;
        yield return new WaitForSeconds(0.5f);
        // 이동
        Debug.Log(_teleportDown.transform.position.y);
        _playerSystem.transform.position = new Vector3(_teleportDown.transform.position.x, _teleportDown.transform.position.y, _teleportDown.transform.position.z);
        // 사운드
        // if(_sector8._purifier){
        //     _soundManager.playBGM1();
        // }else{
        //     _soundManager.playBGM2();
        // }
        // fx 되돌리기
        yield return new WaitForSeconds(1.3f);
        _playerSystem._canMove = true;
        fxD(false);
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

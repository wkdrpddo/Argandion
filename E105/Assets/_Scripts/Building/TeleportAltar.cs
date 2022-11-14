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

    void Start()
    {
        _playerSystem = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _sector8 = GameObject.Find("Sector8").GetComponent<SectorObject>();
    }

    // Enter 했을 때 fx 넣기?

    public void Interaction()
    {
        _uiManager.OnConversationPanel(11);
    }

    // 위로 올라갈 때 call
    public void goUp()
    {
        _playerSystem.transform.position = new Vector3(_teleportUp.transform.position.x, _teleportUp.transform.position.y, _teleportUp.transform.position.z);
        _soundManager.playBGM4();
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


}

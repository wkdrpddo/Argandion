using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAltar : MonoBehaviour
{
    private PlayerSystem _playerSystem;
    private UIManager _uiManager;
    [SerializeField] private GameObject _teleportUp; // 시작 전 넣어주기
    [SerializeField] private GameObject _teleportDown; // 시작 전 넣어주기

    void Start()
    {
        _playerSystem = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
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
    }

    // 밑으로 내려갈 때 call
    private void goDown()
    {
        _playerSystem.transform.position = new Vector3(_teleportDown.transform.position.x, _teleportDown.transform.position.y, _teleportDown.transform.position.z);
    }


}

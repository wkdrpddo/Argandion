using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationHome : MonoBehaviour
{
    public PlayerSystem _playerSystem;
    public UIManager _uiManager;
    public GameObject _teleportHomeInside;
    public GameObject _teleportHomeOutside;


    void Start()
    {
        _playerSystem = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _teleportHomeInside = GameObject.Find("teleportHomeInside");
        _teleportHomeOutside = GameObject.Find("teleportHomeOutside");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("트리거");
        if (gameObject == _teleportHomeInside && other.gameObject.layer == 3)
        {
            goOutside();
        }
    }

    public void Interaction()
    {
        // Debug.Log("인터렉션");
        goInside();
    }

    private void goInside()
    {
        // Debug.Log("안으로");
        _playerSystem.transform.position = new Vector3(_teleportHomeInside.transform.position.x, _teleportHomeInside.transform.position.y, _teleportHomeInside.transform.position.z + 1f);
        // _uiManager.setIsHome(true);
    }

    private void goOutside()
    {
        // Debug.Log("밖으로");
        _playerSystem.transform.position = _teleportHomeOutside.transform.position;
        _playerSystem.transform.GetChild(0).gameObject.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        // _uiManager.setIsHome(false);
    }
}




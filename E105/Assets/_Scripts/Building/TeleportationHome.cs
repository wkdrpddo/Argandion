using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationHome : MonoBehaviour
{
    public PlayerSystem _playerSystem;
    public UIManager _uiManager;
    public GameObject _teleportHomeInside;
    public GameObject _teleportHomeOutside;
    public GameObject _directionalLight;
    public GameObject _homeLight;

    private bool _isInside;


    void Start()
    {
        _playerSystem = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
        _directionalLight = GameObject.Find("Directional Light");
        _homeLight = GameObject.Find("Home Light");
        _teleportHomeInside = GameObject.Find("teleportHomeInside");
        _teleportHomeOutside = GameObject.Find("teleportHomeOutside");
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("트리거");
        if (gameObject == _teleportHomeInside && other.gameObject.layer == 3)
        {
            goOutside();
        }
    }

    public void Interaction()
    {
        Debug.Log("인터렉션");
        goInside();
    }

    private void goInside()
    {
        // Debug.Log("안으로");
        _isInside = true;
        // 집 빛 켜주기, 밖 빛 꺼주기
        setLight();
        // 플레이어 이동
        _playerSystem.transform.position = new Vector3(_teleportHomeInside.transform.position.x, _teleportHomeInside.transform.position.y, _teleportHomeInside.transform.position.z + 1f);
        // '안으로' 메시지 보내기
        _uiManager.setIsHome(_isInside);

    }

    private void goOutside()
    {
        // Debug.Log("밖으로");
        _isInside = false;
        // 집 빛 꺼주기, 밖 빛 켜주기
        setLight();
        // Directional Light
        // 플레이어 이동
        _playerSystem.transform.position = _teleportHomeOutside.transform.position;
        _playerSystem.transform.GetChild(0).gameObject.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        // '밖으로' 메시지 보내기
        _uiManager.setIsHome(!_isInside);
    }
    private void setLight()
    {
        _homeLight.SetActive(_isInside);
        _directionalLight.SetActive(!_isInside);
    }
}




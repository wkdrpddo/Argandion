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
    public WeatherManager _weatherManager;
    public SystemManager _systemManager;
    private bool _isInside;
    public GameObject _particleFX;

    void Start()
    {
        _playerSystem = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
        _directionalLight = GameObject.Find("Directional Light");
        // _homeLight = GameObject.Find("Home Light");
        _teleportHomeInside = GameObject.Find("teleportHomeInside");
        _teleportHomeOutside = GameObject.Find("teleportHomeOutside");
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _weatherManager = GameObject.Find("WeatherManager").GetComponent<WeatherManager>();
        _systemManager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        _particleFX = GameObject.Find("PlayerObject").transform.GetChild(1).transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("트리거");
        if (gameObject == _teleportHomeInside && other.gameObject.layer == 3)
        {
            var camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            camera.clearFlags = CameraClearFlags.Skybox;
            goOutside();
        }
    }

    public void Interaction()
    {
        Debug.Log("인터렉션");
        var camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        camera.backgroundColor = Color.black;
        camera.clearFlags = CameraClearFlags.SolidColor;

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
        _weatherManager.playWeatherFX(_systemManager._weather, _isInside);
        _particleFX.SetActive(false);
    }

    private void goOutside()
    {
        _particleFX.SetActive(true);
        // Debug.Log("밖으로");
        _isInside = false;
        // 집 빛 꺼주기, 밖 빛 켜주기
        setLight();
        // Directional Light
        // 플레이어 이동
        _playerSystem.transform.position = new Vector3(_teleportHomeOutside.transform.position.x, _teleportHomeOutside.transform.position.y, _teleportHomeOutside.transform.position.z - 0.5f);
        _playerSystem.transform.GetChild(0).gameObject.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        // '밖으로' 메시지 보내기
        _uiManager.setIsHome(_isInside);
        _weatherManager.playWeatherFX(_systemManager._weather, _isInside);
    }
    private void setLight()
    {
        _homeLight.SetActive(_isInside);
        _directionalLight.SetActive(!_isInside);
    }

    public bool getIsInside()
    {
        return _isInside;
    }

    public void DayEndTeleport()
    {
        _isInside = true;
        _homeLight.SetActive(true);
        _directionalLight.SetActive(false);
        _uiManager.setIsHome(_isInside);
        _weatherManager.playWeatherFX(_systemManager._weather, _isInside);
        _particleFX.SetActive(false);
        var camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        camera.backgroundColor = Color.black;
        camera.clearFlags = CameraClearFlags.SolidColor;
    }
}




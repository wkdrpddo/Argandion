using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingInteraction : MonoBehaviour
{
    private UIManager _uiManager;
    [SerializeField] private CameraSystem _cameraSystem;
    public GameObject _shelf; // 시작 전에 끼워 넣기
    private PlayerSystem _playerSystem;
    private GameObject _playerBody;
    [SerializeField] private GameObject _cookingLight; // 시작 전에 끼워 넣기

    void Awake()
    {
        _playerSystem = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
        _playerBody = GameObject.Find("PlayerObject").transform.GetChild(0).gameObject;
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _cameraSystem = GameObject.Find("PlayerObject").GetComponent<CameraSystem>();
        Debug.Log(_cameraSystem);
    }

    public void Interaction()
    {
        // Debug.Log("빵 "+gameObject.transform.position);
        // Debug.Log("빵 rot "+gameObject.transform.rotation.eulerAngles);
        // Debug.Log("플레이어 "+GameObject.Find("PlayerObject").transform.position);
        _playerBody.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles);
        _uiManager.OnCookingPanel();
    }

    public void CookingStart()
    {
        // _cameraSystem = GameObject.Find("PlayerObject").GetComponent<CameraSystem>();
        float yRot = GameObject.Find("CookingPlace").transform.rotation.eulerAngles.y + 180f;
        // Debug.Log("rot "+yRot);
        // Debug.Log(_cameraSystem);
        _cameraSystem.CookingCamera(30f, yRot, 0.4f);
        _cookingLight.SetActive(true);
        _shelf.SetActive(false);
    }

    public void CookingEnd()
    {
        _cameraSystem.ResetCamera();
        _cookingLight.SetActive(false);
        _shelf.SetActive(true);
    }
}

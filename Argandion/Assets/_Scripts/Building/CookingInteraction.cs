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
    }

    public void Interaction()
    {
        _playerBody.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles);
        _uiManager.OnCookingPanel();
    }

    public void CookingStart()
    {
        _cameraSystem.CookingCamera();
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

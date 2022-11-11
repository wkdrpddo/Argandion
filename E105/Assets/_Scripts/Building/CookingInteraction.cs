using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingInteraction : MonoBehaviour
{
    private UIManager _uiManager;
    private CameraSystem _cameraSystem;
    public GameObject _shelf; // 시작 전에 끼워 넣기

    void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _cameraSystem = GameObject.Find("PlayerObject").GetComponent<CameraSystem>();
    }

    public void Interaction()
    {
        _uiManager.OnCookingPanel();
    }

    public void CookingStart()
    {
        _cameraSystem.CookingCamera();
        _shelf.SetActive(false);
    }

    public void CookingEnd()
    {
        _cameraSystem.ResetCamera();
        _shelf.SetActive(true);
    }
}

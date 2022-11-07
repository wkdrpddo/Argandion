using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingInteraction : MonoBehaviour
{
    public UIManager _uiManager;
    public CameraSystem _cameraSystem;

    void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _cameraSystem = GameObject.Find("CameraSystem").GetComponent<CameraSystem>();
    }
    public void Interaction()
    {
        _uiManager.OnCookingPanel();
        _cameraSystem.CookingCamera();
    }
}

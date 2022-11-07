using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingInteraction : MonoBehaviour
{
    public UIManager _uiManager;
    // public CameraManager _cameraManager;

    void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        // _cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
    }
    public void Interaction()
    {
        _uiManager.OnCookingPanel();
        // _cameraManager.CookingCamera();
    }
}

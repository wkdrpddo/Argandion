using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingInteraction : MonoBehaviour
{
    public UIManager _uiManager;
    public CameraSystem _cameraSystem;
    private GameObject _shelfParent;

    void Start()
    {
        // _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _cameraSystem = GameObject.Find("PlayerObject").GetComponent<CameraSystem>();
    }
    public void Interaction()
    {
        _uiManager.OnCookingPanel();
        _shelfParent = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;

    }
    public void CookingStart()
    {
        _cameraSystem.CookingCamera();
        _shelfParent.transform.GetChild(2).gameObject.SetActive(false);
        _shelfParent.transform.GetChild(3).gameObject.SetActive(false);
    }
    public void CookingEnd()
    {
        _cameraSystem.ResetCamera();
        _shelfParent.transform.GetChild(2).gameObject.SetActive(true);
        _shelfParent.transform.GetChild(3).gameObject.SetActive(true);
    }
}

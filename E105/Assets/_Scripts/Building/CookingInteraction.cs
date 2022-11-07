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
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _cameraSystem = GameObject.Find("CameraSystem").GetComponent<CameraSystem>();
    }
    public void Interaction()
    {
        _uiManager.OnCookingPanel();
        shelfParent = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;

    }
    public void CookingStart()
    {
        _cameraSystem.CookingCamera();
        shelfParent.GetChild(2).gameObject.SetActive(false);
        shelfParent.GetChild(3).gameObject.SetActive(false);
    }
    public void CookingEnd()
    {
        _cameraSystem.ResetCamera();
        shelfParent.GetChild(2).gameObject.SetActive(true);
        shelfParent.GetChild(3).gameObject.SetActive(true);
    }
}

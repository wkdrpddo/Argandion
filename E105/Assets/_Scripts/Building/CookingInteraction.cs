using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingInteraction : MonoBehaviour
{
    public UIManager _uiManager;
    public CameraSystem _cameraSystem;
    private GameObject _shelfParent;

    public bool start;
    public bool end;

    void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _cameraSystem = GameObject.Find("PlayerObject").GetComponent<CameraSystem>();
    }
    void Update()
    {
        if (start)
        {
            CookingStart();
            start = false;
        }
        if (end)
        {
            CookingEnd();
            end = false;
        }
    }
    public void Interaction()
    {
        Debug.Log("요리 인터렉션");
        _uiManager.OnCookingPanel();
        _shelfParent = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        // 테스트용 코드
        // Invoke("CookingStart", 2);
    }
    public void CookingStart()
    {
        // Debug.Log("요리 시작");
        _cameraSystem.CookingCamera();
        _shelfParent.transform.GetChild(2).gameObject.SetActive(false);
        _shelfParent.transform.GetChild(3).gameObject.SetActive(false);
        // 테스트용 코드
        // Invoke("CookingEnd", 2);
    }
    public void CookingEnd()
    {
        _cameraSystem.ResetCamera();
        _shelfParent.transform.GetChild(2).gameObject.SetActive(true);
        _shelfParent.transform.GetChild(3).gameObject.SetActive(true);
    }
}

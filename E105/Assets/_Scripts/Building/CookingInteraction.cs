using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingInteraction : MonoBehaviour
{
    public UIManager _uiManager;
    public CameraSystem _cameraSystem;
    // public GameObject _shelfParent;
    public GameObject _shelf; // 실행 전 끼워 넣기

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
        // Debug.Log("요리 인터렉션");
        _uiManager.OnCookingPanel();
        // Debug.Log("ui 호출 후");
        // _shelfParent = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        // Debug.Log(_shelfParent);
        // 테스트용 코드
        // Invoke("CookingStart", 2);
    }
    public void CookingStart()
    {
        Debug.Log("요리 시작");
        _cameraSystem.CookingCamera();
        _shelf.SetActive(false);
        // 테스트용 코드
        // Invoke("CookingEnd", 2);
    }
    public void CookingEnd()
    {
        _cameraSystem.ResetCamera();
        _shelf.SetActive(true);
    }
}

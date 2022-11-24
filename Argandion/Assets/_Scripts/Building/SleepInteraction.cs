using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepInteraction : MonoBehaviour
{
    public UIManager _uiManager;

    void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void Interaction()
    {
        Debug.Log("잠자기 콜");
        _uiManager.OnNotificationPanel();
    }
}

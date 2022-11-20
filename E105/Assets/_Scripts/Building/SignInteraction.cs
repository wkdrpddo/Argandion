using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignInteraction : MonoBehaviour
{
    public bool _interactable;
    public UIManager _uiManager;
    public int _sectorNum;

    void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void Interaction()
    {
        Debug.Log("표지판 인터렉션 콜");
        if (_interactable)
        {
            Debug.Log("인터렉터블");
            _uiManager.OnBuildEventPanel(_sectorNum);
        }
    }
}

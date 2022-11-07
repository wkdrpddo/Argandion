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
        if (_interactable)
        {
            // _uiManager.OnBuildEventPanel(_sectorNum);
            _uiManager.OnBuildEventPanel();
        }
    }
}

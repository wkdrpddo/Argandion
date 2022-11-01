using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    public RectTransform _uiGroup;
    public string _objectName;

    public PlayerSystem _enterPlayer;

    private int _downSize = 1300;
    public bool _open;

    public void Enter()
    {
        Debug.Log("Call Enter()");
        _uiGroup.anchoredPosition = Vector3.zero; // UI 위치 이동
        _open = true;
        _enterPlayer._canMove = false;
    }

    public void Exit()
    {
        Debug.Log("Call Exit()");
        _uiGroup.anchoredPosition = Vector3.down * _downSize; // UI 위치 이동
        _open = false;
        _enterPlayer._canMove = true;
    }
}

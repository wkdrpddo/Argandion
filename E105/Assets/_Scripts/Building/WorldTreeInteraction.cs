using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTreeInteraction : MonoBehaviour
{
    private UIManager _uiManager;
    void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void Interaction()
    {
        Debug.Log("인터렉션 콜111");
        _uiManager.OnConversationPanel(9);
    }
}

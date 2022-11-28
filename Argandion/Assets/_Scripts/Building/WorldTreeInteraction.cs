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
        _uiManager.OnConversationPanel(9);
    }
}

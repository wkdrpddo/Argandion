using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTableInteraction : MonoBehaviour
{
    public UIManager _uiManager;
    public int _sectorNum;

    void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void Interaction()
    {
        // Debug.Log("인터렉션 콜");
        _uiManager.OnCraftingPanel(_sectorNum);
        // _uiManager.OnCraftingPanel();
    }
}

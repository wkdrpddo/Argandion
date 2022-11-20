using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryPanel : MonoBehaviour
{
    // [SerializeField] private UIManager ui;
    public void handlePanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        UIManager._uimanagerInstance.setIsOpenInventory(gameObject.activeSelf);
        UIManager._uimanagerInstance.OnInventory(1);
        // ui.setIsOpenInventory(gameObject.activeSelf);
        // ui.OnInventory(1);

        if (gameObject.activeSelf)
        {
            UIManager._uimanagerInstance.stopControllKeys();
            // ui.stopControllKeys();
        }
        else
        {
            UIManager._uimanagerInstance.closeInvenRightClickModal();
            UIManager._uimanagerInstance.runControllKeys();
            // ui.closeInvenRightClickModal();
            // ui.runControllKeys();
        }
    }
    // Start is called before the first frame update
    public void Start()
    {
        // ui = gameObject.GetComponentInParent<UIManager>();
        // ui = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
}

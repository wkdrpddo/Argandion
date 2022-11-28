using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryPanel : MonoBehaviour
{

    public void handlePanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        UIManager._uimanagerInstance.setIsOpenInventory(gameObject.activeSelf);
        UIManager._uimanagerInstance.OnInventory(1);

        if (gameObject.activeSelf)
        {
            UIManager._uimanagerInstance.stopControllKeys();
        }
        else
        {
            UIManager._uimanagerInstance.closeInvenRightClickModal();
            UIManager._uimanagerInstance.runControllKeys();
        }
    }

}

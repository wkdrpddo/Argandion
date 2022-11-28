using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragePanel : MonoBehaviour
{

    public void handlePanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        UIManager._uimanagerInstance.setIsOpenStorage(gameObject.activeSelf);
        UIManager._uimanagerInstance.OnInventory(3);

        if (gameObject.activeSelf)
        {
            UIManager._uimanagerInstance.stopControllKeys();
        }
        else
        {
            UIManager._uimanagerInstance.runControllKeys();
        }
    }
}

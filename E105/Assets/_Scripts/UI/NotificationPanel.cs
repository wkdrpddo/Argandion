using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationPanel : MonoBehaviour
{
    public void handleNoti()
    {
        gameObject.SetActive(!gameObject.activeSelf);
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

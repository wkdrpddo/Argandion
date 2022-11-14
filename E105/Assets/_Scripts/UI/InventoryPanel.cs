using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryPanel : MonoBehaviour
{
    private UIManager ui;
    public void handlePanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        ui.setIsOpenInventory(gameObject.activeSelf);
        ui.toggleCanInteract();

        if (!gameObject.activeSelf)
        {
            ui.closeInvenRightClickModal();
            ui.runControllPlayer();
        }

        ui.OnInventory(1);
    }
    // Start is called before the first frame update
    void Start()
    {
        ui = gameObject.GetComponentInParent<UIManager>();
    }
}

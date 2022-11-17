using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private UIManager ui;
    public void handlePanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        ui.setIsOpenInventory(gameObject.activeSelf);
        ui.OnInventory(1);

        if (gameObject.activeSelf)
        {
            ui.stopControllKeys();
        }
        else
        {
            ui.closeInvenRightClickModal();
            ui.runControllKeys();
        }
    }
    // Start is called before the first frame update
    public void Start()
    {
        ui = gameObject.GetComponentInParent<UIManager>();
    }
}

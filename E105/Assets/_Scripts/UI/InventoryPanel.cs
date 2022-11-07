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
        ui.OnInventory(1);
    }
    // Start is called before the first frame update
    void Start()
    {
        ui = gameObject.GetComponentInParent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

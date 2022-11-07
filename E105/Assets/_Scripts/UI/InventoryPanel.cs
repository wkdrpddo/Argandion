using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryPanel : MonoBehaviour
{
    private Inventory _inventory;
    private string money;
    public void handlePanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    // Start is called before the first frame update
    void Start()
    {
        _inventory = GameObject.Find("InventoryManager").GetComponent<Inventory>();
        money = _inventory.gold.ToString();
        transform.GetChild(1).GetChild(5).GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = money;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

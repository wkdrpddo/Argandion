using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragePanel : MonoBehaviour
{
    [SerializeField] private UIManager ui;
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void handlePanel()
    {
        // Debug.Log("before switch storage active : " + gameObject.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
        // Debug.Log("after switch storage active : " + gameObject.activeSelf);
        ui.setIsOpenStorage(gameObject.activeSelf);
        ui.OnInventory(3);

        if (gameObject.activeSelf)
        {
            ui.stopControllKeys();
        }
        else
        {
            ui.runControllKeys();
        }
    }
}

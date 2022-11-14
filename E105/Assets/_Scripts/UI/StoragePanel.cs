using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragePanel : MonoBehaviour
{
    private UIManager ui;
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void handlePanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        ui.setIsOpenStorage(gameObject.activeSelf);
    }
}

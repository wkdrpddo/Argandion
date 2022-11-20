using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    public UIManager _uiManager;

    void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            ChestOpen();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            ChestClose();
        }
    }

    public void ChestOpen()
    {
        gameObject.transform.GetChild(0).gameObject.transform.localEulerAngles = new Vector3(-10.55f, 0f, 0f);
    }

    public void ChestClose()
    {
        gameObject.transform.GetChild(0).gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
    }

    public void Interaction()
    {
        Debug.Log("UI Storage 오픈");
        _uiManager.OnStoragePanel();
    }
}

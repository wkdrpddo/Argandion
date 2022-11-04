using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public void exitPanel()
    {
        gameObject.SetActive(false);
        gameObject.GetComponentInParent<UIManager>().runControllPlayer();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

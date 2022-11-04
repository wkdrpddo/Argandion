using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingPanel : MonoBehaviour
{
    private UIManager ui;

    public void closeWindow()
    {
        gameObject.SetActive(false);
        ui.runControllPlayer();
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

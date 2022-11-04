using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.UIElements;

public class MainPagePanel : MonoBehaviour
{
    private bool isGameStart;
    private UIManager ui;

    void Start()
    {
        isGameStart = false;
        ui = this.GetComponentInParent<UIManager>();
    }

    public bool getIsGameStart()
    {
        return isGameStart;
    }

    public void setIsGameStart(bool value)
    {
        isGameStart = value;
    }

    public void gameStart()
    {
        isGameStart = true;
        gameObject.SetActive(false);
        ui.OnCreateCharacter();
    }
}

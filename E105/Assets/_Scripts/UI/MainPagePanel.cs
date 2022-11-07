using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.UIElements;

public class MainPagePanel : MonoBehaviour
{
    private UIManager ui;

    void Start()
    {
        ui = this.GetComponentInParent<UIManager>();
    }

    public void setIsGameStart(bool value)
    {
        ui.setGameState(value);
    }

    public void gameStart()
    {
        setIsGameStart(true);
        gameObject.SetActive(false);
        ui.OnCreateCharacter();
    }
}

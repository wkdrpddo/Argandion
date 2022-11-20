using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.UIElements;

public class MainPagePanel : MonoBehaviour
{
    // [SerializeField] private UIManager ui;

    void Start()
    {
        // ui = this.GetComponentInParent<UIManager>();
    }

    public void gameStart()
    {
        string name = UIManager._uimanagerInstance.getPlayerName();

        gameObject.SetActive(false);
        if (name == "")
        {
            UIManager._uimanagerInstance.OnCreateCharacter();
        }
        else
        {
            UIManager._uimanagerInstance.OnBaseUIPanel();
            UIManager._uimanagerInstance.setGameState(true);
            UIManager._uimanagerInstance.delayRunControllKeys();
        }
    }
}

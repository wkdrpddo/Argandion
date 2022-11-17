using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.UIElements;

public class MainPagePanel : MonoBehaviour
{
    [SerializeField] private UIManager ui;

    void Start()
    {
        ui = this.GetComponentInParent<UIManager>();
    }

    public void gameStart()
    {
        gameObject.SetActive(false);
        ui.OnCreateCharacter();
    }
}

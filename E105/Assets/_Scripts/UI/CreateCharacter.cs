using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateCharacter : MonoBehaviour
{
    private UIManager ui;
    private Image manImg;
    private Image womanImg;
    public Sprite unselectedMan;
    public Sprite selectedMan;
    public Sprite unselectedWoman;
    public Sprite selectedWoman;

    private string _name;


    public void selectMan()
    {
        manImg.sprite = selectedMan;
        womanImg.sprite = unselectedWoman;
        gameObject.GetComponentInParent<UIManager>().setCharacterValue(1);
    }

    public void selectWoman()
    {
        manImg.sprite = unselectedMan;
        womanImg.sprite = selectedWoman;
        gameObject.GetComponentInParent<UIManager>().setCharacterValue(0);
    }

    public void backToMain()
    {
        gameObject.SetActive(false);
        gameObject.GetComponentInParent<UIManager>().OnMainPagePanel();
        gameObject.transform.GetChild(2).GetComponent<TMP_InputField>().text = "";
    }

    public void gameStart()
    {
        getName();

        if (_name == "")
        {
            ui.OnResultNotificationPanel("이름은 공백일 수 없습니다-");
        }
        else
        {
            ui.OnBaseUIPanel();
            ui.selectPlayer();
            ui.setPlayerName(_name);
            ui.setGameState(true);

            gameObject.SetActive(false);

            ui.delayRunControllKeys();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ui = gameObject.GetComponentInParent<UIManager>();
        manImg = transform.GetChild(5).GetChild(0).GetComponent<Image>();
        womanImg = transform.GetChild(6).GetChild(0).GetComponent<Image>();

        selectWoman();
    }

    public void getName()
    {
        _name = gameObject.transform.GetChild(2).GetComponent<TMP_InputField>().text;
    }

    public void clickInputField()
    {
        gameObject.transform.GetChild(2).GetComponent<TMP_InputField>().text = "";
    }
}

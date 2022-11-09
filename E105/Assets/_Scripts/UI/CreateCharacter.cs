using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    private UIManager ui;
    private Image manImg;
    private Image womanImg;
    public Sprite unselectedMan;
    public Sprite selectedMan;
    public Sprite unselectedWoman;
    public Sprite selectedWoman;

    public void selectMan()
    {
        manImg.sprite = selectedMan;
        womanImg.sprite = unselectedWoman;
        gameObject.GetComponentInParent<UIManager>().setCharacterValue(0);
    }

    public void selectWoman()
    {
        manImg.sprite = unselectedMan;
        womanImg.sprite = selectedWoman;
        gameObject.GetComponentInParent<UIManager>().setCharacterValue(1);
    }

    public void backToMain()
    {
        gameObject.SetActive(false);
        gameObject.GetComponentInParent<UIManager>().OnMainPagePanel();
    }

    public void gameStart()
    {
        ui.OnBaseUIPanel();
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        ui = gameObject.GetComponentInParent<UIManager>();
        manImg = transform.GetChild(5).GetComponentInChildren<Image>();
        womanImg = transform.GetChild(6).GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

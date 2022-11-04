using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    public GameObject _baseuipanel;
    public Sprite unselectedMan;
    public Sprite selectedMan;
    public Sprite unselectedWoman;
    public Sprite selectedWoman;
    public Image manImg;
    public Image womanImg;

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
        _baseuipanel.SetActive(true);
        gameObject.SetActive(false);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] private SoundManager _soundmanager;
    // [SerializeField] private UIManager ui;
    [SerializeField] private Slider background;
    [SerializeField] private Slider effect;

    void Start()
    {
    }

    void Awake()
    {
        // ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        _soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        background = gameObject.transform.GetChild(0).GetComponent<Slider>();
        effect = gameObject.transform.GetChild(1).GetComponent<Slider>();
    }

    public void handelPanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if (gameObject.activeSelf)
        {
            background.value = _soundmanager.getBackgroundSound();
            effect.value = _soundmanager.getEffectSound();
            UIManager._uimanagerInstance.isPressESC = true;
        }
        else
        {
            UIManager._uimanagerInstance.isPressESC = false;
        }
    }
}

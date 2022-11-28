using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] private SoundManager _soundmanager;
    [SerializeField] private Slider background;
    [SerializeField] private Slider effect;

    void Awake()
    {
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
            UIManager._uimanagerInstance.handleMainBtnInteractable(false);
        }
        else
        {
            UIManager._uimanagerInstance.isPressESC = false;
            UIManager._uimanagerInstance.handleMainBtnInteractable(true);
        }
    }
}

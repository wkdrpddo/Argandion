using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] private SoundManager _soundmanager;
    [SerializeField] private UIManager ui;
    private float _bgmVolume;
    private float _effectVolume;
    // Start is called before the first frame update
    void Start()
    {
        _soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void handelPanel()
    {
        ui.pressedESC();

        if (gameObject.activeSelf)
        {
            gameObject.transform.GetChild(0).GetComponent<Slider>().value = _soundmanager.getBackgroundSound();
            gameObject.transform.GetChild(1).GetComponent<Slider>().value = _soundmanager.getEffectSound();
        }
    }
}

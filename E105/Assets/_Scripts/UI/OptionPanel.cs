using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    private SoundManager _soundmanager;
    private float _bgmVolume;
    private float _effectVolume;
    // Start is called before the first frame update
    void Start()
    {
        _soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        gameObject.transform.GetChild(0).GetComponent<Slider>().value = _soundmanager.getBackgroundSound();
        Debug.Log(_soundmanager.getBackgroundSound());
        gameObject.transform.GetChild(1).GetComponent<Slider>().value = _soundmanager.getEffectSound();
        Debug.Log(_soundmanager.getEffectSound());
    }

    // Update is called once per frame
    void Update()
    {
        // setSound();

    }

    public void setSound()
    {
        _bgmVolume = gameObject.transform.GetChild(0).GetComponent<Slider>().value;
        _effectVolume = gameObject.transform.GetChild(1).GetComponent<Slider>().value;

        _soundmanager.setBackgroundSound(_bgmVolume);
        _soundmanager.setEffectSound(_effectVolume);
    }

    public void handelPanel()
    {
        UIManager ui = this.GetComponentInParent<UIManager>();
        ui.pressedESC();
    }
}

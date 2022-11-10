using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private float Sound_Background = 0.2f;
    private float Sound_Effect = 0.2f;

    // option panal
    // public GameObject _optionpanel;
    // public GameObject _optionpanelfrommain;

    public void setBackgroundSound(Slider slider)
    {
        Sound_Background = slider.value;
    }

    public void setEffectSound(Slider slider)
    {
        Sound_Effect = slider.value;
    }

    public float getBackgroundSound()
    {
        return Sound_Background;
    }

    public float getEffectSound()
    {
        return Sound_Effect;
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

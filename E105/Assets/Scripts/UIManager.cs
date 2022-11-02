using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider _healthbar;
    public Slider _energybar;
    public SystemManager _systemmanager;
    public RectTransform _timer;
    public GameObject _settingpanel;
    private bool isPressESC = false;
    public PlayerSystem _playersystem;
    public GameObject _eventAnnounce;
    public TextMeshProUGUI _announceText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        setTimer();

        if (Input.GetButtonDown("optionKey"))
        {
            pressedESC();
        }
    }

    public void setHealthBar(float value)
    {
        _healthbar.value = value;
    }

    public void setEnergyBar(float value)
    {
        _energybar.value = value;
    }

    public void setTimer()
    {
        float angle = (_systemmanager._hour_display - 6) * 15 + (_systemmanager._minute_display / 4);
        _timer.rotation = Quaternion.Euler(180, 0, angle);
    }

    public void pressedESC()
    {
        isPressESC = !isPressESC;

        if (isPressESC)
        {
            _settingpanel.SetActive(true);
            _playersystem._canMove = false;
        }
        else
        {
            _settingpanel.SetActive(false);
            _playersystem._canMove = true;
        }
    }
}

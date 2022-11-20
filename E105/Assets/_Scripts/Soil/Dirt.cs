using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    private int howMany;
    public int _dirt_number;
    public int watered;
    public int minusWater;
    public int temp;
    public bool isReady = false;
    public bool fullWater = false;
    public GameObject[] _nearObjects = new GameObject[9];

    public CropPosition[] _cropPos = new CropPosition[9];
    ParticleSystem particleObject;
    private SoundManager _sound;
    public SystemManager _system;
    public GameObject _buffManagerObject;
    private BuffManager _buff;
    public GameObject _icon_water;
    public GameObject _icon_dig;
    public bool _is_icon_dig;
    public bool _is_icon_water;
    private int _icon_num;

    void Start()
    {
        _system = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        particleObject = GetComponent<ParticleSystem>();
        temp = _system._day;
        _buffManagerObject = GameObject.Find("BuffManager");
        _buff = _buffManagerObject.GetComponent<BuffManager>();
        _cropPos = gameObject.GetComponentsInChildren<CropPosition>();
        _sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        checkHoe();
        checkWater();
        checkIcon();
    }

    public void DayStart()
    {
        if (temp != _system._day) {
            NewDay();
            temp = _system._day;
        }
    }

    public void Ready()
    {
        isReady = true;
        Invoke("FarmingSound",0.0f);
    }

    private void FarmingSound(){
        _sound.playEffectSound("FARMING");
    }

    void NewDay()
    {
        if (_buff.bluePray || _system._weather == 1 || _system._weather == 5)
        {
            watered = 1500;
            fullWater = true;
            foreach (CropPosition cpo in _cropPos)
            {
                if (cpo._plant)
                {
                    if (cpo._plant.TryGetComponent(out Crop cro))
                    {
                        cro.growUp();
                    }
                }
            }
        }
        else if (watered >= minusWater)
        {
            watered -= minusWater;
            fullWater = false;
            foreach (CropPosition cpo in _cropPos)
            {
                if (cpo._plant)
                {
                    if (cpo._plant.TryGetComponent(out Crop cro))
                    {
                        cro.growUp();
                    }
                }
            }
        }
        else
        {
            watered = 0;
            fullWater = false;
        }
        checkWater();
    }

    public void Water()
    {
        _sound.playEffectSound("WATERING");
        fullWater = true;
        watered = 1500;
        checkWater();
    }

    public void checkWater()
    {
        if (minusWater >= watered)
        {
            _is_icon_water = true;
        }
        else
        {
            _is_icon_water = false;
        }
    }

    public void checkHoe()
    {
        foreach (CropPosition cpo in _cropPos)
        {
            if (cpo._state == 0)
            {
                _is_icon_dig = true;
                break;
            }
        }
    }

    public void Hoe()
    {
        foreach (CropPosition cpo in _cropPos)
        {
            if (cpo._state == 0)
            {
                cpo._state = -1;
            }
        }
        _is_icon_dig = false;
    }

    private void checkIcon()
    {
        if (!_is_icon_dig && !_is_icon_water)
        {
            _icon_dig.SetActive(false);
            _icon_water.SetActive(false);
            _icon_num = 0;
        }
        else if (_is_icon_dig && _icon_num != 1)
        {
            _icon_dig.SetActive(true);
            _icon_water.SetActive(false);
            _icon_num = 1;
        }
        else if (_is_icon_water && _icon_num != 2)
        {
            _icon_water.SetActive(true);
            _icon_dig.SetActive(false);
            _icon_num = 2;
        }
        Invoke("checkIcon",3f);
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("crop"))
    //     {
    //         if (!other.gameObject.GetComponent<Crop>().isIn)
    //         {
    //             for (int idx = 0; idx < 25; idx++)
    //             {
    //                 if (_nearObjects[idx] == null)
    //                 {
    //                     Debug.Log(idx + "에 넣었어!");
    //                     _nearObjects[idx] = other.gameObject;
    //                     other.gameObject.GetComponent<Crop>().isIn = true;
    //                     howMany += 1;
    //                     break;
    //                 }
    //             }
    //         }
    //     }
    // }

    // public void CropGrowUp(GameObject crop)
    // {
    //     for (int idx = 0; idx < 25; idx++)
    //     {
    //         if (_nearObjects[idx] == crop)
    //         {
    //             _nearObjects[idx] = null;
    //             howMany -= 1;
    //             break;
    //         }
    //     }
    // }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public int _month;
    public int _day;
    public int _weather;
    public int _hour_display;
    public int _minute_display;
    private int _hour;
    private float _minute;
    public float _hour_time_changemeter=1000;
    public GameObject _light;
    public GameObject MapObject;

    static int _sector_size=8;
    public bool[] _purification = new bool[_sector_size];
    public GameObject[] _sector = new GameObject[_sector_size];

    public int[,] _timezone = new int[,] {{6,7,18,19},{6,6,19,20},{6,7,18,19},{7,8,18,19}};
    // Start is called before the first frame update
    void Start()
    {
        _month=1;
        _day=1;
        _weather=0;
        _hour=6;
        _hour_display=6;
        _minute=0;
        _minute_display=0;
    }

    // Update is called once per frame
    void Update()
    {
        TimeSystem();
    }
    
    private void UpdateWeather(int index)
    {
        Debug.Log("계절이 바뀌었습니다.");
        _weather = index;
        MapObject.GetComponent<MapObject>().UpdateFieldManager(index);
    }

    private void UpdatePurification(int index)
    {
        _purification[index] = !_purification[index];
    }

    private void TimeSystem()
    {
        _minute += Time.deltaTime*(_hour_time_changemeter/1000f);
            if (_minute >= 60) {
                _minute -= 60;
                _hour += 1;

                if (_hour >= 23) {
                _hour = 6;
                _day += 1;

                    if (_day>=29) {
                        _day -= 28;
                        _month += 1;

                        if (_month >= 9) {
                            _month -= 8;
                        }
                        if (_month%2 == 1) {
                            UpdateWeather(_month/2);
                        }
                    }
                }
            }
        
        _minute_display = ((int)_minute);
        _hour_display = _hour;

        if (_hour<_timezone[_weather,0]) {
            _light.transform.rotation = Quaternion.Euler(-10,-30,_light.transform.rotation.z);
        }
        else if (_timezone[_weather,0]<=_hour && _hour<_timezone[_weather,1]) {
            _light.transform.rotation = Quaternion.Euler(-10+_minute,-30,_light.transform.rotation.z);
        }
        else if (_timezone[_weather,1]<=_hour && _hour<_timezone[_weather,2]) {
            _light.transform.rotation = Quaternion.Euler(50,-30,_light.transform.rotation.z);
        }
        else if (_timezone[_weather,2]<=_hour && _hour<_timezone[_weather,3]) {
            _light.transform.rotation = Quaternion.Euler(50+_minute*2.5f,-30,_light.transform.rotation.z);
        }
        else if (_timezone[_weather,3]<=_hour) {
            _light.transform.rotation = Quaternion.Euler(200,-30,_light.transform.rotation.z);
        }
    }
}

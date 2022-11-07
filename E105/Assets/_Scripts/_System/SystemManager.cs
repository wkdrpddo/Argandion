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
    public float _hour_time_changemeter = 1000;
    public GameObject _light;
    public GameObject MapObject;
    public PlayerSystem _player;
    public BuffManager _buffManager;

    public int _development_level;  // 1부터
    public int _purification_sector;

    static int _sector_size = 8;
    public int _purification_size;
    public bool[] _purification = new bool[_sector_size];
    public GameObject[] _sector = new GameObject[_sector_size+2];
    public SectorObject _sectorTest;
    public GameObject[] _randomNPC = new GameObject[2];

    public int[,] _timezone = new int[,] { { 6, 7, 18, 19 }, { 6, 6, 19, 20 }, { 6, 7, 18, 19 }, { 7, 8, 18, 19 } };
    // Start is called before the first frame update
    void Start()
    {
        _month = 1;
        _day = 1;
        _weather = 0;
        _hour = 6;
        _hour_display = 6;
        _minute = 0;
        _minute_display = 0;
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
        _minute += Time.deltaTime * (_hour_time_changemeter / 1000f);
        if (_minute >= 60)
        {
            _minute -= 60;
            _hour += 1;

            if (_hour >= 23)
            {
                _hour = 6;
                _day += 1;

                DayEnd();

                // if (_buffManager._flowerBuffTargetMonth == _month && _buffManager._flowerBuffTargetDay == _day)
                // {
                //     _buffManager.FlowerBuffEnd();
                //     Debug.Log("꽃 버프 종료!");
                // }


                if (_day >= 29)
                {
                    _day -= 28;
                    _month += 1;
                    // if (_buffManager._flowerBuffTargetMonth == _month && _buffManager._flowerBuffTargetDay == _day)
                    // {
                    //     _buffManager.FlowerBuffEnd();
                    //     Debug.Log("꽃 버프 종료!");
                    // }

                    if (_month >= 9)
                    {
                        _month -= 8;
                    }
                    if (_month % 2 == 1)
                    {
                        UpdateWeather(_month / 2);
                    }
                }
            }
        }

        _minute_display = ((int)_minute);
        _hour_display = _hour;

        if (_hour < _timezone[_weather, 0])
        {
            _light.transform.rotation = Quaternion.Euler(-10, -30, _light.transform.rotation.z);
        }
        else if (_timezone[_weather, 0] <= _hour && _hour < _timezone[_weather, 1])
        {
            _light.transform.rotation = Quaternion.Euler(-10 + _minute, -30, _light.transform.rotation.z);
        }
        else if (_timezone[_weather, 1] <= _hour && _hour < _timezone[_weather, 2])
        {
            _light.transform.rotation = Quaternion.Euler(50, -30, _light.transform.rotation.z);
        }
        else if (_timezone[_weather, 2] <= _hour && _hour < _timezone[_weather, 3])
        {
            _light.transform.rotation = Quaternion.Euler(50 + _minute * 2.5f, -30, _light.transform.rotation.z);
        }
        else if (_timezone[_weather, 3] <= _hour)
        {
            _light.transform.rotation = Quaternion.Euler(200, -30, _light.transform.rotation.z);
        }
    }

    private void DayEnd()
    {

        int npc1_position = RandomPurification();
        int npc2_position = RandomPurification();

        // //순례자, 음악가 랜덤위치 생성
        _randomNPC[0].transform.position = NPCRandomPosition(npc1_position);
        _randomNPC[1].transform.position = NPCRandomPosition(npc2_position);
        SectorObject[] sec = MapObject.GetComponentsInChildren<SectorObject>();
        foreach(var se in sec)
        {
            se.DayEnd();
        }
    }

    //정화된 구역 중에서 랜덤 한 구역 정하기
    private int RandomPurification()
    {
        int[] region = new int[8];  //정화된 지역
        _purification_size = 0;

        for (int i = 0; i < 8; i++)
        {
            if (_purification[i])
            {

                region[_purification_size] = i;
                _purification_size++;

            }
        }

        int rand = Random.Range(0, _purification_size);

        return region[rand];
    }

    //선택된 지역중 랜덤 좌표
    private Vector3 NPCRandomPosition(int region)
    {
        Vector3 position = new Vector3(0, 0, 0);

        switch (region)
        {
            case 0:
                position = new Vector3(Random.Range(0.0f, 60.0f), 0.0f, Random.Range(225.0f, 275.0f));
                break;
            case 1:
                position = new Vector3(Random.Range(87.0f, 202.0f), 0.0f, Random.Range(193.0f, 264.0f));
                break;
            case 2:
                position = new Vector3(Random.Range(193.0f, 233.0f), 0.0f, Random.Range(200.0f, 256.0f));
                break;
            case 3:
                position = new Vector3(Random.Range(33.0f, 79.0f), 0.0f, Random.Range(110.0f, 162.0f));
                break;
            case 4:
                position = new Vector3(Random.Range(80.0f, 192.0f), 0.0f, Random.Range(110.0f, 200.0f));
                break;
            case 5:
                position = new Vector3(Random.Range(193.0f, 251.0f), 0.0f, Random.Range(120.0f, 200.0f));
                break;
            case 6:
                position = new Vector3(Random.Range(0.0f, 166.0f), 0.0f, Random.Range(38.0f, 110.0f));
                break;
            case 7:
                position = new Vector3(Random.Range(180.0f, 194.0f), 0.0f, Random.Range(81.0f, 110.0f));
                break;
        }

        return position;

    }
}

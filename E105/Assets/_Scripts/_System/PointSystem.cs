using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public SectorObject _Sector;
    public int _isDen;
    public GameObject[] _animals = new GameObject[4];
    public Transform _base_transform;
    // Start is called before the first frame update
    void Start()
    {
        _Sector = this.GetComponentInParent<SectorObject>();
        _base_transform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DayEnd()
    {
        // Debug.Log("Point sys dayend");
        switch (_isDen)
        {
            case 0:
                Check_Animal();
                break;
            case 1:
                Spawn_Animal(0, 1);
                break;
            case 2:
                Spawn_Animal(1, 1);
                break;
            case 3:
                Spawn_Animal(2, 1);
                break;
            case 4:
                Spawn_Animal(3, 1);
                break;
        }
    }

    private void Check_Animal()
    {
        float srnd = Random.Range(0f, 1f);
        if (srnd <= _Sector._animal_spawn_base_percent)
        {
            Debug.Log("동물 소환되며 rnd는");
            int rnd = Random.Range(0, _Sector._animal_type_base_factor[3]);
            Debug.Log(rnd);
            int len = 0;
            int sumV = 0;
            int index = 0;
            if (rnd < _Sector._animal_type_base_factor[0])
            {
                len = _Sector._animal_bunny_count_factor.Length;
                sumV = Random.Range(0, _Sector._animal_bunny_count_factor[len - 1]);
                Debug.Log(sumV);
                Debug.Log("토끼 카운트");
                while (!(_Sector._animal_bunny_count_factor[index] <= sumV && sumV <= _Sector._animal_bunny_count_factor[index + 1]))
                {
                    index += 1;
                }
                Debug.Log(index);
                Spawn_Animal(0, index + 1);
            }
            else if (rnd < _Sector._animal_type_base_factor[1])
            {
                len = _Sector._animal_deer_count_factor.Length;
                sumV = Random.Range(0, _Sector._animal_deer_count_factor[len - 1]);
                Debug.Log(sumV);
                Debug.Log("순록 카운트");
                while (!(_Sector._animal_deer_count_factor[index] <= sumV && sumV <= _Sector._animal_deer_count_factor[index + 1]))
                {
                    index += 1;
                }
                Debug.Log(index);
                Spawn_Animal(1, index + 1);
            }
            else if (rnd < _Sector._animal_type_base_factor[2])
            {
                len = _Sector._animal_wolf_count_factor.Length;
                sumV = Random.Range(0, _Sector._animal_wolf_count_factor[len - 1]);
                Debug.Log(sumV);
                Debug.Log("늑대 카운트");
                while (!(_Sector._animal_wolf_count_factor[index] <= sumV && sumV <= _Sector._animal_wolf_count_factor[index + 1]))
                {
                    index += 1;
                }
                Debug.Log(index);
                Spawn_Animal(2, index + 1);
            }
            else if (rnd < _Sector._animal_type_base_factor[3])
            {
                len = _Sector._animal_bear_count_factor.Length;
                sumV = Random.Range(0, _Sector._animal_bear_count_factor[len - 1]);
                Debug.Log(sumV);
                Debug.Log("곰 카운트");
                while (!(_Sector._animal_bear_count_factor[index] <= sumV && sumV <= _Sector._animal_bear_count_factor[index + 1]))
                {
                    index += 1;
                }
                Debug.Log(index);
                Spawn_Animal(3, index + 1);
            }
        }
    }

    private void Spawn_Animal(int index, int count)
    {
        Vector3 pos = new Vector3(_base_transform.position.x, _base_transform.position.y, _base_transform.position.z);
        for (int i = 0; i < count; i++)
        {
            Instantiate(_animals[index], pos, new Quaternion()).transform.parent = GameObject.Find("Animals").transform;
        }
    }
}

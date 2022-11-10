using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSpawn : MonoBehaviour
{
    public SectorObject _Sector;
    public GameObject[] _mushroom = new GameObject[3];
    public Transform _base_transform;
    public float _radius;
    private Collider[] _buffer;
    // Start is called before the first frame update
    void Start()
    {
        _Sector = this.GetComponentInParent<SectorObject>();
        _base_transform = gameObject.transform;
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
    public void DayStart()
    {
        Check_Mushroom();
    }

    private void Check_Mushroom()
    {
        float srnd = Random.Range(0f,1f);
        if (srnd <= (_Sector._purifier ? _Sector._mushroom_spawn_base_percent : _Sector._mushroom_spawn_base_percent * _Sector._mushroom_ruin_factor))
        {
            Debug.Log("버섯소환");
            int rnd = Random.Range(0,_Sector._mushroom_count_factor[1]);
            if (rnd < _Sector._mushroom_count_factor[0])
            {
                Spawn_Mushroom(1);
            }
            else if (rnd < _Sector._mushroom_count_factor[1])
            {
                Spawn_Mushroom(2);
            }
        }
    }

    private void Spawn_Mushroom(int count)
    {
        while (count>0)
        {
            float X = Random.Range(-(_radius+0.5f),(_radius+0.5f));
            int ZM = Random.Range(0,2);
            float Z = (ZM==0 ? Mathf.Sqrt((_radius+0.5f)-(X*X)) : -Mathf.Sqrt((_radius+0.5f)-(X*X)));
            Vector3 pos = new Vector3(_base_transform.position.x+X,_base_transform.position.y+3f,_base_transform.position.z+Z);

            _buffer = Physics.OverlapBox(pos,new Vector3(0.5f,3f,0.5f),new Quaternion(),layerMask:320);
            Debug.Log(_buffer.Length);
            if (_buffer.Length==0)
            {
                int rnd = Random.Range(0,_Sector._mushroom_type_factor[2]);
                if (rnd < _Sector._mushroom_type_factor[0])
                {
                    Instantiate(_mushroom[0],pos,new Quaternion(),_Sector.gameObject.transform);
                }
                else if (rnd < _Sector._mushroom_type_factor[1])
                {
                    Instantiate(_mushroom[1],pos,new Quaternion(),_Sector.gameObject.transform);
                }
                else if (rnd < _Sector._mushroom_type_factor[2])
                {
                    Instantiate(_mushroom[2],pos,new Quaternion(),_Sector.gameObject.transform);
                }
            }
            count -= 1;
        }
    }
}

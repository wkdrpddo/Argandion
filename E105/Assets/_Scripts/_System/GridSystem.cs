using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public SectorObject _Sector;
    public GameObject[] _trees = new GameObject[8];
    public GameObject _branch;
    public GameObject _stone;
    public GameObject[] _ores = new GameObject[3];
    public float _tree_spawn_percent;
    public Transform _base_transform;
    private Collider[] _buffer;
    private bool _purified;
    public bool _ore_spawn;
    void Start()
    {
        _Sector = this.GetComponentInParent<SectorObject>();
        _tree_spawn_percent = _Sector._tree_spawn_base_percent;
        _base_transform = gameObject.transform;
    }

    public void DayStart()
    {
        _purified = _Sector._purifier;
        if (_ore_spawn)
        {
            Check_Ore();
        }
        else
        {
            Check_Tree();
            Check_Branch();
            Check_Stone();
        }
        TreeObject[] trees = this.GetComponentsInChildren<TreeObject>();
        foreach (var tree in trees)
        {
            tree.DayEnd();
        }
    }

    private void Check_Tree()
    {
        if (_Sector._tree_remain < (_purified ? _Sector._tree_max : _Sector._tree_max * _Sector._tree_ruin_max_factor))
        {
            float rnd = Random.Range(0f,1f);
            if (rnd <= _tree_spawn_percent)
            {
                Spawn_Tree();
            }
            else
            {
                _tree_spawn_percent = _tree_spawn_percent * _Sector._tree_spawn_factor * (_purified ? 1 : _Sector._tree_ruin_spawn_factor);
                _tree_spawn_percent = Mathf.Min(100,_tree_spawn_percent);
            }
        }
    }

    private void Spawn_Tree()
    {
        Debug.Log("나무가 생성됨!");
        Vector3 pos = new Vector3(_base_transform.position.x+Random.Range(-5f,5f),_base_transform.position.y+0.5f,_base_transform.position.z+Random.Range(-5f,5f));
        int cnt = 0;
        while (cnt < 24 && _Sector._tree_remain < (_purified ? _Sector._tree_max : _Sector._tree_max * _Sector._tree_ruin_max_factor))
        {
            _buffer = Physics.OverlapBox(center:pos,halfExtents:new Vector3(0.5f,0.5f,0.5f),new Quaternion(),layerMask:1<<7);
                // Debug.Log(_buffer.Length);
                if (_buffer.Length==0)
                {
                    // 여기 큰일났어요 싯발
                    int treenum = Random.Range(0,1);
                    GameObject tr = Instantiate(_trees[treenum],pos,new Quaternion(),gameObject.transform);
                    _Sector._tree_remain += 1;
                    _tree_spawn_percent = _Sector._tree_spawn_base_percent;
                    break;
                }
                else
                {
                    float new_x = pos.x + ((cnt+1)%2)*Mathf.Pow(-1,cnt/2);
                    float new_z = pos.z + (cnt%2)*Mathf.Pow(-1,cnt/2);
                    pos = new Vector3(new_x,pos.y,new_z);
                    cnt += 1;
                }
        }
    }

    private void Check_Branch()
    {
        float srnd = Random.Range(0f,1f);
        if (srnd <= (_purified ? _Sector._branch_spawn_base_percent : _Sector._branch_spawn_base_percent * _Sector._branch_ruin_factor))
        {
            int rnd = Random.Range(0,_Sector._branch_type_factor[2]);
            if (rnd < _Sector._branch_type_factor[0])
            {
                Spawn_Branch(3);
            }
            else if (rnd < _Sector._branch_type_factor[1])
            {
                Spawn_Branch(4);
            }
            else if (rnd < _Sector._branch_type_factor[2])
            {
                Spawn_Branch(5);
            }
        }
    }

    private void Spawn_Branch(int count)
    {
        Debug.Log("나뭇가지 생성됨!");
        while (count>0)
        {
            Vector3 pos = new Vector3(_base_transform.position.x+Random.Range(-5f,5f),_base_transform.position.y+0.5f,_base_transform.position.z+Random.Range(-5f,5f));
            int cnt = 0;
            while (cnt < 24)
            {
                _buffer = Physics.OverlapBox(pos,new Vector3(0.5f,0.5f,0.5f));
                // Debug.Log(_buffer.Length);
                if (_buffer.Length==0)
                {
                    Instantiate(_branch,pos,new Quaternion(),_Sector._items.transform);
                    count -= 1;
                    break;
                }
                else
                {
                    float new_x = pos.x + ((cnt+1)%2)*Mathf.Pow(-1,cnt/2);
                    float new_z = pos.z + (cnt%2)*Mathf.Pow(-1,cnt/2);
                    pos = new Vector3(new_x,pos.y,new_z);
                    cnt += 1;
                }
            }
            if (cnt >= 24) {
                count -= 1;
            }
        }
    }

    private void Check_Stone()
    {
        Debug.Log("돌맹이 생성됨!");
        float srnd = Random.Range(0f,1f);
        if (srnd <= (_purified ? _Sector._stone_spawn_base_percent : _Sector._stone_spawn_base_percent * _Sector._stone_ruin_factor))
        {
            int rnd = Random.Range(0,_Sector._stone_type_factor[2]);
            if (rnd < _Sector._stone_type_factor[0])
            {
                Spawn_Stone(2);
            }
            else if (rnd < _Sector._stone_type_factor[1])
            {
                Spawn_Stone(3);
            }
            else if (rnd < _Sector._stone_type_factor[2])
            {
                Spawn_Stone(4);
            }
        }
    }

    private void Spawn_Stone(int count)
    {
        while (count>0)
        {
            Vector3 pos = new Vector3(_base_transform.position.x+Random.Range(-5f,5f),_base_transform.position.y+0.5f,_base_transform.position.z+Random.Range(-5f,5f));
            int cnt = 0;
            while (cnt < 24)
            {
                _buffer = Physics.OverlapBox(pos,new Vector3(0.5f,0.5f,0.5f));
                // Debug.Log(_buffer.Length);
                if (_buffer.Length==0)
                {
                    Instantiate(_stone,pos,new Quaternion(),_Sector._items.transform);
                    count -= 1;
                    break;
                }
                else
                {
                    float new_x = pos.x + ((cnt+1)%2)*Mathf.Pow(-1,cnt/2);
                    float new_z = pos.z + (cnt%2)*Mathf.Pow(-1,cnt/2);
                    pos = new Vector3(new_x,pos.y,new_z);
                    cnt += 1;
                }
            }
            if (cnt >= 24) {
                count -= 1;
            }
        }
    }

    private void Check_Ore()
    {
        float srnd = Random.Range(0f,1f);
        if (srnd <= (_purified ? _Sector._ore_spawn_base_percent : _Sector._ore_spawn_base_percent * _Sector._ore_ruin_factor))
        {
            int rnd = Random.Range(0,_Sector._ore_count_factor[1]);
            if (rnd < _Sector._ore_count_factor[0])
            {
                Spawn_Ore(1);
            }
            else if (rnd < _Sector._ore_count_factor[1])
            {
                Spawn_Ore(2);
            }
        }
    }

    private void Spawn_Ore(int count)
    {
        while (count>0)
        {
            Vector3 pos = new Vector3(_base_transform.position.x+Random.Range(-5f,5f),_base_transform.position.y+0.5f,_base_transform.position.z+Random.Range(-5f,5f));
            int cnt = 0;
            while (cnt < 24)
            {
                _buffer = Physics.OverlapBox(pos,new Vector3(0.5f,0.5f,0.5f));
                // Debug.Log(_buffer.Length);
                if (_buffer.Length==0)
                {
                    int rnd = Random.Range(0,_Sector._ore_type_factor[2]);
                    if (rnd < _Sector._ore_type_factor[0])
                    {
                        Instantiate(_ores[0],pos,new Quaternion());
                        count -= 1;
                        break;
                    }
                    else if (rnd < _Sector._ore_type_factor[1])
                    {
                        Instantiate(_ores[1],pos,new Quaternion());
                        count -= 1;
                        break;
                    }
                    else if (rnd < _Sector._ore_type_factor[2])
                    {
                        Instantiate(_ores[2], pos, new Quaternion());
                    }
                }
                else
                {
                    float new_x = pos.x + ((cnt+1)%2)*Mathf.Pow(-1,cnt/2);
                    float new_z = pos.z + (cnt%2)*Mathf.Pow(-1,cnt/2);
                    pos = new Vector3(new_x,pos.y,new_z);
                    cnt += 1;
                }
            }
            if (cnt >= 24) {
                count -= 1;
            }
        }
    }
}

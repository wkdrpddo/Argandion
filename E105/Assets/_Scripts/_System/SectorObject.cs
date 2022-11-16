using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorObject : MonoBehaviour
{
    public int _sectorNumber;
    public bool _purifier;
    public int _flower_max;
    public int _flower_remain;
    public float _flower_spawn_base_percent;
    public float _flower_spawn_percent;
    public float _flower_spawn_factor;
    public float _flower_spawn_bonus_factor = 1.05f;
    public float _flower_spawn_distance;
    public int _tree_max;
    public int _tree_remain;
    public float _tree_spawn_base_percent;
    public float _tree_spawn_factor;
    public float _tree_ruin_spawn_factor;
    public float _tree_ruin_max_factor;
    public float _mushroom_spawn_base_percent;
    public int[] _mushroom_type_factor = new int[3];
    public int[] _mushroom_count_factor = new int[2];
    public float _mushroom_ruin_factor;
    public float _branch_spawn_base_percent;
    public float _branch_ruin_factor;
    public int[] _branch_type_factor = new int[3];
    public float _ore_spawn_base_percent;
    public float _ore_ruin_factor;
    public int[] _ore_type_factor = new int[3];
    public int[] _ore_count_factor = new int[3];
    public float _stone_spawn_base_percent;
    public float _stone_ruin_factor;
    public int[] _stone_type_factor = new int[3];
    public float _animal_spawn_base_percent;
    public float _animal_spawn_ruin_percent;
    public int[] _animal_type_base_factor = new int[4];
    public int[] _animal_type_ruin_factor = new int[4];
    public int[] _animal_bunny_count_factor;
    public int[] _animal_deer_count_factor;
    public int[] _animal_wolf_count_factor;
    public int[] _animal_bear_count_factor;
    public Collider[] _SectorCollider;
    public List<float> _SectorArea;
    public GameObject _floweObject;
    public GameObject _items;
    private BuffManager _BuffManager;
    private float _sumSector;
    // Start is called before the first frame update
    void Start()
    {
        _items = GameObject.Find("Items");
        _BuffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        _SectorArea.Add(0f);
        foreach (Collider col in _SectorCollider)
        {
            float sumArea = (col.bounds.extents.x*2) * (col.bounds.extents.z*2);
            // Debug.Log(sumArea);
            _sumSector += sumArea;
            _SectorArea.Add(_sumSector);
        }
    }


    public void DayEnd()
    {
    }

    public void DayStart()
    {
        Check_Flower();
        Check_Grid();
    }

    private void Check_Flower()
    {
        if (_flower_remain < _flower_max)
        {
            float rnd = Random.Range(0f,1f);
            if (rnd < _flower_spawn_percent)
            {
                Spawn_Flower();
            }
            else
            {
                _flower_spawn_percent = _flower_spawn_percent*_flower_spawn_factor*(_BuffManager.yellowPray ? _flower_spawn_bonus_factor : 1.0f);
                _flower_spawn_percent = Mathf.Min(0.8f,_flower_spawn_percent);
            }
        }
    }

    private void Spawn_Flower()
    {
        Debug.Log("꽃 생성");
        float rnd = Random.Range(0f,_sumSector);
        int count = 0;
        while (!(_SectorArea[count]<=rnd && rnd<=_SectorArea[count+1]))
        {
            count+=1;
        }
        Debug.Log(count);
        Debug.Log("번 지역에 생성");
        Collider tcol = _SectorCollider[count];
        Vector3 pos = new Vector3(tcol.bounds.center.x+Random.Range(-tcol.bounds.extents.x,tcol.bounds.extents.x),tcol.bounds.center.y+3f,tcol.bounds.center.z+Random.Range(-tcol.bounds.extents.z,tcol.bounds.extents.z));
        GameObject flower = Instantiate(_floweObject,pos,new Quaternion(),gameObject.transform);
        flower.GetComponent<GatheringObject>().setFlower(true);
        Debug.Log(pos);
        _flower_remain += 1;
    }

    private void Check_Grid()
    {
        GridSystem[] grids = this.GetComponentsInChildren<GridSystem>();
        PointSystem[] points = this.GetComponentsInChildren<PointSystem>();
        MushroomSpawn[] mushroom = this.GetComponentsInChildren<MushroomSpawn>();
        foreach (var grid in grids)
        {
            grid.DayStart();
        }
        foreach (var point in points)
        {
            point.DayStart();
        }
        foreach (var mush in mushroom)
        {
            mush.DayStart();
        }
    }

    public void Purifier()
    {
        _purifier = true;
        
        //나무 오브젝트 등등 정화된 걸로 바꾸기
    }
}

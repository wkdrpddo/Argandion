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
    public float _flower_spawn_bonus_factor;
    public float _flower_spawn_distance;
    public int _tree_max;
    public int _tree_remain;
    public float _tree_spawn_base_percent;
    public float _tree_spawn_factor;
    public float _tree_ruin_spawn_factor;
    public float _tree_ruin_max_factor;
    public float _mushroom_spawn_base_percent;
    public float _mushroom_spawn_factor;
    public int[] _mushroom_type_factor = new int[3];
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
    public float[] _animal_ruin_factor = new float[4];
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void DayEnd()
    {
        Check_Flower();
        Check_Grid();
    }

    private void Check_Flower()
    {
        if (_flower_remain < _flower_max)
        {
            float rnd = Random.Range(0f,100f);
            if (rnd < _flower_spawn_percent)
            {
                Spawn_Flower();
            }
            else
            {
                _flower_spawn_percent = _flower_spawn_percent*_flower_spawn_factor*_flower_spawn_bonus_factor;
                _flower_spawn_percent = Mathf.Min(100,_flower_spawn_percent);
            }
        }
    }

    private void Spawn_Flower()
    {
        Debug.Log("꽃 생성");
        _flower_remain += 1;
        _flower_spawn_percent = _flower_spawn_base_percent;
    }

    private void Check_Grid()
    {
        GridSystem[] grids = this.GetComponentsInChildren<GridSystem>();
        foreach (var grid in grids)
        {
            grid.DayEnd();
        }
    }
}

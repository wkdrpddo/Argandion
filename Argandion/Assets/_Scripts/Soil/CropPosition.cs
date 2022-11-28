using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropPosition : MonoBehaviour
{
    public int _num;
    public int _parent_dirt;
    public int _state;
    public PlayerSystem _ps;
    public GameObject[] _crops;
    public Dirt _pd;
    public GameObject _plant;

    void Awake()
    {
        _ps = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
        _crops = _ps._crops;
        _pd = gameObject.transform.parent.gameObject.GetComponent<Dirt>();
        _parent_dirt = _pd._dirt_number;
    }

    void Start()
    {
        _ps = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
        _crops = _ps._crops;
        _pd = gameObject.transform.parent.gameObject.GetComponent<Dirt>();
        _parent_dirt = _pd._dirt_number;
    }

    public void Interaction(int itemCode)
    {
        // Debug.Log("심기");
        _plant = Instantiate(_crops[itemCode - 212], gameObject.transform.position, gameObject.transform.rotation,gameObject.transform);
        _plant.GetComponent<Crop>()._pCpo = this;
        _plant.GetComponent<Crop>()._pd = _pd;
        _state = itemCode - 100;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    public int watered;
    public int minusWater;
    private GameObject[] _nearObjects;
    ParticleSystem particleObject;
    public SystemManager _system;
    public int temp;
    public GameObject _buffManagerObject;
    private BuffManager _buff;

    void Start()
    {
        particleObject = GetComponent<ParticleSystem>();
        temp = _system._day;
        _buffManagerObject = GameObject.Find("BuffManager");
        _buff = _buffManagerObject.GetComponent<BuffManager>();
    }

    void Update()
    {
        if (temp != _system._day) {
            NewDay();
            temp = _system._day;
        }
    }

    void NewDay()
    {  
        if (gameObject.tag == "wateredDirt")
        {
            _nearObjects = GameObject.FindGameObjectsWithTag("crop");
            watered -= minusWater;
            if (_buff.bluePray) {
                watered += 25 * _nearObjects.Length;
            }
            if (watered < 0) {
                watered = 0;
                gameObject.tag = "dirt";
                particleObject.Stop();
            } else {
                for (int i = 0; i < _nearObjects.Length; i++){
                    if ( _nearObjects[i].GetComponent<Crop>() != null ) {
                        Crop crop = _nearObjects[i].GetComponent<Crop>();
                        crop.growUp();
                    }
                    else {
                    }
                }
            }
        }
    }

    public void Water()
    {
        gameObject.tag = "wateredDirt";
        watered = 15000;
        particleObject.Play();
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("wheat")) {
    //         minusWater += 20;
    //     }
    // }

    // private void OnTriggerExit(Collider other) {
    //     if (other.gameObject.CompareTag("wheat")) {
    //         minusWater -= 20;
    //     }
    // }
}

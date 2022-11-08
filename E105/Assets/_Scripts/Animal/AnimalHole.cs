using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalHole : MonoBehaviour
{
    // [SerializeField] private GameObject _systemManager;
    [SerializeField] private GameObject animal;
    [SerializeField] private int num;

    private bool day_start = false;

    // Start is called before the first frame update
    void Start()
    {
        // _systemManager = GameObject.Find("SystemManager");
        animal_generation();

    }

    // Update is called once per frame
    void Update()
    {

    }


    //아침마다 아래 함수 호출하기 
    public void animal_generation()
    {
        for (int i = 0; i < num; i++)
        {
            Instantiate(animal, transform.position, Quaternion.identity).transform.parent = GameObject.Find("Animals").transform;
        }
    }

}

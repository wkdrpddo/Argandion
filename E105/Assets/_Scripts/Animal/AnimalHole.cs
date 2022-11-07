using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalHole : MonoBehaviour
{
    [SerializeField] private GameObject animal;
    [SerializeField] private int num;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < num; i++)
        {   
            Instantiate(animal, transform.position, Quaternion.identity).transform.parent = this.transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitHole : MonoBehaviour
{
    [SerializeField] public GameObject rabbit1;
    [SerializeField] public GameObject rabbit2;
    [SerializeField] public GameObject rabbit3;
    [SerializeField] public int num;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < num; i++)
        {
            Instantiate(rabbit1, transform.position, Quaternion.identity).transform.parent = GameObject.Find("Animals").transform;

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}

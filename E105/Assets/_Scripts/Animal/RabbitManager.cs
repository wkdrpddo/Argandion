using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitManager : MonoBehaviour
{
    [SerializeField] public Vector3 pos;
    [SerializeField] public GameObject rabbit1;
    // public GameObject rabbit1 = Resources.Load("Prefabs/Animal/Rabbit_01") as GameObject;
    // public GameObject rabbit2 = Resources.Load("Prefabs/Animal/Rabbit_02") as GameObject;
    // public GameObject rabbit3 = Resources.Load("Prefabs/Animal/Rabbit_03") as GameObject;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(rabbit1, pos, Quaternion.identity).transform.parent = this.transform;

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}

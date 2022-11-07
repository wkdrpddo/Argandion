using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitHole : MonoBehaviour
{
    [SerializeField] public Vector3 pos;
    [SerializeField] public GameObject rabbit1;
    [SerializeField] public GameObject rabbit2;
    [SerializeField] public GameObject rabbit3;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(rabbit1, pos, Quaternion.identity).transform.parent = this.transform;
            Instantiate(rabbit2, pos, Quaternion.identity).transform.parent = this.transform;
            Instantiate(rabbit3, pos, Quaternion.identity).transform.parent = this.transform;
        }
    
    }

    // Update is called once per frame
    void Update()
    {

    }
}

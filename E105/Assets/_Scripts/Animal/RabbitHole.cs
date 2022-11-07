using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitHole : MonoBehaviour
{
    [SerializeField] public GameObject rabbit1;
    [SerializeField] public GameObject rabbit2;
    [SerializeField] public GameObject rabbit3;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            Instantiate(rabbit1, transform.position, Quaternion.identity).transform.parent = this.transform;
            Instantiate(rabbit2, transform.position, Quaternion.identity).transform.parent = this.transform;
            Instantiate(rabbit3, transform.position, Quaternion.identity).transform.parent = this.transform;
        }
    
    }

    // Update is called once per frame
    void Update()
    {

    }
}

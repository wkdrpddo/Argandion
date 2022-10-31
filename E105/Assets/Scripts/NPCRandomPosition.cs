using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomPosition : MonoBehaviour
{
    public Transform _myTransform = null;
    public SystemManager _SystemManager;

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RandomPosition()
    {
        _myTransform.position = new Vector3(Random.Range(-60.0f, 60.0f), 0, Random.Range(-60.0f, 60.0f));
    }
}

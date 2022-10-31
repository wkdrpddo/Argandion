using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat3 : MonoBehaviour
{
    public GameObject wheatOnField;
    public Transform wheat3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Harvested()
    {
        Destroy(gameObject);
        Instantiate(wheatOnField, wheat3.position, wheat3.rotation);
        Debug.Log("수확했당!");
    }
}

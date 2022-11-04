using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvested : MonoBehaviour
{
    public GameObject cropOnField;
    public Transform self;
    public int[] howMany;

    // Start is called before the first frame update
    Vector3 aboveSoil(Vector3 pos) {
        return new Vector3(pos.x, pos.y + 1, pos.z); 
    }

    void Start()
    {

    }

    public void Harvesting()
    {
        Destroy(gameObject);
        int many = howMany[Random.Range(0, howMany.Length)];
        for (int i = 0; i < many; i++) {
            Instantiate(cropOnField, aboveSoil(self.position), self.rotation);
        }
    }
}

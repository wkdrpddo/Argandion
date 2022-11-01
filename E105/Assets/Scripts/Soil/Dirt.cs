using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    public int watered;
    public int minusWater;
    private GameObject[] _nearObjects;
    ParticleSystem particleObject;

    void Start()
    {
        particleObject = GetComponent<ParticleSystem>();
    }

    void newDay()
    {
        if (gameObject.tag == "wateredDirt")
        {
            watered -= minusWater;
            if (watered < 0) {
                watered = 0;
                gameObject.tag = "dirt";
                particleObject.Stop();
            } else {
                
            }
        }
    }

    public void Water()
    {
        gameObject.tag = "wateredDirt";
        watered = 1500;
        particleObject.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wheat")) {
            minusWater += 20;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("wheat")) {
            minusWater -= 20;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    float wateredTime;
    ParticleSystem particleObject;

    void Start()
    {
        particleObject = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (gameObject.tag == "wateredDirt")
        {
            wateredTime -= Time.deltaTime;
            if (wateredTime < 0.0f) {
                gameObject.tag = "dirt";
                particleObject.Stop();
            }
        }
    }

    public void Water()
    {
        gameObject.tag = "wateredDirt";
        wateredTime = 50f;
        particleObject.Play();
    }
}

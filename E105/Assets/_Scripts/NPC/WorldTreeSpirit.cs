using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTreeSpirit : MonoBehaviour
{

    private GameObject _UIManager;

    private bool is_up;

    void Start()
    {
        _UIManager = GameObject.Find("UIManager");
    }

    void Update()
    {

        if (is_up)
        {
            this.transform.position += new Vector3(0, 0.01f, 0);
            if (this.transform.position.y > 3)
            {
                is_up = false;
            }
        }
        else
        {
            this.transform.position += new Vector3(0, -0.01f, 0);
            if (this.transform.position.y < 1)
            {
                is_up = true;
            }
        }
    }

    public void FlowerInteraction(int flowerCode)
    {
        _UIManager.GetComponent<UIManager>().prayToSpirit(flowerCode);
    }
}

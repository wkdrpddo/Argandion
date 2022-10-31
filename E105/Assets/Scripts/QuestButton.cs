using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestButton : MonoBehaviour
{
    public int idx;

    public void destroyed()
    {
        MenuPanel obj = this.GetComponentInParent<MenuPanel>();
        obj.deleteObject(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

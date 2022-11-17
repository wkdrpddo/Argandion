using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIconRot : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.rotation = Quaternion.Euler(90,0,0);
    }


}

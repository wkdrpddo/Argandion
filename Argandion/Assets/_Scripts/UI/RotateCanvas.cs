using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCanvas : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.forward * 30 * Time.deltaTime);
    }
}

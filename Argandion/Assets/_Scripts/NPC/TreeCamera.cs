using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCamera : MonoBehaviour
{
    private SystemManager _system;
    private CameraSystem _camera;

    void Start()
    {
        _system = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        _camera = GameObject.Find("PlayerObject").GetComponent<CameraSystem>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer==3)
        {
            _camera.treeCamera();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer==3)
        {
            _camera.ResetCamera();
        }
    }
}

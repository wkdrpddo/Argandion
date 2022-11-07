using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private bool _hold_cam;
    private PlayerSystem _ps;
    private SystemManager _sm;
    private GameObject _cam;
    // Start is called before the first frame update
    void Start()
    {
        _ps = GameObject.Find("Playerobject").GetComponent<PlayerSystem>();
        _sm = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        _cam = GameObject.Find("Main Camera");
    }

    public void CookingCamera()
    {
        Vector3 _pos = new Vector3(0.15f,2.51f,2.75f);
        Quaternion _rot = Quaternion.Euler(30f,180f,0.4f);
        _cam.transform.SetPositionAndRotation(_pos,_rot);
    }
}

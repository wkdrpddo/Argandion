using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public bool _hold_cam;
    private PlayerSystem _ps;
    private SystemManager _sm;
    private GameObject _cam;
    private GameObject _po;
    private GameObject _worldtree;
    // Start is called before the first frame update
    void Start()
    {
        _po = GameObject.Find("PlayerObject");
        _ps = _po.GetComponent<PlayerSystem>();
        _sm = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        _cam = GameObject.Find("Main Camera");
        _worldtree = GameObject.Find("WorldTree");
    }

    public void CookingCamera(float xRot, float yRot, float zRot)
    {
        Debug.Log("쿠킹 카메라");
        Vector3 pos = new Vector3(_po.transform.position.x + 2.17f, _po.transform.position.y + 2.51f, _po.transform.position.z + 1.7f);
        Quaternion rot = Quaternion.Euler(xRot, yRot, zRot);
        _cam.transform.SetPositionAndRotation(pos, rot);
    }

    public void ResetCamera()
    {
        _cam.transform.parent = _po.transform;
        Vector3 pos = new Vector3(_po.transform.position.x + 0f, _po.transform.position.y + 5.5f, _po.transform.position.z + -8f);
        Quaternion rot = Quaternion.Euler(30f, 0f, 0f);
        _cam.transform.SetPositionAndRotation(pos, rot);
    }

    public void treeCamera()
    {
        _cam.transform.parent = _worldtree.transform;
        Vector3 pos = new Vector3(_worldtree.transform.position.x, _worldtree.transform.position.y + 13.0f, _worldtree.transform.position.z + -36f);
        Quaternion rot = Quaternion.Euler(0f, 0f, 0f);
        
        switch(_sm.getDevelopLevel())
        {
            case 1:
                pos = new Vector3(_worldtree.transform.position.x, _worldtree.transform.position.y + 13.0f, _worldtree.transform.position.z + -36f);
                rot = Quaternion.Euler(0f, 0f, 0f);
                break;
            case 2:
                pos = new Vector3(_worldtree.transform.position.x, _worldtree.transform.position.y + 13.0f, _worldtree.transform.position.z + -36f);
                rot = Quaternion.Euler(-3.6f, 0f, 0f);
                break;
            case 3:
                pos = new Vector3(_worldtree.transform.position.x, _worldtree.transform.position.y + 13.0f, _worldtree.transform.position.z + -55f);
                rot = Quaternion.Euler(-12f, 0f, 0f);
                break;
        }

        _cam.transform.SetPositionAndRotation(pos, rot);
    }

    // test code
    // void Update()
    // {
    //     if (_hold_cam)
    //     {
    //         CookingCamera();
    //         _hold_cam = false;
    //     }
    // }
}

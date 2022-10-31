using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBuildingFence : MonoBehaviour
{
    public GameObject[] _Fence = new GameObject[5];
    public GameObject _sign;

    private Vector3 _colliderSize;
    private Vector3 _colliderCenter;
    private Vector3 _fenceSizeMax;

    public BoxCollider _collider;

    public int _sectorNum;

    public SystemManager _systemManager;


    void Start()
    {

        setValue();
        makeFence();




    }


    // public void checkPurification()
    // {
    //     // 정화 판정
    //     if (_systemManager._purification(_sectorNum))
    //     {
    //         Destory(gameObject);
    //     }
    // }


    public void setValue()
    {
        // 펜스 범위 collider
        _collider = GetComponent<BoxCollider>();
        _colliderSize = _collider.size;
        _colliderCenter = _collider.bounds.center;

        Debug.Log(_colliderSize);
        Debug.Log(_colliderCenter);

        // fench 최대 사이즈 설정
        Vector3 fence0Size = _Fence[0].GetComponent<BoxCollider>().size;
        Vector3 fence1Size = _Fence[1].GetComponent<BoxCollider>().size;
        Vector3 fence2Size = _Fence[2].GetComponent<BoxCollider>().size;
        _fenceSizeMax = new Vector3(0, 0, 0);
        _fenceSizeMax.x = (fence0Size.x > fence1Size.x) ? fence0Size.x : (fence1Size.x > fence2Size.x ? fence1Size.x : fence2Size.x);
        // _fenceSizeMax.y = (fence0Size.y > fence1Size.y) ? fence0Size.y : (fence1Size.y > fence2Size.y ? fence1Size.y : fence2Size.y);
        _fenceSizeMax.z = (fence0Size.z > fence1Size.z) ? fence0Size.z : (fence1Size.z > fence2Size.z ? fence1Size.z : fence2Size.z);

        Debug.Log(fence0Size);
        Debug.Log(fence1Size);
        Debug.Log(fence2Size);


    }

    public void makeFence()
    {
        // 위쪽 밑쪽
        int xCnt = (int)(_colliderSize.x / _fenceSizeMax.x);
        float xGap = (_colliderSize.x % _fenceSizeMax.x) / (xCnt - 1);
        // Debug.Log(xGap);
        int cnt = 0;
        while (cnt < xCnt)
        {
            Instantiate(_Fence[Random.Range(0, 5)], new Vector3(_colliderCenter.x + _colliderSize.x / 2 - cnt * (_fenceSizeMax.x + xGap), 0, _colliderCenter.z + _colliderSize.z / 2), Quaternion.identity, this.transform);
            Instantiate(_Fence[Random.Range(0, 5)], new Vector3(_colliderCenter.x + _colliderSize.x / 2 - cnt * (_fenceSizeMax.x + xGap), 0, _colliderCenter.z - _colliderSize.z / 2), Quaternion.identity, this.transform);
            cnt++;
        }

        // 옆쪽
        int zCnt = (int)(_colliderSize.z / _fenceSizeMax.x);
        float zGap = (_colliderSize.z % _fenceSizeMax.x) / (zCnt - 1);
        // Debug.Log(zGap);
        cnt = 0;
        while (cnt < zCnt)
        {
            Instantiate(_Fence[Random.Range(0, 5)], new Vector3(_colliderCenter.x + _colliderSize.x / 2, 0, _colliderCenter.z + _colliderSize.z / 2 - cnt * (_fenceSizeMax.x + zGap)), Quaternion.Euler(0, -90, 0), this.transform);
            Instantiate(_Fence[Random.Range(0, 5)], new Vector3(_colliderCenter.x - _colliderSize.x / 2, 0, _colliderCenter.z + _colliderSize.z / 2 - cnt * (_fenceSizeMax.x + zGap)), Quaternion.Euler(0, -90, 0), this.transform);
            cnt++;
        }

        // 표지판 생성
        Instantiate(_sign, new Vector3(_colliderCenter.x, -0.8f, _colliderCenter.z), Quaternion.Euler(0, -180, 0), this.transform);
    }
}

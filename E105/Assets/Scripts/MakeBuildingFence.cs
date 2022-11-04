using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBuildingFence : MonoBehaviour
{
    // 펜스 관련
    public GameObject[] _fencePrefabs = new GameObject[5];
    public GameObject _fences;

    // 표지판 아이콘
    public Material _icon;

    // 펜스가 들어갈 collider 관련
    private BoxCollider _collider;
    private Vector3 _colliderSize;
    private Vector3 _colliderCenter;

    // 기준 펜스 사이즈
    private Vector3 _fenceSizeMax;

    // 연결 스크립트
    public SystemManager _systemManager;
    public MakeSign _makeSign;
    // public RectTransform _uiSet;

    // 지역 위치
    public int _sectorNum;

    void Start()
    {
        setValue();
        makeFence(); // 테스트 용
    }

    public void checkPurification()
    {
        // 정화 판정
        if (_systemManager._purification[_sectorNum])
        {
            Destroy(gameObject);
        }
    }


    public void setValue()
    {
        // 펜스 범위 collider
        _collider = GetComponent<BoxCollider>();
        _colliderSize = _collider.size;
        _colliderCenter = _collider.bounds.center;

        // fench 최대 사이즈 설정
        Vector3 fence0Size = _fencePrefabs[0].GetComponent<BoxCollider>().size;
        Vector3 fence1Size = _fencePrefabs[1].GetComponent<BoxCollider>().size;
        Vector3 fence2Size = _fencePrefabs[2].GetComponent<BoxCollider>().size;
        _fenceSizeMax = new Vector3(0, 0, 0);
        _fenceSizeMax.x = (fence0Size.x > fence1Size.x) ? fence0Size.x : (fence1Size.x > fence2Size.x ? fence1Size.x : fence2Size.x);
        _fenceSizeMax.z = (fence0Size.z > fence1Size.z) ? fence0Size.z : (fence1Size.z > fence2Size.z ? fence1Size.z : fence2Size.z);
    }

    public void makeFence()
    {
        // 펜스 위아래 위치(y)
        float fencePositionY = gameObject.transform.parent.position.y;

        // 위쪽 밑쪽
        int xCnt = (int)(_colliderSize.x / _fenceSizeMax.x);
        float xGap = (_colliderSize.x % _fenceSizeMax.x) / (xCnt - 1);
        int cnt = 0;
        while (cnt < xCnt)
        {
            Instantiate(_fencePrefabs[Random.Range(0, 5)], new Vector3(_colliderCenter.x + _colliderSize.x / 2 - cnt * (_fenceSizeMax.x + xGap), fencePositionY, _colliderCenter.z + _colliderSize.z / 2), Quaternion.identity, _fences.transform);
            Instantiate(_fencePrefabs[Random.Range(0, 5)], new Vector3(_colliderCenter.x + _colliderSize.x / 2 - cnt * (_fenceSizeMax.x + xGap), fencePositionY, _colliderCenter.z - _colliderSize.z / 2), Quaternion.identity, _fences.transform);
            cnt++;
        }

        // 양옆쪽
        int zCnt = (int)(_colliderSize.z / _fenceSizeMax.x);
        float zGap = (_colliderSize.z % _fenceSizeMax.x) / (zCnt - 1);
        cnt = 0;
        while (cnt < zCnt)
        {
            Instantiate(_fencePrefabs[Random.Range(0, 5)], new Vector3(_colliderCenter.x + _colliderSize.x / 2, fencePositionY, _colliderCenter.z + _colliderSize.z / 2 - cnt * (_fenceSizeMax.x + zGap)), Quaternion.Euler(0, -90, 0), _fences.transform);
            Instantiate(_fencePrefabs[Random.Range(0, 5)], new Vector3(_colliderCenter.x - _colliderSize.x / 2, fencePositionY, _colliderCenter.z + _colliderSize.z / 2 - cnt * (_fenceSizeMax.x + zGap)), Quaternion.Euler(0, -90, 0), _fences.transform);
            cnt++;
        }

        // 표지판 생성
        _makeSign.makeSign(_colliderCenter.x, fencePositionY, _colliderCenter.z - _colliderSize.z / 2 - 0.12f, _fences.transform, _icon);

    }
}

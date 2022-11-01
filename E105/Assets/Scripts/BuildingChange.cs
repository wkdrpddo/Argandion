using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingChange : MonoBehaviour
{
    public GameObject[] _buildings = new GameObject[3];
    public bool _underConstruction;
    public int _phase = -1;
    public MakeBuildingFence _fences;


    // 테스트용 변수
    public bool _buildStart;

    // 테스트용 update();
    void Update()
    {
        if (_buildStart)
        {
            BuildStart();
            _buildStart = false;
        }

    }

    // 상호작용해서 건축 시작하기
    public void BuildStart()
    {
        _underConstruction = true; // 건축 시작
        _phase = 0; // 0단계
        // 중간 건축 생성
        Instantiate(gameObject, new Vector3(_fences._colliderCenter.x, 0, _fences._colliderCenter.z), Quaternion.identity);
    }

    // 하루 끝
    // 건축 중이면 건물 오브젝트 바꾸기
    // 건축 마지막 날이면 Destory()
    public void DayEnd()
    {

    }

    // 건물 오브젝트 바꾸기
    public void updateObject(int index)
    {
        // 중간 건축물 바꾸기

        // 최종 건축물로 바꾸기


    }


}

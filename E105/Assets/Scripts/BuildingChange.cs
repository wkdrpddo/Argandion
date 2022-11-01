using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingChange : MonoBehaviour
{
    public GameObject[] _buildings = new GameObject[3];
    public bool _underConstruction;
    public int _phase = -1;
    public MakeBuildingFence _makeFences;

    public GameObject _constructionSet;
    public GameObject _buildingPlace;

    // 테스트용 변수
    public bool _buildStartTest;

    // 테스트용 update();
    void Update()
    {
        if (_buildStartTest)
        {
            BuildStart();
            _buildStartTest = false; // 테스트용 변수
        }

    }

    // 상호작용해서 건축 시작하기
    public void BuildStart()
    {
        // construction 위치 => 수정하기 편하게 변수화
        float constructionX = _buildingPlace.transform.position.x + -2.39f;
        float constructionY = _buildingPlace.transform.position.y + -9.42f;
        float constructionZ = _buildingPlace.transform.position.z + 2.38f;
        _underConstruction = true; // 건축 시작
        _phase = 0; // 0단계
        // 중간 건축 생성
        Debug.Log(_makeFences._colliderCenter);
        Instantiate(_constructionSet, new Vector3(constructionX, constructionY, constructionZ), Quaternion.identity, _buildingPlace.transform);
        Destroy(_makeFences._fences);
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

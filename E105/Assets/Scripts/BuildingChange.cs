using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingChange : MonoBehaviour
{
    // public GameObject[] _buildings;
    public bool _underConstruction;
    public int _phase = -1;
    public MakeBuildingFence _makeFences;

    public GameObject _constructionPrefab;
    public GameObject _buildingPlace;

    private GameObject _constructionSet;

    // 테스트용 변수
    public bool _buildStartTest;
    public bool _dayEnd;

    // 테스트용 update();
    void Update()
    {
        if (_buildStartTest)
        {
            _buildStartTest = false; // 테스트용 변수
            BuildStart();
        }
        if (_dayEnd)
        {
            DayEnd();
            _dayEnd = false;
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
        _phase = 1; // 1단계 (펜스에서 자재)
        // 중간 건축 생성
        Debug.Log(_makeFences._colliderCenter);
        _constructionSet = Instantiate(_constructionPrefab, new Vector3(constructionX, constructionY, constructionZ), Quaternion.identity, _buildingPlace.transform);
        Debug.Log(_constructionSet);
        // 배열 저장
        // _buildings = _constructionSet.transform.GetChild(); //
        Destroy(_makeFences._fences);
    }

    // 하루 끝
    // 건축 중이면 건물 오브젝트 바꾸기
    // 건축 마지막 날이면 Destory()
    public void DayEnd()
    {
        if (_underConstruction)
        {
            // 자재에서 돌 벽, 돌 벽에서 벽
            if (_phase == 1 || _phase == 2)
            {
                updateObject(_phase++);
            }
            // 벽에서 완성
            else if (_phase == 3)
            {
                updateObject(_phase);
                Destroy(_constructionSet);
            }
        }
    }

    // 건물 오브젝트 바꾸기 (현재 단계 들어옴)
    public void updateObject(int index)
    {
        Debug.Log("index: " + index);
        // 중간 건축물 바꾸기
        _constructionSet.transform.GetChild(index - 1).gameObject.SetActive(false);
        _constructionSet.transform.GetChild(index).gameObject.SetActive(true);


        // 최종 건축물로 바꾸기


    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingChange : MonoBehaviour
{

    public bool _underConstruction;
    public int _phase = -1;
    public MakeBuildingFence _makeFences;

    public GameObject _constructionPrefab;
    public GameObject _buildingPlace;
    public GameObject _buildingFinish;

    private GameObject _constructionSet;

    // construction 위치
    public float _constructionX;
    public float _constructionY;
    public float _constructionZ;

    // building finish 위치
    public float _fBuildingX;
    public float _fBuildingY;
    public float _fBuildingZ;

    // building finish y 회전값
    public float _fBuildingRotY;


    // ==================================================== 지울 위치 (시작) ==================================

    // 테스트용 변수
    public bool _buildStartTest;
    public bool _dayEndTest;

    // 테스트용 update();
    void Update()
    {
        if (_buildStartTest)
        {
            Debug.Log("buildStart");
            _buildStartTest = false; // 테스트용 변수
            BuildStart();
        }
        if (_dayEndTest)
        {
            Debug.Log("dayEnd");
            DayEnd();
            _dayEndTest = false;
        }

    }

    // ==================================================== 지울 위치 (끝) ==================================

    // 상호작용해서 건축 시작하기
    public void BuildStart()
    {
        if (!_underConstruction)
        {
            // _constructionX = _buildingPlace.transform.position.x + -2.39f;
            // _constructionY = _buildingPlace.transform.position.y + -9.42f;
            // _constructionZ = _buildingPlace.transform.position.z + 2.38f;
            _underConstruction = true; // 건축 시작
            _phase = 1; // 1단계 (펜스에서 자재)
                        // 중간 건축 생성
                        // Debug.Log(_makeFences._colliderCenter);
            _constructionSet = Instantiate(_constructionPrefab, new Vector3(_buildingPlace.transform.position.x + _constructionX, _buildingPlace.transform.position.y + _constructionY, _buildingPlace.transform.position.z + _constructionZ), Quaternion.identity, _buildingPlace.transform);
            // Debug.Log(_constructionSet);
            Destroy(_makeFences._fences);
        }
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
                _underConstruction = false;
                Destroy(gameObject);
            }
        }
    }

    // 건물 오브젝트 바꾸기 (현재 단계 들어옴)
    public void updateObject(int index)
    {
        // Debug.Log("index: " + index);
        // 최종 건축물로 바꾸기
        if (index == 3)
        {
            // finish building 위치 => 수정하기 편하게 변수화
            // float _fBuildingX = _buildingPlace.transform.position.x + 3.51f;
            // float _fBuildingY = _buildingPlace.transform.position.y;
            // float _fBuildingZ = _buildingPlace.transform.position.z + -3.29f;

            Instantiate(_buildingFinish, new Vector3(_buildingPlace.transform.position.x + _fBuildingX, 0, _buildingPlace.transform.position.z + _fBuildingZ), Quaternion.Euler(0, _fBuildingRotY, 0), _buildingPlace.transform.parent.gameObject.transform);
            return;
        }
        // 중간 건축물 바꾸기
        _constructionSet.transform.GetChild(index - 1).gameObject.SetActive(false);
        _constructionSet.transform.GetChild(index).gameObject.SetActive(true);
    }


}

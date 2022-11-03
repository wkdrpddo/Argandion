using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingChange : MonoBehaviour
{

    // public bool _underConstruction;
    public int _phase = 0; // 0: fence, 1: 자재, 2: 돌담, 3: 벽, 4: 완성
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
    public bool _fulfillTest;
    public bool _dayEndTest;

    // 테스트용 update();
    void Update()
    {
        if (_fulfillTest)
        {
            Debug.Log("condition fulfill");
            _fulfillTest = false; // 테스트용 변수
            _phase = 1;
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
        // if (!_underConstruction)
        // {
        //     // _constructionX = _buildingPlace.transform.position.x + -2.39f;
        //     // _constructionY = _buildingPlace.transform.position.y + -9.42f;
        //     // _constructionZ = _buildingPlace.transform.position.z + 2.38f;
        //     _underConstruction = true; // 건축 시작
        //     _phase = 1; // 1단계 (펜스에서 자재)
        //                 // 중간 건축 생성
        //                 // Debug.Log(_makeFences._colliderCenter);
        //     _constructionSet = Instantiate(_constructionPrefab, new Vector3(_buildingPlace.transform.position.x + _constructionX, _buildingPlace.transform.position.y + _constructionY, _buildingPlace.transform.position.z + _constructionZ), Quaternion.identity, _buildingPlace.transform);
        //     // Debug.Log(_constructionSet);
        //     Destroy(_makeFences._fences);
        // }
        // _underConstruction = true; // 건축 시작
        _constructionSet = Instantiate(_constructionPrefab, new Vector3(_buildingPlace.transform.position.x + _constructionX, _buildingPlace.transform.position.y + _constructionY, _buildingPlace.transform.position.z + _constructionZ), Quaternion.identity, _buildingPlace.transform);
        Destroy(_makeFences._fences);
        // Destroy(gameObject.GetComponent<BoxCollider>());

    }

    // 하루 끝
    // 건축 중이면 건물 오브젝트 바꾸기
    // 건축 마지막 날이면 Destory()
    public void DayEnd()
    {
        switch (_phase)
        {
            case 1:
                BuildStart();
                break;
            case 2:
            case 3:
                UpdateObject();
                break;
            case 4:
                FinishConstruction();
                Destroy(_constructionSet);
                // _underConstruction = false;
                Destroy(gameObject);
                break;
        }
        _phase++;
    }

    // 중간 건축물 바꾸기
    void UpdateObject()
    {
        _constructionSet.transform.GetChild(_phase - 2).gameObject.SetActive(false);
        _constructionSet.transform.GetChild(_phase - 1).gameObject.SetActive(true);
    }

    // 최종 건축물로 바꾸기
    void FinishConstruction()
    {
        Instantiate(_buildingFinish, new Vector3(_buildingPlace.transform.position.x + _fBuildingX, _fBuildingY, _buildingPlace.transform.position.z + _fBuildingZ), Quaternion.Euler(0, _fBuildingRotY, 0), _buildingPlace.transform.parent.gameObject.transform);
    }


}

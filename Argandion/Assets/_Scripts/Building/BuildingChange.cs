using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingChange : MonoBehaviour
{
    public int _phase = 0; // 0: fence, 1: 자재, 2: 돌담, 3: 벽, 4: 완성

    // 스크립트
    public MakeBuildingFence _makeFences;
    public MakeSign _makeSign;
    public SystemManager _systemManager;

    // 게임 오브젝트
    public GameObject _constructionPrefab;
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

    // 상호작용해서 건축 시작하기 => 자재 건축물 넣기
    public void BuildStart()
    {
        // 건축 과정 건축물 만들기
        _constructionSet = Instantiate(_constructionPrefab, new Vector3(gameObject.transform.position.x + _constructionX, gameObject.transform.position.y + _constructionY, gameObject.transform.position.z + _constructionZ), Quaternion.identity, gameObject.transform);
        // build 1 Collider 가지고 있는 Object
        GameObject floor = _constructionSet.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        // 표지판 만들기
        _makeSign.makeSign(gameObject.transform.position.x, gameObject.transform.parent.position.y, gameObject.transform.position.z - floor.GetComponent<BoxCollider>().size.z * floor.transform.localScale.z / 2, _constructionSet.transform, _makeFences._icon, false, 0);
        // 펜스 지우기
        Destroy(_makeFences._fences);
        Destroy(gameObject.GetComponent<BoxCollider>());
    }

    // 하루 시작
    // 건축 중이면 건물 오브젝트 바꾸기
    // 건축 마지막 날이면 Destory()
    public void DayStart()
    {
        switch (_phase)
        {
            case 1:
                BuildStart();
                _phase++;
                break;
            case 2:
                UpdateObject();
                _phase++;
                break;
            case 3:
                UpdateObject();
                _phase++;
                break;
            case 4:
                FinishConstruction();
                // 정화 콜
                _systemManager.UpdatePurification(_makeFences._sectorNum);
                Destroy(_constructionSet);
                Destroy(gameObject);
                break;
        } 
    }

    // 중간 건축물 바꾸기
    void UpdateObject()
    {
        _constructionSet.transform.GetChild(_phase - 2).gameObject.SetActive(false);
        _constructionSet.transform.GetChild(_phase - 1).gameObject.SetActive(true);
        BoxCollider boxCollider = _constructionSet.transform.GetChild(_phase - 1).gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider>();
        _makeSign.moveSign(gameObject.transform.position.x, gameObject.transform.position.z - boxCollider.size.z / 2 - 0.8f);
    }

    // 최종 건축물로 바꾸기
    void FinishConstruction()
    {
        Instantiate(_buildingFinish, new Vector3(gameObject.transform.position.x + _fBuildingX, gameObject.transform.parent.position.y + _fBuildingY, gameObject.transform.position.z + _fBuildingZ), Quaternion.Euler(0, _fBuildingRotY, 0), gameObject.transform.parent.gameObject.transform);
        if (_tableObject)
        {
            _tableObject.SetActive(true);
        }
    }
}

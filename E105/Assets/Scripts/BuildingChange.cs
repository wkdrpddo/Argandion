using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingChange : MonoBehaviour
{
    public GameObject[] _buildings = new GameObject[3];
    public bool _underConstruction;
    public int _phase = -1;

    // 상호작용해서 건축 시작하기
    public void BuildStart()
    {
        _underConstruction = true; // 건축 시작


    }

    // 하루 끝
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

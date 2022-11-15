using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // playerSystem - 플레이어 이름
    public string _playerName;

    // systemManager - 플레이어 현금
    public int _playerMoney;

    // playerSystem - 플레이어 외형 타입
    public int _playertype;

    // systemManager - 달 / 일 / 날씨(이벤트) / 계절(0,1,2,3)
    public int _month;
    public int _day;
    public int _weather;
    public int _season;

    // soundManager - 배경음 , 효과음
    public float _sound1;
    public float _sound2;

    // Inventory - 인벤토리 25칸 , 창고 100칸, 장비 4칸, 퀵슬롯 7칸, 미끼 1칸
    public int[,] _inventory = new int [25,2];
    public int[,] _storage = new int [100,2];
    public int[] _equipment = new int[4];
    public int[,] _quickslot = new int[8,2];

    // 동물 개체수 및 잔여 수용량
    public int _chicken;
    public int _sheep;
    public int _cow;
    public int _animal_cost;

    // 구역 정화 여부
    public bool[] _sectorPurifierInfo = new bool[8];

    // 정화된 지역 갯수
    public int _sectorPurifierCount = 0;

    // 발전도 레벨
    public int _PurifierLevel = 0;

    // 버프 매니저용 버프기간
    public int _flowerBuffTargetMonth;
    public int _flowerBuffTargetDay;

    // 정령 버프 확인용
    public bool[] _flowerBuffBoolList = new bool[16];

    // 각 농작물의 작물 번호(위치), 생장 수
    public int[,] _farm = new int [729,2];

    // 각 농토의 번호(위치), 잔여 수분량
    public int[,] _dirt = new int [81,2];

    // 정령 버프 제사 진행도 및 꽃의 번호
    public int[] pray = new int[2];
}

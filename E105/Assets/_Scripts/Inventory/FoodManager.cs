using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using FoodCodeToIndex;
using FoodCodeToEventCode;

[System.Serializable]
public class FoodObject
{
    public int ItemCode;
    public int Health;
    public int Stamina;
}

[System.Serializable]
public class FoodData
{
    public List<FoodObject> Food;
}

public class FoodManager : MonoBehaviour
{
    private PlayerSystem _player;
    // public GameObject _buffManagerObject;
    private BuffManager _buffManager;
    // private EventManager _eventManager;
    // private UIManager _uiManager;s
    public EventPanel _eventPanel;
    private ParticleSystem _particle;

    private int[] drinks = { 128, 129, 130 };
    private int[] foods = { 120, 121, 122, 123, 124, 125, 126, 127, 131, 132 };

    private bool[] _foodBuff = new bool[7]; // 버프 음식 개수만큼

    private int _itemCode;
    private int _eventCode;
    private int _buffIdx;

    private int stewCount = 0;
    private int mushroomCount = 0;

    // 테스트 코드
    // public bool _eating;
    // public bool _buffChecking;
    // private float time;
    // public int _eatItemCode;

    void Start()
    {
        _player = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
        _buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        _particle = _player.transform.Find("PlayerBody").transform.Find("Particle").GetComponent<ParticleSystem>();
        _particle.Stop();
        // time = 0; // 테스트 용
    }

    // 테스트 코드
    // void Update()
    // {
    //     time += Time.deltaTime;

    //     if (_eating)
    //     {
    //         Debug.Log("=====================================");
    //         UseFood(_eatItemCode);
    //         _eating = false;
    //     }
    //     if (_buffChecking)
    //     {
    //         foodBuffChecking();
    //         _buffChecking = false;
    //     }
    // }

    // 테스트 코드
    // private void foodBuffChecking()
    // {
    //     Debug.Log("=====버프 체킹 시작====");
    //     for (int i = 0; i < 7; i++)
    //     {
    //         Debug.Log("i: " + i + ", 버프:" + _foodBuff[i]);
    //     }
    //     Debug.Log("=====버프 체킹 끝====");
    // }

    // 음식 먹음
    public void UseFood(int foodCode)
    {
        _particle.Play();
        // 변수 세팅
        _itemCode = foodCode;
        _eventCode = FoodToEventCodeArray.arr[foodCode];
        _buffIdx = _eventCode - 100;

        // ================================================================
        Debug.Log("아이템 코드 " + _itemCode);
        Debug.Log("이벤트 코드 " + _eventCode);
        Debug.Log("버프 인덱스: " + _buffIdx);
        // ================================================================
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/FoodTable.json");
        var foodData = JsonHelper.FromJson<FoodObject>(jsonString);
        // ================================================================
        // Debug.Log("증가하는 hp: " + foodData[FoodIndexArray.arr[_itemCode]].Health);
        // Debug.Log("증가하는 sp: " + foodData[FoodIndexArray.arr[_itemCode]].Stamina);
        // ================================================================
        float hp = foodData[FoodIndexArray.arr[_itemCode]].Health * (_buffManager.redSpirit ? 1.2f : 1) * (_buffManager.redPray ? 1.2f : 1);
        float sp = foodData[FoodIndexArray.arr[_itemCode]].Stamina * (_buffManager.redSpirit ? 1.2f : 1) * (_buffManager.redPray ? 1.2f : 1);

        // if (_foodBuff[2])
        // {
        //     Debug.Log("스튜 버프 받고 있음");
        // }
        // else if (_foodBuff[3])
        // {
        //     Debug.Log("버섯 버프 받고 있음");
        // }
        if (IsExist(_itemCode, drinks))
        { // 음료를 마셨을경우
            // Debug.Log("음료 마심 체킹");
            // if (_foodBuff[5])
            // {
            //     Debug.Log("샌드위치 버프 받고 있음");
            // }
            // if (_foodBuff[6])
            // {
            //     Debug.Log("음료 버프 받고 있음");
            // }
            _player.changeHealth((-1) * hp * (_foodBuff[_buffIdx] ? 0.75f : 1) * (_foodBuff[5] ? 2 : 1));
            _player.changeEnergy((-1) * sp * (_foodBuff[_buffIdx] ? 0.75f : 1) * (_foodBuff[5] ? 2 : 1));
            Drinking();
        }
        else if (IsExist(_itemCode, foods))
        { // 음식을 먹었을 경우
            // Debug.Log("음식 먹음 체킹");
            if (_itemCode == 120)
            { // 지금 먹는게 빵일 때
                if (!_foodBuff[_buffIdx])
                {
                    // 직전에 먹은게 우유일 때
                    if (_foodBuff[1])
                    {
                        // Debug.Log("우유버프 받고 있음");
                        hp += 20;
                        sp += 15;
                    }
                    // 직전에 먹은게 ...일 때
                    createFoodIcon(_eventCode);
                }

            }
            // if (_foodBuff[4])
            // {
            //     Debug.Log("스테이크 버프 받고 있음");
            // }
            _player.changeHealth((-1) * hp * (_foodBuff[4] ? 0.75f : 1));
            _player.changeEnergy((-1) * sp * (_foodBuff[4] ? 0.75f : 1));

            if (_itemCode == 126)
            { // 스테이크를 먹었을 경우 배부름 효과 켜지게
                EatingSteak();
            }
            else if (_itemCode == 123)
            { // 스튜의 경우 체력 회복 버프
                stewCount = 0;
                if (_foodBuff[_buffIdx] == false)
                { // 직전에 스튜 안 먹었을 때만 panel call
                    createFoodIcon(_eventCode);
                }
                else
                {
                    _eventPanel.activeIcon(_eventCode);
                }
                CancelInvoke("HpUp");
                Stew();
            }
            else if (_itemCode == 125)
            { // 버섯볶음의 경우 기력 회복 버프
                mushroomCount = 0;
                if (_foodBuff[_buffIdx] == false)
                { // 직전에 버섯볶음 안 먹었을 때만 panel call
                    createFoodIcon(_eventCode);
                }
                else
                {
                    _eventPanel.activeIcon(_eventCode);
                }
                CancelInvoke("SpUp");
                MushroomFood();
            }
            else if (_itemCode == 131 && !_foodBuff[_buffIdx])
            { // 샌드위치 중복아니게 먹었을 때만 샌드위치 panel call
                createFoodIcon(_eventCode);
            }
            else
            {
                destroyPreviousBuff(_eventCode);
            }
        }
        else
        { // 그 외 음식 재료 및 우유를 먹었을 경우
            // Debug.Log("음식재료 및 우유 먹음 체킹");
            if (_itemCode == 110)
            { // 지금 먹는게 우유일 때
                if (!_foodBuff[_buffIdx])
                {
                    // 직전에 먹은게 빵일 때
                    if (_foodBuff[0])
                    {
                        // Debug.Log("빵버프 받고 있음");
                        hp += 20;
                        sp += 15;
                    }
                    // 직전에 먹은게 ...일 때
                    createFoodIcon(_eventCode);
                }
            }
            else
            {
                destroyPreviousBuff(_eventCode);
            }
            _player.changeHealth((-1) * hp);
            _player.changeEnergy((-1) * sp);
        }
        // ================================================================
        // Debug.Log("현재 hp: " + _player._health);
        // Debug.Log("현재 sp: " + _player._stamina);
        // ================================================================
    }

    private bool IsExist(int itemCode, int[] arrays)
    {
        for (int idx = 0; idx < arrays.Length; idx++)
        {
            if (arrays[idx] == itemCode)
            {
                return true;
            }
        }
        return false;
    }

    private void Drinking()
    {
        // Debug.Log("time: " + time + ", 음료 마심");
        CancelInvoke("Thirsty");
        Invoke("Thirsty", 120.0f);
        if (_foodBuff[_buffIdx] == false)
        { // 직전에 음료 안 마셨을 때만 panel call
            createFoodIcon(_eventCode);
        }
        else
        {
            _eventPanel.activeIcon(_eventCode);
        }
    }
    private void Thirsty()
    {
        destroyFoodIcon(106);
        // Debug.Log("time: " + time + ", 주스 효과 해제");
    }

    private void EatingSteak()
    {
        CancelInvoke("Hungry");
        Invoke("Hungry", 180.0f);
        if (_foodBuff[_buffIdx] == false)
        { // 직전에 스테이크 안 먹었을 때만 panel call
            createFoodIcon(_eventCode);
        }
        else
        {
            _eventPanel.activeIcon(_eventCode);
        }
    }

    private void Hungry()
    {
        destroyFoodIcon(104);
        // Debug.Log("time: " + time + ", 스테이크 효과 해제");
    }

    private void Stew()
    {
        if (stewCount >= 20)
        {
            // 스튜 효과 해제
            destroyFoodIcon(102);
            // Debug.Log("time: " + time + ", 스튜 효과 해제");
            return;
        }
        else
        {
            // Debug.Log("스튜 효과 받는 중 | " + stewCount);
            Invoke("HpUp", 3.0f);
        }
    }

    private void HpUp()
    {
        stewCount += 1;
        _player.changeHealth((-1));
        Stew();
    }

    private void MushroomFood()
    {
        if (mushroomCount >= 20)
        {
            // 버섯볶음 효과 해제
            destroyFoodIcon(103);
            // Debug.Log("time: " + time + ", 버섯볶음 효과 해제");
            return;
        }
        else
        {
            // Debug.Log("버섯볶음 효과 받는 중 | " + mushroomCount);
            Invoke("SpUp", 3.0f);
        }
    }

    private void SpUp()
    {
        mushroomCount += 1;
        _player.changeEnergy((-1));
        MushroomFood();
    }

    // 이미 버프 실행 중인 경우에는 call X
    private void createFoodIcon(int eventCode)
    {
        // Debug.Log("크리에이트 패널 콜");
        // 패널 call
        _eventPanel.activeIcon(eventCode);
        // 상태 변경
        destroyPreviousBuff(eventCode);
        _foodBuff[eventCode - 100] = true;
        // foodBuffChecking();
    }

    private void destroyFoodIcon(int eventCode)
    {
        // Debug.Log("디스트로이 패널 콜");
        // 패널 call
        _eventPanel.inactiveIcon(eventCode);
        // 상태 변경
        _foodBuff[eventCode - 100] = false;
        // foodBuffChecking();
    }

    private void destroyPreviousBuff(int eventCode)
    {
        if (eventCode != 100 && _foodBuff[0])
        { // 직전 빵
            // Debug.Log("직전 빵");
            destroyFoodIcon(100);
        }
        else if (eventCode != 101 && _foodBuff[1])
        { // 직전 우유
            // Debug.Log("직전 우유");
            destroyFoodIcon(101);
        }
        else if (eventCode != 105 && _foodBuff[5])
        {// 직전 샌드위치
            // Debug.Log("직전 샌드위치");
            destroyFoodIcon(105);
        }
    }



}
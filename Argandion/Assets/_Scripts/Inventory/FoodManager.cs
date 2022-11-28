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
    private BuffManager _buffManager;
    public EventPanel _eventPanel;
    private ParticleSystem _particle;
    private SoundManager _sound;

    private int[] drinks = { 128, 129, 130 };
    private int[] foods = { 120, 121, 122, 123, 124, 125, 126, 127, 131, 132 };

    private bool[] _foodBuff = new bool[7]; // 버프 음식 개수만큼

    private int _itemCode;
    private int _eventCode;
    private int _buffIdx;

    private int stewCount = 0;
    private int mushroomCount = 0;

    void Start()
    {
        _player = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
        _buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        _sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _particle = _player.transform.Find("PlayerBody").transform.Find("Particle").GetComponent<ParticleSystem>();
        _particle.Stop();
    }

    // 음식 먹음
    public void UseFood(int foodCode)
    {
        _particle.Play();
        _itemCode = foodCode;
        _eventCode = FoodToEventCodeArray.arr[foodCode];
        _buffIdx = _eventCode - 100;
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/FoodTable.json");
        var foodData = JsonHelper.FromJson<FoodObject>(jsonString);
        float hp = foodData[FoodIndexArray.arr[_itemCode]].Health * (_buffManager.redSpirit ? 1.2f : 1) * (_buffManager.redPray ? 1.2f : 1);
        float sp = foodData[FoodIndexArray.arr[_itemCode]].Stamina * (_buffManager.redSpirit ? 1.2f : 1) * (_buffManager.redPray ? 1.2f : 1);
        if (IsExist(_itemCode, drinks))
        { // 음료를 마셨을 경우
            _player.changeHealth((-1) * hp * (_foodBuff[_buffIdx] ? 0.75f : 1) * (_foodBuff[5] ? 2 : 1));
            _player.changeEnergy((-1) * sp * (_foodBuff[_buffIdx] ? 0.75f : 1) * (_foodBuff[5] ? 2 : 1));
            Drinking();
            _sound.playEffectSound("DRINKING");
        }
        else if (IsExist(_itemCode, foods))
        { // 음식을 먹었을 경우
            _sound.playEffectSound("EATING");
            if (_itemCode == 120)
            { // 지금 먹는게 빵일 때
                if (!_foodBuff[_buffIdx])
                {
                    // 직전에 먹은게 우유일 때
                    if (_foodBuff[1])
                    {
                        hp += 20;
                        sp += 15;
                    }
                    // 직전에 먹은게 ...일 때
                    createFoodIcon(_eventCode);
                }

            }
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
            if (_itemCode == 110)
            { // 지금 먹는게 우유일 때
                _sound.playEffectSound("DRINKING");
                if (!_foodBuff[_buffIdx])
                {
                    // 직전에 먹은게 빵일 때
                    if (_foodBuff[0])
                    {
                        hp += 20;
                        sp += 15;
                    }
                    // 직전에 먹은게 ...일 때
                    createFoodIcon(_eventCode);
                }
            }
            else
            {
                _sound.playEffectSound("EATING");
                destroyPreviousBuff(_eventCode);
            }
            _player.changeHealth((-1) * hp);
            _player.changeEnergy((-1) * sp);
        }
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
    }

    private void Stew()
    {
        if (stewCount >= 20)
        {
            // 스튜 효과 해제
            destroyFoodIcon(102);
            return;
        }
        else
        {
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
            return;
        }
        else
        {
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
        _eventPanel.activeIcon(eventCode); // 이벤트 패널 call
        destroyPreviousBuff(eventCode); // 상태 변경
        _foodBuff[eventCode - 100] = true;
    }

    private void destroyFoodIcon(int eventCode)
    {
        _eventPanel.inactiveIcon(eventCode); // 패널 call
        _foodBuff[eventCode - 100] = false; // 상태 변경
    }

    private void destroyPreviousBuff(int eventCode)
    {
        if (eventCode != 100 && _foodBuff[0])
        { // 직전 빵
            destroyFoodIcon(100);
        }
        else if (eventCode != 101 && _foodBuff[1])
        { // 직전 우유
            destroyFoodIcon(101);
        }
        else if (eventCode != 105 && _foodBuff[5])
        {// 직전 샌드위치
            destroyFoodIcon(105);
        }
    }



}
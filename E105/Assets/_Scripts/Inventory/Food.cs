using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using FoodCodeToIndex;

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

public class Food : MonoBehaviour
{
    private PlayerSystem _player;
    // public GameObject _buffManagerObject;
    private BuffManager _buffManager;
    private EventManager _eventManager;

    private int[] drinks = { 128, 129, 130 };
    private int[] foods = { 120, 121, 122, 123, 124, 125, 126, 127, 131, 132 };

    private int stewCount = 0;
    private int mushroomCount = 0;

    public bool _testing; // 테스트 코드
    private float time;

    void Start()
    {
        time = 0;
        // _buffManagerObject = ;
        _buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        _player = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
        _eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
    }

    // 테스트 코드 작성
    void Update()
    {
        time += Time.deltaTime;

        if (_testing)
        {
            UseFood(128);
            _testing = false;
        }
    }

    public void UseFood(int itemCode)
    {
        // Debug.Log("============== UseFood() 콜: " + itemCode);
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/FoodTable.json");
        var foodData = JsonHelper.FromJson<FoodObject>(jsonString);
        float hp = foodData[FoodIndexArray.arr[itemCode]].Health * (_buffManager.redSpirit ? 1.2f : 1) * (_buffManager.redPray ? 1.2f : 1);
        float sp = foodData[FoodIndexArray.arr[itemCode]].Stamina * (_buffManager.redSpirit ? 1.2f : 1) * (_buffManager.redPray ? 1.2f : 1);

        if (IsExist(itemCode, drinks))
        { // 음료를 마셨을경우
            _player.changeHealth((-1) * hp * (_buffManager.notThirsty ? 0.75f : 1) * (_buffManager.eatSandwich ? 2 : 1));
            _player.changeEnergy((-1) * sp * (_buffManager.notThirsty ? 0.75f : 1) * (_buffManager.eatSandwich ? 2 : 1));
            Drinking();
        }
        else if (IsExist(itemCode, foods))
        { // 음식을 먹었을 경우
            if (itemCode == 120 && _buffManager.eatMilk)
            { // 지금 먹는게 빵이고 직전에 먹은게 우유일경우 보너스효과
                hp += 20;
                sp += 15;
            }

            _player.changeHealth((-1) * hp * (_buffManager.isFull ? 0.75f : 1));
            _player.changeEnergy((-1) * sp * (_buffManager.isFull ? 0.75f : 1));

            if (itemCode == 126)
            { // 스테이크를 먹었을 경우 배부름 효과 켜지게
                _player.changeHealth((-1) * hp * (_buffManager.isFull ? 0.75f : 1));
                _player.changeEnergy((-1) * sp * (_buffManager.isFull ? 0.75f : 1));
            }
            else if (itemCode == 123)
            { // 스튜의 경우 체력 회복 버프
                stewCount = 0;
                Stew();
            }
            else if (itemCode == 125)
            { // 버섯볶음의 경우 기력 회복 버프
                mushroomCount = 0;
                MushroomFood();
            }
        }
        else
        { // 그 외 음식재료 및 우유를 먹었을 경우
            if (itemCode == 110 && _buffManager.eatBread)
            { // 직전에 먹은게 빵이고 우유를 먹었을 경우
                hp += 4;
                sp += 3;
            }
            _player.changeHealth((-1) * hp);
            _player.changeEnergy((-1) * sp);
        }

        // 현재 먹은 음식 체크하여 다음 먹을 음식에 버프효과를 반영할수 있도록 함.
        if (itemCode == 110)
        { // 우유
            _buffManager.eatMilk = true;
            _buffManager.eatBread = false;
            _buffManager.eatSandwich = false;
        }
        else if (itemCode == 120)
        { // 빵
            _buffManager.eatMilk = false;
            _buffManager.eatBread = true;
            _buffManager.eatSandwich = false;
        }
        else if (itemCode == 131)
        { // 샌드위치
            _buffManager.eatMilk = false;
            _buffManager.eatBread = false;
            _buffManager.eatSandwich = true;
        }
        else
        { // 그외
            _buffManager.eatMilk = false;
            _buffManager.eatBread = false;
            _buffManager.eatSandwich = false;
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
        _buffManager.notThirsty = true;
        CancelInvoke("Thirsty");
        Invoke("Thirsty", 120.0f);
    }

    private void Thirsty()
    {
        // Debug.Log("Thirsty: " + time);
        _buffManager.notThirsty = false;
        // 주스 효과 해제
        _eventManager.endFoodBuff(128);

    }

    private void EatingSteak()
    {
        _buffManager.isFull = true;
        CancelInvoke("Hungry");
        Invoke("Hungry", 180.0f);
    }

    private void Hungry()
    {
        _buffManager.isFull = false;
        // 스테이크 효과 해제
        _eventManager.endFoodBuff(126);
    }

    private void Stew()
    {
        if (stewCount >= 20)
        {
            // 스튜 효과 해제
            _eventManager.endFoodBuff(123);
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
            // 버섯 효과 해제
            _eventManager.endFoodBuff(125);
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
}
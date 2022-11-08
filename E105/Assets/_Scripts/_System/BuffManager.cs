using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public int _flowerBuffTargetMonth = 0;
    public int _flowerBuffTargetDay= 0;
    public bool _isFlowerBuffActived = false;
    public bool whiteSpirit = false;
    public bool whitePray = false;
    public bool orangePray = false;
    public bool bluePray = false;
    public bool redSpirit = false;
    public bool redPray = false;
    public bool pinkSpirit = false;
    public bool pinkPray = false;
    public bool yellowSpirit = false;
    public bool yellowPray = false;
    public bool skySpirit = false;
    public bool skyPray = false;

    public bool isFull = false; // 스테이크에 들어감
    public bool notThirsty = false; // 음료에 들어감
    public bool eatSandwich = false;
    public bool eatBread = false;
    public bool eatMilk = false;

    public void FlowerBuffEnd() 
    {
        Debug.Log("버프끗");
        _flowerBuffTargetMonth = 0;
        _flowerBuffTargetDay= 0;
        _isFlowerBuffActived = false;
        whiteSpirit = false;
        whitePray = false;
        orangePray = false;
        bluePray = false;
        redSpirit = false;
        redPray = false;
        pinkSpirit = false;
        pinkPray = false;
        yellowSpirit = false;
        yellowPray = false;
        skySpirit = false;
        skyPray = false;
    }

    public void Thirsty()
    {
        notThirsty = false;
    }

    public void Hungry()
    {
        isFull = false;
    }
}

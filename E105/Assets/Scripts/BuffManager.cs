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
    public bool yeloowSpirit = false;
    public bool yeloowPray = false;
    public bool skySpirit = false;
    public bool skyPray = false;

    public bool isDringkingMilk = false; // 우유에 들어감
    public bool isEatingBread = false; // 빵에 들어감


    public bool isFull = false; // 스테이크에 들어감
    public bool notThirsty = false; // 음료에 들어감

    public void FlowerBuffEnd() {
        _isFlowerBuffActived = false;
        whiteSpirit = false;
        whitePray = false;
        orangePray = false;
        bluePray = false;
        redSpirit = false;
        redPray = false;
        pinkSpirit = false;
        pinkPray = false;
        yeloowSpirit = false;
        yeloowPray = false;
        skySpirit = false;
        skyPray = false;
    }
}

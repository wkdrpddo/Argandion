using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneChange : MonoBehaviour
{

    public GameObject _rune1Light; // 미리 넣어놓기
    public GameObject _rune2Light; // 미리 넣어놓기
    public GameObject _rune3Light; // 미리 넣어놓기

    // ==================================================== 지울 위치 (시작) ==================================

    // 테스트용 변수
    public bool _up;
    int tmp = 0;

    // 테스트용 update();
    // void Update()
    // {
    //     if (_up)
    //     {
    //         turnOnRune(++tmp);
    //         _up = false; // 테스트용 변수

    //     }
    // }

    // ==================================================== 지울 위치 (끝) ==================================

    public void turnOnRune(int runeNum)
    {
        if (runeNum == 1)
        {
            _rune1Light.SetActive(true);
        }
        else if (runeNum == 2)
        {
            _rune2Light.SetActive(true);
        }
        else if (runeNum == 3)
        {
            _rune3Light.SetActive(true);
        }
    }
}

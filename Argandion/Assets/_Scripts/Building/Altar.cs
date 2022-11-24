using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField] private GameObject _runes; // 미리 넣어놓기
    [SerializeField] private GameObject _tableFlowers; // 미리 넣어놓기
    [SerializeField] private GameObject[] _runeLight; // 미리 넣어놓기
    [SerializeField] private GameObject[] _runeEffect; // 미리 넣어놓기
    [SerializeField] private GameObject _effect; // 미리 넣어놓기
    [SerializeField] private GameObject _tableLight; // 미리 넣어놓기
    private GameObject _onFlower;

    private PrayBuff _prayBuff;
    private BuffManager _buffManager;
    private SoundManager _sound;

    private int _dayEndBuffCnt;
    private bool _buffStart;

    void Start()
    {
        _prayBuff = GameObject.Find("BuffManager").GetComponent<PrayBuff>();
        _buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        _sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }
    // 하루 끝나면
    public void DayEnd(){
        Debug.Log("데이엔드");
        if(_buffStart){ // 버프 시작하면(조건 만족)
            Debug.Log("버프 시작");
            _dayEndBuffCnt = 0;
            _effect.SetActive(false); // 이펙트 빼고
            // 라이트 켜고
            _tableLight.SetActive(true);

        }else{ // 버프 시작 안 하면
            Debug.Log("버프 시작 안 함");
            if(_onFlower!=null){
                _onFlower.SetActive(false); // 테이블 위 꽃 없애기
                turnOnEffect(_dayEndBuffCnt); // 테이블 이펙트 바꿔주기
            }
        }
    }
    
    // load 할 때 필요한 것 해주는 함수 필요
    public void load(){
        int flowerCode = _prayBuff.flowerIdx;
        if(_dayEndBuffCnt==1 || _dayEndBuffCnt==2){ // 제사 중
            _effect.SetActive(true);
            // 룬 켜기, 테이블 이펙트 바꿔주기
            for (int i = 1; i <= _dayEndBuffCnt; i++)
            {
                _runeLight[i-1].SetActive(true);
                _effect.transform.GetChild(i-1).gameObject.SetActive(true);
            }

        }else if(_dayEndBuffCnt==3 || _buffManager._isPrayBuffActived){ // 제사 시작 && 제사 중
            _dayEndBuffCnt = 0; // 날짜 갱신 
            _effect.SetActive(false); // 이펙트 빼고
            activeFlower(flowerCode); // 꽃 올리고
            for (int i = 0; i < 3; i++)
            {
                _runeLight[i].SetActive(true); // 룬 다 켜고
            }
            _tableLight.SetActive(true); // 라이트 켜고
        }
    }

    // 제사 끝나는 날 효과 다 빼는 함수
    public void buffEnd(){
        _buffStart = false; // 버프 시작 아님
        _onFlower.SetActive(false); // 테이블 위 꽃 없애기
        // 테이블 이펙트 하나만 켜기
        _effect.SetActive(true);
        _effect.transform.GetChild(0).gameObject.SetActive(false);
        _effect.transform.GetChild(1).gameObject.SetActive(false);
        // 룬 끄기
        _runeLight[0].SetActive(false);
        _runeLight[1].SetActive(false);
        _runeLight[2].SetActive(false);
        // 라이트 끄기
        _tableLight.SetActive(false);
        
    }

    // 제사를 시도할 때
    public void goPray(int itemCode){
        _sound.playEffectSound("ALTER");
        _prayBuff.Pray(itemCode); // 제사
        activeFlower(itemCode); // 꽃 올리기
        _dayEndBuffCnt = _prayBuff.prayDay;
        turnOnRune(_dayEndBuffCnt); // 룬 켜기
        if(_prayBuff.prayDay==3){
            _buffStart = true; // 버프 시작
        }
    }

    // 제사 시도할 때 꽃 보이는 함수
    private void activeFlower(int itemCode){
        _onFlower = _tableFlowers.transform.GetChild(itemCode-50).gameObject;
        _onFlower.SetActive(true);
    }
    
    // 제사 시도할 때 테이블 이펙트 바꿔주는 함수
    private void turnOnEffect(int day){
        _effect.transform.GetChild(day-1).gameObject.SetActive(true);
    }

    // 제사 시도할 때 룬 켜주는 함수
    private void turnOnRune(int level)
    {
        _runeLight[level-1].SetActive(true);
        _runeEffect[level-1].SetActive(true); // active로 자동 실행 되나? // 그리고 자동으로 꺼짐?
    }


}

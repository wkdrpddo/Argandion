using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    public int phase = 0;

    public int[] fishingRod = {320,321,322};
    public int myRod = 0;
    public float[] reaction = {1.0f, 1.5f, 2.0f};
    private float reactionTime;
    private float myReactionTime;

    public float waitingTime;
    public float myWaitingTime;

    public int[] bait = {502, 503, 504};
    public float[] baitEffect = {1.0f, 1.25f, 1.4f};
    public int myBait = 0;

    private bool isFishing = false;
    private bool isBaiting = false;

    private bool fishingDelay = false;

    public Inventory theInventory;
    public Item item;
    public GameObject _buffManagerObject;
    private BuffManager _buff;
    public GameObject _po;
    public PlayerSystem _ps;
    private SoundManager _sound;

    private Vector3 _Dpos;
    private GameObject _cha;
    

    void Start()
    {
        _buffManagerObject = GameObject.Find("BuffManager");
        item = GameObject.Find("ItemManager").GetComponent<Item>();
        _buff = _buffManagerObject.GetComponent<BuffManager>();
        _po = GameObject.Find("PlayerObject");
        _cha = _po.transform.Find("PlayerBody").gameObject;
        _ps = _po.GetComponent<PlayerSystem>();
        _Dpos = gameObject.transform.forward;
        _sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public void Interaction(float rodNumber, float baitNum)
    {
        DoFishing((int)rodNumber, (int)baitNum);
    }

    void Update() {
        if (isFishing) {
            myWaitingTime += Time.deltaTime;
            // Debug.Log("물기 기다리는중...");
            if ( myWaitingTime > waitingTime ) {
                isBaiting = true;
                phase = 3;
                // Debug.Log("미끼를 물었다!");
                _ps.setAnimator(12,0);
            }
        }

        if (isBaiting) {
            myReactionTime += Time.deltaTime;
            if ( myReactionTime > reactionTime ) {
                isBaiting = false;
                isFishing = false;
                myWaitingTime = 0.0f;
                myReactionTime = 0.0f;
                phase = 5;
                // Debug.Log("물고기 도망감");
                _ps._canMove = true;
                _ps.setCanAction(true);
                _ps._playerAnimator.SetBool("fishingFail",true);
                _ps.setAnimator(0,0);
                _ps.setHandItem(true);
                fishingDelay = false;
                phase = 0;

            }
        }
    }

    public void DoFishing (int rodIdx, int baitIdx)
    {
        if (_buff.inColdWave && !_buff.redPray) {
            // Debug.Log("호수가 얼어있어 낚시를 하지못합니다 T.T");
            return ;
        }

        if ( !isFishing && phase == 0) {
            _ps.setHandItem(false,6);
            myRod = rodIdx;
            myBait = baitIdx;
            phase = 1;
            _cha.transform.forward = _Dpos;
            _ps._canMove = false;
            _ps.setCanAction(false);
            // Debug.Log("낚시대 투척!");
            _ps.damageStamina(2f);
            _ps.setAnimator(9,0);
            _ps._playerAnimator.SetBool("fishingFail",false);
            Invoke("FishingStart", 2.0f);
        } else if ( isBaiting ) {
            FishingSuccess();
        }
    }

    void FishingStart()
    {   
        if (!fishingDelay) {
            // Debug.Log("낚시시작!");
            for (int idx = 0; idx < 3; idx++) {
                
                if (fishingRod[idx] == myRod) {
                    reactionTime = reaction[idx];

                    if ( idx ==  0 ) {
                        waitingTime = Random.Range(4.0f, 10.0f);
                    } else if( idx == 1) {
                        waitingTime = Random.Range(3.0f, 8.0f);
                    } else {
                        waitingTime = Random.Range(2.0f, 6.0f);
                    }
                    break;
                }
            }
            _ps._UIManager.quickUse((int)_ps._equipList[7, 0], 1, 7);

            for (int idx = 0; idx < 3; idx++) {
                if (bait[idx] == myBait) {
                    waitingTime = waitingTime / baitEffect[idx];
                    break;
                }
            }

            if( _buff.bluePray ) {
                waitingTime *= 0.5f;
            }

            fishingDelay = true;
            isFishing = true;
            _ps.setAnimator(11,0);
            phase = 2;
            Invoke("FishingSound", 0.1f);
        }
    }

    private void FishingSound() {
        _sound.playEffectSound("FISHING");
    }

    void FishingSuccess()
    {
        if ( myReactionTime > 0.0f && myReactionTime < reactionTime) {
            // Debug.Log("낚시 성공");
            theInventory.AcquireItem(item.FindItem(106),1);
            phase = 4;
            isBaiting = false;
            isFishing = false;
            myWaitingTime = 0.0f;
            myReactionTime = 0.0f;
            _ps.setAnimator(13,0);
            Invoke("FishingDelay", 3.0f);
        } else {
            // Debug.Log("아직 미끼를 물지 않았습니다!");
        }
    }

    void FishingDelay()
    {
        fishingDelay = false;
        phase = 0;
        _ps._canMove = true;
        _ps.setCanAction(true);
        _ps.setHandItem(true);
    }
}
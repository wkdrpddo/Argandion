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

    void Start()
    {
        _buffManagerObject = GameObject.Find("BuffManager");
        _buff = _buffManagerObject.GetComponent<BuffManager>();
    }

    void Update() {
        if (isFishing) {
            myWaitingTime += Time.deltaTime;
            Debug.Log("물기 기다리는중...");
            if ( myWaitingTime > waitingTime ) {
                isBaiting = true;
                phase = 3;
                Debug.Log("미끼를 물었다!");
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
                Debug.Log("물고기가 튀었어");
                Invoke("FishingDelay", 1.0f);
            }
        }
    }

    public void DoFishing (int rodIdx, int baitIdx)
    {
        if (_buff.inColdWave) {
            Debug.Log("호수가 얼어있어 낚시를 하지못합니다 T.T");
            return ;
        }
        
        if ( !isFishing ) {
            myRod = rodIdx;
            myBait = baitIdx;
            phase = 1;
            Debug.Log("낚시대 투척!");
            Invoke("FishingStart", 1.0f);
        } else if ( isBaiting ) {
            FishingSuccess();
        }
    }

    void FishingStart()
    {   
        if (!fishingDelay) {
            Debug.Log("낚시시작!");
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

            for (int idx = 0; idx < 3; idx++) {
                if (bait[idx] == myBait) {
                    waitingTime = waitingTime / baitEffect[idx];
                    break;
                }
            }

            if( _buff.bluePray ) {
                waitingTime *= 0.5f;
            }

            Debug.Log(waitingTime);
            fishingDelay = true;
            isFishing = true;
            phase = 2;
        }
    }

    void FishingSuccess()
    {
        if ( myReactionTime > 0.0f && myReactionTime < reactionTime) {
            Debug.Log("낚시 성공");
            theInventory.AcquireItem(item.FindItem(106),1);
            phase = 4;
            isBaiting = false;
            isFishing = false;
            myWaitingTime = 0.0f;
            myReactionTime = 0.0f;
            Invoke("FishingDelay", 3.0f);
        } else {
            Debug.Log("아직 미끼를 물지 않았습니다!");
        }
    }

    void FishingDelay()
    {
        fishingDelay = false;
    }
}
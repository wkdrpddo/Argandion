using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCObject : MonoBehaviour
{
    // Start is called before the first frame update

    // public bool _useKey_status;
    private Animator _animator;
    public GameObject _PlayerObject;
    // public GameObject _conversationUI;

    void Start()
    {
        // _useKey_status= false;
        _animator = GetComponent<Animator>();
        //_conversationUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //NPCInteraction();
    }

    public void Interaction()
    {
        _animator.SetTrigger("move");
        this.transform.LookAt(_PlayerObject.transform);
            
        ConversationStart();
        // if(_useKey_status && Input.GetButtonDown("interactionKey"))
        // {
        //     _animator.SetTrigger("move");
        //     this.transform.LookAt(_PlayerObject.transform);
            
        //     ConversationStart();
        // }
    }

    public void ConversationStart()
    {
        _PlayerObject.GetComponent<PlayerSystem>()._canMove = false;
        // -----------------------------------> UI 담당자랑 얘기 대화    ->  NPC 대화 UI 창 오픈 함수 호출
        //_conversationUI.SetActive(true);
    }

    public void ConversationEnd()
    {
        // -----------------------------------> UI 담당자랑 얘기 대화    ->  NPC 대화 UI 창 닫기 버튼 누르면  해당 함수 호출
        //_conversationUI.SetActive(false);
        _PlayerObject.GetComponent<PlayerSystem>()._canMove = true;
    }

}

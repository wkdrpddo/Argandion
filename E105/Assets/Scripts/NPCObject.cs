using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCObject : MonoBehaviour
{
    // Start is called before the first frame update

    public bool _useKey_status = false;
    private Animator _animator;
    public GameObject _PlayerObject;
    public GameObject _conversationUI;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _conversationUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        NPCInteraction();
    }

    private void NPCInteraction()
    {
        if(_useKey_status && Input.GetButtonDown("interactionKey"))
        {
            _animator.SetTrigger("move");
            this.transform.LookAt(_PlayerObject.transform);
            
            ConversationStart();
        }
    }

    public void ConversationStart()
    {
        _PlayerObject.GetComponent<PlayerSystem>()._canMove = false;
        _conversationUI.SetActive(true);
    }

    public void ConversationEnd()
    {
        _conversationUI.SetActive(false);
        _PlayerObject.GetComponent<PlayerSystem>()._canMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("플레이어 들어옴");
            _useKey_status = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("플레이어 나감");
            _useKey_status = false;
        }
    }
}

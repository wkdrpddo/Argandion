using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCObject : MonoBehaviour
{

    private Animator _animator;
    private GameObject _PlayerObject;
    private GameObject _UIManager;


    void Start()
    {
        _animator = GetComponent<Animator>();
        _PlayerObject = GameObject.Find("PlayerObject");
        _UIManager = GameObject.Find("UIManager");
    }


    public void Interaction()
    {
        _animator.SetTrigger("move");

        if(this.name == "Spirit"){
            this.transform.LookAt(_PlayerObject.transform.position + new Vector3(0,this.transform.position.y,0));
        }else{
            this.transform.LookAt(_PlayerObject.transform);
        }
        
        ConversationStart();
    }


    public void ConversationStart()
    {
        switch(this.name)
        {
            case "NPC1" :
                _UIManager.GetComponent<UIManager>().OnConversationPanel(1);
                break;
            case "NPC2" :
                _UIManager.GetComponent<UIManager>().OnConversationPanel(2);
                break;
            case "NPC3" :
                _UIManager.GetComponent<UIManager>().OnConversationPanel(3);
                break;
            case "NPC4" :
                _UIManager.GetComponent<UIManager>().OnConversationPanel(4);
                break;
            case "NPC5" :
                _UIManager.GetComponent<UIManager>().OnConversationPanel(5);
                break;
            case "NPC6" :
                _UIManager.GetComponent<UIManager>().OnConversationPanel(6);
                break;
            case "NPC7" :
                _UIManager.GetComponent<UIManager>().OnConversationPanel(7);
                break;
            case "NPC8" :
                _UIManager.GetComponent<UIManager>().OnConversationPanel(8);
                break;
            case "Spirit" :
                _UIManager.GetComponent<UIManager>().OnConversationPanel(10);
                break;
        }

    }


}

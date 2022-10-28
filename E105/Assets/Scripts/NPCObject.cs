using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCObject : MonoBehaviour
{
    // Start is called before the first frame update

    public bool _useKey_status = false;
    public Transform _Player;
    private Animator _animator;
    public GameObject _PlayerObject;
    public GameObject _UI;
    public bool _talk;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_useKey_status && Input.GetButtonDown("interactionKey"))
        {
            _animator.SetTrigger("move");
            this.transform.LookAt(_Player);
            Debug.Log("UI 띄우기");
           
            _PlayerObject.GetComponent<PlayerSystem>()._canMove = false;
            _UI.SetActive(true);
        }
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

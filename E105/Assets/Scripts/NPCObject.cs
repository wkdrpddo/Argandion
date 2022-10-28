using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCObject : MonoBehaviour
{
    // Start is called before the first frame update
    public bool usekey_status = false;
    public Transform Player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(usekey_status && Input.GetButtonDown("usekey"))
        {
            Debug.Log("z 키 입력");
            this.transform.LookAt(Player);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("플레이어 들어옴");
            usekey_status = true;
           
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("플레이어 나감");
            usekey_status = false;
           
        }
    }
}

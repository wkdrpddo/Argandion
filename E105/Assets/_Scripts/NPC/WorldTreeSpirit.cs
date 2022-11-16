using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTreeSpirit : MonoBehaviour
{
    private bool right_move;
    private bool left_move;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if( right_move)
        // {
        //     transform.position = transform.position + new Vector3(0.01f,0,0);
        //     transform.rotation = Quaternion.Euler(0, 90 , 0);
        //     if(transform.position.x > 125){
        //         right_move = false;
        //     }
        // }
        // else{
        //     transform.position = transform.position + new Vector3(-0.01f,0,0);
        //     transform.rotation = Quaternion.Euler(0, -90 , 0);
        //     if(transform.position.x < 105){
        //         right_move = true;
        //     }
        // }
    }

    public void Interaction(int flowerCode)
    {
        // UIManager에 함수 호출
    }
}

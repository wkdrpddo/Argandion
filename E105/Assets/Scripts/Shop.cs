using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    PlayerSystem enterplayer;
    public void Enter(PlayerSystem player)
    {
        enterplayer = player;
    }

    // Update is called once per frame
    public void Exit()
    {
        
    }
}

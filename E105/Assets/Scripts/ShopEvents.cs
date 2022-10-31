// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ShopEvents : MonoBehaviour
// {
//     public RectTransform uiGroup;
//     public Animator anim;

//     // Player enterPlayer;

//     public void Enter(Player player)
//     {
//         enterPlayer = player;
//         uiGroup.anchoredPosition = Vector3.zero;
//     }

//     public void Exit()
//     {
//         uiGroup.anchoredPosition = Vector3.down * 1000;
//     }

//     public void Buy(int index)
//     {
//         int price = itemPrice[index];
//         if (price > enterPlayer.coin) {
//             return;
//         }

//         enterPlayer.coin -= price;
        
//     }

//     public void Craft()
//     {

//     }
// }

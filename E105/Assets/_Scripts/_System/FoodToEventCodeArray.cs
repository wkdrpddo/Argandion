using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using FoodCodeToEventCode;

namespace FoodCodeToEventCode
{
    public class FoodToEventCodeArray : MonoBehaviour
    {
        //이벤트 코드 - 이벤트 인덱스 배열
        public static int[] arr = new int[140];

        //Static code block in c#
        static FoodToEventCodeArray()
        {
            arr[120] = 100;
            arr[110] = 101;
            arr[123] = 102;
            arr[125] = 103;
            arr[126] = 104;
            arr[131] = 105;
            arr[128] = 106;
            arr[129] = 106;
            arr[130] = 106;
        }
    }
}
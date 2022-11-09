using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ItemCodeToIndex;

namespace EventCodeToIndex
{
    public class EventIndexArray : MonoBehaviour
    {
        //이벤트 코드 - 이벤트 인덱스 배열
        public static int[] arr = new int[110];

        //Static code block in c#
        static EventIndexArray()
        {
            arr[1] = 0;
            arr[2] = 1;
            arr[3] = 2;
            arr[4] = 3;
            arr[5] = 4;
            arr[6] = 5;
            arr[7] = 6;
            arr[11] = 7;
            arr[12] = 8;
            arr[13] = 9;
            arr[14] = 10;
            arr[15] = 11;
            arr[16] = 12;
            arr[17] = 13;
            arr[50] = 14;
            arr[51] = 15;
            arr[52] = 16;
            arr[53] = 17;
            arr[54] = 18;
            arr[55] = 19;
            arr[56] = 20;
            arr[100] = 21;
            arr[101] = 22;
            arr[102] = 23;
            arr[103] = 24;
            arr[104] = 25;
            arr[105] = 26;
            arr[106] = 27;

        }
    }
}
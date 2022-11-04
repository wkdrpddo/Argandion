using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ItemCodeToIndex;

namespace ItemCodeToIndex
{
    public class ItemIndexArray
    {

        //아이템코드 - 아이템인덱스 배열
        public static int[] arr = new int[510];

        //Static code block in c#
        static ItemIndexArray(){
            arr[1] = 0;
            arr[2] = 1;
            arr[3] = 2;
            arr[4] = 3;
            arr[10] = 4;
            arr[11] = 5;
            arr[12] = 6;
            arr[13] = 7;
            arr[14] = 8;
            arr[15] = 9;
            arr[16] = 10;
            arr[20] = 11;
            arr[21] = 12;
            arr[22] = 13;
            arr[100] = 14;
            arr[101] = 15;
            arr[102] = 16;
            arr[103] = 17;
            arr[104] = 18;
            arr[105] = 19;
            arr[106] = 20;
            arr[107] = 21;
            arr[108] = 22;
            arr[109] = 23;
            arr[110] = 24;
            arr[111] = 25;
            arr[112] = 26;
            arr[113] = 27;
            arr[114] = 28;
            arr[115] = 29;
            arr[116] = 30;
            arr[117] = 31;
            arr[118] = 32;
            arr[119] = 33;
            arr[120] = 34;
            arr[121] = 35;
            arr[122] = 36;
            arr[123] = 37;
            arr[124] = 38;
            arr[125] = 39;
            arr[126] = 40;
            arr[127] = 41;
            arr[128] = 42;
            arr[129] = 43;
            arr[130] = 44;
            arr[131] = 45;
            arr[132] = 46;
            arr[50] = 47;
            arr[51] = 48;
            arr[52] = 49;
            arr[53] = 50;
            arr[54] = 51;
            arr[55] = 52;
            arr[56] = 53;
            arr[300] = 54;
            arr[301] = 55;
            arr[302] = 56;
            arr[303] = 57;
            arr[304] = 58;
            arr[305] = 59;
            arr[306] = 60;
            arr[307] = 61;
            arr[308] = 62;
            arr[309] = 63;
            arr[310] = 64;
            arr[311] = 65;
            arr[312] = 66;
            arr[313] = 67;
            arr[314] = 68;
            arr[315] = 69;
            arr[316] = 70;
            arr[317] = 71;
            arr[318] = 72;
            arr[319] = 73;
            arr[320] = 74;
            arr[321] = 75;
            arr[322] = 76;
            arr[323] = 77;
            arr[324] = 78;
            arr[400] = 79;
            arr[401] = 80;
            arr[402] = 81;
            arr[403] = 82;
            arr[404] = 83;
            arr[500] = 84;
            arr[501] = 85;
            arr[502] = 86;
            arr[503] = 87;
            arr[504] = 88;
            arr[505] = 89;
        }
    }
}

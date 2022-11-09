using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] public bool[] NPC_open = new bool[8];
    private GameObject parent_NPC;
    private GameObject[] NPC = new GameObject[8];


    // Start is called before the first frame update
    void Start()
    {
        NPC_open[0] = true; //test
        parent_NPC = GetComponent<Transform>().parent.gameObject;
        for (int i = 0; i < 8; i++)
        {
            NPC[i] = parent_NPC.transform.GetChild(i + 1).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // for(int i=0; i<8; i++){
        //     if(NPC_open[i]){
        //         NPC[i].SetActive(true);
        //     }
        //     else{
        //         NPC[i].SetActive(false);
        //     }
        // }
    }
}

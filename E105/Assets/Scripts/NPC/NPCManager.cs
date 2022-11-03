using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public bool[] NPC_open = new bool[8];

    [SerializeField] private GameObject[] NPC = new GameObject[8];
    
    // Start is called before the first frame update
    void Start()
    {
        NPC_open[0] = true; //test
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<8; i++){
            if(NPC_open[i]){
                NPC[i].SetActive(true);
            }
            else{
                NPC[i].SetActive(false);
            }
        }

    }
}

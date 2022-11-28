using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    private GameObject parent_NPC;
    [SerializeField] private GameObject[] NPC = new GameObject[8];

    void Start()
    {
        parent_NPC = GetComponent<Transform>().parent.gameObject;
        for (int i = 0; i < 8; i++)
        {
            NPC[i] = parent_NPC.transform.GetChild(i + 1).gameObject;
        }
    }

    public void NPCActive(int index)
    {
        NPC[index].SetActive(true);
    }
}

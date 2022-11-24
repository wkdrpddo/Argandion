using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiationExample : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject cowPrefab;
    private List<GameObject> cowPrefabs = new List<GameObject>();
    private int cows = 0;
    private int howMany = 3;

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        // 생성
        Vector3 position = new Vector3(244f, 0, 174f);
        for (int i = cows; i < howMany; i++)
        {
            Vector2 randomPoint = Random.insideUnitCircle * 20;
            GameObject addData = Instantiate(cowPrefab, new Vector3(position.x + randomPoint.x, 2, position.z + randomPoint.y), Quaternion.identity);
            cowPrefabs.Add(addData);
        }
        
        // 삭제
        for (int i = howMany-1; i >= cows; i--)
        {
            Debug.Log(cowPrefabs[i]);
            Destroy(cowPrefabs[i], 3f);
        }
    }
}
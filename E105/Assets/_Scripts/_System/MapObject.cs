using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] SectorObject;
    void Start()
    {
        SectorObject = GameObject.FindGameObjectsWithTag("sector");
    }

    public void UpdateFieldManager(int index)
    {
        foreach (var obj in SectorObject) {
            ObjectReplacer[] SectorProps;
            SectorProps = obj.GetComponentsInChildren<ObjectReplacer>();
            foreach (var prop in SectorProps) {
                prop.updateObject(index);
            }
        }
    }

    public void ChangePurifier(int index)
    {
    }
}

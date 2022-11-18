using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] SectorObject;
    public SystemManager _systemManager;
    void Start()
    {
        // SectorObject = GameObject.FindGameObjectsWithTag("sector");
        _systemManager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
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
        SectorObject[index].transform.GetComponent<SectorObject>().Purifier();
        SectorObject[index].transform.GetChild(0).gameObject.SetActive(false);
        SectorObject[index].transform.GetChild(1).gameObject.SetActive(true);
        UpdateFieldManager(_systemManager._season);
    }
}

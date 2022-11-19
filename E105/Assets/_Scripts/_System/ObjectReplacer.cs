using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReplacer : MonoBehaviour
{
    public GameObject[] _Object= new GameObject[8];
    public bool _purification;
    public SectorObject _parent_sector;
    public void Awake()
    {
        GameObject pos = gameObject;
        while (true)
        {
            if (pos.TryGetComponent<SectorObject>(out SectorObject _sector))
            {
                _parent_sector = _sector;
                break;
            }
            else if (pos.gameObject.transform.parent)
            {
                pos = pos.transform.parent.gameObject;
            }
            else
            {
                break;
            }
        }
    }

    public void updateObject(int index)
    {
        _purification = _parent_sector._purifier;
        if (_purification) {
            foreach (var obj in _Object) {
                if (obj) {
                    obj.SetActive(false);
                }
            }
            if (_Object[index]) {
                _Object[index].SetActive(true);
            }
        }
        else {
            foreach (var obj in _Object) {
                if (obj) {
                    obj.SetActive(false);
                }
            }
            if (_Object[index+4]) {
                _Object[index+4].SetActive(true);
            }
        }
    }
}

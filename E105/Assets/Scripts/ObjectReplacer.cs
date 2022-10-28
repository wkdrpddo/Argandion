using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReplacer : MonoBehaviour
{
    public GameObject[] _Object= new GameObject[8];
    public bool _purification;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateObject(int index)
    {
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

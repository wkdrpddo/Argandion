using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvested : MonoBehaviour
{
    public GameObject cropOnField;
    public Transform self;
    public int[] howMany;
    public GameObject _buffManagerObject;
    private BuffManager _buff;
    public CropPosition _pCpo;
    public Dirt _pd;
    public int _cropCode;

    // Start is called before the first frame update
    Vector3 aboveSoil(Vector3 pos) {
        return new Vector3(pos.x, pos.y + 1, pos.z); 
    }

    void Start()
    {
        _buffManagerObject = GameObject.Find("BuffManager");
        _buff = _buffManagerObject.GetComponent<BuffManager>();
    }

    public void Harvesting()
    {
        if (_pCpo)
        {
            _pCpo._plant = null;
            _pCpo._state = 0;
            _pd._is_icon_dig = true;
        }
        Destroy(gameObject);
        int many = howMany[Random.Range(0, howMany.Length)];
        if ( _buff.orangePray ) {
            float manyFloat = (float)many;
            manyFloat *= 1.4f;
            many = Mathf.RoundToInt(manyFloat);
        }
        for (int i = 0; i < many; i++) {
            Instantiate(cropOnField, this.transform.position + new Vector3(Random.Range(-2f,2f),1f,Random.Range(-2f,2f)), self.rotation);
        }
    }
}

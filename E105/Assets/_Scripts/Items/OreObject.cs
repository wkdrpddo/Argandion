using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreObject : MonoBehaviour
{
    public float _health;
    public int type;
    public GameObject[] _droppedItem = new GameObject[2];
    public int[,] _droppedCount = new int[2,2];
    public BuffManager _buff;
    private SoundManager _sound;

    void Start()
    {
        _buff = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        _sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        switch(type)
        {
            case 0:
                _droppedCount[0,0] = 0;
                _droppedCount[0,0] = 4;
                _droppedCount[1,0] = 1;
                _droppedCount[1,1] = 3;
                break;
            case 1:
                _droppedCount[0,0] = 0;
                _droppedCount[0,0] = 4;
                _droppedCount[1,0] = 1;
                _droppedCount[1,1] = 3;
                break;
            case 2:
                _droppedCount[0,0] = 0;
                _droppedCount[0,0] = 5;
                _droppedCount[1,0] = 1;
                _droppedCount[1,1] = 2;
                break;
        }
    }
    // Start is called before the first frame update
    public void DayEnd()
    {
        Destroy(gameObject);
    }

    public void Damaged(int itemcode)
    {
        switch (itemcode)
        {
            case 302:
                _health -= 10;
                break;
            case 307:
                _health -= 15;
                break;
            case 312:
                _health -= 20;
                break;
            case 317:
                _health -= 30;
                break;
        }
        Invoke("DamagedSound", 0.15f);
        if (_health <= 0)
        {
            DestroyOre();
        }
    }

    private void DamagedSound()
    {
        _sound.playEffectSound("PICKING");
    }


    private void DestroyOre()
    {
        Transform trans = gameObject.transform;
        Destroy(this.gameObject);
        int many = Random.Range(_droppedCount[0,0],_droppedCount[0,1]);
        for (int i=0;i<many;i++) {
            Instantiate(_droppedItem[0], trans.position + new Vector3(Random.Range(-2f,2f),1.5f,Random.Range(-2f,2f)), trans.rotation);
        }
        int many2 = Random.Range(_droppedCount[1,0],_droppedCount[1,1]);
        float final = many2;
        if ( _buff.redPray ) {
            final *= Random.Range(1.2f,2.0f);
        }
        while (final >= 1)
        {
            final -= 1;
            Instantiate(_droppedItem[1], trans.position + new Vector3(Random.Range(-2f,2f),1.5f,Random.Range(-2f,2f)), trans.rotation);
        }
        float lotto = Random.Range(0f,1f);
        if (lotto < final)
        {
            Instantiate(_droppedItem[1], trans.position + new Vector3(Random.Range(-2f,2f),1.5f,Random.Range(-2f,2f)), trans.rotation);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSystem : MonoBehaviour
{
    private int _sector;
    public GameObject _branch;
    public GameObject _fruit;
    private Quaternion _qua = Quaternion.Euler(0,0,0);

    void Start()
    {
        _sector = gameObject.transform.parent.GetComponent<SectorObject>()._sectorNumber;
    }
    // Start is called before the first frame update
    public void DayStart()
    {
        int rnd = Random.Range(0,3);
        spawnBranch(rnd);
        int rnd2 = Random.Range(0,3);
        spawnFruit(rnd2);
    }

    private void spawnBranch(int count)
    {
        while (count>0)
        {
            Vector3 pos = new Vector3(gameObject.transform.position.x+Random.Range(-2f,2f),gameObject.transform.position.y+0.5f,gameObject.transform.position.z+Random.Range(-2f,2f));
            Instantiate(_branch,pos,_qua);
            count -= 1;
        }
    }

    private void spawnFruit(int count)
    {
        while (count>0)
        {
            Vector3 pos = new Vector3(gameObject.transform.position.x+Random.Range(-2f,2f),gameObject.transform.position.y+0.5f,gameObject.transform.position.z+Random.Range(-2f,2f));
            Instantiate(_fruit,pos,_qua);
            count -= 1;
        }
    }
}

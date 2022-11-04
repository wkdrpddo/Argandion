using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class CropObject
{
    public int CropCode;
    public string CropName;
    public int Water;
    public int NextPhaseDay;
    public int Season;
}

[System.Serializable]
public class CropData
{
    public List<CropObject> Crop;
}

public class Crop : MonoBehaviour
{
    public int cropCode;
    public CropObject cropObject;
    public GameObject nextCrop;
    public Transform self;
    public int updateDay = 0;
    private GameObject nearSoil;
    public GameObject _buffManagerObject;
    private BuffManager _buff;
    private int[] extraDays = {0,1};

    // Start is called before the first frame update
    void Start()
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Scripts/CropsTable.json");
        var cropData = JsonHelper.FromJson<CropObject>(jsonString);
        cropObject = cropData[cropCode];
        _buffManagerObject = GameObject.Find("BuffManager");
        _buff = _buffManagerObject.GetComponent<BuffManager>();
    }

    public void growUp()
    {
        updateDay += (1 + (_buff.orangePray ? extraDays[Random.Range(0,2)] : 0));
        if (updateDay >= cropObject.NextPhaseDay) {
            Dirt dirt = nearSoil.GetComponent<Dirt>();
            dirt.minusWater -= cropObject.Water;
            Instantiate(nextCrop, self.position, self.rotation);
            Destroy(gameObject);
        }
    }

     void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("wateredDirt"))
        {
            nearSoil = other.gameObject;
            Dirt dirt = nearSoil.GetComponent<Dirt>();
            dirt.minusWater += cropObject.Water/2;
        }
        else if (other.gameObject.CompareTag("dirt"))
        {
            nearSoil = other.gameObject;
            Dirt dirt = nearSoil.GetComponent<Dirt>();
            dirt.minusWater += cropObject.Water/2;
        }
    }
}

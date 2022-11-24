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
    private BuffManager _buff;
    public SystemManager _systemManager;
    public CropPosition _pCpo;
    public Dirt _pd;

    private int[] extraDays = {0,1};
    public bool isIn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/CropsTable.json");
        var cropData = JsonHelper.FromJson<CropObject>(jsonString);
        cropObject = cropData[cropCode];
        minuswater();
        _buff = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        _systemManager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
    }

    public void growUp()
    {
        if ((_systemManager._season != cropObject.Season) && ( cropObject.Season != 4) && (_buff.yellowPray)) {
            Debug.Log("이 계절에는 자랄수 없는 작물입니다!");
            return ;
        }

        updateDay += (1 + (_buff.orangePray ? extraDays[Random.Range(0,2)] : 0));
        if (updateDay >= cropObject.NextPhaseDay) {
            _pd.minusWater -= cropObject.Water;
            // _pd.CropGrowUp(gameObject);
            GameObject plant = Instantiate(nextCrop, self.position, self.rotation, gameObject.transform.parent);
            _pCpo._plant = plant;
            if (plant.TryGetComponent(out Crop _crop))
            {
                _crop._pCpo = gameObject.transform.parent.GetComponent<CropPosition>();
                _crop._pd = _pd;
            }
            if (plant.TryGetComponent(out Harvested _harv))
            {
                _harv._pCpo = gameObject.transform.parent.GetComponent<CropPosition>();
                _harv._pd = _pd;
            }
            Destroy(gameObject);
        }
    }

    public void minuswater()
    {
        _pd.minusWater += cropObject.Water;
    }

    // void OnTriggerEnter(Collider other) {
    //     if (other.gameObject.CompareTag("wateredDirt"))
    //     {
    //         nearSoil = other.gameObject;
    //         Dirt dirt = nearSoil.GetComponent<Dirt>();
    //         dirt.minusWater += cropObject.Water;
    //     }
    //     else if (other.gameObject.CompareTag("dirt"))
    //     {
    //         nearSoil = other.gameObject;
    //         Dirt dirt = nearSoil.GetComponent<Dirt>();
    //         dirt.minusWater += cropObject.Water;
    //     }
    // }
}

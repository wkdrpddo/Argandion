using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class JsonSave
{
    public List<float> treepos = new List<float>();
    public List<int> treeday = new List<int>();
    public List<int> treeSector = new List<int>();
    public List<bool> treebool = new List<bool>();
    public int treecount;

    public List<float> flowerpos = new List<float>();
    public List<int> flowerIndex = new List<int>();
    public List<int> flowerSector = new List<int>();
    public int flowercount;
}

public class SaveSystem : MonoBehaviour
{
    public SaveData _savedata;

    public bool _isSave;
    public bool _isLoad;
    public PlayerSystem _ps;
    public SystemManager _sys;
    public UIManager _ui;
    public BuffManager _bm;
    public SoundManager _sm;
    public PrayBuff _pb;
    public Item _item;
    public Dirt[] _Dirts;
    public GameObject[] _tree;
    public GameObject[] _fallen_tree;
    public GameObject[] _flowers;
    public MapObject _MapObject;
    public GameObject[] _CropData;

    void Start()
    {
        _savedata = new SaveData();
        _ps = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
        _sys = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        _ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        _bm = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        _sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _pb = GameObject.Find("BuffManager").GetComponent<PrayBuff>();
        _item = GameObject.Find("ItemManager").GetComponent<Item>();
        _MapObject = GameObject.Find("Map").GetComponent<MapObject>();
    }
    public void Save(SaveData _data)
    {
        _savedata._playerName = _ps.getPlayerName();
        _savedata._playerMoney = _sys.getPlayerGold();
        _savedata._playertype = _ps.getPlayerType();
        _savedata._month = _sys._month;
        _savedata._day = _sys._day;
        _savedata._weather = _sys._weather;
        _savedata._season = _sys._season;
        _savedata._sound1 = _sm.getBackgroundSound();
        _savedata._sound2 = _sm.getEffectSound();

        // 인벤토리 아이템 저장
        Slot[] inventorySlot = UIManager._uimanagerInstance.getInventorySlots();
        for (int i=0;i<25;i++)
        {
            if (inventorySlot[i].itemCount != 0)
            {
                _savedata._inventory[i,0] = inventorySlot[i].item.ItemCode;
                _savedata._inventory[i,1] = inventorySlot[i].itemCount;
            }
        }

        // 저장고 아이템 저장
        Slot[] storageSlot = UIManager._uimanagerInstance.getStorageSlots();
        for (int i=0;i<100;i++)
        {
            if (storageSlot[i].itemCount != 0)
            {
                _savedata._storage[i,0] = storageSlot[i].item.ItemCode;
                _savedata._storage[i,1] = storageSlot[i].itemCount;
            }
        }

        // 퀵슬롯의 아이템 등록
        for (int i = 0; i < 8; i++)
        {
            _savedata._quickslot[i, 0] = (int)_ps._equipList[i, 0];
            _savedata._quickslot[i, 1] = (int)_ps._equipList[i, 4];
        }

        // 장비창 아이템 저장
        for (int i = 0; i < 4; i++)
        {
            _savedata._equipment[i,0] = _ps.getSlotEquip(i);
        }

        // 정화 정보
        _savedata._sectorPurifierCount = _sys._purification_sector;
        _savedata._PurifierLevel = _sys._development_level;
        for (int i = 0; i < 8; i++)
        {
            _savedata._sectorPurifierInfo[i] = _sys._purification[i];
        }

        // 버프 지속시간
        _savedata._flowerBuffTargetMonth = _bm._flowerBuffTargetMonth;
        _savedata._flowerBuffTargetDay = _bm._flowerBuffTargetDay;
        
        // 버프 종류 확인
        _savedata._flowerBuffBoolList[0] = _bm._isFlowerBuffActived; // 버프 진행 중
        _savedata._flowerBuffBoolList[1] = _bm._isPrayBuffActived; // 제단 버프 진행 중
        _savedata._flowerBuffBoolList[2] = _bm.whiteSpirit;
        _savedata._flowerBuffBoolList[3] = _bm.whitePray;
        _savedata._flowerBuffBoolList[4] = _bm.orangeSpirit;
        _savedata._flowerBuffBoolList[5] = _bm.orangePray;
        _savedata._flowerBuffBoolList[6] = _bm.blueSpirit;
        _savedata._flowerBuffBoolList[7] = _bm.bluePray;
        _savedata._flowerBuffBoolList[8] = _bm.redSpirit;
        _savedata._flowerBuffBoolList[9] = _bm.redPray;
        _savedata._flowerBuffBoolList[10] = _bm.pinkSpirit;
        _savedata._flowerBuffBoolList[11] = _bm.pinkPray;
        _savedata._flowerBuffBoolList[12] = _bm.yellowSpirit;
        _savedata._flowerBuffBoolList[13] = _bm.yellowPray;
        _savedata._flowerBuffBoolList[14] = _bm.skySpirit;
        _savedata._flowerBuffBoolList[15] = _bm.skyPray;

        // 장마 , 한파 정보 저장
        _savedata._rainy_month = _bm.rainyMonth;
        _savedata._rainy_day = _bm.rainyDay;
        _savedata._coldwave_month = _bm.coldWaveMonth;
        _savedata._coldwave_day = _bm.coldWaveDay;

        // 농토 정보 저장
        for (int i = 0; i < 81; i++)
        {
            _savedata._dirt[i, 0] = _Dirts[i].watered;
            _savedata._dirt[i, 1] = _Dirts[i].minusWater;

            CropPosition[] _croppos = _Dirts[i].gameObject.GetComponentsInChildren<CropPosition>();
            foreach (CropPosition _crpo in _croppos)
            {
                _savedata._farm[i,0] = _crpo._parent_dirt;
                _savedata._farm[i,1] = _crpo._state;
                if (_crpo._state > 0)
                {
                    if (_crpo._plant.gameObject.TryGetComponent(out Crop crop))
                    {
                        _savedata._farm[i,2] = crop.cropCode;
                        _savedata._farm[i,3] = crop.updateDay;
                    }
                    else if (_crpo._plant.gameObject.TryGetComponent(out Harvested harv))
                    {
                        _savedata._farm[i,2] = harv._cropCode;
                        _savedata._farm[i,3] = 0;
                    }
                }
                else
                {
                    _savedata._farm[i,2] = 0;
                    _savedata._farm[i,3] = 0;
                }
            }
        }

        // 정령 버프 제사 진행도 / 꽃 번호
        _savedata.pray[0] = _pb.prayDay;
        _savedata.pray[1] = _pb.flowerIdx;

        string filePath = "saveFile.dat";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Create);
        formatter.Serialize(stream, _data);
        stream.Close();
    }

    public void Load()
    {
        string filePath = "saveFile.dat";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;
        if (File.Exists(filePath)) {
            stream = File.Open(filePath, FileMode.Open);
        } else
        {
            stream = null;
        }
        if (stream != null && stream.Length > 0)
        {
            SaveData _LoadData = (SaveData)formatter.Deserialize(stream);
            // Debug.Log("로드 동작");
            _ps.setPlayerName(_LoadData._playerName);
            _sys.setPlayerGold(_LoadData._playerMoney);
            _ps.setPlayerType(_LoadData._playertype);
            _sys._month = _LoadData._month;
            _sys._day = _LoadData._day;
            _sys._weather = _LoadData._weather;
            _sys._season = _LoadData._season;
            _ps.setPlayerType(_LoadData._playertype);
            _ui.loadItemData(_LoadData._inventory,0);
            _ui.loadItemData(_LoadData._quickslot,1);
            _ui.loadItemData(_LoadData._equipment,2);
            _ui.loadItemData(_LoadData._storage,3);

            for (int i = 0; i < 7; i++)
            {
                _ps.setQuickItem(i,_LoadData._quickslot[i,0],_LoadData._quickslot[i,1]);
            }

            for (int i = 0; i< 4; i++)
            {
                _ps.setEquipItem(_LoadData._equipment[i,0],i);
            }
            for (int i = 0; i < 8; i++)
            {
                _sys._purification[i] = _LoadData._sectorPurifierInfo[i];
                if (_LoadData._sectorPurifierInfo[i] && i!=4)
                {
                    _sys.UpdatePurification(i+1);
                }
            }
            _bm._flowerBuffTargetMonth = _LoadData._flowerBuffTargetMonth;
            _bm._flowerBuffTargetDay = _LoadData._flowerBuffTargetDay;
            
            // 버프 지속시간
        
            // 버프 종류 확인
            _bm._isFlowerBuffActived = _LoadData._flowerBuffBoolList[0]; // 버프 진행 중
            _bm._isPrayBuffActived = _LoadData._flowerBuffBoolList[1]; // 제단 버프 진행 중
            _bm.whiteSpirit = _LoadData._flowerBuffBoolList[2];
            if (_bm.whiteSpirit){
                _sys.callActiveIcon(11);
            }
            _bm.whitePray = _LoadData._flowerBuffBoolList[3];
            if (_bm.whitePray){
                _sys.callActiveIcon(1);
            }
            _bm.orangeSpirit = _LoadData._flowerBuffBoolList[4];
            if (_bm.orangeSpirit){
                _sys.callActiveIcon(12);
            }
            _bm.orangePray = _LoadData._flowerBuffBoolList[5];
            if (_bm.orangePray){
                _sys.callActiveIcon(2);
            }
            _bm.blueSpirit = _LoadData._flowerBuffBoolList[6];
            if (_bm.blueSpirit){
                _sys.callActiveIcon(13);
            }
            _bm.bluePray = _LoadData._flowerBuffBoolList[7];
            if (_bm.bluePray){
                _sys.callActiveIcon(3);
            }
            _bm.redSpirit = _LoadData._flowerBuffBoolList[8];
            if (_bm.redSpirit){
                _sys.callActiveIcon(14);
            }
            _bm.redPray = _LoadData._flowerBuffBoolList[9];
            if (_bm.redPray){
                _sys.callActiveIcon(4);
            }
            _bm.pinkSpirit = _LoadData._flowerBuffBoolList[10];
            if (_bm.pinkSpirit){
                _sys.callActiveIcon(15);
            }
            _bm.pinkPray = _LoadData._flowerBuffBoolList[11];
            if (_bm.pinkPray){
                _sys.callActiveIcon(5);
            }
            _bm.yellowSpirit = _LoadData._flowerBuffBoolList[12];
            if (_bm.yellowSpirit){
                _sys.callActiveIcon(16);
            }
            _bm.yellowPray = _LoadData._flowerBuffBoolList[13];
            if (_bm.yellowPray){
                _sys.callActiveIcon(6);
            }
            _bm.skySpirit = _LoadData._flowerBuffBoolList[14];
            if (_bm.skySpirit){
                _sys.callActiveIcon(17);
            }
            _bm.skyPray = _LoadData._flowerBuffBoolList[15];
            if (_bm.skyPray){
                _sys.callActiveIcon(7);
            }

            // 장마 , 한파
            _bm.rainyDay = _LoadData._rainy_day;
            _bm.rainyMonth = _LoadData._rainy_month;
            _bm.coldWaveDay = _LoadData._coldwave_day;
            _bm.coldWaveMonth = _LoadData._coldwave_month;

            // 정령 버프 제사 진행도 / 꽃 번호
            _pb.prayDay = _LoadData.pray[0];
            _pb.flowerIdx = _LoadData.pray[1];

            // 농토 정보 확인
            for (int i = 0; i < 81; i++)
            {
                if (_Dirts[i].isActiveAndEnabled)
                {
                    _Dirts[i].watered = _LoadData._dirt[i,0];
                    CropPosition[] _croppos = _Dirts[i].gameObject.GetComponentsInChildren<CropPosition>();
                    foreach (CropPosition _crpo in _croppos)
                    {
                        _crpo._state = _LoadData._farm[i,1];
                        if (_crpo._state > 0)
                        {
                            GameObject plant;
                            if (_LoadData._farm[i,2] >= 0 && _LoadData._farm[i,2] < 100)
                            {
                                plant = Instantiate(_CropData[_LoadData._farm[i,2]],_crpo.gameObject.transform.position,_crpo.gameObject.transform.rotation,_crpo.gameObject.transform);
                                plant.GetComponent<Crop>()._pCpo=_crpo;
                                plant.GetComponent<Crop>()._pd=_Dirts[i];
                                plant.GetComponent<Crop>().updateDay = _LoadData._farm[i,3];
                                _crpo._plant=plant;
                            }
                            else if (_LoadData._farm[i,2] >= 100 && _LoadData._farm[i,3] < 200)
                            {
                                
                                plant = Instantiate(_CropData[_LoadData._farm[i,2]-96],_crpo.gameObject.transform.position,_crpo.gameObject.transform.rotation,_crpo.gameObject.transform);
                                plant.GetComponent<Harvested>()._pCpo=_crpo;
                                plant.GetComponent<Harvested>()._pd=_Dirts[i];
                                _crpo._plant=plant;
                            }
                        }
                        else
                        {
                            _savedata._farm[i,2] = 0;
                            _savedata._farm[i,3] = 0;
                        }
                    }
                    _Dirts[i]._is_icon_dig = false;
                    _Dirts[i].checkHoe();
                    _Dirts[i].checkWater();
                }
            }

            _sys.UpdateSeason(_sys._season);
            _sys.LoadWeather();
            stream.Close();
        }
    }

    void Update()
    {
        if (_isSave)
        {
            _isSave = !_isSave;
            Save(_savedata);

        }
        if (_isLoad)
        {
            _isLoad = !_isLoad;
            Load();
        }
    }
}

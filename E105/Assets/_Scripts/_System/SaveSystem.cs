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
    public int treecount;

    public List<int> buffIndex = new List<int>();
    public List<int> buffRemainDay = new List<int>();
    public int buffcount;

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
    public Item _item;

    void Start()
    {
        _savedata = new SaveData();
        // _ps = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
        // _sys = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        // _ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        // _bm = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        // _sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        // _item = GameObject.Find("Item").GetComponent<Item>();
    }
    public void Save(SaveData _data)
    {
        _savedata._playerName = _ps.getPlayerName();
        _savedata._playerMoney = _sys.getPlayerGold();
        _savedata._playertype = _ps.getPlayerType();
        _savedata._month = _sys._month;
        _savedata._day = _sys._day;
        // _savedata._weather = ;
        _savedata._season = _sys._season;
        // _savedata._sound1 = ;
        // _savedata._sound2 = ;

        // 인벤토리 아이템 저장
        // Slot[] inventorySlot = GameObject.Find("InventorySlotParent").GetComponentsInChildren<Slot>();
        // for (int i=0;i<25;i++)
        // {
        //     _savedata._inventory[i,0] = inventorySlot[i].item.ItemCode;
        //     _savedata._inventory[i,1] = inventorySlot[i].itemCount;
        // }

        // 저장고 아이템 저장
        // Slot[] storageSlot = GameObject.Find("StorageSlotParent").GetComponentsInChildren<Slot>();
        // for (int i=0;i<100;i++)
        // {
        //     _savedata._storage[i,0] = storageSlot[i].item.ItemCode;
        //     _savedata._storage[i,1] = storageSlot[i].itemCount;
        // }

        // 퀵슬롯의 아이템 등록
        for (int i = 0; i < 7; i++)
        {
            _savedata._quickslot[i, 0] = (int)_ps._equipList[i, 0];
            _savedata._quickslot[i, 1] = (int)_ps._equipList[i, 4];
        }

        // 장비창 아이템 저장
        // ??

        _savedata._sectorPurifierCount = _sys._purification_sector;
        _savedata._PurifierLevel = _sys._development_level;
        for (int i = 0; i < 8; i++)
        {
            _savedata._sectorPurifierInfo[i] = _sys._purification[i];
        }
        _savedata._flowerBuffTargetMonth = _bm._flowerBuffTargetMonth;
        _savedata._flowerBuffTargetDay = _bm._flowerBuffTargetDay;

        // 농토 정보 저장
        Dirt[] dirts = GameObject.Find("DirtParent").GetComponentsInChildren<Dirt>();
        for (int i = 0; i < 81; i++)
        {
            _savedata._dirt[i, 0] = dirts[i].watered;
            _savedata._dirt[i, 1] = dirts[i].minusWater;
        }

        // 정령 버프 제사 진행도 / 꽃 번호
        // _savedata.pray[0] = 1;
        // _savedata.pray[1] = 2;

        string filePath = "saveFile.dat";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Create);
        formatter.Serialize(stream, _data);
        stream.Close();

        JsonSave saveJson = new JsonSave();
        GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");
        for (int i = 0; i < trees.Length; i++)
        {
            saveJson.treepos.Add(trees[i].transform.position.x);
            saveJson.treepos.Add(trees[i].transform.position.y);
            saveJson.treepos.Add(trees[i].transform.position.z);
            saveJson.treeday.Add(trees[i].GetComponent<TreeObject>()._days);
        }
        saveJson.treecount = trees.Length;
        string json = JsonUtility.ToJson(saveJson, true);
        File.WriteAllText("jsonSave.json", json);
    }

    public void Load()
    {
        string filePath = "saveFile.dat";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Open(filePath, FileMode.Open);

        if (stream != null && stream.Length > 0)
        {
            SaveData _LoadData = (SaveData)formatter.Deserialize(stream);
            Debug.Log("로드 동작");

            // _savedata._day = LoadData._month;
            _ps.setPlayerName(_LoadData._playerName);
            _sys.setPlayerGold(_LoadData._playerMoney);
            _ps.setPlayerType(_LoadData._playertype);
            _sys._month = _LoadData._month;
            _sys._day = _LoadData._day;
            // = _LoadData._weather;
            _sys._season = _LoadData._season;
            // _sm._optionpanel = _LoadData._sound1;
            // _sm._optionpanel = _LoadData._sound2;

            // 인벤토리 아이템 호출
            // Slot[] inventorySlot = GameObject.Find("InventorySlotParent").GetComponentsInChildren<Slot>();
            // for (int i=0;i<25;i++)
            // {
            //     inventorySlot[i].AddItem(_item.FindItem(_LoadData._inventory[i,0]),_LoadData._inventory[i,1]);
            // }

            // 저장고 아이템 호출
            // Slot[] storageSlot = GameObject.Find("StorageSlotParent").GetComponentsInChildren<Slot>();
            // for (int i=0;i<100;i++)
            // {
            //     storageSlot[i].AddItem(_item.FindItem(_LoadData._storage[i,0]),_LoadData._storage[i,1]);
            // }

            for (int i = 0; i < 7; i++)
            {
                // 아이템 장착 함수 (갯수도)
            }

            _sys._purification_sector = _LoadData._sectorPurifierCount;
            _sys._development_level = _LoadData._PurifierLevel;
            for (int i = 0; i < 8; i++)
            {
                _sys._purification[i] = _LoadData._sectorPurifierInfo[i];
            }
            _bm._flowerBuffTargetMonth = _LoadData._flowerBuffTargetMonth;
            _bm._flowerBuffTargetDay = _LoadData._flowerBuffTargetDay;

            // 농토 정보 저장
            Dirt[] dirts = GameObject.Find("DirtParent").GetComponentsInChildren<Dirt>();
            for (int i = 0; i < 81; i++)
            {
                dirts[i].watered = _LoadData._dirt[i, 0];
                dirts[i].minusWater = _LoadData._dirt[i, 1];
            }

            // 정령 버프 제사 진행도 / 꽃 번호
            // 1 = _LoadData.pray[0];
            // 2 = _LoadData.pray[1];
        }
    }

    // void Update()
    // {
    //     if (_isSave)
    //     {
    //         _isSave = !_isSave;
    //         Save(_savedata);

    //     }
    //     if (_isLoad)
    //     {
    //         _isLoad = !_isLoad;
    //         Load();
    //     }
    // }
}

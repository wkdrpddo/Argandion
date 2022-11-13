using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    [SerializeField]
    private string _playerName;
    private int _playertype;
    public GameObject _SystemManager;
    public UIManager _UIManager;
    public Transform _camera;
    public Transform _character;
    public Animator _playerAnimator;
    public GameObject _wheat;
    private GameObject _nearBiome;
    public Inventory _theInventory;
    public Chest _theChest;
    public GameObject _buffManagerObject;
    private BuffManager _buff;

    private GameObject _nearObject;

    private SoundManager _soundManager;

    // { itemcode, 장비코드(0그외 1채집 2괭이 3도끼 4곡괭이 5검 6낚싯대), 이동불가 시간, 작업시간}
    public float[,] _equipList = new float[,] { { 300, 1, 1f, 1f, 1 }, { 301, 3, 0.8f, 0.8f, 1 }, { 302, 4, 0.8f, 0.8f, 1 }, { 303, 2, 1.5f, 0, 1 }, { 304, 5, 0.6f, 0.6f, 1 }, { 10, 0, 0, 0, 20 }, { 20, 0, 0, 0, 10 } };
    public GameObject[] _equipment = new GameObject[7];
    public int _equipItem = 0;

    public float _walkspeed;
    public float _runspeed;
    public float _health_max;
    public float _health;
    public float _stamina_max;
    public float _stamina;
    private float _delayedTimer;
    private float _movedDelay;
    private bool _setHand = false;
    private bool _onSoil = false;
    public bool _canMove = true;
    private bool _canAction = true;

    private bool _ikDown;

    private bool _nearAlter = false;
    private bool _nearSpirit = false;
    private bool _nearChest = false;
    private bool _nearCrops = false;
    private bool _readyToHarvest = false;
    private bool _nearCarpentor = false;
    private bool _nearItem = false;

    [SerializeField]
    private float _act_speed = 1.0f;
    private float _delay_speed = 1.0f;

    public float _gold;


    public int chestIdx = 0;
    public int chestCount = 1;
    public int invenIdx = 0;
    public int invenCount = 1;
    public float _interactRadius;
    private Rigidbody rb;
    private Vector3 speed;

    private Collider[] _colset;
    public int _onAir = 0;
    [SerializeField]
    private bool _canInteract;
    public float _gravity;

    private int _current_region;  //플레이어가 현재 어느 있는 지역의 상태 

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        _buffManagerObject = GameObject.Find("BuffManager");
        _buff = _buffManagerObject.GetComponent<BuffManager>();
        _UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _current_region = 0;
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // GetBuff();
        GetInput();
        Move();
        changeItem();
        checkHand();
        Interaction();
        watering();
        fff();
    }

    void GetBuff()
    {
        if (_buff.pinkPray)
        {
            _health_max = 200;
            _stamina_max = 150;
        }
        else
        {
            _health_max = 100;
            _stamina_max = 100;
            if (_health > _health_max)
            {
                _health = _health_max;
            }
            if (_stamina > _stamina_max)
            {
                _stamina = _stamina_max;
            }
        }
    }

    void GetInput()
    {
        _ikDown = Input.GetButtonDown("interactionKey");
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        bool _MoveMag = moveInput.magnitude != 0;
        if (!_MoveMag)
        {
            // speed = moveInput * 0;
            speed.Set(moveInput.x * 0, 0, moveInput.y * 0);
            if (_onAir == 0)
            {
                speed.y = -_gravity;
            }
            rb.velocity = (speed);
        }
        if (_movedDelay <= 0 && _canMove && _MoveMag)
        // if (_movedDelay <= 0 && _canMove)
        {
            Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            _character.forward = moveDir;
            if (Input.GetAxisRaw("run") != 0)
            {
                // transform.position += moveDir * Time.deltaTime * _runspeed;
                speed = moveDir * _runspeed * (_buff.skyPray ? 1.3f : 1.0f) * (_buff.skySpirit ? 1.3f : 1.0f);
                // speed.y = -1f;
                if (_onAir == 0)
                {
                    speed.y = -_gravity;
                }
                rb.velocity = (speed);
                _playerAnimator.SetInteger("action", 2);

            }
            else
            {
                // transform.position += moveDir * Time.deltaTime * _walkspeed;
                speed = moveDir * _runspeed * (_buff.skyPray ? 1.3f : 1.0f) * (_buff.skySpirit ? 1.3f : 1.0f);
                // speed.y = -1f;
                if (_onAir == 0)
                {
                    speed.y = -_gravity;
                }
                rb.velocity = (speed);
                _playerAnimator.SetInteger("action", 1);
            }
        }
        else
        {
            speed = new Vector3(0, 0, 0);
            if (_onAir == 0)
            {
                speed.y = -_gravity;
            }
            rb.velocity = (speed);
            _playerAnimator.SetInteger("action", 0);
            _movedDelay -= Time.deltaTime;
            _movedDelay = Mathf.Max(0, _movedDelay);
        }
        if (_canAction && Input.GetAxisRaw("useKey") == 1 && _delayedTimer <= 0)
        {
            if (_equipList[_equipItem, 1] >= 3)
            {
                _playerAnimator.SetInteger("action", ((int)_equipList[_equipItem, 1]));
                _delayedTimer = _equipList[_equipItem, 2] / _delay_speed;
                _movedDelay = _equipList[_equipItem, 3] / _act_speed;
                _equipment[_equipItem].SetActive(true);
                _setHand = true;
            }

            if (_equipList[_equipItem, 1] == 4)
            {
                Collider[] ores = Physics.OverlapBox(new Vector3(_character.position.x, _character.position.y, _character.position.z) + (_character.forward * 0.5f), new Vector3(0.5f, 0.5f, 0.5f));
                foreach (var ore in ores)
                {
                    if (ore.tag == "Ore")
                    {
                        ore.gameObject.TryGetComponent(out OreObject O);
                        {
                            O.Damaged(15);
                        }
                    }
                }
            }

            if (_equipList[_equipItem, 1] == 3)
            {
                // Debug.Log(_character.forward);
                Vector3 pos = new Vector3(_character.position.x, _character.position.y, _character.position.z) + (_character.forward * 0.5f);
                Collider[] trees = Physics.OverlapBox(pos, new Vector3(0.5f, 0.5f, 0.5f));
                foreach (var tree in trees)
                {
                    if (tree.gameObject.layer == 7)
                    {
                        tree.gameObject.TryGetComponent(out TreeObject T);
                        {
                            T.Damaged(15);
                        }
                    }
                }
            }
        }
        else
        {
            _delayedTimer -= Time.deltaTime;
            _delayedTimer = Mathf.Max(0, _delayedTimer);
        }
        _character.position = transform.position;
    }

    private void changeItem()
    {
        if (Input.GetAxisRaw("equip1") == 1)
        {
            _equipItem = 0;
            _UIManager.setEquipPointer(1);
        }
        else if (Input.GetAxisRaw("equip2") == 1)
        {
            _equipItem = 1;
            _UIManager.setEquipPointer(2);
        }
        else if (Input.GetAxisRaw("equip3") == 1)
        {
            _equipItem = 2;
            _UIManager.setEquipPointer(3);
        }
        else if (Input.GetAxisRaw("equip4") == 1)
        {
            _equipItem = 3;
            _UIManager.setEquipPointer(4);
        }
        else if (Input.GetAxisRaw("equip5") == 1)
        {
            _equipItem = 4;
            _UIManager.setEquipPointer(5);
        }
        else if (Input.GetAxisRaw("equip6") == 1)
        {
            _equipItem = 5;
            _UIManager.setEquipPointer(6);
        }
        else if (Input.GetAxisRaw("equip7") == 1)
        {
            _equipItem = 6;
            _UIManager.setEquipPointer(7);
        }
    }

    private void checkHand()
    {
        if (_movedDelay <= 0 && _setHand)
        {
            _setHand = false;
            foreach (var obj in _equipment)
            {
                if (obj)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    // private void Interaction()
    // {
    //     if (_ikDown && _nearObject != null)
    //     {
    //         Debug.Log("Interaction(): " + _nearObject.tag);
    //         if (_nearObject.tag == "CraftingTable" || _nearObject.tag == "Sign")
    //         {
    //             InteractionUI crafting = _nearObject.GetComponent<InteractionUI>();

    //             if (crafting._open)
    //             {
    //                 crafting.Exit();
    //             }
    //             else
    //             {
    //                 crafting.Enter();
    //             }ㅉㅉ
    //         }
    //         _ikDown = false;
    //     }

    // }

    private void Interaction()
    {
        if (Input.GetButtonDown("interactionKey") && _canInteract)
        {
            Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            _colset = Physics.OverlapSphere(pos, _interactRadius, layerMask: 1633);
            Debug.Log(_colset.Length);
            foreach (var col in _colset)
            {
                if (col.TryGetComponent(out Interactable inter))
                {
                    Debug.Log(col);
                    // 이런 형태로 작성
                    // if (col.TryGetComponent(out NPCObject npc))
                    // {
                    //     npc.Interaction();
                    // }
                    if (col.TryGetComponent(out NPCObject npc))
                    {
                        npc.Interaction();
                    }
                    if (_equipList[_equipItem, 1] == 1 && col.TryGetComponent(out GatheringObject Gat))
                    {
                        Debug.Log("버..섯?");
                        Debug.Log(Gat);
                        Gat.Interaction(_equipList[_equipItem, 1]);
                        _delayedTimer = _equipList[_equipItem, 2] / _delay_speed;
                        _movedDelay = _equipList[_equipItem, 3] / _act_speed;
                    }
                    // building쪽 ==============
                    if (col.TryGetComponent(out SignInteraction signInteraction))
                    {
                        Debug.Log("표지판 인식");
                        signInteraction.Interaction();
                    }
                    if (col.TryGetComponent(out CraftingTableInteraction craftingTableInteraction))
                    {
                        Debug.Log("제작대 인식");
                        craftingTableInteraction.Interaction();
                    }
                    if (col.TryGetComponent(out CookingInteraction cookingInteraction))
                    {
                        Debug.Log("주방 인식");
                        cookingInteraction.Interaction();
                    }
                    if (col.TryGetComponent(out SleepInteraction sleepInteraction))
                    {
                        sleepInteraction.Interaction();
                    }
                    if (col.TryGetComponent(out TeleportationHome teleportationHome))
                    {
                        Debug.Log("이동 인식");
                        teleportationHome.Interaction();
                    }
                    if (col.TryGetComponent(out ChestInteraction chestInteraction))
                    {
                        Debug.Log("상자 인식");
                        chestInteraction.Interaction();
                    }
                    // ===========================
                }
            }
        }
    }

    public void StopActionTime(float timer)
    {
        _delayedTimer = timer;
    }
    public void StopMoveTime(float timer)
    {
        _movedDelay = timer;
    }

    private Vector3 nearSoil(Vector3 pos)
    {
        float tempz = (int)pos.z + (pos.z > 0 ? 0.5f : -0.5f);
        float tempx = (int)pos.x + (pos.x > 0 ? 0.5f : -0.5f);
        return new Vector3(tempx, pos.y, tempz);
    }

    void fff()
    {
        if (_readyToHarvest && Input.GetKeyDown(KeyCode.F))
        {
            Harvested harvested = _nearObject.GetComponent<Harvested>();
            harvested.Harvesting();
            _nearCrops = false;
            _nearObject = null;
            _readyToHarvest = false;
            return;
        }

        if (_onSoil && Input.GetKeyDown(KeyCode.F) && !_nearCrops)
        {
            Instantiate(_wheat, nearSoil(_character.position), _character.rotation);
        }

        if (_nearItem && Input.GetKeyDown(KeyCode.F))
        {
            Item item = _nearObject.GetComponent<Item>();
            ItemObject itemObject = item.itemObject;
            if (itemObject != null)
            {
                _theInventory.AcquireItem(itemObject);
            }
        }


        if (_nearCarpentor && Input.GetButtonDown("fff"))
        {
            CombCarpentor combCarpentor = _nearObject.GetComponent<CombCarpentor>();
            combCarpentor.Trade(2, 2);
        }

        if (_nearChest && Input.GetButtonDown("fff"))
        {
            ItemObject item = _theInventory.StoreItem(invenIdx, -invenCount);
            Debug.Log(item);
            _theChest.PutItem(item, invenCount);
        }

        if (_nearChest && Input.GetKeyDown(KeyCode.G))
        {
            ItemObject item = _theChest.TakeItem(chestIdx, -chestCount);
            Debug.Log("햇당");
            Debug.Log(item);
            _theInventory.AcquireItem(item, chestCount);
        }

        if (_nearSpirit && Input.GetButtonDown("fff"))
        {
            if (_buff._isFlowerBuffActived)
            {
                Debug.Log("이미 다른버프가 발동중이라구!");
            }
            else
            {
                ItemObject item = _theInventory.StoreItem(0, -1);
                if (item.Category == "꽃")
                {
                    SpiritBuff spirit = _nearObject.GetComponent<SpiritBuff>();
                    spirit.Spirit(item);
                    _buff._isFlowerBuffActived = true;
                }
                else if (item.ItemCode == 0)
                {
                    Debug.Log("아무것도 없는데?");
                }
                else
                {
                    _theInventory.AcquireItem(item, 1);
                    Debug.Log("이건뭐야?");
                }
            }
        }

        if (_nearAlter && Input.GetButtonDown("fff"))
        {
            ItemObject item = _theInventory.StoreItem(0, 0);
            if (item.Category == "꽃")
            {
                PrayBuff pray = _nearObject.GetComponent<PrayBuff>();
                pray.Pray(item);
            }
            else
            {
                Debug.Log("꽃가져와");
            }
        }

    }
    void watering()
    {
        if (_onSoil && Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("물줬당");
            Dirt dirt = _nearBiome.GetComponent<Dirt>();
            dirt.Water();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("dirt") || other.gameObject.CompareTag("wateredDirt"))
        {
            _onSoil = true;
            _nearBiome = other.gameObject;
        }

        if (other.gameObject.CompareTag("crop"))
        {
            _nearCrops = true;
        }

        if (other.gameObject.CompareTag("harvested"))
        {
            _nearCrops = true;
            _readyToHarvest = true;
            _nearObject = other.gameObject;
        }

        if (other.gameObject.CompareTag("item"))
        {
            _nearObject = other.gameObject;
            _nearItem = true;
        }

        // if (other.gameObject.CompareTag("droppedItem")){
        //     _nearObject = other.gameObject;
        //     _nearDroppedItem = true;
        // }

        if (other.gameObject.CompareTag("chest"))
        {
            _nearObject = other.gameObject;
            _nearChest = true;
        }

        if (other.gameObject.CompareTag("spirit"))
        {
            _nearObject = other.gameObject;
            _nearSpirit = true;
        }

        if (other.gameObject.CompareTag("alter"))
        {
            _nearObject = other.gameObject;
            _nearAlter = true;
        }
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("carpentor"))
        {
            _nearObject = other.gameObject;
            _nearCarpentor = true;
            CombCarpentor combCarpentor = _nearObject.GetComponent<CombCarpentor>();
            combCarpentor.Hello();
        }

        if (other.gameObject.CompareTag("droppedItem"))
        {
            Debug.Log("아이템 가까이에 있음");
            _nearObject = other.gameObject;
            DroppedItem item = _nearObject.GetComponent<DroppedItem>();
            _theInventory.AcquireItem(item.itemObject, 1);
            Debug.Log(_nearObject.transform.parent);
            Destroy(_nearObject.transform.parent.gameObject);
        }

        // Debug.Log("OnTriggerEnter(): " + other.tag);
        if (other.tag == "CraftingTable" || other.tag == "Sign")
        {
            _nearObject = other.gameObject;
            // Debug.Log("작업 영역");
        }

        if (other.tag == "sector")
        {
            //현재 들어온 지역
            if (other.transform.GetComponent<SectorObject>()._purifier)  //정화된 구역에 들어왔을때
            {
                if (_current_region != 0)  //이전 구역이 정화구역이 아니었을때만 사운드 체인지
                {
                    _soundManager.playBGM1();
                    _current_region = 0;
                }
            }
            else  //황폐화 구역에 들어왔을때
            {
                if (_current_region != 1) //이전 구역이 황폐화구역이 아니었을때만 사운드 체인지
                {
                    _soundManager.playBGM2();
                    _current_region = 1;
                }
            }
        }

        if (other.tag == "forest")   //숲으로 이동
        {
            if (_current_region != 2)
            {
                _soundManager.playBGM3();
                _current_region = 2;
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("dirt"))
        {
            _onSoil = false;
            _nearBiome = null;
        }

        if (other.gameObject.CompareTag("crop"))
        {
            _nearCrops = false;
        }

        if (other.gameObject.CompareTag("harvested"))
        {
            _nearCrops = false;
            _readyToHarvest = false;
            _nearObject = null;
        }

        if (other.gameObject.CompareTag("item"))
        {
            _nearObject = null;
            _nearItem = false;
        }

        if (other.gameObject.CompareTag("carpentor"))
        {
            _nearObject = null;
            _nearCarpentor = false;
        }

        if (other.gameObject.CompareTag("chest"))
        {
            _nearObject = null;
            _nearChest = false;
        }

        if (other.gameObject.CompareTag("spirit"))
        {
            _nearObject = null;
            _nearSpirit = false;
        }

        if (other.gameObject.CompareTag("alter"))
        {
            _nearObject = null;
            _nearAlter = false;
        }

        if (other.gameObject.CompareTag("droppedItem"))
        {
            _nearObject = null;
        }

    }


    public void changeHealth(float value)
    {
        _health -= value;
        if (_health <= 0)
        {
            _health = 0;
            playerDeath();
        }
        else if (_health == _health_max)
        {
            _health = _health_max;
        }
        _UIManager.setHealthBar(_health / _health_max);
    }

    public void changeEnergy(float value)
    {
        _stamina -= value * (_buff.pinkPray ? 0.8f : 1.0f) * (_buff.pinkSpirit ? 0.8f : 1.0f);
        if (_stamina <= 0)
        {
            _stamina = 0;
            playerDeath();
        }
        else if (_stamina == _stamina_max)
        {
            _stamina = _stamina_max;
        }
        // _UIManager.setEnergyBar(_stamina/_stamina_max);
    }

    private void playerDeath()
    {
        Debug.Log("플레이어 체력 또는 기력 0");
    }

    public void setPlayerName(string name)
    {
        _playerName = name;
    }

    public string getPlayerName()
    {
        return _playerName;
    }

    public void setPlayerType(int index)
    {
        _playertype = index;
    }

    public int getPlayerType()
    {
        return _playertype;
    }

    public void toggleCanAction()
    {
        _canAction = !_canAction;
    }

    public void setCanAction(bool value)
    {
        _canAction = value;
    }

    public void toggleCanInteract()
    {
        _canInteract = !_canInteract;
    }

    public void setCanInteract(bool value)
    {
        _canInteract = value;
    }

    public void setActionSpeed()
    {
        _act_speed = 1;
        if (_buff.skyPray)
        {
            _act_speed *= 1.3f;
        }
    }
}

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
    public GameObject[] _crops;
    private GameObject _nearObject;
    private Item _itemManager;
    private SoundManager _soundManager;

    // { itemcode, 장비코드(0그외 1채집 2괭이 3도끼 4곡괭이 5검 6낚싯대 7씨앗), 이동불가 시간, 작업시간}
    public float[,] _equipList = new float[8,5];
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
    private float _runtime;
    private bool _removeHandItem;

    [SerializeField]
    private float _act_speed = 1.0f;
    private float _delay_speed = 1.0f;
    public int chestIdx = 0;
    public int chestCount = 1;
    public int invenIdx = 0;
    public int invenCount = 1;
    public float _interactRadius;
    private Rigidbody rb;
    private Vector3 speed;
    public GameObject[] _player_character;

    private Collider[] _colset;
    public int _onAir = 0;
    [SerializeField]
    private bool _canInteract;
    public float _gravity;

    private bool otherAnimated;
    private float _gatherspeed = 2.5f;
    private float _shovelspeed = 2f;
    private int _stamina_percent = 100;
    private int _health_percent = 100;
    private int _damage_percent = 100;
    private int _defense_percent = 100;
    private int _movement_percent = 100;
    private int[] _slot_equipment = new int[] { 0, 0, 0, 0 };
    private int _current_region;  //플레이어가 현재 어느 지역에 있는지 상태 저장 

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        _buffManagerObject = GameObject.Find("BuffManager");
        _buff = _buffManagerObject.GetComponent<BuffManager>();
        _UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _itemManager = GameObject.Find("ItemManager").GetComponent<Item>();
        _character = GameObject.Find("PlayerBody").transform;
        _SystemManager = GameObject.Find("SystemManager");
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _theInventory = GameObject.Find("UIManager").transform.GetChild(8).transform.GetChild(1).GetComponent<Inventory>();
    }

    void Update()
    {
        GetInput();
        Move();
        changeItem();
        checkHand();
        Interaction();
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
            speed.Set(moveInput.x * 0, 0, moveInput.y * 0);
            if (_onAir == 0)
            {
                speed.y = -_gravity;
            }
            rb.velocity = (speed);
        }
        if (_movedDelay <= 0 && _canMove && _MoveMag)
        {
            Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            _character.forward = moveDir;
            if (Input.GetAxisRaw("run") != 0)
            {
                speed = moveDir * _runspeed * _movement_percent / 100 * (_buff.skyPray ? 1.3f : 1.0f) * (_buff.skySpirit ? 1.3f : 1.0f);
                _soundManager.playerEffectSound("RUNNING");
                if (_onAir == 0)
                {
                    speed.y = -_gravity;
                }
                rb.velocity = (speed);
                _playerAnimator.SetInteger("action", 2);
                _runtime += Time.deltaTime;
            }
            else
            {
                speed = moveDir * _walkspeed * _movement_percent / 100 * (_buff.skyPray ? 1.3f : 1.0f) * (_buff.skySpirit ? 1.3f : 1.0f);
                _soundManager.playerEffectSound("MOVING");
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
            if (!otherAnimated)
            {
                _playerAnimator.SetInteger("action", 0);
            }
            else
            {
                otherAnimated = false;
            }
            _movedDelay -= Time.deltaTime;
            _movedDelay = Mathf.Max(0, _movedDelay);
        }
        if (_canAction && Input.GetButtonDown("useKey") && _delayedTimer <= 0)
        {
            // 아이템 사용(식량)
            if (_equipList[_equipItem, 0] >= 100 && _equipList[_equipItem, 0] < 200)
            {
                _UIManager.quickUse((int)_equipList[_equipItem, 0], 1, _equipItem);
            }

            // 장비 휘두르기 (괭이 도끼 곡괭이 검)
            if (_equipList[_equipItem, 1] >= 3 && _equipList[_equipItem, 1] <= 5)
            {
                _playerAnimator.SetInteger("action", ((int)_equipList[_equipItem, 1]));
                _delayedTimer = _equipList[_equipItem, 2] / _delay_speed;
                _movedDelay = _equipList[_equipItem, 3] / _act_speed;
                _equipment[(int)_equipList[_equipItem, 1]].SetActive(true);
                _setHand = true;
            }
            if (_equipList[_equipItem, 1] == 2)
            {
                _playerAnimator.SetInteger("action", 15);
                _delayedTimer = _equipList[_equipItem, 2] / _delay_speed;
                _movedDelay = _equipList[_equipItem, 3] / _act_speed;
                _equipment[(int)_equipList[_equipItem, 1]].SetActive(true);
                _setHand = true;
                if (_equipList[_equipItem, 0] == 303)
                {
                    damageStamina(3.0f);
                }
                else if (_equipList[_equipItem, 0] == 308)
                {
                    damageStamina(2.0f);
                }
                else if (_equipList[_equipItem, 0] == 313)
                {
                    damageStamina(1.5f);
                }
                else if (_equipList[_equipItem, 0] == 318)
                {
                    damageStamina(1.0f);
                }
            }

            // 곡괭이 휘두르기 물리 판정
            if (_equipList[_equipItem, 1] == 4)
            {
                damageStamina(0.7f);
                Collider[] ores = Physics.OverlapBox(new Vector3(_character.position.x, _character.position.y, _character.position.z) + (_character.forward * 0.5f), new Vector3(0.5f, 1.5f, 0.5f));
                foreach (var ore in ores)
                {
                    if (ore.tag == "Ore")
                    {
                        ore.gameObject.TryGetComponent(out OreObject O);
                        {
                            O.Damaged((int)_equipList[_equipItem,0]);
                        }
                    }
                }
            }

            if (_equipList[_equipItem, 1] == 2)
            {
                Collider[] dirts = Physics.OverlapBox(new Vector3(_character.position.x, _character.position.y, _character.position.z) + (_character.forward * 0.5f), new Vector3(0.5f, 1.5f, 0.5f));
                foreach (var dirt in dirts)
                {
                    if (dirt.CompareTag("dirt") && dirt.TryGetComponent(out Dirt dir))
                    {
                        dir.Hoe();
                    }
                }
            }

            // 도끼 사용
            if (_equipList[_equipItem, 1] == 3)
            {
                damageStamina(0.5f);
                Vector3 pos = new Vector3(_character.position.x, _character.position.y, _character.position.z) + (_character.forward * 0.5f);
                Collider[] trees = Physics.OverlapBox(pos, new Vector3(0.5f, 1.5f, 0.5f));
                foreach (var tree in trees)
                {
                    if (tree.gameObject.layer == 7)
                    {
                        tree.gameObject.TryGetComponent(out TreeObject T);
                        {
                            T.Damaged((int)_equipList[_equipItem,0]);
                        }
                    }
                }
            }
            if(_equipList[_equipItem, 1] == 5){
                damageStamina(1f);
            }
        }
        else
        {
            _delayedTimer -= Time.deltaTime;
            _delayedTimer = Mathf.Max(0, _delayedTimer);
        }
        _character.position = transform.position;
        if (Input.GetButtonDown("next"))
        {
            QEItemEquip(1);
        }
        if (Input.GetButtonDown("prev"))
        {
            QEItemEquip(-1);
        }
        if (_runtime >= 3)
        {
            _runtime -= 3;
            damageStamina(1);
        }
    }

    private void changeItem()
    {
        if (_canInteract)
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
    }

    private void QEItemEquip(int value)
    {
        if (value > 0)
        {
            _equipItem += 1;
            if (_equipItem > 6) {
                _equipItem -= 7;
            }
        }
        else if (value < 0)
        {
            _equipItem -= 1;
            if (_equipItem < 0) {
                _equipItem += 7;
            }
        }
        _UIManager.setEquipPointer(_equipItem+1);
    }

    private void checkHand()
    {
        if (_movedDelay <= 0 && _setHand || _removeHandItem)
        {
            _setHand = false;
            foreach (var obj in _equipment)
            {
                if (obj)
                {
                    obj.SetActive(false);
                }
            }
            _removeHandItem = false;
        }
    }

    public void setHandItem(bool value = false, int hand = 0)
    {
        if (!value)
        {
            _equipment[hand].SetActive(true);
        }
        else
        {
            _removeHandItem = value;
        }
    }

    private void Interaction()
    {
        if (Input.GetButtonDown("interactionKey") && _canInteract)
        {
            Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            _colset = Physics.OverlapSphere(pos, _interactRadius, layerMask: 1633);
            foreach (var col in _colset)
            {
                if (col.TryGetComponent(out Interactable inter))
                {
                    if (50 <= _equipList[_equipItem, 0] && _equipList[_equipItem, 0] <= 56 && col.TryGetComponent(out WorldTreeSpirit fairy))
                    {
                        fairy.FlowerInteraction((int)_equipList[_equipItem, 0]);
                        break;
                    }
                    else
                    {
                        if (col.TryGetComponent(out NPCObject npc))
                        {
                            npc.Interaction();
                            break;
                        }
                    }
                    if (col.TryGetComponent(out Dirt dirt))
                    {
                        if (!dirt.fullWater)
                        {
                            dirt.Water();
                            _delayedTimer = 3f / _delay_speed;
                            _movedDelay = 3f / _act_speed;
                            _equipment[0].SetActive(true);
                            setAnimator(14,3f/_act_speed);
                            _canInteract = false;
                            damageStamina(1f);
                            Invoke("EndWatering",3f/_act_speed);
                            break;
                        }
                    }
                    if (_equipList[_equipItem, 1] == 7 && col.TryGetComponent(out CropPosition crop))
                    {
                        if (crop._pd.fullWater) {
                            if (crop._state==-1)
                            {
                                crop.Interaction((int)_equipList[_equipItem, 0]);
                                _UIManager.quickUse((int)_equipList[_equipItem, 0],1,_equipItem);
                                break;
                            }
                        }
                    }
                    if (col.TryGetComponent(out Harvested harv))
                    {
                        harv.Harvesting();
                        break;
                    }
                    if (col.TryGetComponent(out WorldTreeInteraction worldTreeInteraction))
                    {
                        worldTreeInteraction.Interaction();
                        break;
                    }
                    if (_equipList[_equipItem, 1] == 1 && col.TryGetComponent(out GatheringObject Gat))
                    {
                        if (Gat._isremain == false)
                        {
                            Gat.Interaction(_equipList[_equipItem, 1]);
                            _delayedTimer = _equipList[_equipItem, 2] / _delay_speed;
                            _movedDelay = _equipList[_equipItem, 3] / _act_speed;
                            setCanInteract(false);
                            Invoke("onCanInteract",_movedDelay);
                            break;
                        }
                        else if (Gat._isHave)
                        {
                            Gat.Interaction(_equipList[_equipItem, 1]);
                            _delayedTimer = _equipList[_equipItem, 2] / _delay_speed;
                            _movedDelay = _equipList[_equipItem, 3] / _act_speed;
                            setCanInteract(false);
                            Invoke("onCanInteract",_movedDelay);
                            break;
                        }
                    }
                    if (_equipList[_equipItem, 1] == 6 && _equipList[7, 4] > 0 && col.TryGetComponent(out Fishing fis))
                    {
                        fis.Interaction(_equipList[_equipItem, 0], _equipList[7, 0]);
                        break;
                    }
                    if (_equipList[_equipItem, 0] >= 50 && _equipList[_equipItem, 0] <= 56 && col.TryGetComponent(out AltarTableInteraction alt))
                    {
                        alt.Interaction((int)_equipList[_equipItem, 0], _equipItem);
                        break;
                    }
                    if (col.TryGetComponent(out SignInteraction signInteraction))
                    {
                        signInteraction.Interaction();
                        break;
                    }
                    if (col.TryGetComponent(out CraftingTableInteraction craftingTableInteraction))
                    {
                        craftingTableInteraction.Interaction();
                        break;
                    }
                    if (col.TryGetComponent(out CookingInteraction cookingInteraction))
                    {
                        cookingInteraction.Interaction();
                        break;
                    }
                    if (col.TryGetComponent(out SleepInteraction sleepInteraction))
                    {
                        sleepInteraction.Interaction();
                        break;
                    }
                    if (col.TryGetComponent(out TeleportationHome teleportationHome))
                    {
                        teleportationHome.Interaction();
                        break;
                    }
                    if (col.TryGetComponent(out ChestInteraction chestInteraction))
                    {
                        chestInteraction.Interaction();
                        break;
                    }
                    if (col.TryGetComponent(out TeleportAltar teleportAltar))
                    {
                        teleportAltar.Interaction();
                        break;
                    }
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
        float tempx = Mathf.Round(pos.x);
        return new Vector3(tempx, pos.y, tempz);
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
            _soundManager.playEffectSound("GETITEM");
            // Debug.Log("아이템 가까이에 있음"); 
            _nearObject = other.gameObject;
            DroppedItem item = _nearObject.GetComponent<DroppedItem>();
            if (_theInventory.CheckInven(item.itemObject))
            {
                _theInventory.AcquireItem(item.itemObject, 1);
                Destroy(_nearObject.transform.parent.gameObject);
            }
        }
        if (other.tag == "sector" && !_SystemManager.GetComponent<SystemManager>().getTimeSystem())
        {
            //현재 들어온 지역
            if (other.transform.GetComponent<SectorObject>()._purifier)  //정화된 구역에 들어왔을때
            {
                if (_current_region != 0)  //이전 구역이 정화구역이 아니었을 때만 사운드 체인지
                {
                    _soundManager.playBGM1();
                    _current_region = 0;
                }
            }
            else  //황폐화 구역에 들어왔을때
            {
                if (_current_region != 1) //이전 구역이 황폐화구역이 아니었을 때만 사운드 체인지
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
    }


    public void changeHealth(float value)
    {
        _health -= value;
        if (_health <= 0)
        {
            _health = 0;
            playerDeath();
        }
        else if (_health >= _health_max)
        {
            _health = _health_max;
        }
        _UIManager.setHealthBar(_health,_health_max);
    }

    public void damageHealth(float value)
    {
        float dmg = value * _defense_percent / 100;
        _health -= (int)dmg;
        dmg -= (int)dmg;
        if (dmg > 0)
        {
            float rnd = Random.Range(0f, 1f);
            if (rnd < dmg)
            {
                _health -= 1;
            }
        }
        if (_health <= 0)
        {
            _health = 0;
            playerDeath();
        }
        _UIManager.setHealthBar(_health,_health_max);
    }

    public void changeEnergy(float value)
    {
        _stamina -= value * (_buff.pinkPray ? 0.8f : 1.0f) * (_buff.pinkSpirit ? 0.8f : 1.0f);
        if (_stamina <= 0)
        {
            _stamina = 0;
            playerDeath();
        }
        else if (_stamina >= _stamina_max)
        {
            _stamina = _stamina_max;
        }
        _UIManager.setEnergyBar(_stamina,_stamina_max);
    }

    public void damageStamina(float value)
    {
        float dmg = value * _stamina_percent / 100;
        _stamina -= (int)dmg;
        dmg -= (int)dmg;
        if (dmg > 0)
        {
            float rnd = Random.Range(0f, 1f);
            if (rnd < dmg)
            {
                _stamina -= 1;
            }
        }
        if (_stamina <= 0)
        {
            _stamina = 0;
            playerDeath();
        }
        _UIManager.setEnergyBar(_stamina,_stamina_max);
    }

    private void playerDeath()
    {
        _SystemManager.GetComponent<SystemManager>().playerDeath();
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
        ChangePlayerCharacter(index);
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

    private void EndWatering()
    {
        _canInteract = true;
        _equipment[0].SetActive(false);
    }

    public void toggleCanInteract()
    {
        _canInteract = !_canInteract;
    }

    public void setCanInteract(bool value)
    {
        _canInteract = value;
    }

    public void onCanInteract()
    {
        _canInteract = true;
    }

    public void setActionSpeed()
    {
        _act_speed = 1;
        if (_buff.skyPray)
        {
            _act_speed *= 1.3f;
        }
    }

    public void setAnimator(int index, float time)
    {
        otherAnimated = true;
        _playerAnimator.SetInteger("action", index);
        _movedDelay = time;
        _delayedTimer = time;
    }

    public void setQuickItem(int index, int itemCode, int Count)
    {
        ItemObject input = _itemManager.FindItem(itemCode);
        if (input.Category == "장비")
        {
            _equipList[index, 0] = itemCode;
            _equipList[index, 4] = 1;
            if (itemCode == 300 || itemCode == 305 || itemCode == 310 || itemCode == 315)
            {
                _equipList[index, 1] = 1;
                switch (itemCode)
                {
                    case 300:
                        _equipList[index, 2] = 2f;
                        _equipList[index, 3] = 2f;
                        break;
                    case 305:
                        _equipList[index, 2] = 1.5f;
                        _equipList[index, 3] = 1.5f;
                        break;
                    case 310:
                        _equipList[index, 2] = 1f;
                        _equipList[index, 3] = 1f;
                        break;
                    case 315:
                        _equipList[index, 2] = 0.5f;
                        _equipList[index, 3] = 0.5f;
                        break;
                }
            }
            if (itemCode == 301 || itemCode == 306 || itemCode == 311 || itemCode == 316)
            {
                _equipList[index, 1] = 3;
                _equipList[index, 2] = 0.8f;
                _equipList[index, 3] = 0.8f;
            }
            if (itemCode == 302 || itemCode == 307 || itemCode == 312 || itemCode == 317)
            {
                _equipList[index, 1] = 4;
                _equipList[index, 2] = 0.8f;
                _equipList[index, 3] = 0.8f;
            }
            if (itemCode == 303 || itemCode == 308 || itemCode == 313 || itemCode == 318)
            {
                _equipList[index, 1] = 2;
                switch (itemCode)
                {
                    case 303:
                        _equipList[index, 2] = 2f;
                        _equipList[index, 3] = 2f;
                        break;
                    case 308:
                        _equipList[index, 2] = 1.5f;
                        _equipList[index, 3] = 1.5f;
                        break;
                    case 313:
                        _equipList[index, 2] = 1f;
                        _equipList[index, 3] = 1f;
                        break;
                    case 318:
                        _equipList[index, 2] = 0.5f;
                        _equipList[index, 3] = 0.5f;
                        break;
                }
            }
            if (itemCode == 304 || itemCode == 309 || itemCode == 314 || itemCode == 319)
            {
                _equipList[index, 1] = 5;
                _equipList[index, 2] = 0.6f;
                _equipList[index, 3] = 0.6f;
            }
            if (itemCode == 320 || itemCode == 321 || itemCode == 322)
            {
                _equipList[index, 1] = 6;
            }
        }
        else
        {
            _equipList[index, 0] = itemCode;
            if (itemCode < 300 && 200 < itemCode)
            {
                _equipList[index, 1] = 7;
            }
            else
            {
                _equipList[index, 1] = 0;
            }
            _equipList[index, 2] = 0;
            _equipList[index, 3] = 0;
            _equipList[index, 4] = Count;
        }
    }

    public void setEquipItem(int itemcode, int slot)
    {
        if (_slot_equipment[slot] == 0)
        {
            addEquipItem(itemcode);
            _slot_equipment[slot] = itemcode;
        }
        else
        {
            subEquipItem(itemcode);
            addEquipItem(itemcode);
            _slot_equipment[slot] = itemcode;
        }
    }

    private void addEquipItem(int itemcode)
    {
        switch (itemcode)
        {
            case 400:
                _stamina_percent -= 10;
                break;
            case 401:
                _damage_percent += 10;
                break;
            case 402:
                _defense_percent += 10;
                break;
            case 403:
                _movement_percent += 10;
                break;
            case 404:
                removeDebuff();
                break;
        }
    }

    private void subEquipItem(int itemcode)
    {
        switch (itemcode)
        {
            case 400:
                _stamina_percent += 10;
                break;
            case 401:
                _damage_percent -= 10;
                break;
            case 402:
                _defense_percent -= 10;
                break;
            case 403:
                _movement_percent -= 10;
                break;
            case 404:
                break;
        }
    }

    private void removeDebuff()
    {

    }

    public void ChangePlayerCharacter(int index)
    {
        foreach (var obj in _player_character)
        {
            obj.SetActive(false);
        }
        _player_character[index].SetActive(true);
    }

    public int getSlotEquip(int index)
    {
        return _slot_equipment[index];
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    public GameObject _SystemManager;
    public UIManager _UIManager;
    public Transform _camera;
    public Transform _character;
    public Animator _playerAnimator;
    public GameObject _wheat;
    private GameObject _nearBiome;
    public Inventory _theInventory;
    public Chest _theChest;
    public BuffManager _buffManager;

    private GameObject _nearObject;

    // { itemcode, 장비코드(0그외 1채집 2도끼 3곡괭이 4괭이 5검 6낚싯대), 이동불가 시간, 작업시간}
    public float[,] _equipList = new float[,] { { 300, 1, 1f, 1f, 1 }, { 301, 3, 0.8f, 0.8f, 1 }, { 302, 4, 0.8f, 0.8f, 1 }, { 303, 2, 1.5f, 0, 1 }, { 304, 5, 0.6f, 0.6f, 1 }, { 10, 0, 0, 0, 20 }, { 20, 0, 0, 0, 10 } };
    public GameObject[] _equipment = new GameObject[5];
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

    private bool _ikDown;

    private bool _nearAlter = false;
    private bool _nearSpirit = false;
    private bool _nearChest = false;
    private bool _nearCrops = false;
    private bool _readyToHarvest = false;
    private bool _nearCarpentor = false;
    private bool _nearItem = false;

    public float _gold;


    public int chestIdx = 0;
    public int chestCount = 1;
    public int invenIdx = 0;
    public int invenCount = 1;
    public float _interactRadius;
    private Rigidbody rb;
    private Vector3 speed;

    private Collider[] _colset;
    private bool _canInteract;
    public int _onAir = 0;
    public float _gravity;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        changeItem();
        checkHand();
        Interaction();
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
                speed = moveDir * _runspeed;
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
                speed = moveDir * _walkspeed;
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
            rb.velocity = speed;
            _playerAnimator.SetInteger("action", 0);
            _movedDelay -= Time.deltaTime;
            _movedDelay = Mathf.Max(0, _movedDelay);
        }
        if (Input.GetAxisRaw("useKey") == 1 && _delayedTimer <= 0)
        {
            if (_equipList[_equipItem, 1] >= 3)
            {
                _playerAnimator.SetInteger("action", ((int)_equipList[_equipItem, 1]));
                _delayedTimer = _equipList[_equipItem, 2];
                _movedDelay = _equipList[_equipItem, 3];
                Debug.Log(((int)_equipList[_equipItem, 1]));
                _equipment[_equipItem].SetActive(true);
                _setHand = true;
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
        if (Input.GetButtonDown("interactionKey"))
        {
            Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            _colset = Physics.OverlapSphere(pos, _interactRadius, layerMask: 1633);
            foreach (var col in _colset)
            {
                if (col.TryGetComponent(out Interactable inter))
                {
                    // 이런 형태로 작성
                    // if (col.TryGetComponent(out NPCObject npc))
                    // {
                    //     npc.Interaction();
                    // }
                    if (col.TryGetComponent(out NPCObject npc))
                    {
                        npc.Interaction();
                    }
                    if (_equipList[_equipItem, 0] == 1 && col.TryGetComponent(out GatheringObject Gat))
                    {
                        Debug.Log("버..섯?");
                        Debug.Log(Gat);
                        Gat.Interaction(_equipList[_equipItem, 1]);
                        _delayedTimer = _equipList[_equipItem, 2];
                        _movedDelay = _equipList[_equipItem, 3];
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
                        teleportationHome.Interaction();
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
        if (_readyToHarvest && Input.GetButtonDown("fff"))
        {
            Harvested harvested = _nearObject.GetComponent<Harvested>();
            harvested.Harvesting();
            _nearCrops = false;
            _nearObject = null;
            _readyToHarvest = false;
            return;
        }

        if (_onSoil && Input.GetButtonDown("fff") && !_nearCrops)
        {
            Instantiate(_wheat, nearSoil(_character.position), _character.rotation);
        }

        if (_nearItem && Input.GetButtonDown("fff"))
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

        if (_nearChest && Input.GetButtonDown("water"))
        {
            ItemObject item = _theChest.TakeItem(chestIdx, -chestCount);
            Debug.Log("햇당");
            Debug.Log(item);
            _theInventory.AcquireItem(item, chestCount);
        }

        if (_nearSpirit && Input.GetButtonDown("fff"))
        {
            if (_buffManager._isFlowerBuffActived)
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
                    _buffManager._isFlowerBuffActived = true;
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
        if (_onSoil && Input.GetButtonDown("water"))
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
            _nearObject = other.gameObject;
            DroppedItem item = _nearObject.GetComponent<DroppedItem>();
            _theInventory.AcquireItem(item.itemObject, 1);
            Destroy(_nearObject);
        }

        Debug.Log("OnTriggerEnter(): " + other.tag);
        if (other.tag == "CraftingTable" || other.tag == "Sign")
        {
            _nearObject = other.gameObject;
            // Debug.Log("작업 영역");
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
        _UIManager.setHealthBar(_health / _health_max);
    }

    public void changeEnergy(float value)
    {
        _stamina -= value;
        if (_stamina <= 0)
        {
            _stamina = 0;
            playerDeath();
        }
        _UIManager.setEnergyBar(_stamina / _stamina_max);
    }

    private void playerDeath()
    {
        Debug.Log("플레이어 체력 또는 기력 0");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    public GameObject _SystemManager;
    public UIManager _UIManager;
    public Transform _camera;
    public Transform _character;
    public Rigidbody _rigid;
    public Animator _playerAnimator;

    public GameObject _wheat;

    private GameObject _nearBiome;
    public Inventory _theInventory;
    public Chest _theChest;
    public BuffManager _buffManager;

    private GameObject _nearObject;
    private InteractionUI _interactionUi;

    public float[,] _equipList = new float[,] { { 300, 1, 1.5f, 0 }, { 301, 3, 0.8f, 0.8f }, { 302, 4, 0.8f, 0.8f }, { 303, 2, 1.5f, 0 }, { 304, 5, 0.6f, 0.6f } };
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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        changeItem();
        checkHand();


    }

    void GetInput()
    {
        _ikDown = Input.GetButtonDown("interactionKey");
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        bool _MoveMag = moveInput.magnitude != 0;
        if (_movedDelay <= 0 && _canMove && _MoveMag)
        {
            Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            if (Input.GetAxisRaw("run") != 0)
            {
                transform.position += moveDir * Time.deltaTime * _runspeed;
                _playerAnimator.SetInteger("action", 2);

            }
            else
            {
                transform.position += moveDir * Time.deltaTime * _walkspeed;
                _playerAnimator.SetInteger("action", 1);
            }
            _character.forward = moveDir;
        }
        else
        {
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
        }
        else if (Input.GetAxisRaw("equip2") == 1)
        {
            _equipItem = 1;
        }
        else if (Input.GetAxisRaw("equip3") == 1)
        {
            _equipItem = 2;
        }
        else if (Input.GetAxisRaw("equip4") == 1)
        {
            _equipItem = 3;
        }
        else if (Input.GetAxisRaw("equip5") == 1)
        {
            _equipItem = 4;
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

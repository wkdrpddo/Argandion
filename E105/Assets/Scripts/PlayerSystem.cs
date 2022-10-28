using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    public GameObject _SystemManager;
    public Transform _camera;
    public Transform _character;
    public Rigidbody _rigid;
    public Animator _playerAnimator;
    public GameObject _wheat;
    private GameObject _nearBiome;
    private GameObject _nearObject;

    public float[,] _equipList = new float[,] {{300,1,1.5f,0},{301,3,0.8f,0.8f},{302,4,0.8f,0.8f},{303,2,1.5f,0},{304,5,0.6f,0.6f}};
    public GameObject[] _equipment = new GameObject[5];
    public int _equipItem=0; 

    public float _walkspeed;
    public float _runspeed;
    public float _health_max;
    public float _health;
    public float _stamina_max;
    public float _stamina;
    private float _delayedTimer;
    private float _movedDelay;
    private bool _setHand=false;
    private bool _onSoil=false;
    private bool _nearCrops = false;
    private bool _readyToHarvest = false;
    public float _gold;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        changeItem();
        checkHand();
        fff();
        watering();
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        bool canMove = moveInput.magnitude != 0;
        if (_movedDelay <= 0 && canMove) {
            Vector3 moveDir = new Vector3(moveInput.x , 0 , moveInput.y).normalized;
            if (Input.GetAxisRaw("run") != 0) {
                transform.position += moveDir * Time.deltaTime * _runspeed;
                _playerAnimator.SetInteger("action",2);
                
            }
            else {
                transform.position += moveDir * Time.deltaTime * _walkspeed;
                _playerAnimator.SetInteger("action",1);
            }
            _character.forward = moveDir;
        }
        else {
            _playerAnimator.SetInteger("action",0);
            _movedDelay -= Time.deltaTime;
            _movedDelay = Mathf.Max(0, _movedDelay);
        }
        if (Input.GetAxisRaw("usekey")==1 && _delayedTimer<=0) {
            if (_equipList[_equipItem,1]>=3) {
                _playerAnimator.SetInteger("action",((int)_equipList[_equipItem,1]));
                _delayedTimer = _equipList[_equipItem,2];
                _movedDelay = _equipList[_equipItem,3];
                Debug.Log(((int)_equipList[_equipItem,1]));
                _equipment[_equipItem].SetActive(true);
                _setHand = true;
            }
        } else {
            _delayedTimer -= Time.deltaTime;
            _delayedTimer = Mathf.Max(0,_delayedTimer);
        }
        _character.position = transform.position;
    }

    private void changeItem() {
        if (Input.GetAxisRaw("equip1")==1) {
            _equipItem = 0;
        } else if (Input.GetAxisRaw("equip2")==1) {
            _equipItem = 1;
        } else if (Input.GetAxisRaw("equip3")==1) {
            _equipItem = 2;
        } else if (Input.GetAxisRaw("equip4")==1) {
            _equipItem = 3;
        } else if (Input.GetAxisRaw("equip5")==1) {
            _equipItem = 4;
        }
    }

    private void checkHand() {
        if (_movedDelay <= 0 && _setHand) {
            _setHand=false;
            foreach (var obj in _equipment) {
                if (obj) {
                    obj.SetActive(false);
                }
            }
        }
    }

    void fff() {
        if(_readyToHarvest && Input.GetButtonDown("fff")) {
            Wheat3 wheat = _nearObject.GetComponent<Wheat3>();
            wheat.Harvested();
            _nearCrops = false;
            _nearObject = null;
            _readyToHarvest = false;
            return ;
        }

        if (_onSoil && Input.GetButtonDown("fff") && !_nearCrops) {
            Instantiate(_wheat, _character.position, _character.rotation);
        }
    }

    void watering() {
        if (_onSoil && Input.GetButtonDown("water") ) {
            Debug.Log("물줬당");
            Dirt dirt = _nearBiome.GetComponent<Dirt>();
            dirt.Water();
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("dirt") || other.gameObject.CompareTag("wateredDirt")){
            _onSoil = true;
            _nearBiome = other.gameObject;
        }

        if (other.gameObject.CompareTag("wheat"))
        {
            _nearCrops = true;
        }

        if (other.gameObject.CompareTag("wheatCrop"))
        {
            _nearCrops = true;
            _readyToHarvest = true;
            _nearObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("dirt")) {
            _onSoil = false;
            _nearBiome = null;
        }

        if (other.gameObject.CompareTag("wheat")){
            _nearCrops = false;
        }

        if (other.gameObject.CompareTag("wheatCrop"))
        {
            _nearCrops = false;
            _readyToHarvest = false;
            _nearObject = null;
        }
    }
}

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
    public bool _canMove = true;

    private bool _ikDown;

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



    private void Interaction()
    {
        if (_ikDown && _nearObject != null)
        {
            Debug.Log(_nearObject.tag);
            if (_nearObject.tag == "CraftingTable" || _nearObject.tag == "Sign")
            {
                InteractionUI crafting = _nearObject.GetComponent<InteractionUI>();

                if (crafting._open)
                {
                    crafting.Exit();
                }
                else
                {
                    crafting.Enter();
                }
            }
            _ikDown = false;
        }

    }



    // 일단 참고한 거 대로 만들어 봄
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag); // 왜 안 뜸 열받아
        if (other.tag == "CraftingTable" || other.tag == "Sign")
        {
            _nearObject = other.gameObject;
            // Debug.Log("작업 영역");
        }
    }

    // private void OnCollisionEnter(Collider other)
    // {
    //     Debug.Log(other.tag);
    // }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CraftingTable" || other.tag == "Sign")
        {
            InteractionUI crafting = _nearObject.GetComponent<InteractionUI>();
            crafting.Exit();
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

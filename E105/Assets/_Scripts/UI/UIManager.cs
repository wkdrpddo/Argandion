using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Panel var
    private GameObject _optionpanel;
    private GameObject _optionfrommain;
    private GameObject _mapuipanel;
    private GameObject _createcharacter;
    private GameObject _conversationpanel;
    private GameObject _inventorypanel;
    private GameObject _transactionpanel;
    [SerializeField] private GameObject _cookingpanel;
    private GameObject _craftingpanel;
    private GameObject _buildeventpanel;
    private GameObject _storagepanel;
    private GameObject _trademodal;
    private GameObject _mainpage;
    private GameObject _transactionanimalpanel;
    private GameObject _baseuipanel;
    private GameObject _notificationpanel;

    public GameObject _eventAnnounce;
    public TextMeshProUGUI _announceText;

    private GameObject _nowequip;

    private SystemManager _systemmanager;
    private PlayerSystem _playersystem;
    private Slider _healthbar;
    private Slider _energybar;
    public RectTransform _timer;
    // 저장 데이터
    public Quaternion rotateZero = Quaternion.Euler(new Vector3(0, 0, 0));     // 회전값 기본값 세팅
    public int conversationNPC = 0;
    private int selectCharacter = -1;
    private bool isPressESC = false;
    private bool isGameStart = false;
    private bool isMyHome = false;
    // Start is called before the first frame update
    void Start()
    {
        isGameStart = false;

        // GameObject.Find("SoundManager").GetComponent<SoundManager>()._optionpanel = GameObject.Find("OptionPanel");
        // GameObject.Find("SoundManager").GetComponent<SoundManager>()._optionpanelfrommain = GameObject.Find("OptionPanelFromMainPage");

        _systemmanager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        _playersystem = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();

        _mainpage = GameObject.Find("MainPagePanel");

        _optionpanel = GameObject.Find("OptionPanel");
        _optionpanel.SetActive(false);

        _optionfrommain = GameObject.Find("OptionPanelFromMainPage");
        _optionfrommain.SetActive(false);

        _mapuipanel = GameObject.Find("MapUIPanel");
        _mapuipanel.SetActive(false);

        _createcharacter = GameObject.Find("CreateCharacter");
        _createcharacter.SetActive(false);

        _conversationpanel = GameObject.Find("ConversationPanel");
        _conversationpanel.SetActive(false);

        _inventorypanel = GameObject.Find("InventoryPanel");
        _inventorypanel.SetActive(false);

        _transactionpanel = GameObject.Find("TransactionPanel");
        _transactionpanel.SetActive(false);

        _cookingpanel = GameObject.Find("CookingPanel");
        _cookingpanel.SetActive(false);

        _craftingpanel = GameObject.Find("CraftingPanel");
        _craftingpanel.SetActive(false);

        _storagepanel = GameObject.Find("StoragePanel");
        _storagepanel.SetActive(false);

        _buildeventpanel = GameObject.Find("BuildEventPanel");
        _buildeventpanel.SetActive(false);

        _trademodal = GameObject.Find("TradeModal");
        _trademodal.SetActive(false);

        // _transactionanimalpanel = GameObject.Find("TransactionAnimalPanel");
        // _transactionanimalpanel.SetActive(false);

        // _notificationpanel = GameObject.Find("NotificationPanel");
        // _notificationpanel.SetActive(false);

        _baseuipanel = GameObject.Find("BaseUIPanel");
        _healthbar = GameObject.Find("HealthSlider").GetComponent<Slider>();
        _energybar = GameObject.Find("EnergySlider").GetComponent<Slider>();
        _baseuipanel.SetActive(false);

        _eventAnnounce = GameObject.Find("EventUIAnnounce");
        _announceText = _eventAnnounce.GetComponentInChildren<TextMeshProUGUI>();
        _eventAnnounce.SetActive(false);

        _nowequip = GameObject.Find("NowEquip");
    }

    // Update is called once per frame
    void Update()
    {
        setTimer();

        if (Input.GetButtonDown("optionKey"))
        {
            pressedESC();
        }

        if (Input.GetButtonDown("InventoryKey"))
        {
            if (isGameStart)
            {
                OnInventoryPanel();
            }
        }
        if (Input.GetButtonDown("mapKey"))
        {
            if (isGameStart && !isMyHome)
            {
                OnMapUIPanel();
            }
        }

        // if (Input.GetButtonDown("interactionKey") && _conversationpanel.GetComponent<ConversationPanel>().getIsOn())
        // {
        //     _conversationpanel.GetComponent<ConversationPanel>().secondConversation();
        // }
        // if (Input.GetButtonDown("interactionKey"))
        // {
        //     // test buildEvent
        //     if (_buildeventpanel.GetComponent<BuildEventPanel>().isOnPanel)
        //     {
        //         _buildeventpanel.GetComponent<BuildEventPanel>().closeWindow();
        //     }
        //     else
        //     {
        //         int random = Random.Range(1, 7);
        //         OnBuildEventPanel(random);
        //     }

        //     // // test conversation
        //     // int conversationCnt = _conversationpanel.GetComponent<ConversationPanel>().getConversationCnt();
        //     // if (_conversationpanel.GetComponent<ConversationPanel>().getIsConversation())
        //     // {
        //     //     _conversationpanel.GetComponent<ConversationPanel>().conversation();
        //     // }
        //     // else
        //     // {
        //     //     switch (conversationCnt)
        //     //     {
        //     //         case -1:
        //     //             OnConversationPanel(1);
        //     //             break;
        //     //         case 0:
        //     //             _conversationpanel.GetComponent<ConversationPanel>().secondConversation();
        //     //             break;
        //     //         case 1:
        //     //             _conversationpanel.GetComponent<ConversationPanel>().thirdConversation();
        //     //             break;
        //     //     }
        //     // }
        // }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // test conversation
            int conversationCnt = _conversationpanel.GetComponent<ConversationPanel>().getConversationCnt();
            if (_conversationpanel.GetComponent<ConversationPanel>().getIsConversation())
            {
                _conversationpanel.GetComponent<ConversationPanel>().conversation();
            }
            else
            {
                int randomNum = Random.Range(1, 11);
                // int randomNum = 1;
                switch (conversationCnt)
                {
                    case -1:
                        OnConversationPanel(randomNum);
                        break;
                    case 0:
                        _conversationpanel.GetComponent<ConversationPanel>().secondConversation();
                        break;
                    case 1:
                        _conversationpanel.GetComponent<ConversationPanel>().thirdConversation();
                        break;
                }
            }
        }
    }

    // ======================== UI 호출 함수 Start
    public void OnTransactionPanel()
    {
        _transactionpanel.GetComponent<TransactionPanel>().OnPanel(conversationNPC);
        conversationNPC = 0;
        _conversationpanel.GetComponent<ConversationPanel>().resetConversationPanel();
        stopControllPlayer();
    }

    public void OnTransactionAnimalPanel()
    {
        _transactionanimalpanel.SetActive(true);
    }

    public void OnCraftingPanel()
    {
        stopControllPlayer();
        _craftingpanel.SetActive(true);
    }

    public void OnCookingPanel()
    {
        Debug.Log("쿠킹패널 열기");
        Debug.Log(_cookingpanel);
        stopControllPlayer();
        _cookingpanel.SetActive(true);
    }

    public void OnBuildEventPanel(int value)
    {
        stopControllPlayer();
        _buildeventpanel.GetComponent<BuildEventPanel>().OnPanel(value);
    }

    public void OnStoragePanel()
    {
        stopControllPlayer();
        _storagepanel.SetActive(true);
    }

    public void OnInventoryPanel()
    {
        if (_inventorypanel.activeSelf)
        {
            runControllPlayer();
        }
        else
        {
            stopControllPlayer();
        }
        _inventorypanel.GetComponent<InventoryPanel>().handlePanel();
    }

    public void OnConversationPanel(int value)
    {
        _conversationpanel.GetComponent<ConversationPanel>().setConversationNPC(value);
        stopControllPlayer();
    }

    public void OnCreateCharacter()
    {
        _createcharacter.SetActive(true);
    }

    public void OnMainPagePanel()
    {
        _mainpage.SetActive(true);
        isGameStart = false;
    }

    public void OnMapUIPanel()
    {
        if (isGameStart)
        {
            if (_mapuipanel.activeSelf)
            {
                _mapuipanel.SetActive(false);
            }
            else
            {
                _mapuipanel.SetActive(true);
            }
        }
    }

    public void OnNotificationPanel()
    {
        _notificationpanel.SetActive(true);
    }

    public void OnTradeModal(string name, string iconName, int maxCnt, int checkMod, int cost)
    {
        _trademodal.GetComponent<TradeModal>().setModal(name, iconName, maxCnt, checkMod, cost);
    }

    // ======================= UI 호출 함수 End

    // 캐릭터 선택 관련 함수
    public void setCharacterValue(int value)
    {
        selectCharacter = value;
    }

    public void setHealthBar(float value)
    {
        _healthbar.value = value;
    }

    public void setEnergyBar(float value)
    {
        _energybar.value = value;
    }

    public void setTimer()
    {
        float angle = (_systemmanager._hour_display - 6) * 15 + (_systemmanager._minute_display / 4);
        _timer.rotation = Quaternion.Euler(180, 0, angle);
    }

    public void pressedESC()
    {
        isPressESC = !isPressESC;

        if (isPressESC)
        {
            if (isGameStart)
            {
                _optionpanel.SetActive(true);
            }
            else
            {
                _optionfrommain.SetActive(true);
            }
            _playersystem._canMove = false;
        }
        else
        {
            if (isGameStart)
            {
                _optionpanel.SetActive(false);
            }
            else
            {
                _optionfrommain.SetActive(false);
            }
            _playersystem._canMove = true;
        }
    }

    // 플레이어 조작 정지
    private void stopControllPlayer()
    {
        _playersystem._canMove = false;
    }

    public void runControllPlayer()
    {
        _playersystem._canMove = true;
    }

    // 퀵슬롯 변경 함수
    public void setEquipPointer(int num)
    {
        _nowequip.transform.SetLocalPositionAndRotation(new Vector3((num - 1) * 31 + 2, 0, 0), rotateZero);
    }

    // 집인지 확인하는 코드
    public void setIsHome(bool value)
    {
        isMyHome = value;
        _baseuipanel.transform.GetChild(2).gameObject.SetActive(!value);
    }

    // 소지금 관련
    public int getPlayerGlod()
    {
        return 0;
        // return _systemmanager.getPlayerGold();
    }

    // 게임 시작 종료
    public void setGameState(bool value)
    {
        // _systemmanager.setGameState(value);
        isGameStart = value;
    }

    public bool getGameState()
    {
        // return _systemmanager.getGameState();
        return isGameStart;
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Panel var
    private GameObject _baseuipanel;
    private GameObject _mapuipanel;
    private MainPagePanel _mainpage;
    private OptionPanel _optionpanel;
    private OptionPanel _optionfrommain;

    private CreateCharacter _createcharacter;

    private ConversationPanel _conversationpanel;

    private CraftingPanel _craftingpanel;
    private CookingPanel _cookingpanel;

    private BuildEventPanel _buildeventpanel;

    private TransactionAnimalPanel _transactionanimalpanel;
    private TransactionPanel _transactionpanel;
    private InventoryPanel _inventorypanel;
    private GameObject _storagepanel;
    private TradeModal _trademodal;
    private GameObject _inventory;
    private TextMeshProUGUI _invenMoney;

    private GameObject _notificationpanel;
    private ResultNotificationPanel _resultnotificationpanel;
    private TransactionDoubleCheck _transactiondoublecheck;

    public GameObject _eventAnnounce;
    public TextMeshProUGUI _announceTitle;
    public TextMeshProUGUI _announceText;

    private GameObject _nowequip;

    [SerializeField] private SystemManager _systemmanager;
    private PlayerSystem _playersystem;
    private Item _itemmanager;

    private Slider _healthbar;
    private Slider _energybar;
    public RectTransform _timer;

    // 상태 저장 데이터
    public Quaternion rotateZero = Quaternion.Euler(new Vector3(0, 0, 0));     // 회전값 기본값 세팅
    public int conversationNPC;
    private int selectCharacter;
    private bool isPressESC;
    private bool isMyHome;
    private bool isTransactionOpen;


    private Dictionary<int, Sprite> Dic = new Dictionary<int, Sprite>();

    public Sprite getItemIcon(int key)
    {
        if (Dic.ContainsKey(key))
        {
            return Dic[key];
        }
        Sprite icon = Resources.Load<Sprite>("Sprites/" + key);
        Dic.Add(key, icon);
        return icon;
    }
    // Start is called before the first frame update
    void Start()
    {
        conversationNPC = 0;
        selectCharacter = -1;
        isPressESC = false;
        isMyHome = false;
        isTransactionOpen = false;

        _systemmanager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        _playersystem = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
        _itemmanager = GameObject.Find("ItemManager").GetComponent<Item>();

        _systemmanager.setPlayerGold(245000);

        _baseuipanel = gameObject.transform.Find("BaseUIPanel").gameObject;
        _healthbar = _baseuipanel.transform.GetChild(0).GetComponent<Slider>();
        _energybar = _baseuipanel.transform.GetChild(1).GetComponent<Slider>();
        _nowequip = GameObject.Find("NowEquip").gameObject;
        _baseuipanel.SetActive(false);

        _mapuipanel = GameObject.Find("MapUIPanel");
        _mapuipanel.SetActive(false);

        _mainpage = gameObject.transform.Find("MainPagePanel").GetComponent<MainPagePanel>();

        _optionpanel = gameObject.transform.Find("OptionPanel").GetComponent<OptionPanel>();
        _optionpanel.gameObject.SetActive(false);

        _optionfrommain = gameObject.transform.Find("OptionPanelFromMainPage").GetComponent<OptionPanel>();
        _optionfrommain.gameObject.SetActive(false);

        _createcharacter = gameObject.transform.Find("CreateCharacter").GetComponent<CreateCharacter>();
        _createcharacter.gameObject.SetActive(false);

        _conversationpanel = gameObject.transform.Find("ConversationPanel").GetComponent<ConversationPanel>();
        _conversationpanel.gameObject.SetActive(false);

        _cookingpanel = gameObject.transform.Find("CookingPanel").GetComponent<CookingPanel>();
        _cookingpanel.gameObject.SetActive(false);

        _craftingpanel = gameObject.transform.Find("CraftingPanel").GetComponent<CraftingPanel>();
        _craftingpanel.gameObject.SetActive(false);

        _buildeventpanel = gameObject.transform.Find("BuildEventPanel").GetComponent<BuildEventPanel>();
        _buildeventpanel.gameObject.SetActive(false);

        _transactionanimalpanel = gameObject.transform.Find("TransactionAnimalPanel").GetComponent<TransactionAnimalPanel>();
        _transactionanimalpanel.gameObject.SetActive(false);

        _transactionpanel = gameObject.transform.Find("TransactionPanel").GetComponent<TransactionPanel>();
        _transactionpanel.gameObject.SetActive(false);

        _inventorypanel = gameObject.transform.Find("InventoryPanel").GetComponent<InventoryPanel>();
        _inventorypanel.gameObject.SetActive(false);

        _storagepanel = gameObject.transform.Find("StoragePanel").gameObject;
        _storagepanel.SetActive(false);

        _trademodal = gameObject.transform.Find("TradeModal").GetComponent<TradeModal>();
        _trademodal.gameObject.SetActive(false);

        _inventory = gameObject.transform.Find("Inventory").gameObject;
        _invenMoney = _inventory.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        _invenMoney.text = _systemmanager.getPlayerGold().ToString();
        _inventory.gameObject.SetActive(false);

        _notificationpanel = GameObject.Find("NotificationPanel");
        _notificationpanel.SetActive(false);
        _resultnotificationpanel = gameObject.transform.Find("ResultNotificationPanel").GetComponent<ResultNotificationPanel>();
        _resultnotificationpanel.gameObject.SetActive(false);
        _transactiondoublecheck = gameObject.transform.Find("TransactionDoubleCheckModal").GetComponent<TransactionDoubleCheck>();
        _transactiondoublecheck.gameObject.SetActive(false);

        _eventAnnounce = GameObject.Find("EventUIAnnounce");
        _announceTitle = _eventAnnounce.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        _announceText = _eventAnnounce.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        _eventAnnounce.SetActive(false);


        ItemObject item1 = findItem(2);
        acquireItem(item1, 98);
        ItemObject item2 = findItem(3);
        acquireItem(item2, 99);
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
            if (getGameState())
            {
                OnInventoryPanel();
            }
        }
        if (Input.GetButtonDown("mapKey"))
        {
            if (getGameState() && !isMyHome)
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
        // }

        if (Input.GetKeyDown(KeyCode.V))
        {
            int randNum = Random.Range(1, 7);
            if (randNum == 3 || randNum == 5)
            {
                randNum++;
            }
            if (!_craftingpanel.gameObject.activeSelf)
            {
                OnCraftingPanel(randNum);
            }
            else
            {
                _craftingpanel.GetComponent<CraftingPanel>().closePanel();
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            OnCookingPanel();
        }

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
                // int randomNum = 5;
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

        if (Input.GetKeyDown(KeyCode.O))
        {
            _storagepanel.SetActive(!_storagepanel.activeSelf);
            OnInventory(3);
        }
    }

    // ======================== UI 호출 함수 Start
    public void OnBaseUIPanel()
    {
        _baseuipanel.SetActive(true);
    }
    public void OnTransactionPanel()
    {
        _transactionpanel.GetComponent<TransactionPanel>().OnPanel(conversationNPC);
        _conversationpanel.GetComponent<ConversationPanel>().resetConversationPanel();
        conversationNPC = 0;
        stopControllPlayer();
        OnInventory(2);
    }

    public void OnTransactionAnimalPanel()
    {
        _transactionanimalpanel.GetComponent<TransactionAnimalPanel>().onPanel();
        _conversationpanel.GetComponent<ConversationPanel>().resetConversationPanel();
        conversationNPC = 0;
        stopControllPlayer();
    }

    public void OnCraftingPanel(int value)
    {
        stopControllPlayer();
        _craftingpanel.GetComponent<CraftingPanel>().OnPanel(value);
    }

    public void OnCookingPanel()
    {
        stopControllPlayer();
        _cookingpanel.GetComponent<CookingPanel>().openCooking();
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
        if (_inventorypanel.gameObject.activeSelf)
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
        _createcharacter.gameObject.SetActive(true);
    }

    public void OnMainPagePanel()
    {
        _mainpage.gameObject.SetActive(true);
        setGameState(false);
        // isGameStart = false;
    }

    public void OnMapUIPanel()
    {
        if (getGameState())
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

    public void OnResultNotificationPanel(string text)
    {
        _resultnotificationpanel.GetComponent<ResultNotificationPanel>().handelNoti(text);
    }

    public void OnTransactionDoubleCheckPanel(string name, int store, int itemIdx, int itemCode)
    {
        // Debug.Log("============ " + itemIdx);
        _transactiondoublecheck.setData(name, store, itemIdx, itemCode);
        _transactiondoublecheck.handleModal();
    }

    public void OnTradeModal(string name, string iconName, int maxCnt, int cost, int checkMod, int storeIdx, int itemIdx)
    {
        _trademodal.GetComponent<TradeModal>().setModal(name, iconName, maxCnt, cost, checkMod, storeIdx, itemIdx);
    }

    public void closeTradeModal()
    {
        _trademodal.GetComponent<TradeModal>().closeModal();
    }

    public void OnInventory(int value)
    {
        switch (value)
        {
            case 1:
                _inventory.GetComponent<RectTransform>().SetLocalPositionAndRotation(new Vector3(110, -30, 0), rotateZero);
                break;
            case 2:
                _inventory.GetComponent<RectTransform>().SetLocalPositionAndRotation(new Vector3(110, -15.06f, 0), rotateZero);
                break;
            case 3:
                _inventory.GetComponent<RectTransform>().SetLocalPositionAndRotation(new Vector3(-211.66f, -5.58f, 0), rotateZero);
                _inventory.transform.GetChild(1).GetComponent<Image>().color = new Color(225, 225, 225, 0);
                _inventory.transform.GetChild(1).GetChild(1).GetComponent<Image>().color = new Color(225, 225, 225, 0);
                _inventory.transform.GetChild(1).GetChild(2).GetComponent<Image>().color = new Color(225, 225, 225, 0);
                break;
        }
        _inventory.gameObject.SetActive(!_inventory.gameObject.activeSelf);
    }

    // ======================= UI 호출 함수 End

    // 거래 패널 오픈 여부 함수
    public bool getIsOpenTransaction()
    {
        return isTransactionOpen;
    }

    public void setIsOpenTransaction(bool value)
    {
        isTransactionOpen = value;
    }

    // 캐릭터 선택 관련 함수
    public void setCharacterValue(int value)
    {
        selectCharacter = value;
    }

    // ======================= Base UI 관련 함수
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

    // ======================= Base UI 관련 함수 끝

    // 동물 수 동기화 함수
    public void syncAnimalPanel(int capacity, int sheepCnt, int chickenCnt, int cowCnt)
    {
        _transactionanimalpanel.syncRanchData(capacity, sheepCnt, chickenCnt, cowCnt);
    }

    // ESC 클릭 시 동작
    public void pressedESC()
    {
        isPressESC = !isPressESC;

        if (isPressESC)
        {
            if (getGameState())
            {
                _optionpanel.gameObject.SetActive(true);
            }
            else
            {
                _optionfrommain.gameObject.SetActive(true);
            }
            _playersystem._canMove = false;
        }
        else
        {
            if (getGameState())
            {
                _optionpanel.gameObject.SetActive(false);
            }
            else
            {
                _optionfrommain.gameObject.SetActive(false);
            }
            _playersystem._canMove = true;
        }
    }

    // inventory 접근 함수
    public bool checkInventory(ItemObject _item, int _count)
    {
        Inventory inven = _inventory.transform.GetChild(1).GetComponent<Inventory>();
        return inven.CheckInven(_item, _count);
    }

    public void acquireItem(ItemObject _item, int _count)
    {
        _inventory.transform.GetChild(1).GetComponent<Inventory>().AcquireItem(_item, _count);
    }

    public void reductItem(ItemObject _item, int _count)
    {
        _inventory.transform.GetChild(1).GetComponent<Inventory>().ReductItem(_item, _count);
    }

    public void sellItem(int slotIdx, int _count)
    {
        _inventory.transform.GetChild(1).GetComponent<Inventory>().SellInventoryItem(slotIdx, _count);
    }

    public void onSlotOverModal(string _text, Vector3 _position)
    {
        _inventory.transform.GetChild(3).gameObject.SetActive(true);
        _inventory.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = _text;
        _inventory.transform.GetChild(3).transform.position = _position;
    }

    public void offSlotOverModal()
    {
        _inventory.transform.GetChild(3).gameObject.SetActive(false);
    }

    public Slot[] getInventorySlots()
    {
        return _inventory.transform.GetChild(1).GetComponent<Inventory>().getInventorySlots();
    }

    // 제작관련 함수
    public CraftingPanel getCraftPanel()
    {
        return _craftingpanel;
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

    // item 관련 함수
    public ItemObject findItem(int value)
    {
        return _itemmanager.FindItem(value);
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
    public int getPlayerGold()
    {
        int gold = _systemmanager.getPlayerGold();
        _invenMoney.text = gold.ToString();
        return gold;
    }

    public void addPlayerGold(int value)
    {
        _systemmanager.addPlayerGold(value);
        _invenMoney.text = getPlayerGold().ToString();
    }

    // 게임 시작 종료
    public void setGameState(bool value)
    {
        _systemmanager.setGameState(value);
        // isGameStart = value;
    }

    public bool getGameState()
    {
        return _systemmanager.getGameState();
        // return isGameStart;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Panel var
    [SerializeField] private GameObject _baseuipanel;
    [SerializeField] private GameObject _mapuipanel;
    [SerializeField] private MainPagePanel _mainpage;
    [SerializeField] private OptionPanel _optionpanel;
    [SerializeField] private OptionPanel _optionfrommain;

    [SerializeField] private CreateCharacter _createcharacter;

    [SerializeField] private ConversationPanel _conversationpanel;

    [SerializeField] private CraftingPanel _craftingpanel;
    [SerializeField] private CookingPanel _cookingpanel;

    [SerializeField] private BuildEventPanel _buildeventpanel;

    [SerializeField] private TransactionAnimalPanel _transactionanimalpanel;
    [SerializeField] private TransactionPanel _transactionpanel;
    [SerializeField] private InventoryPanel _inventorypanel;
    [SerializeField] private GameObject _storagepanel;
    [SerializeField] private TradeModal _trademodal;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private TextMeshProUGUI _invenMoney;

    [SerializeField] private GameObject _notificationpanel;
    [SerializeField] private ResultNotificationPanel _resultnotificationpanel;
    [SerializeField] private TransactionDoubleCheck _transactiondoublecheck;

    public GameObject _eventAnnounce;
    public TextMeshProUGUI _announceTitle;
    public TextMeshProUGUI _announceText;

    private GameObject _nowequip;

    [SerializeField] private SystemManager _systemmanager;
    [SerializeField] private PlayerSystem _playersystem;
    [SerializeField] private Item _itemmanager;
    [SerializeField] private WorldTree _worldtree;
    [SerializeField] private TeleportAltar _alterdown;
    [SerializeField] private TeleportAltar _alterup;
    [SerializeField] private Altar _alter;

    private Slider _healthbar;
    private Slider _energybar;
    public RectTransform _timer;
    public GameObject _daytime;

    // 상태 저장 데이터
    public Quaternion rotateZero = Quaternion.Euler(new Vector3(0, 0, 0));     // 회전값 기본값 세팅

    public int conversationNPC;
    private int selectCharacter;
    private bool isPressESC;
    private bool isMyHome;

    // 패널 오픈 여부 변수
    [SerializeField] private bool isTransactionOpen;
    [SerializeField] private bool isInventoryOpen;
    [SerializeField] private bool isInvenRightModal;
    [SerializeField] private bool isStorageOpen;
    private bool isCraftOpen;
    private bool isCookOpen;
    private bool isBuildEventOpen;
    private bool isMapOpen;
    private bool isConversationOpen;

    // 주연 추가
    public GameObject _eventpanel;
    // private EventManager _eventmanager;
    public FoodManager _foodmanager;

    // Sprite 이미지 저장 Map
    private Dictionary<int, Sprite> Dic = new Dictionary<int, Sprite>();
    // Sprite 탐색 해서 저장하는 함수
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
        selectCharacter = 0;
        isPressESC = false;
        isMyHome = false;
        isTransactionOpen = false;
        isInventoryOpen = false;
        isStorageOpen = false;
        isCraftOpen = false;
        isCookOpen = false;
        isBuildEventOpen = false;
        isMapOpen = false;
        isConversationOpen = false;

        _systemmanager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        _playersystem = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
        _itemmanager = GameObject.Find("ItemManager").GetComponent<Item>();
        _foodmanager = GameObject.Find("FoodManager").GetComponent<FoodManager>();
        _worldtree = GameObject.Find("WorldTree").GetComponent<WorldTree>();
        _alterdown = GameObject.Find("teleportDown").GetComponent<TeleportAltar>();
        _alterup = GameObject.Find("teleportUp").GetComponent<TeleportAltar>();
        // _alter = GameObject.Find("Altar").GetComponent<Altar>();


        _systemmanager.setPlayerGold(9999999);

        _baseuipanel = gameObject.transform.Find("BaseUIPanel").gameObject;
        _healthbar = _baseuipanel.transform.GetChild(0).GetComponent<Slider>();
        _energybar = _baseuipanel.transform.GetChild(1).GetComponent<Slider>();
        _eventpanel = _baseuipanel.transform.GetChild(4).gameObject;
        _eventpanel.GetComponent<EventPanel>().setting();
        _daytime = _baseuipanel.transform.GetChild(2).GetChild(1).gameObject;
        _foodmanager._eventPanel = _eventpanel.GetComponent<EventPanel>();

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

        // 꽃
        ItemObject item1 = findItem(50);
        acquireItem(item1, 10);
        ItemObject item2 = findItem(51);
        acquireItem(item2, 10);
        ItemObject item3 = findItem(52);
        acquireItem(item3, 10);
        ItemObject item4 = findItem(53);
        acquireItem(item4, 10);
        ItemObject item5 = findItem(54);
        acquireItem(item5, 10);
        ItemObject item10 = findItem(55);
        acquireItem(item10, 10);
        ItemObject item11 = findItem(56);
        acquireItem(item11, 10);
        // 그 외 재료
        ItemObject item6 = findItem(1);
        acquireItem(item6, 40);
        ItemObject item7 = findItem(2);
        acquireItem(item7, 27);
        ItemObject item8 = findItem(3);
        acquireItem(item8, 27);
        ItemObject item9 = findItem(4);
        acquireItem(item9, 99);
        acquireItem(item9, 71);

        ItemObject item12 = findItem(10);
        acquireItem(item12, 99);
        acquireItem(item12, 41);
        ItemObject item13 = findItem(11);
        acquireItem(item13, 10);
        ItemObject item14 = findItem(12);
        acquireItem(item14, 40);
        ItemObject item15 = findItem(13);
        acquireItem(item15, 2);
        ItemObject item16 = findItem(14);
        acquireItem(item16, 5);

        ItemObject item17 = findItem(20);
        acquireItem(item17, 40);
        ItemObject item18 = findItem(21);
        acquireItem(item18, 15);
        ItemObject item19 = findItem(22);
        acquireItem(item19, 35);

        ItemObject item20 = findItem(104);
        acquireItem(item20, 20);
        ItemObject item21 = findItem(112);
        acquireItem(item21, 25);
        ItemObject item22 = findItem(505);
        acquireItem(item22, 60);

    }

    // Update is called once per frame
    void Update()
    {
        setTimer();

        if (Input.GetButtonDown("optionKey"))
        {
            pressedESC();
        }

        // 다른 키 다 X, inventory key만 동작
        if ((!isPanelOpen() && Input.GetButtonDown("InventoryKey")) || (isInventoryOpen && Input.GetButtonDown("InventoryKey")))
        {
            if (getGameState())
            {
                OnInventoryPanel();
            }
        }

        // 다른 키 다 X, map key만 동작
        if ((!isPanelOpen() && Input.GetButtonDown("mapKey")) || (isMapOpen && Input.GetButtonDown("mapKey")))
        {
            if (getGameState() && !isMyHome)
            {
                OnMapUIPanel();
            }
        }

        if (Input.GetButtonDown("interactionKey") && isInvenRightModal)
        {
            closeInvenRightClickModal();
        }

        if (Input.GetButtonDown("interactionKey"))
        {
            if (isConversationOpen)
            {
                int conversationCnt = _conversationpanel.GetComponent<ConversationPanel>().getConversationCnt();
                if (_conversationpanel.GetComponent<ConversationPanel>().getIsConversation())
                {
                    _conversationpanel.GetComponent<ConversationPanel>().conversation();
                }
                else
                {
                    switch (conversationCnt)
                    {
                        case 0:
                            if (conversationNPC == 9)
                            {
                                break;
                            }
                            _conversationpanel.GetComponent<ConversationPanel>().secondConversation();
                            break;
                        case 1:
                            _conversationpanel.GetComponent<ConversationPanel>().thirdConversation();
                            break;
                    }
                }

            }
        }
    }

    // ======================== UI 호출 함수 Start

    public void OnBaseUIPanel()
    {
        _baseuipanel.SetActive(true);
    }

    public void OnTransactionPanel()
    {
        _conversationpanel.GetComponent<ConversationPanel>().resetConversationPanel();
        _transactionpanel.GetComponent<TransactionPanel>().handelPanel(conversationNPC);
    }

    public void OnTransactionAnimalPanel()
    {
        _conversationpanel.GetComponent<ConversationPanel>().resetConversationPanel();
        _transactionanimalpanel.GetComponent<TransactionAnimalPanel>().handelPanel();
    }

    public void OnCraftingPanel(int value)
    {
        _craftingpanel.GetComponent<CraftingPanel>().handelPanel(value);
    }

    public void OnCookingPanel()
    {
        // Debug.Log("온쿠킹");
        _cookingpanel.GetComponent<CookingPanel>().handelPanel();
    }

    public void OnBuildEventPanel(int value)
    {
        int step = _systemmanager.getPuriCount();
        _buildeventpanel.GetComponent<BuildEventPanel>().handelPanel(value, step);
    }

    public void OnStoragePanel()
    {
        _storagepanel.GetComponent<StoragePanel>().handlePanel();
    }

    public void OnInventoryPanel()
    {
        _inventorypanel.GetComponent<InventoryPanel>().handlePanel();

        if (_inventorypanel.gameObject.activeSelf)
        {
            stopControllKeys();
        }
        else
        {
            runControllKeys();
        }
    }

    public void OnConversationPanel(int value)
    {
        _conversationpanel.GetComponent<ConversationPanel>().setConversationNPC(value);
        stopControllKeys();
    }

    public void OnCreateCharacter()
    {
        _createcharacter.gameObject.SetActive(true);
    }

    public void OnMainPagePanel()
    {
        _mainpage.gameObject.SetActive(true);
        setGameState(false);
    }

    public void OnMapUIPanel()
    {
        if (getGameState())
        {
            _mapuipanel.SetActive(!_mapuipanel.activeSelf);
        }
    }

    public void OnNotificationPanel()
    {
        _notificationpanel.SetActive(!_notificationpanel.activeSelf);
        if (_notificationpanel.activeSelf)
        {
            stopControllKeys();
        }
        else
        {
            runControllKeys();
        }
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
        int _storeKey = storeIdx;
        if (_storeKey == -1)
        {
            _storeKey = conversationNPC;
        }
        // Debug.Log(_storeKey);
        _trademodal.GetComponent<TradeModal>().setModal(name, iconName, maxCnt, cost, checkMod, _storeKey, itemIdx);
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

    // 패널 오픈 여부 함수
    public bool getIsOpenTransaction()
    {
        return isTransactionOpen;
    }

    public void setIsOpenTransaction(bool value)
    {
        isTransactionOpen = value;
    }

    public bool getIsOpenInventory()
    {
        return isInventoryOpen;
    }

    public void setIsOpenInventory(bool value)
    {
        isInventoryOpen = value;
    }

    public bool getIsOpenStorage()
    {
        return isStorageOpen;
    }

    public void setIsOpenStorage(bool value)
    {
        isStorageOpen = value;
    }

    public bool getIsOpenConversation()
    {
        return isConversationOpen;
    }

    public void setIsOpenConversation(bool value)
    {
        isConversationOpen = value;
    }

    public bool getIsOpenCraft()
    {
        return isCraftOpen;
    }

    public void setIsOpenCraft(bool value)
    {
        isCraftOpen = value;
    }

    public bool getIsOpenCook()
    {
        return isCookOpen;
    }

    public void setIsOpenCook(bool value)
    {
        isCookOpen = value;
    }

    public bool getIsOpenBuildEvent()
    {
        return isBuildEventOpen;
    }

    public void setIsOpenBuildEvent(bool value)
    {
        isBuildEventOpen = value;
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
            stopControllKeys();
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
            runControllKeys();
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

    public void sellItem(int slotIdx, int _count, int _key)
    {
        if (_key == 1)
        {
            _inventory.transform.GetChild(1).GetComponent<Inventory>().SellInventoryItem(slotIdx, _count);
        }
        else if (_key == 2)
        {
            _inventory.transform.GetChild(0).GetComponent<Quickslot>().SellQuickslotItem(slotIdx, _count);
        }
        else if (_key == 3)
        {
            switch (slotIdx)
            {
                case 0:
                    _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Slot>().SetSlotCount(-1);
                    break;
                case 1:
                    _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Slot>().SetSlotCount(-1);
                    break;
                case 2:
                    _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(3).GetComponent<Slot>().SetSlotCount(-1);
                    break;
                case 3:
                    _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(4).GetComponent<Slot>().SetSlotCount(-1);
                    break;
            }
        }
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

    // 인벤 마우스 우클릭
    public void clickRightSlotModal(int _key, Vector3 _position, ItemObject _item, int _count, int _index)
    {
        isInvenRightModal = true;
        Debug.LogWarning("마우스 우클릭 모달 호출");
        _inventory.transform.GetChild(4).gameObject.SetActive(true);
        _inventory.transform.GetChild(4).transform.position = _position;
        switch (_key)
        {
            case 1:
                _inventory.transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
                _inventory.transform.GetChild(4).GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                _inventory.transform.GetChild(4).GetChild(0).GetComponent<Button>().onClick.AddListener(() => rightEquip(_item, _count, _index));
                break;
            case 2:
                _inventory.transform.GetChild(4).GetChild(2).gameObject.SetActive(true);
                _inventory.transform.GetChild(4).GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
                _inventory.transform.GetChild(4).GetChild(2).GetComponent<Button>().onClick.AddListener(() => rightQuick(_item, _count, _index));
                break;
            case 3:
                _inventory.transform.GetChild(4).GetChild(1).gameObject.SetActive(true);
                _inventory.transform.GetChild(4).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                _inventory.transform.GetChild(4).GetChild(1).GetComponent<Button>().onClick.AddListener(() => rightUse(_item, _count, _index));
                break;
            case 4:
                _inventory.transform.GetChild(4).GetChild(3).gameObject.SetActive(true);
                _inventory.transform.GetChild(4).GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
                _inventory.transform.GetChild(4).GetChild(3).GetComponent<Button>().onClick.AddListener(() => rightDismiss(_item, _count, _index));
                break;

        }
    }

    // 인벤 우클릭 모달 close
    public void closeInvenRightClickModal()
    {
        isInvenRightModal = false;
        _inventory.transform.GetChild(4).gameObject.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            _inventory.transform.GetChild(4).GetChild(i).gameObject.SetActive(false);
        }
    }

    // 인벤토리 아이템 처리 - 우클릭
    public void rightEquip(ItemObject _item, int _count, int invenIdx)
    {
        // Debug.Log("여기 몇 번 동작해?");
        ItemObject itemObj = null;
        int equiptCnt = -1;
        switch (_item.ItemCode)
        {
            case 400:
                equiptCnt = _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Slot>().getSlotItemCount();
                if (equiptCnt > 0)
                {
                    itemObj = _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Slot>().getSlotItemData();
                }

                _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Slot>().AddItem(_item);
                sellItem(invenIdx, _count, 1);
                setPlayerEquipSlot(_item.ItemCode, 0);
                break;
            case 401:
                equiptCnt = _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Slot>().getSlotItemCount();
                if (equiptCnt > 0)
                {
                    itemObj = _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Slot>().getSlotItemData();
                }

                _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Slot>().AddItem(_item);
                sellItem(invenIdx, _count, 1);
                setPlayerEquipSlot(_item.ItemCode, 1);
                break;
            case 402:
                equiptCnt = _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(3).GetComponent<Slot>().getSlotItemCount();
                if (equiptCnt > 0)
                {
                    itemObj = _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(3).GetComponent<Slot>().getSlotItemData();
                }

                _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(3).GetComponent<Slot>().AddItem(_item);
                sellItem(invenIdx, _count, 1);
                setPlayerEquipSlot(_item.ItemCode, 2);
                break;
            case 403:
            case 404:
                equiptCnt = _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(4).GetComponent<Slot>().getSlotItemCount();
                if (equiptCnt > 0)
                {
                    itemObj = _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(4).GetComponent<Slot>().getSlotItemData();
                }

                _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(4).GetComponent<Slot>().AddItem(_item);
                sellItem(invenIdx, _count, 1);
                setPlayerEquipSlot(_item.ItemCode, 3);
                break;
            case 502:
            case 503:
            case 504:
                int _cnt = _inventory.transform.GetChild(1).GetComponent<Inventory>().getLessBaitCount(_item.ItemCode);
                _baseuipanel.transform.GetChild(3).GetChild(1).GetComponentInChildren<Slot>().AddItem(_item, _cnt);
                setPlayerQuickSlot(7, _item.ItemCode, _cnt);
                break;
            default:
                break;
        }

        if (itemObj != null)
        {
            acquireItem(itemObj, 1);
        }

        closeInvenRightClickModal();
    }

    public void rightQuick(ItemObject _item, int _count, int _index)
    {
        if (_inventory.transform.GetChild(0).GetComponent<Quickslot>().CheckInven(_item, _count))
        {
            _inventory.transform.GetChild(0).GetComponent<Quickslot>().AcquireItem(_item, _count);
            sellItem(_index, _count, 1);
        }
        else
        {
            OnResultNotificationPanel("퀵슬롯에 빈 공간이 없습니다.");
        }
        closeInvenRightClickModal();
    }

    public void rightDismiss(ItemObject _item, int _count, int _index)
    {
        if (checkInventory(_item, _count))
        {
            if (_item.Category == "옷")
            {
                sellItem(_index, _count, 3);
            }
            else
            {
                sellItem(_index, _count, 2);
            }
            acquireItem(_item, _count);
        }
        closeInvenRightClickModal();
    }

    public void rightUse(ItemObject _item, int _count, int _index)
    {
        Debug.Log(_item.ItemCode);
        _foodmanager.UseFood(_item.ItemCode);
        sellItem(_index, 1, 1);
        closeInvenRightClickModal();
    }

    public void quickUse(int _itemCode, int _count, int _slotIdx)
    {
        if (findItem(_itemCode).Category == "식량")
        {
            _foodmanager.UseFood(_itemCode);
        }

        if (_slotIdx == 8)
        {
            reductItem(findItem(_itemCode), -1 * _count);
        }
        else
        {
            sellItem(_slotIdx, _count, 2);
        }
    }

    public Slot[] getInventorySlots()
    {
        return _inventory.transform.GetChild(1).GetComponent<Inventory>().getInventorySlots();
    }

    // 창고 관련
    public void addToStorage(ItemObject _item, int _count, int slotIdx)
    {
        int value = _storagepanel.transform.GetChild(1).GetChild(4).GetComponent<Storage>().checkStorage(_item, _count);
        if (value <= 0)
        {
            _storagepanel.transform.GetChild(1).GetChild(4).GetComponent<Storage>().AcquireItem(_item, _count);
            _inventory.transform.GetChild(1).GetComponent<Inventory>().SellInventoryItem(slotIdx, _count);
        }
        else if (value == _count)
        {
            OnResultNotificationPanel("창고에 " + _item.Name + "이(가) 가득 찼습니다.");
        }
        else
        {
            _storagepanel.transform.GetChild(1).GetChild(4).GetComponent<Storage>().AcquireItem(_item, _count - value);
            _inventory.transform.GetChild(1).GetComponent<Inventory>().SellInventoryItem(slotIdx, _count - value);
        }
    }

    public void removeToStorage(ItemObject _item, int _count, int slotIdx)
    {
        int nowCnt = _count;
        while (nowCnt > 99)
        {
            if (checkInventory(_item, 99))
            {
                acquireItem(_item, 99);
                _storagepanel.transform.GetChild(1).GetChild(4).GetComponent<Storage>().AcquireItem(_item, -99);
                nowCnt -= 99;
            }
        }
        if (checkInventory(_item, nowCnt))
        {
            acquireItem(_item, nowCnt);
            _storagepanel.transform.GetChild(1).GetChild(4).GetComponent<Storage>().AcquireItem(_item, -1 * nowCnt);
        }
    }

    // 제작관련 함수
    public CraftingPanel getCraftPanel()
    {
        return _craftingpanel;
    }

    // 플레이어 함수 관련
    public void stopControllPlayer()
    {
        _playersystem._canMove = false;
    }

    public void runControllPlayer()
    {
        _playersystem._canMove = true;
    }

    public void runCookingAnimation()
    {
        Debug.LogWarning("======== ui cooking call ========");
        // _playersystem._playerAnimator.SetInteger("action", 6);
        _playersystem.setAnimator(6, 5.0f);
        Debug.LogWarning(_playersystem._playerAnimator);
        Debug.LogWarning(_playersystem._playerAnimator.GetInteger("action"));
    }

    // input 관련 함수
    public void toggleCanInteract()
    {
        _playersystem.toggleCanInteract();
    }

    public void toggleCanAction()
    {
        _playersystem.toggleCanAction();
    }

    public void setCanInteract(bool value)
    {
        _playersystem.setCanInteract(value);
    }

    public void setCanAction(bool value)
    {
        _playersystem.setCanAction(value);
    }

    public void stopControllKeys()
    {
        stopControllPlayer();
        setCanAction(false);
        setCanInteract(false);
    }

    public void runControllKeys()
    {
        runControllPlayer();
        setCanAction(true);
        setCanInteract(true);
    }

    public bool isPanelOpen()
    {
        return isInventoryOpen || isMapOpen || isStorageOpen || isTransactionOpen ||
                    isCraftOpen || isCookOpen || isBuildEventOpen || isConversationOpen;
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

    // 퀵슬롯 동기 함수
    public void syncQuickSlot()
    {
        Slot[] baseuiQuick = _baseuipanel.transform.GetChild(3).GetChild(0).GetChild(0).GetComponentsInChildren<Slot>();
        Slot[] quickSlotData = _inventory.transform.GetChild(0).GetComponent<Quickslot>().getInventorySlots();

        for (int i = 0; i < 7; i++)
        {
            if (quickSlotData[i].itemCount <= 0)
            {
                baseuiQuick[i].SetSlotCount(-1 * baseuiQuick[i].itemCount);
            }
            else
            {
                baseuiQuick[i].AddItem(quickSlotData[i].item, quickSlotData[i].itemCount);
            }
        }
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

    // 플레이어 선택
    public void selectPlayer()
    {
        _playersystem.ChangePlayerCharacter(selectCharacter);
    }

    public void setPlayerName(string _name)
    {
        _playersystem.setPlayerName(_name);
    }


    // 플레이어 체력 / 기력
    public void healPlayer()
    {
        _playersystem.changeHealth(-10000);
        _playersystem.changeEnergy(-10000);
        _conversationpanel.selectHeal();
    }

    // 플레이어 장비 관련
    public void setPlayerQuickSlot(int index, int itemCode, int count)
    {
        _playersystem.setQuickItem(index, itemCode, count);
    }

    public void setPlayerEquipSlot(int itemCode, int idx)
    {
        _playersystem.setEquipItem(itemCode, idx);
    }

    // 사운드 관련
    public void playRandomBGM()
    {
        // GameObject.Find("SoundManager").GetComponent<SoundManager>().playRandom();
    }
    // public void BGMChanger(string _bgmName)
    // {
    //     _conversationpanel.selectMusic(_bgmName);
    // }

    // 정령 버프 및 제단 관련 코드
    public void prayToSpirit(int _flowerCode)
    {
        if (_systemmanager._buffManager._isFlowerBuffActived || _systemmanager._buffManager._isPrayBuffActived)
        {
            _conversationpanel.conversationWhenAlterBuff(0);
        }
        else
        {
            _conversationpanel.conversationWhenAlterBuff(_flowerCode);
        }
    }

    public void prayToAltar(int _nowFlowerCode, int _newFlowerCode, int quickIdx)
    {
        int nowCode = _nowFlowerCode;
        if (_systemmanager._buffManager._isPrayBuffActived)
        {
            nowCode = -1;
        }
        _conversationpanel.selectWhenAlterPray(nowCode, _newFlowerCode, quickIdx);
    }

    public void callSpiritBuff(int _flower)
    {
        // _systemmanager._SpiritBuff.Spirit(findItem(_flower));
    }

    public void callPrayBuff(int _flower)
    {
        // _alter.goPray(_flower);
    }

    // 제사 몇 일 째인지 얻어오는 함수
    public int getNowPrayDate()
    {
        return _systemmanager._PrayBuff.prayDay;
    }

    // 세계수 텔포
    public void doTeleport(int value)
    {
        _worldtree.doTeleport(value);
        _conversationpanel.resetConversationPanel();
    }

    // 제단 텔포
    public void upTeleport()
    {
        _alterdown.goUp();
    }

    public void downTeleport()
    {
        _alterup.goDown();
    }

    // 게임 시작 종료
    public void setGameState(bool value)
    {
        _systemmanager.setGameState(value);
    }

    public bool getGameState()
    {
        return _systemmanager.getGameState();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    // 아이템 정보 Load
    // 0 : 인벤 || 1 : 퀵슬롯 || 2. 장비 || 3. 창고
    public void loadItemData(int[,] data, int _key)
    {
        Slot[] slots = null;
        switch (_key)
        {
            case 0:
                slots = _inventory.transform.GetChild(1).GetComponent<Inventory>().getInventorySlots();
                break;
            case 1:
                slots = _inventory.transform.GetChild(0).GetComponent<Quickslot>().getInventorySlots();
                break;
            case 2:
                slots = _inventorypanel.transform.GetChild(0).GetChild(2).GetComponentsInChildren<Slot>();
                break;
            case 3:
                slots = _storagepanel.transform.GetChild(1).GetChild(4).GetComponent<Storage>().getInventorySlots();
                break;
        }

        for (int i = 0; i < data.Length; i++)
        {
            if (data[i, 0] == 0)
            {
                continue;
            }

            if (_key == 1 && i == 7)
            {
                _baseuipanel.transform.GetChild(3).GetChild(1).GetComponentInChildren<Slot>().AddItem(findItem(data[i, 0]), data[i, 1]);
                continue;
            }

            slots[i].AddItem(findItem(data[i, 0]), data[i, 1]);
        }

        if (_key == 1)
        {
            syncQuickSlot();
        }
    }

    public void DayStart()
    {
        Transform trans = _daytime.GetComponent<RectTransform>().transform;
        Image img = _daytime.GetComponent<Image>();
        if (_systemmanager._buffManager.whitePray || _systemmanager._buffManager.whiteSpirit)
        {
            trans.SetLocalPositionAndRotation(trans.localPosition, Quaternion.Euler(180, 180, 15));
            img.fillAmount = 0.83333f;
        }
        else
        {
            trans.SetLocalPositionAndRotation(trans.localPosition, Quaternion.Euler(180, 180, 0));
            img.fillAmount = 0.70833f;
        }
    }
}

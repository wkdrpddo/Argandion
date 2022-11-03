using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Panel var
    public GameObject _optionpanel;
    public GameObject _optionfrommain;
    public GameObject _mapuipanel;
    public GameObject _createcharacter;
    public GameObject _conversationpanel;
    public GameObject _inventorypanel;
    public GameObject _transactionpanel;
    public GameObject _cookingpanel;
    public GameObject _craftingpanel;
    public GameObject _buildeventpanel;
    public GameObject _storagepanel;
    public GameObject _trademodal;
    public GameObject _mainpage;


    // 저장 데이터
    public int conversationNPC = 0;
    public Slider _healthbar;
    public Slider _energybar;
    public SystemManager _systemmanager;
    public RectTransform _timer;
    private int selectCharacter = -1;
    private bool isPressESC = false;
    public PlayerSystem _playersystem;
    public GameObject _eventAnnounce;
    public TextMeshProUGUI _announceText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        setTimer();

        if (Input.GetButtonDown("optionKey"))
        {
            pressedESC();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            _inventorypanel.SetActive(true);
        }
    }

    // ======================== UI 호출 함수 Start
    public void OnTransactionPanel()
    {
        _transactionpanel.SetActive(true);
    }

    public void OnCraftingPanel()
    {
        _craftingpanel.SetActive(true);
    }

    public void OnCookingPanel()
    {
        _cookingpanel.SetActive(true);
    }

    public void OnBuildEventPanel()
    {
        _buildeventpanel.SetActive(true);
    }

    public void OnStoragePanel()
    {
        _storagepanel.SetActive(true);
    }

    public void OnInventoryPanel()
    {
        stopControllPlayer();
        _inventorypanel.SetActive(true);
    }

    public void OnConversationPanel(int value)
    {
        switch (_conversationpanel.GetComponent<ConversationPanel>().conversationCount)
        {
            case -1:
                _conversationpanel.GetComponent<ConversationPanel>().setConversationNPC(value);
                _conversationpanel.SetActive(true);
                break;
            case 0:
                _conversationpanel.GetComponent<ConversationPanel>().secondConversation();
                break;
        }
    }

    public void OnCreateCharacter()
    {
        _createcharacter.SetActive(true);
    }

    public void OnMainPagePanel()
    {
        _mainpage.SetActive(true);
        _mainpage.GetComponent<MainPagePanel>().setIsGameStart(false);
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
            if (_mainpage.GetComponent<MainPagePanel>().getIsGameStart())
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
            if (_mainpage.GetComponent<MainPagePanel>().getIsGameStart())
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

    // 게임 종료
    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}

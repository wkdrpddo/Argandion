using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConversationPanel : MonoBehaviour
{
    /*
        1. 재단사 [포목점]
        2. 목수 [공방]
        3. 호수지기 [호수]
        4. 대장장이 [대장간]
        5. 목장지기
        6. 사냥꾼 [사냥꾼 오두막]
        7. 음악가
        8. 순례자
        9. 세계수
        10. 세계수의 정령
        11. 제단 텔포 (아래서 제단으로)
        12. 제단 텔포 (제단에서 아래로)
    */
    [SerializeField] private TextMeshProUGUI _npcname;
    [SerializeField] private TextMeshProUGUI _nomaltalk;
    [SerializeField] private GameObject _selectpanel;
    [SerializeField] private int conversationCount;        // -1 : 대화 전, 0 : 대화 시작, 1 : 마지막 대화

    [SerializeField] public GameObject conversationButton;
    // [SerializeField] private UIManager ui;
    // 선택창 '대화' 선택 시 관련 변수
    private bool isConversation;
    private int selectConversationCount;
    // 선택창 '도움말' 선택 시 관련 변수
    private bool isInformation;
    private int informationCount;
    private int isSpirit;

    // 변수 get 함수
    public int getConversationCnt()
    {
        return conversationCount;
    }

    public bool getIsConversation()
    {
        return isConversation;
    }

    public bool getIsInformation()
    {
        return isInformation;
    }

    // Start is called before the first frame update
    void Awake()
    {
        // ui = gameObject.GetComponentInParent<UIManager>();
        _npcname = transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        _nomaltalk = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        conversationCount = -1;

        _selectpanel = transform.GetChild(0).GetChild(0).gameObject;

        isConversation = false;
        selectConversationCount = 0;
        isInformation = false;
        informationCount = 0;
        isSpirit = -1;
    }

    // NPC 및 상호작용 대화 최초 시작
    public void setConversationNPC(int value)
    {
        UIManager._uimanagerInstance.conversationNPC = value;
        // ui.conversationNPC = value;

        int randCnt = Random.Range(0, 3);
        conversationCount++;
        if (value != 9 && value != 11 && value != 12)
        {
            _nomaltalk.text = conversations[value - 1, randCnt];
        }
        _npcname.gameObject.transform.parent.gameObject.SetActive(true);
        switch (value)
        {
            case 1:
                _npcname.text = "재단사";
                break;
            case 2:
                _npcname.text = "목수";
                break;
            case 3:
                _npcname.text = "호수지기";
                break;
            case 4:
                _npcname.text = "대장장이";
                break;
            case 5:
                _npcname.text = "목장지기";
                break;
            case 6:
                _npcname.text = "사냥꾼";
                break;
            case 7:
                _npcname.text = "음악가";
                break;
            case 8:
                _npcname.text = "순례자";
                break;
            case 10:
                _npcname.text = "세계수의 정령";
                break;
            case 9:
                _npcname.text = "세계수";
                conversationCount++;
                break;
            case 11:
            case 12:
                // NPC가 아닌 상호작용이므로 이름이 필요 없음
                _npcname.gameObject.transform.parent.gameObject.SetActive(false);
                conversationCount++;
                conversationCount++;
                _selectpanel.SetActive(true);
                break;
        }
        Debug.Log("초기대화 시작");

        UIManager._uimanagerInstance.setIsOpenConversation(true);
        // ui.setIsOpenConversation(true);
        setSelectList();
        gameObject.SetActive(true);
    }

    // NPC별 선택지 표시
    private void setSelectList()
    {
        GameObject talkBtn = Instantiate(conversationButton, _selectpanel.transform);
        RectTransform talkBtnRect = talkBtn.GetComponent<RectTransform>();
        talkBtnRect.SetLocalPositionAndRotation(new Vector3(0, 55, 0), UIManager._uimanagerInstance.rotateZero);
        talkBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "대화";
        talkBtn.GetComponent<Button>().onClick.AddListener(selectConversation);

        GameObject helpBtn = Instantiate(conversationButton, _selectpanel.transform);
        RectTransform helpBtnRect = helpBtn.GetComponent<RectTransform>();
        helpBtnRect.SetLocalPositionAndRotation(new Vector3(0, 22, 0), UIManager._uimanagerInstance.rotateZero);
        helpBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "도움말";
        helpBtn.GetComponent<Button>().onClick.AddListener(() => selectInfo(-1));

        int npcNumber = UIManager._uimanagerInstance.conversationNPC;
        // int npcNumber = ui.conversationNPC;
        if (npcNumber == 1 || npcNumber == 7 || npcNumber == 8)
        {
            Destroy(helpBtn.gameObject);
        }

        switch (npcNumber)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                // case 6:
                GameObject tradeBtn = Instantiate(conversationButton, _selectpanel.transform);
                RectTransform tradeBtnRect = tradeBtn.GetComponent<RectTransform>();
                tradeBtnRect.SetLocalPositionAndRotation(new Vector3(0, -11, 0), UIManager._uimanagerInstance.rotateZero);
                tradeBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "거래";

                if (npcNumber == 1)
                {
                    tradeBtnRect.SetLocalPositionAndRotation(new Vector3(0, 22, 0), UIManager._uimanagerInstance.rotateZero);
                }

                if (npcNumber == 5)
                {
                    tradeBtn.GetComponent<Button>().onClick.AddListener(UIManager._uimanagerInstance.OnTransactionAnimalPanel);
                }
                else
                {
                    tradeBtn.GetComponent<Button>().onClick.AddListener(UIManager._uimanagerInstance.OnTransactionPanel);
                }
                break;
            case 7:
                GameObject bgmSelectBtn = Instantiate(conversationButton, _selectpanel.transform);
                RectTransform bgmSelectBtnRect = bgmSelectBtn.GetComponent<RectTransform>();
                bgmSelectBtnRect.SetLocalPositionAndRotation(new Vector3(0, 22, 0), UIManager._uimanagerInstance.rotateZero);
                bgmSelectBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "BGM 변경";
                bgmSelectBtn.GetComponent<Button>().onClick.AddListener(UIManager._uimanagerInstance.playRandomBGM);
                break;
            case 8:
                GameObject healBtn = Instantiate(conversationButton, _selectpanel.transform);
                RectTransform healBtnRect = healBtn.GetComponent<RectTransform>();
                healBtnRect.SetLocalPositionAndRotation(new Vector3(0, 22, 0), UIManager._uimanagerInstance.rotateZero);
                healBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "순례자의 기도 [회복]";
                healBtn.GetComponent<Button>().onClick.AddListener(UIManager._uimanagerInstance.healPlayer);
                break;
            case 10:
                helpBtn.GetComponent<Button>().onClick.RemoveAllListeners();
                helpBtn.GetComponent<Button>().onClick.AddListener(spiritInfoSelect);

                GameObject seedBtn = Instantiate(conversationButton, _selectpanel.transform);
                RectTransform seedBtnRect = seedBtn.GetComponent<RectTransform>();
                seedBtnRect.SetLocalPositionAndRotation(new Vector3(0, -11, 0), UIManager._uimanagerInstance.rotateZero);
                seedBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "씨앗이 필요해";
                seedBtn.GetComponent<Button>().onClick.AddListener(UIManager._uimanagerInstance.OnTransactionPanel);
                break;
            case 9:
                _nomaltalk.gameObject.SetActive(true);
                _nomaltalk.GetComponent<TextMeshProUGUI>().text = "힘을 나누어드리겠습니다.\n어느 위치로 이동하시겠습니까?";

                selectTeleport();
                break;
            case 11:
                Destroy(talkBtn.gameObject);
                selectAlterTeleport(0);
                _nomaltalk.gameObject.SetActive(true);
                _nomaltalk.GetComponent<TextMeshProUGUI>().text = "정령의 제단으로 이동하시겠습니까?";
                break;
            case 12:
                Destroy(talkBtn.gameObject);
                selectAlterTeleport(1);
                _nomaltalk.gameObject.SetActive(true);
                _nomaltalk.GetComponent<TextMeshProUGUI>().text = "정령의 제단을 떠나시겠습니까?";
                break;
        }
    }

    // 선택창 전 대사 출력
    public void secondConversation()
    {
        Debug.Log("초기 대화 두번째");
        _nomaltalk.text = conversations[UIManager._uimanagerInstance.conversationNPC - 1, 3];
        conversationCount++;
    }

    // 선택창 출력
    public void thirdConversation()
    {
        Debug.Log("초기 대화 세번째");
        _nomaltalk.gameObject.SetActive(false);
        _selectpanel.SetActive(true);
    }

    // Conversation Panel 초기화
    public void resetConversationPanel()
    {
        Debug.Log("모든 대화 상호작용 종료");
        _nomaltalk.gameObject.SetActive(true);
        _selectpanel.SetActive(false);
        gameObject.SetActive(false);
        isConversation = false;
        selectConversationCount = 0;
        isInformation = false;
        informationCount = 0;
        conversationCount = -1;
        isSpirit = -1;
        UIManager._uimanagerInstance.setIsOpenConversation(false);

        _nomaltalk.GetComponent<RectTransform>().localPosition = new Vector3(0, 115, 0);
        _selectpanel.GetComponent<RectTransform>().localPosition = new Vector3(0, 60, 0);

        Transform[] selectObjectList = _selectpanel.GetComponentsInChildren<Transform>();
        for (int i = 1; i < selectObjectList.Length; i++)
        {
            if (selectObjectList[i] != _selectpanel.transform)
            {
                Destroy(selectObjectList[i].gameObject);
            }
        }

        UIManager._uimanagerInstance.delayRunControllKeys();
        // ui.runControllKeys();
    }

    // 순례자 - 기도 선택 시, 마무리 대사
    public void selectHeal()
    {
        _selectpanel.SetActive(false);
        _nomaltalk.text = "[순례자의 기도로 체력과 기력이 모두 회복되었습니다.]\n\n언제나 정령이 함께하길..";
        _nomaltalk.gameObject.SetActive(true);
        isConversation = true;
        selectConversationCount = 4;
    }

    // 음악가 - BGM 변경 선택 시, 마무리 대사
    public void selectMusic(string _bgmName)
    {
        _selectpanel.SetActive(false);
        _nomaltalk.text = "새로운 멜로디를 들려드리죠-\n\n현재 BGM : " + _bgmName;
        _nomaltalk.gameObject.SetActive(true);
        isConversation = true;
        selectConversationCount = 4;
    }

    // 세계수 상호작용 시, 출력 선택지
    public void selectTeleport()
    {
        Transform[] selectObjectList = _selectpanel.GetComponentsInChildren<Transform>();
        for (int i = 1; i < selectObjectList.Length; i++)
        {
            if (selectObjectList[i] != _selectpanel.transform)
            {
                Destroy(selectObjectList[i].gameObject);
            }
        }

        GameObject teleportBtn = Instantiate(conversationButton, _selectpanel.transform);
        RectTransform teleportBtnRect = teleportBtn.GetComponent<RectTransform>();
        teleportBtnRect.SetLocalPositionAndRotation(new Vector3(0, 55, 0), UIManager._uimanagerInstance.rotateZero);
        teleportBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "포목점 너머 사냥터";
        teleportBtn.GetComponent<Button>().onClick.AddListener(() => UIManager._uimanagerInstance.doTeleport(0));

        GameObject teleportBtn2 = Instantiate(conversationButton, _selectpanel.transform);
        RectTransform teleportBtnRect2 = teleportBtn2.GetComponent<RectTransform>();
        teleportBtnRect2.SetLocalPositionAndRotation(new Vector3(0, 22, 0), UIManager._uimanagerInstance.rotateZero);
        teleportBtn2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "사냥꾼 오두막 너머 사냥터";
        teleportBtn2.GetComponent<Button>().onClick.AddListener(() => UIManager._uimanagerInstance.doTeleport(1));

        GameObject teleportBtn3 = Instantiate(conversationButton, _selectpanel.transform);
        RectTransform teleportBtnRect3 = teleportBtn3.GetComponent<RectTransform>();
        teleportBtnRect3.SetLocalPositionAndRotation(new Vector3(0, -11, 0), UIManager._uimanagerInstance.rotateZero);
        teleportBtn3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "취소";
        teleportBtn3.GetComponent<Button>().onClick.AddListener(resetConversationPanel);
    }

    // 제단 상호작용 시 출력 선택지 : conversation 과 별개로 호출됨
    public void selectAlterTeleport(int _key)
    {
        Transform[] selectObjectList = _selectpanel.GetComponentsInChildren<Transform>();
        for (int i = 1; i < selectObjectList.Length; i++)
        {
            if (selectObjectList[i] != _selectpanel.transform)
            {
                Destroy(selectObjectList[i].gameObject);
            }
        }
        // Debug.Log(_nomaltalk.GetComponent<RectTransform>().localPosition);
        // Debug.Log(_selectpanel.GetComponent<RectTransform>().localPosition);

        _nomaltalk.GetComponent<RectTransform>().localPosition = new Vector3(12, 120, 0);
        _selectpanel.GetComponent<RectTransform>().localPosition = new Vector3(0, 55, 0);

        GameObject teleportBtn = Instantiate(conversationButton, _selectpanel.transform);
        RectTransform teleportBtnRect = teleportBtn.GetComponent<RectTransform>();
        teleportBtnRect.SetLocalPositionAndRotation(new Vector3(0, 33, 0), UIManager._uimanagerInstance.rotateZero);
        if (_key == 0)
        {
            teleportBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "제단으로 이동합니다.";
            teleportBtn.GetComponent<Button>().onClick.AddListener(() => UIManager._uimanagerInstance.upTeleport());
        }
        else if (_key == 1)
        {
            teleportBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "제단을 떠납니다.";
            teleportBtn.GetComponent<Button>().onClick.AddListener(() => UIManager._uimanagerInstance.downTeleport());
        }

        Debug.Log("체킹");
        GameObject teleportBtn2 = Instantiate(conversationButton, _selectpanel.transform);
        RectTransform teleportBtnRect2 = teleportBtn2.GetComponent<RectTransform>();
        teleportBtnRect2.SetLocalPositionAndRotation(new Vector3(0, 0, 0), UIManager._uimanagerInstance.rotateZero);
        teleportBtn2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "취소";
        teleportBtn2.GetComponent<Button>().onClick.AddListener(resetConversationPanel);
    }

    // NPC 선택지 중 - 대화 선택 시 setting
    private void selectConversation()
    {
        Debug.Log("선택지 대화 시작");
        isConversation = true;
        _selectpanel.SetActive(false);
        _nomaltalk.gameObject.SetActive(true);

        int randCnt = Random.Range(0, 3);
        _nomaltalk.text = conversations[UIManager._uimanagerInstance.GetComponent<UIManager>().conversationNPC - 1, randCnt];
        selectConversationCount++;
    }

    // NPC 선택지 중 - 대화 선택 시 동작
    public void conversation()
    {
        if (selectConversationCount < 4)
        {
            Debug.Log("선택지 대화");
            int randCnt = Random.Range(0, 3);
            _nomaltalk.text = conversations[UIManager._uimanagerInstance.GetComponent<UIManager>().conversationNPC - 1, randCnt];
            selectConversationCount++;
        }
        else
        {
            Debug.Log("선택지 대화 종료");
            resetConversationPanel();
        }
    }

    // NPC 선택지 중 - '정령'의 '도움말' 선택 시 setting
    private void spiritInfoSelect()
    {
        Transform[] selectBtns = _selectpanel.GetComponentsInChildren<Transform>();
        // Debug.LogError(selectBtns[1].name);
        // Debug.LogError(selectBtns[3].name);
        // Debug.LogError(selectBtns[5].name);

        selectBtns[1].GetChild(0).GetComponent<TextMeshProUGUI>().text = "농사";
        selectBtns[1].GetComponent<Button>().onClick.RemoveAllListeners();
        selectBtns[1].GetComponent<Button>().onClick.AddListener(() => selectInfo(0));

        selectBtns[3].GetChild(0).GetComponent<TextMeshProUGUI>().text = "꽃";
        selectBtns[3].GetComponent<Button>().onClick.RemoveAllListeners();
        selectBtns[3].GetComponent<Button>().onClick.AddListener(() => selectInfo(1));

        selectBtns[5].GetChild(0).GetComponent<TextMeshProUGUI>().text = "제단";
        selectBtns[5].GetComponent<Button>().onClick.RemoveAllListeners();
        selectBtns[5].GetComponent<Button>().onClick.AddListener(() => selectInfo(2));

    }

    // NPC 선택지 중 - 도움말 선택 시 setting
    private void selectInfo(int _key)
    {
        Debug.Log("도움말 대화 시작");
        isInformation = true;
        _selectpanel.SetActive(false);
        _nomaltalk.gameObject.SetActive(true);

        if (_key == -1)
        {
            _key = UIManager._uimanagerInstance.conversationNPC;
            _nomaltalk.text = informations[_key - 1, informationCount];
        }
        else
        {
            isSpirit = _key;
            _nomaltalk.text = spiritInformations[_key, informationCount];
        }
        informationCount++;
    }

    // NPC 선택지 중 - 도움말 선택 시 동작
    public void information()
    {
        int _key = -1;
        int maxLen = 0;
        if (isSpirit >= 0)
        {
            _key = isSpirit;
            maxLen = spiritInfoLength[_key];
        }
        else
        {
            _key = UIManager._uimanagerInstance.conversationNPC;
            maxLen = infoLength[_key - 1];
        }


        if (informationCount < maxLen)
        {
            Debug.Log("도움말 대화");
            if (isSpirit >= 0)
            {
                _nomaltalk.text = spiritInformations[_key, informationCount];
            }
            else
            {
                _nomaltalk.text = informations[_key - 1, informationCount];
            }
            informationCount++;
        }
        else
        {
            Debug.Log("도움말 대화 종료");
            resetConversationPanel();
        }
    }

    // 세계수 정령에게 축복 상호작용 대사창
    public void conversationWhenAlterBuff(int _key)
    {
        UIManager._uimanagerInstance.conversationNPC = 10;
        _npcname.text = "세계수의 정령";
        gameObject.SetActive(true);
        if (_key == 0)
        {
            isConversation = true;
            selectConversationCount = 4;
            _nomaltalk.text = "이미 축복을 받고 있군요, 저희의 축복은 다음에 찾아오세요";
        }
        else
        {
            string flowerName = UIManager._uimanagerInstance.findItem(_key).Name;
            _nomaltalk.text = flowerName + "(이)군요, 정령들과 함께 축복을 해드릴게요-";
            _selectpanel.SetActive(true);

            GameObject prayBtn1 = Instantiate(conversationButton, _selectpanel.transform);
            RectTransform prayBtnRect1 = prayBtn1.GetComponent<RectTransform>();
            prayBtnRect1.SetLocalPositionAndRotation(new Vector3(0, 22, 0), UIManager._uimanagerInstance.rotateZero);
            prayBtn1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "꽃을 정령에게 건넨다.";
            prayBtn1.GetComponent<Button>().onClick.AddListener(() => UIManager._uimanagerInstance.callSpiritBuff(_key));

            GameObject prayBtn2 = Instantiate(conversationButton, _selectpanel.transform);
            RectTransform prayBtnRect2 = prayBtn2.GetComponent<RectTransform>();
            prayBtnRect2.SetLocalPositionAndRotation(new Vector3(0, -11, 0), UIManager._uimanagerInstance.rotateZero);
            prayBtn2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "꽃을 건네지 않는다.";
            prayBtn2.GetComponent<Button>().onClick.AddListener(resetConversationPanel);
        }
    }

    // 제단 제사 선택
    public void selectWhenAlterPray(int nowCode, int newCode, int quickIdx)
    {
        Debug.Log("selectWhenAlterPray 콜");
        gameObject.SetActive(true);

        string flowerName = "";
        if (nowCode == -1)
        {
            _nomaltalk.text = "이미 정령 제단의 축복이 흐르고 있습니다.\n"
                    + UIManager._uimanagerInstance.findItem(newCode).Name + "을(를) 새롭게 제단에 바치고 제사를 지내겠습니까?";
        }
        else if (nowCode == 0)
        {
            flowerName = UIManager._uimanagerInstance.findItem(newCode).Name;
            _nomaltalk.text = flowerName + "을 제단에 바치고 제사를 지내겠습니까?";
        }
        else if (nowCode == newCode)
        {
            _nomaltalk.text = UIManager._uimanagerInstance.findItem(nowCode).Name + "의 기운이 아르간디움에 흐르고 있습니다.\n계속해서 "
                    + UIManager._uimanagerInstance.findItem(newCode).Name + "을(를) 제단에 바치고 제사를 지내겠습니까?";
        }
        else
        {
            _nomaltalk.text = UIManager._uimanagerInstance.findItem(nowCode).Name + "의 기운이 아르간디움에 흐르고 있습니다.\n"
                    + UIManager._uimanagerInstance.findItem(newCode).Name + "을(를) 새롭게 제단에 바치고 제사를 지내겠습니까?";
        }

        int flowerCnt = 0;
        switch (UIManager._uimanagerInstance.getNowPrayDate())
        {
            case 0:
            case 1:
                flowerCnt = 1;
                break;
            case 2:
                flowerCnt = 2;
                break;
        }

        GameObject prayBtn1 = Instantiate(conversationButton, _selectpanel.transform);
        RectTransform prayBtnRect1 = prayBtn1.GetComponent<RectTransform>();
        prayBtnRect1.SetLocalPositionAndRotation(new Vector3(0, 22, 0), UIManager._uimanagerInstance.rotateZero);
        prayBtn1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "꽃을 제단에 바친다.";
        Debug.Log("===============" + UIManager._uimanagerInstance.getNowPrayDate());
        if (nowCode == -1 && UIManager._uimanagerInstance.getNowPrayDate() == 0)
        {
            Debug.Log("fx 리셋 콜");
            prayBtn1.GetComponent<Button>().onClick.AddListener(() => UIManager._uimanagerInstance.resetPrayBuffFx());
        }
        prayBtn1.GetComponent<Button>().onClick.AddListener(() => UIManager._uimanagerInstance.callPrayBuff(newCode));
        prayBtn1.GetComponent<Button>().onClick.AddListener(() => UIManager._uimanagerInstance.quickUse(newCode, flowerCnt, quickIdx));
        prayBtn1.GetComponent<Button>().onClick.AddListener(resetConversationPanel);

        GameObject prayBtn2 = Instantiate(conversationButton, _selectpanel.transform);
        RectTransform prayBtnRect2 = prayBtn2.GetComponent<RectTransform>();
        prayBtnRect2.SetLocalPositionAndRotation(new Vector3(0, -11, 0), UIManager._uimanagerInstance.rotateZero);
        prayBtn2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "꽃을 바치지 않고 떠난다.";
        prayBtn2.GetComponent<Button>().onClick.AddListener(resetConversationPanel);

        _selectpanel.SetActive(true);
    }

    // NPC 시작 대사 저장 배열
    private string[,] conversations = new string[,]{
        {"호호, 안녕하세요! 괜찮은 옷이 있어요-", "우리 남편 못 보셨나요? 목장에도 없던데,,", "요즘 괜찮은 가죽이 없어서 옷을 만들 수 없어요..", "호호, 필요한 거 있으세요??"},
        {"좋~~은 목재가 들어왔어요! 목재 필요 없어요?", "나무만 패기엔 너무 좋은 날씨군요-", "더 좋은 도구가 필요하면 언제든 찾아주세요-!!", "오늘은 무슨 일로 오셨나요??"},
        {"호수 너머를 가만히 바라보면 마음이 평온해진다네..", "낚시를 하며 지친 몸을 쉬기에 딱 좋은 호수라네 허허", "호수에는 언제나 자연이 찾아와 쉰다네", "허허, 도움이라도 필요한겐가..?"},
        {"대장간 일 하기에 아주 좋은 날이구만!! 어서오게!", "뭐? 더 튼튼한 무기가 필요하다고???", "드라우프... 나도 거의 본 적이 없는 귀~~한 광물이지", "어디, 광질이라도 좀 할텐가??"},
        {"오늘도 동물들은 건강합니다-!!", "요즘 양털의 상태가 아주 좋아요-! 아내도 기뻐하고 있죠", "동물들이 기운이 별로 없네요.. 건강에는 문제 없으니 걱정마세요!", "뭐 필요한 거 있으세요??"},
        {"무기는 잘 관리하고 있어? 숲에서는 늘 조심해야해!", "오두막에는 무슨 일이야? 이제 사냥 할 시간이라고", "숲이 어수선해, 순찰을 가야겠어", "왜? 뭐 필요해??"},
        {"자연의 소리만큼 아름다운 선율은 존재하지 않죠", "음악은 마음에 평화를 가져다줍니다-", "오늘은 신나는 음악이 필요해 보이는군요!", "어떤 음악이 듣고싶으신가요??"},
        {"길이 없으면 길을 잃지 않습니다.", "정령의 기운을 따라 가세요, 빛이 당신을 인도합니다.", "제가 가는 곳이 제 집이고 제가 있는 곳이 저의 터전입니다.", "정령의 수호자여 무슨 일입니까?"},
        {"어서오세요, 오늘도 평화가 함께하길..", "그대의 노력으로 숲이 평화를 되찾고 있습니다..", "오늘도 정령의 빛이 그대와 함께 할 것입니다..", "힘을 나누어드리죠"},
        {"수 많은 정령들이 빛을 잃어갑니다. 당신만이 정령을 구할 수 있어요.", "정령의 꽃을 발견하셨나요? 정령의 축복을 받을 수 있겠네요.", "옛날에는 세계수의 근처에 정령의 제단이 있었다고 합니다.. 후후, 지금은 모르겠네요.", "수호자여, 도움이 필요한가요?"}
    };

    // 도움말 배열
    // npc 별 도움말 count
    private int[] infoLength = new int[] { 0, 4, 3, 2, 3, 4 };
    // 정령 도움말 count - 선택지 선택 후 출력예정
    private int[] spiritInfoLength = new int[] { 5, 3, 3 };

    // npc별 도움말 배열
    private string[,] informations = new string[,] {
        {"","","",""},
        {"숲을 돌아다니다보면 죽은 나무들을 볼 수 있을 거에요.","윤곽선이 표시된 나무를 도끼로 공격하면 나무를 얻을 수 있어요.","또한 숲에서는 블루베리 덤불, 버섯도 자라고 있어요.","채집용 칼을 들고 상호작용할 경우 채집을 시도할 수 있어요."},
        {"호수에서는 낚시를 할 수 있네.. 낚싯대와 미끼만 있다면 가능하지","만약 물고기가 미끼를 물면 반응이 올걸세, 그때 다시 상호작용을 시도하면 낚시를 성공할 수 있네","낚싯대와 미끼의 품질에 따라 물고기를 더 빨리, 더 쉽게 잡을수 있을걸세",""},
        {"곡괭이를 사용하여 광물 캐서 얻을 수 있네!","광물은 몇 안되는 광맥지대에서만 생성되니 유의하게, 하루가 지난 광맥을 사라지니 말일세","",""},
        {"목장에서는 동물을 키울수 있어요-!","필요한 동물은 저를 통해 구입하시면 되고, 매일 부산물을 생성할 거에요.","목장에서 매일 자라는 풀은 제한되어 있어서, 제한적인 숫자의 동물만 키울 수 있어요-!",""},
        {"야생 동물은 검으로 공격해서 잡을 수 있어!","정화되지 않은 지역은 사나운 동물이 많으니 조심하라구","사냥터에서는 더 많은 동물을 찾을 수 있을거야","밤이 늦으면 동물들도 자러가서 보이지 않게 될 테니 유의하고"}
    };

    private string[,] spiritInformations = new string[,] {
        {"수호자는 농장에서 원하는 작물을 기를수 있어요.","호미를 사용해 땅을 갈면 작물을 심을 수 있고, 몇일이 지나면 수확할 수 있을거에요.","모든 작물은 매일 물을 소비하고 물이 부족하다면 알림을 띄워줄 거에요. 잊지 말고 살펴보도록 해요.","수확을 한 땅은 땅을 다시 갈기 전까지 작물을 심을 수 없어요.","모든 작물은 심을 수 있는 계절이 정해져 있고, 필요한 물의 양이 달라요."},
        {"꽃은 아주 귀한 자원이에요. 채집용 칼을 통해 조심히 얻을 수 있답니다.","구역의 정화에는 해당 구역에서만 자라는 꽃이 필요해요.","이외에도 꽃을 저에게 주시면 작은 축복을 내려줄 수 있어요.","",""},
        {"제단은 오래전부터 존재했던 저희의 성소에요.","3일간 매일 제단에 꽃을 바치면 정령이 당신을 축복해줄 거에요.","하지만 중간에 꽃을 바치지 않으면 처음부터 다시 꽃을 바쳐야해요.","",""}
    };

}

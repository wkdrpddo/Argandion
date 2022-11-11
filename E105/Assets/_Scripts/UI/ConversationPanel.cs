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
    */
    private TextMeshProUGUI _npcname;
    private TextMeshProUGUI _nomaltalk;
    private GameObject _selectpanel;
    private int conversationCount = -1;         // -1 : 대화 전, 0 : 대화 시작, 1 : 마지막 대화

    private bool isOnPanel = false;
    public GameObject conversationButton;
    private UIManager ui;
    private bool isConversation;
    private int selectConversationCount = 0;        // 선택창에서 '대화' 선택 시의 카운트

    // 대화 시작
    public void setConversationNPC(int value)
    {
        ui.conversationNPC = value;

        int randCnt = Random.Range(0, 3);
        conversationCount++;
        _nomaltalk.text = conversations[value - 1, randCnt];
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
            case 9:
                _npcname.text = "세계수";
                break;
            case 10:
                _npcname.text = "세계수의 정령";
                break;
        }

        isOnPanel = true;
        setSelectList();
        gameObject.SetActive(true);
    }

    public void secondConversation()
    {
        _nomaltalk.text = conversations[ui.conversationNPC - 1, 3];
        conversationCount++;
    }

    public void thirdConversation()
    {
        _nomaltalk.gameObject.SetActive(false);
        _selectpanel.SetActive(true);
    }

    public void resetConversationPanel()
    {
        _nomaltalk.gameObject.SetActive(true);
        _selectpanel.SetActive(false);
        gameObject.SetActive(false);
        isConversation = false;
        selectConversationCount = 0;
        conversationCount = -1;
        ui.conversationNPC = 0;
        ui.runControllPlayer();

        Transform[] selectObjectList = _selectpanel.GetComponentsInChildren<Transform>();
        for (int i = 1; i < selectObjectList.Length; i++)
        {
            if (selectObjectList[i] != _selectpanel.transform)
            {
                Destroy(selectObjectList[i].gameObject);
            }
        }
    }

    private void setSelectList()
    {
        GameObject talkBtn = Instantiate(conversationButton, _selectpanel.transform);
        RectTransform talkBtnRect = talkBtn.GetComponent<RectTransform>();
        talkBtnRect.SetLocalPositionAndRotation(new Vector3(0, 55, 0), ui.rotateZero);
        talkBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "대화";
        talkBtn.GetComponent<Button>().onClick.AddListener(selectConversation);

        switch (ui.conversationNPC)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                // case 6:
                GameObject tradeBtn = Instantiate(conversationButton, _selectpanel.transform);
                RectTransform tradeBtnRect = tradeBtn.GetComponent<RectTransform>();
                tradeBtnRect.SetLocalPositionAndRotation(new Vector3(0, 22, 0), ui.rotateZero);
                tradeBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "거래";
                tradeBtn.GetComponent<Button>().onClick.AddListener(ui.OnTransactionPanel);
                break;
            case 5:
                GameObject animalTradeBtn = Instantiate(conversationButton, _selectpanel.transform);
                RectTransform animalTradeBtnRect = animalTradeBtn.GetComponent<RectTransform>();
                animalTradeBtnRect.SetLocalPositionAndRotation(new Vector3(0, 22, 0), ui.rotateZero);
                animalTradeBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "거래";
                animalTradeBtn.GetComponent<Button>().onClick.AddListener(ui.OnTransactionAnimalPanel);
                break;
            case 7:
                GameObject bgmSelectBtn = Instantiate(conversationButton, _selectpanel.transform);
                RectTransform bgmSelectBtnRect = bgmSelectBtn.GetComponent<RectTransform>();
                bgmSelectBtnRect.SetLocalPositionAndRotation(new Vector3(0, 22, 0), ui.rotateZero);
                bgmSelectBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "BGM 선택";
                bgmSelectBtn.GetComponent<Button>().onClick.AddListener(ui.OnTransactionPanel);
                break;
            case 8:
                GameObject healBtn = Instantiate(conversationButton, _selectpanel.transform);
                RectTransform healBtnRect = healBtn.GetComponent<RectTransform>();
                healBtnRect.SetLocalPositionAndRotation(new Vector3(0, 22, 0), ui.rotateZero);
                healBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "순례자의 기도 [회복]";
                healBtn.GetComponent<Button>().onClick.AddListener(ui.OnTransactionPanel);
                break;
            case 9:
                GameObject seedBtn = Instantiate(conversationButton, _selectpanel.transform);
                RectTransform seedBtnRect = seedBtn.GetComponent<RectTransform>();
                seedBtnRect.SetLocalPositionAndRotation(new Vector3(0, 22, 0), ui.rotateZero);
                seedBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "씨앗이 필요해";
                seedBtn.GetComponent<Button>().onClick.AddListener(ui.OnTransactionPanel);

                GameObject warpBtn = Instantiate(conversationButton, _selectpanel.transform);
                RectTransform warpBtnRect = warpBtn.GetComponent<RectTransform>();
                warpBtnRect.SetLocalPositionAndRotation(new Vector3(0, -11, 0), ui.rotateZero);
                warpBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "빠른 이동";
                warpBtn.GetComponent<Button>().onClick.AddListener(ui.OnTransactionPanel);
                break;
        }
    }

    private void selectConversation()
    {
        isConversation = true;
        _selectpanel.SetActive(false);
        _nomaltalk.gameObject.SetActive(true);

        int randCnt = Random.Range(0, 3);
        _nomaltalk.text = conversations[ui.GetComponent<UIManager>().conversationNPC - 1, randCnt];
        selectConversationCount++;
    }

    public void conversation()
    {
        if (selectConversationCount < 4)
        {
            int randCnt = Random.Range(0, 3);
            _nomaltalk.text = conversations[ui.GetComponent<UIManager>().conversationNPC - 1, randCnt];
            selectConversationCount++;
        }
        else
        {
            resetConversationPanel();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ui = gameObject.GetComponentInParent<UIManager>();
        _npcname = transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        _nomaltalk = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

        _selectpanel = transform.GetChild(0).GetChild(1).gameObject;
        isConversation = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool getIsOn()
    {
        return isOnPanel;
    }

    public int getConversationCnt()
    {
        return conversationCount;
    }

    public bool getIsConversation()
    {
        return isConversation;
    }

    // NPC 대사 저장 배열
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

}

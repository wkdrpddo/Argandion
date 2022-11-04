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
    */
    public TextMeshProUGUI _npcname;
    public TextMeshProUGUI _nomaltalk;
    public int conversationCount = -1;         // -1 : 대화 전, 0 : 대화 시작, 1 : 마지막 대화

    public GameObject _selectpanel;

    // 대화 시작
    public void setConversationNPC(int value)
    {
        gameObject.GetComponentInParent<UIManager>().conversationNPC = value - 1;

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
        }
    }

    public void secondConversation()
    {
        _nomaltalk.text = conversations[gameObject.GetComponentInParent<UIManager>().conversationNPC, 3];
        conversationCount++;
    }

    public void resetConversationCount()
    {
        _nomaltalk.gameObject.SetActive(false);
        _selectpanel.gameObject.SetActive(true);
        conversationCount = -1;
        gameObject.GetComponentInParent<UIManager>().conversationNPC = 0;
    }

    // public void setSelectList()
    // {
    //     switch (gameObject.GetComponentInParent<UIManager>().conversationNPC)
    //     {

    //     }
    // }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // NPC 대사 저장 배열
    private string[,] conversations = new string[,]{
        {"호호, 안녕하세요! 괜찮은 옷이 있어요-", "우리 남편 못 보셨나요? 목장에도 없던데,,", "요즘 괜찮은 가죽이 없어서 옷을 만들 수 없어요..", "호호, 필요한 거 있으세요??"},
        {"좋~~은 목재가 들어왔어요! 목재 필요 없어요?", "나무만 패기엔 너무 좋은 날씨군요-", "더 좋은 도구가 필요하면 언제든 찾아주세요-!!", "오늘은 무슨 일로 오셨나요??"},
        {"호수 너머를 가만히 바라보면 마음이 평온해진다네..", "낚시를 하며 지친 몸을 쉬기에 딱 좋은 호수라네 허허", "호수에는 언제나 자연이 찾아와 쉰다네", "허허, 도움이라도 필요한겐가..?"},
        {"대장간 일 하기에 아주 좋은 날이구만!! 어서오게!", "뭐? 더 튼튼한 무기가 필요하다고???", "드라우프... 나도 거의 본 적이 없는 귀~~한 광물이지", "어디, 광질이라도 좀 할텐가??"},
        {"오늘도 동물들은 건강합니다-!!", "요즘 양털의 상태가 아주 좋아요-! 아내도 기뻐하고 있죠", "동물들이 기운이 별로 없네요.. 건강에는 문제 없으니 걱정마세요!", "뭐 필요한 거 있으세요??"},
        {"무기는 잘 관리하고 있어? 숲에서는 늘 조심해야해!", "오두막에는 무슨 일이야? 이제 사냥 할 시간이라고", "숲이 어수선해, 순찰을 가야겠어", "왜? 뭐 필요해??"},
        {"", "", "", ""},
        {"", "", "", ""}
    };

}

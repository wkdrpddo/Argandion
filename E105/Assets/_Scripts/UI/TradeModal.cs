using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TradeModal : MonoBehaviour
{
    /* checkMod
        0. 판매 = 목장 과 그 외
        1. 구매 - 각각 구분 필요 [5번 제외]

        storeIdx
        1. 재단사
        2. 공방
        3. 호수
        4. 대장간
        5. 목장
        6. 사냥꾼
    */

    [SerializeField] private Ranch _ranch;
    [SerializeField] private UIManager ui;
    [SerializeField] private int tradeMod;
    [SerializeField] private int tradeCost;
    [SerializeField] private string iconName;
    [SerializeField] private int storeIndex;
    [SerializeField] private int itemIndex;

    public void setModal(string name, string iconName, int maxCnt, int cost, int checkMod, int storeIdx, int itemIdx)
    {
        gameObject.SetActive(true);
        transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + iconName);
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = name;
        gameObject.GetComponentInChildren<Slider>().value = 0;
        gameObject.GetComponentInChildren<Slider>().maxValue = maxCnt;
        transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>().text = "0";
        tradeMod = checkMod;
        tradeCost = cost;
        this.iconName = iconName;
        storeIndex = storeIdx;
        itemIndex = itemIdx;
    }

    public void closeModal()
    {
        gameObject.SetActive(false);
    }

    public void clickOk()
    {
        int tradeCount = (int)gameObject.GetComponentInChildren<Slider>().value;

        Debug.Log("====== click On function ======");
        if (tradeMod == 0)
        {
            sellEvent(tradeCount);
        }
        else
        {
            buyEvent(tradeCount);
        }
    }

    private void sellEvent(int tradeCnt)
    {
        Debug.Log("------ ------ sellEvent function ------ ------");
        if (storeIndex == 5)
        {
            switch (itemIndex)
            {
                case 1:
                    _ranch.SellSheep(tradeCnt);
                    break;
                case 2:
                    _ranch.SellChick(tradeCnt);
                    break;
                case 3:
                    _ranch.SellCow(tradeCnt);
                    break;
                default:
                    Debug.LogError("인수 관계가 잘못되었습니다");
                    break;
            }
            ui.syncAnimalPanel(_ranch.getPoint(), _ranch.sheeps, _ranch.chicks, _ranch.cows);
            closeModal();
        }
        else
        {

        }
    }

    private void buyEvent(int tradeCnt)
    {

    }

    public void matchSliderValue()
    {
        transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>().text = gameObject.GetComponentInChildren<Slider>().value.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        _ranch = GameObject.Find("NPCManager").GetComponent<Ranch>();
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

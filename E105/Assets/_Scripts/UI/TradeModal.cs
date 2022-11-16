using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TradeModal : MonoBehaviour
{
    /* tradeMod
        0. 판매 = 목장 과 그 외
        1. 구매 - 각각 구분 필요 [5번 제외]
        2. 제작
        3. 창고 넣기
        4. 창고 빼기

        storeIdx
        1. 재단사
        2. 공방
        3. 호수
        4. 대장간
        5. 목장
        6. 사냥꾼
        10. 세계수의 정령
    */

    [SerializeField] private GameObject _npcmanager;
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

        int pos = Array.IndexOf(iconNameInString, iconName);
        if (pos == -1)
        {
            transform.GetChild(1).GetComponent<Image>().sprite = ui.getItemIcon(int.Parse(iconName));
        }
        else
        {
            transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + iconName);
        }

        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = name;
        gameObject.GetComponentInChildren<Slider>().value = 0;
        if (checkMod == 1)
        {
            int howmany = ui.getPlayerGold() / cost;
            if (howmany > 99)
            {
                howmany = 99;
            }
            gameObject.GetComponentInChildren<Slider>().maxValue = howmany;
        }
        else if (checkMod == 4)
        {
            if (maxCnt > 396)
            {
                maxCnt = 396;
            }
            gameObject.GetComponentInChildren<Slider>().maxValue = maxCnt;
        }
        else
        {
            gameObject.GetComponentInChildren<Slider>().maxValue = maxCnt;
        }
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
        else if (tradeMod == 1)
        {
            if (tradeCount > 0)
            {
                buyEvent(tradeCount);
            }
        }
        else if (tradeMod == 2)
        {
            // Debug.LogWarning("여기까지는 왔니?");
            switch (storeIndex)
            {
                case 1:
                    ui.getCraftPanel().GetComponent<CombDesigner>().Trade(itemIndex, tradeCount);
                    break;
                case 2:
                    ui.getCraftPanel().GetComponent<CombCarpentor>().Trade(itemIndex, tradeCount);
                    break;
                case 4:
                    ui.getCraftPanel().GetComponent<CombSmith>().Trade(itemIndex, tradeCount);
                    break;
                case 6:
                    ui.getCraftPanel().GetComponent<CombHunter>().Trade(itemIndex, tradeCount);
                    break;
            }
            ui.OnResultNotificationPanel("제작이 완료되었습니다.");
        }
        else if (tradeMod == 3)
        {
            Debug.Log("Item Code : " + iconName);
            ui.addToStorage(ui.findItem(Int32.Parse(iconName)), tradeCount, itemIndex);
        }
        else if (tradeMod == 4)
        {
            ui.removeToStorage(ui.findItem(Int32.Parse(iconName)), tradeCount, itemIndex);
        }

        ui.getCraftPanel().syncCanMakeList();
        closeModal();
    }

    private void sellEvent(int tradeCnt)
    {
        Debug.Log("------ ------ sellEvent function ------ ------");
        if (storeIndex == 5)
        {
            switch (itemIndex)
            {
                case 1:
                    _npcmanager.GetComponent<Ranch>().SellSheep(tradeCnt);
                    break;
                case 2:
                    _npcmanager.GetComponent<Ranch>().SellChick(tradeCnt);
                    break;
                case 3:
                    _npcmanager.GetComponent<Ranch>().SellCow(tradeCnt);
                    break;
                default:
                    Debug.LogError("인수 관계가 잘못되었습니다");
                    break;
            }
            ui.syncAnimalPanel(_ranch.getPoint(), _ranch.sheeps, _ranch.chicks, _ranch.cows);
            closeModal();
        }
        else if (storeIndex == 2)
        {
            if (tradeCnt != 0)
            {
                ui.sellItem(itemIndex, tradeCnt, 2);
                ui.addPlayerGold((tradeCost * tradeCnt));
            }
        }
        else
        {
            if (tradeCnt != 0)
            {
                ui.sellItem(itemIndex, tradeCnt, 1);
                ui.addPlayerGold((tradeCost * tradeCnt));
            }
        }
    }

    private void buyEvent(int tradeCnt)
    {
        if (storeIndex == 2)
        {
            _npcmanager.GetComponent<BuyingCarpentor>().Buy(itemIndex, tradeCnt);
        }
        else if (storeIndex == 3)
        {
            _npcmanager.GetComponent<BuyingFisher>().Buy(itemIndex, tradeCnt);
        }
        else if (storeIndex == 6)
        {
            _npcmanager.GetComponent<BuyingHunter>().Buy(itemIndex, tradeCnt);
        }
        else if (storeIndex == 10)
        {
            _npcmanager.GetComponent<BuyingSeed>().Buy(itemIndex, tradeCnt);
        }
        else
        {
            Debug.LogError("=== 상점구매 상점idx 정보가 맞지 않음 ===");
        }
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
        _npcmanager = GameObject.Find("NPCManager").gameObject;
    }

    private string[] iconNameInString = new string[] { "chicken", "cow", "sheep" };

}

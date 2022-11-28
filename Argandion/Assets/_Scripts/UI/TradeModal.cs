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
    // [SerializeField] private UIManager ui;
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
            transform.GetChild(1).GetComponent<Image>().sprite = UIManager._uimanagerInstance.getItemIcon(int.Parse(iconName));
        }
        else
        {
            transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + iconName);
        }

        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = name;
        gameObject.GetComponentInChildren<Slider>().value = 0;
        if (checkMod == 1)
        {
            int howmany = UIManager._uimanagerInstance.getPlayerGold() / cost;
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
                    UIManager._uimanagerInstance.getCraftPanel().GetComponent<CombDesigner>().Trade(itemIndex, tradeCount);
                    break;
                case 2:
                    UIManager._uimanagerInstance.getCraftPanel().GetComponent<CombCarpentor>().Trade(itemIndex, tradeCount);
                    break;
                case 4:
                    UIManager._uimanagerInstance.getCraftPanel().GetComponent<CombSmith>().Trade(itemIndex, tradeCount);
                    break;
                case 6:
                    UIManager._uimanagerInstance.getCraftPanel().GetComponent<CombHunter>().Trade(itemIndex, tradeCount);
                    break;
            }
            UIManager._uimanagerInstance.OnResultNotificationPanel("제작이 완료되었습니다.");
        }
        else if (tradeMod == 3)
        {
            int itemCode = Int32.Parse(iconName);
            syncBaitSlot(itemCode);

            Debug.Log("Item Code : " + iconName);
            UIManager._uimanagerInstance.addToStorage(UIManager._uimanagerInstance.findItem(Int32.Parse(iconName)), tradeCount, itemIndex);
        }
        else if (tradeMod == 4)
        {
            UIManager._uimanagerInstance.removeToStorage(UIManager._uimanagerInstance.findItem(Int32.Parse(iconName)), tradeCount, itemIndex);
        }

        UIManager._uimanagerInstance.getCraftPanel().syncCanMakeList();
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
            UIManager._uimanagerInstance.syncAnimalPanel(_ranch.getPoint(), _ranch.sheeps, _ranch.chicks, _ranch.cows);
            closeModal();
        }
        else if (storeIndex == 2)
        {
            if (tradeCnt != 0)
            {
                UIManager._uimanagerInstance.sellItem(itemIndex, tradeCnt, 2);
                UIManager._uimanagerInstance.addPlayerGold((tradeCost * tradeCnt));
            }
        }
        else
        {
            if (tradeCnt != 0)
            {
                int itemCode = Int32.Parse(iconName);
                syncBaitSlot(itemCode);

                UIManager._uimanagerInstance.sellItem(itemIndex, tradeCnt, 1);
                UIManager._uimanagerInstance.addPlayerGold((tradeCost * tradeCnt));
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

            int itemCode = Int32.Parse(iconName);
            syncBaitSlot(itemCode, 1);
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
        // ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        _npcmanager = GameObject.Find("NPCManager").gameObject;
    }

    private void syncBaitSlot(int itemCode, int key = 0) {
        Debug.Log("key value : " + key);
        int tradeCount = (int)gameObject.GetComponentInChildren<Slider>().value;

        if(UIManager._uimanagerInstance.getBaitSlotData() != null && itemCode != UIManager._uimanagerInstance.getBaitSlotData().ItemCode) {
            return;
        }

        if(itemCode >= 502 && itemCode <= 504) {
            Slot[] slots = UIManager._uimanagerInstance.getInventorySlots();
            Debug.Log("itemCode : " + itemCode);
            foreach(Slot slot in slots) {
                if(slot.itemCount == 0) {
                    continue;
                }
                Debug.Log("slotItemCode : " + slot.item.ItemCode);
                if(key == 1) {
                    if(slot.item.ItemCode == itemCode) {
                        UIManager._uimanagerInstance.rightEquip(UIManager._uimanagerInstance.findItem(itemCode), slot.itemCount, slot.idx);
                        break;
                    }
                    continue;
                }
                else if(slot.idx == itemIndex && slot.itemCount == tradeCount) {
                    UIManager._uimanagerInstance.clearBaitSlot();
                    continue;
                }
                else if(slot.idx == itemIndex && slot.itemCount != tradeCount) {
                    UIManager._uimanagerInstance.rightEquip(UIManager._uimanagerInstance.findItem(itemCode), slot.itemCount-tradeCount, slot.idx);
                    break;
                }
                else if(slot.item.ItemCode == itemCode) {
                    UIManager._uimanagerInstance.rightEquip(UIManager._uimanagerInstance.findItem(itemCode), slot.itemCount, slot.idx);
                    break;
                }
            }
        }
    }

    private string[] iconNameInString = new string[] { "chicken", "cow", "sheep" };

}

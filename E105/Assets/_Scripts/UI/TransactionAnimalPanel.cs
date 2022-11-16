using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransactionAnimalPanel : MonoBehaviour
{
    // private 
    [SerializeField] private UIManager ui;
    private Ranch _ranch;

    public void callCellModal(int value)
    {
        switch (value)
        {
            case 1:
                ui.OnTradeModal("닭", "chicken", _ranch.chicks, 325, 0, 5, 2);
                break;
            case 2:
                ui.OnTradeModal("소", "cow", _ranch.cows, 500, 0, 5, 3);
                break;
            case 3:
                ui.OnTradeModal("양", "sheep", _ranch.sheeps, 175, 0, 5, 1);
                break;
        }
    }

    public void handelPanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        ui.setIsOpenTransaction(gameObject.activeSelf);

        if (gameObject.activeSelf)
        {
            transform.GetChild(1).GetChild(4).GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = ui.getPlayerGold().ToString();
            ui.stopControllKeys();
        }
        else
        {
            ui.closeTradeModal();
            ui.runControllKeys();
            ui.conversationNPC = 0;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        ui = gameObject.GetComponentInParent<UIManager>();

        gameObject.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() => ui.OnTransactionDoubleCheckPanel("닭", 5, 2, -1));
        gameObject.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => ui.OnTransactionDoubleCheckPanel("소", 5, 3, -1));
        gameObject.transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Button>().onClick.AddListener(() => ui.OnTransactionDoubleCheckPanel("양", 5, 1, -1));

        _ranch = GameObject.Find("NPCManager").GetComponent<Ranch>();
    }

    public void syncRanchData(int point, int sheepCnt, int chickenCnt, int cowCnt)
    {
        // 수용량 동기화
        gameObject.transform.GetChild(1).Find("Capacity").GetComponentInChildren<TextMeshProUGUI>().text = point.ToString() + "/80";

        // 보유 동물 수 동기화
        gameObject.transform.GetChild(1).GetChild(3).Find("Sheep").Find("HowManyHave").GetComponent<TextMeshProUGUI>().text = sheepCnt.ToString();
        gameObject.transform.GetChild(1).GetChild(3).Find("Chicken").Find("HowManyHave").GetComponent<TextMeshProUGUI>().text = chickenCnt.ToString();
        gameObject.transform.GetChild(1).GetChild(3).Find("Cow").Find("HowManyHave").GetComponent<TextMeshProUGUI>().text = cowCnt.ToString();

        // 소지금 동기화
        transform.GetChild(1).GetChild(4).GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = ui.getPlayerGold().ToString();
    }
}

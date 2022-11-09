using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransactionAnimalPanel : MonoBehaviour
{
    // private 
    private UIManager ui;
    private bool isOnPanel;

    public void callCellModal(int value)
    {
        switch (value)
        {
            case 1:
                ui.OnTradeModal("닭", "chicken", 10, 1, 325);
                break;
            case 2:
                ui.OnTradeModal("소", "cow", 10, 1, 500);
                break;
            case 3:
                ui.OnTradeModal("양", "sheep", 10, 1, 175);
                break;
        }

    }

    public bool getIsOn()
    {
        return isOnPanel;
    }

    public void onPanel()
    {
        gameObject.SetActive(true);
    }

    public void closePanel()
    {
        gameObject.SetActive(false);
        ui.runControllPlayer();
        ui.closeTradeModal();
    }

    // Start is called before the first frame update
    void Start()
    {
        isOnPanel = false;
        ui = gameObject.GetComponentInParent<UIManager>();

        transform.GetChild(1).GetChild(4).GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = ui.getPlayerGold().ToString();

        gameObject.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() => ui.OnTransactionDoubleCheckPanel("닭", 5, 2));
        gameObject.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => ui.OnTransactionDoubleCheckPanel("소", 5, 3));
        gameObject.transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Button>().onClick.AddListener(() => ui.OnTransactionDoubleCheckPanel("양", 5, 1));
    }

    public void syncRanchData()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

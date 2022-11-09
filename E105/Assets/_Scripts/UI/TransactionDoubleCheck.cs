using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransactionDoubleCheck : MonoBehaviour
{
    [SerializeField] private Ranch _ranch;
    [SerializeField] private UIManager ui;
    [SerializeField] private int storeIndex;
    [SerializeField] private int itemIndex;
    [SerializeField] private int itemCode;

    // public Inventory _inventory;

    public void handleModal()
    {
        if (storeIndex != 5)
        {
            if (ui.checkInventory(ui.findItem(itemCode), 1, true))
            {
                ui.OnResultNotificationPanel("구매가 불가능 합니다. 인벤토리를 확인 해 주세요!!");
                return;
            }
        }
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void setData(string name, int storeIdx, int itemIdx, int itemCode)
    {
        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name + " 을(를) 구매하시겠습니까?";
        storeIndex = storeIdx;
        itemIndex = itemIdx;
        this.itemCode = itemCode;
    }

    public void clickOK()
    {
        switch (storeIndex)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                if (itemIndex == 1)
                {
                    _ranch.BuySheep();
                }
                else if (itemIndex == 2)
                {
                    _ranch.BuyChick();
                }
                else if (itemIndex == 3)
                {
                    _ranch.BuyCow();
                }
                else
                {
                    Debug.LogError("인수관계가 잘못 되었습니다.");
                }

                ui.syncAnimalPanel(_ranch.getPoint(), _ranch.sheeps, _ranch.chicks, _ranch.cows);
                handleModal();
                break;
            case 6:
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        _ranch = GameObject.Find("NPCManager").GetComponent<Ranch>();
        storeIndex = -1;
        itemIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransactionDoubleCheck : MonoBehaviour
{
    [SerializeField] private Ranch _ranch;
    private BuyingCarpentor _carpentor;
    private BuyingDesigner _designer;
    private BuyingFisher _fisher;
    private BuyingHunter _hunter;
    private BuyingSmith _smith;
    [SerializeField] private UIManager ui;
    [SerializeField] private int storeIndex;
    [SerializeField] private int itemIndex;
    [SerializeField] private int itemCode;

    // public Inventory _inventory;

    public void handleModal()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void setData(string name, int storeIdx, int itemIdx, int itemCode)
    {
        // Debug.Log("name : " + name + " | itemcode : " + itemCode);
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
                _designer.Buy(itemIndex, 1);
                break;
            case 2:
                _carpentor.Buy(itemIndex, 1);
                break;
            case 3:
                _fisher.Buy(itemIndex, 1);
                break;
            case 4:
                _smith.Buy(itemIndex, 1);
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
                break;
            case 6:
                _hunter.Buy(itemIndex, 1);
                break;
        }
        handleModal();
    }
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        _ranch = GameObject.Find("NPCManager").GetComponent<Ranch>();
        _carpentor = GameObject.Find("NPCManager").GetComponent<BuyingCarpentor>();
        _designer = GameObject.Find("NPCManager").GetComponent<BuyingDesigner>();
        _fisher = GameObject.Find("NPCManager").GetComponent<BuyingFisher>();
        _hunter = GameObject.Find("NPCManager").GetComponent<BuyingHunter>();
        _smith = GameObject.Find("NPCManager").GetComponent<BuyingSmith>();
        storeIndex = -1;
        itemIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

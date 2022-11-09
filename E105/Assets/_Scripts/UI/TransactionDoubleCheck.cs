using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransactionDoubleCheck : MonoBehaviour
{
    [SerializeField] private NPCManager _npcmanager;
    [SerializeField] private UIManager ui;
    private int storeIndex;
    private int itemIndex;

    public void handleModal()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void setData(string name, int storeIdx, int itemIdx)
    {
        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name + " 을(를) 판매하시겠습니까?";
        storeIndex = storeIdx;
        itemIndex = itemIdx;
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
                    _npcmanager.GetComponent<Ranch>().BuySheep();
                }
                else if (itemIndex == 2)
                {
                    _npcmanager.GetComponent<Ranch>().BuyChick();
                }
                else if (itemIndex == 3)
                {
                    _npcmanager.GetComponent<Ranch>().BuyCow();
                }
                else
                {
                    Debug.LogError("인수관계가 잘못 되었습니다.");
                }

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
        _npcmanager = GameObject.Find("NPCManager").GetComponent<NPCManager>();
        storeIndex = -1;
        itemIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

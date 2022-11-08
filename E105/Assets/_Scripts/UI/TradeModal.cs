using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TradeModal : MonoBehaviour
{
    /*
        1. 동물 판매
        2. 아이템 구매
        3. 아이템 판매
    */

    private int tradeMod;
    private int tradeCost;
    private string iconName;

    public void setModal(string name, string iconName, int maxCnt, int checkMod, int cost)
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
    }

    public void closeModal()
    {
        gameObject.SetActive(false);
    }

    public void clickOk()
    {
        int tradeCount = (int)gameObject.GetComponentInChildren<Slider>().value;
        int calculCost = tradeCount * tradeCost;
        switch (tradeMod)
        {
            case 1:
                // 해당 동물 수, 수용량, 
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }

    public void matchSliderValue()
    {
        transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>().text = gameObject.GetComponentInChildren<Slider>().value.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

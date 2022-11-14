using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BuildEventPanel : MonoBehaviour
{
    private UIManager ui;
    private Image buildIcon;
    private TextMeshProUGUI buildName;
    private TextMeshProUGUI flowerName;
    private TextMeshProUGUI[] materials = new TextMeshProUGUI[6];
    private GameObject _failmodal;

    public bool isOnPanel = false;

    public Sprite Fishing;
    public Sprite Farm;
    public Sprite Clothshop;
    public Sprite Forge;
    public Sprite Hunter;
    public Sprite Workshop;

    [SerializeField] private int[] myItems;
    [SerializeField] private int[] howItems;
    [SerializeField] private bool[] canMake;
    [SerializeField] private Slot[] slots;
    [SerializeField] private bool canBuild;

    // Start is called before the first frame update
    void Start()
    {
        isOnPanel = false;
        canBuild = false;

        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        buildIcon = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        buildName = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        flowerName = transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();

        for (int i = 0; i < 6; i++)
        {
            materials[i] = transform.GetChild(0).GetChild(2).GetChild(i + 2).GetComponent<TextMeshProUGUI>();
        }

        _failmodal = transform.GetChild(4).gameObject;

        myItems = new int[25];
        howItems = new int[25];
        canMake = new bool[7];
    }

    private void setting(int _buildKey, int _step)
    {
        slots = ui.getInventorySlots();

        foreach (Slot slot in slots)
        {
            if (slot.itemCount > 0)
            {
                for (int i = 0; i < 25; i++)
                {
                    if (myItems[i] == 0 || myItems[i] == slot.item.ItemCode)
                    {
                        myItems[i] = slot.item.ItemCode;
                        howItems[i] += slot.itemCount;
                        break;
                    }
                }
            }
        }

        canBuild = CanMake(_buildKey, _step, myItems, howItems);
    }

    private bool CanMake(int _buildKey, int _step, int[] myItems, int[] howItems)
    {
        // 꽃 체크
        for (int i = 0; i < 25; i++)
        {
            if (flowerName.text == ui.findItem(myItems[i]).Name && howItems[i] >= buildCost[_step, 1])
            {
                canMake[0] = true;
                break;
            }

            if (myItems[i] == 0)
            {
                canMake[0] = false;
                break;
            }
        }

        // 다른 재료 체크
        for (int i = 0; i < 6; i++)
        {
            if (materials[i].text == "")
            {
                canMake[i + 1] = true;
                continue;
            }

            for (int j = 0; j < 25; j++)
            {
                if (materials[i].text == ui.findItem(myItems[j]).Name && howItems[j] >= buildConditionCount[_buildKey - 1, i + 1])
                {
                    canMake[i + 1] = true;
                    materials[i].color = new Color(0, 0, 0);
                    break;
                }

                materials[i].color = new Color(255, 0, 0);
                if (myItems[j] == 0)
                {
                    canMake[i + 1] = false;
                    break;
                }
            }
        }

        if (ui.getPlayerGold() < buildCost[_step, 0])
        {
            gameObject.transform.GetChild(0).GetChild(2).GetChild(8).GetComponent<TextMeshProUGUI>().color = new Color(255, 0, 0);
            return false;
        }
        else
        {
            gameObject.transform.GetChild(0).GetChild(2).GetChild(8).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0);
        }

        if (!canMake[0])
        {
            flowerName.color = new Color(255, 0, 0);
        }
        else
        {
            flowerName.color = new Color(0, 0, 0);
        }

        for (int i = 0; i < 7; i++)
        {
            if (!canMake[i])
            {
                return false;
            }
        }


        return true;
    }

    public void closeWindow()
    {
        isOnPanel = false;
        gameObject.SetActive(false);
        ui.runControllPlayer();
        _failmodal.SetActive(false);
    }

    public void OnPanel(int value, int step)
    {
        isOnPanel = true;
        gameObject.SetActive(true);
        setBuildCondition(value, step);
    }

    public void clickOkBtn()
    {
        if (canBuild)
        {
            Debug.Log("============= 건설 시작 =============");
        }
        else
        {
            _failmodal.SetActive(true);
            Invoke("closeFailModal", 1.5f);
        }
    }

    private void closeFailModal()
    {
        _failmodal.SetActive(false);
    }

    private void setBuildCondition(int value, int step)
    {
        buildName.text = buildConditions[value - 1, 0];
        flowerName.text = buildConditions[value - 1, 1];

        switch (value)
        {
            case 1:
                buildIcon.sprite = Clothshop;
                break;
            case 2:
                buildIcon.sprite = Workshop;
                break;
            case 3:
                buildIcon.sprite = Fishing;
                break;
            case 4:
                buildIcon.sprite = Forge;
                break;
            case 6:
                buildIcon.sprite = Farm;
                break;
            case 7:
                buildIcon.sprite = Hunter;
                break;
        }

        gameObject.transform.GetChild(0).GetChild(2).GetChild(8).GetComponent<TextMeshProUGUI>().text = buildCost[step, 0].ToString();

        for (int i = 0; i < 6; i++)
        {
            if (buildConditionCount[value - 1, i + 1] != 0)
            {
                materials[i].text = buildConditions[value - 1, i + 2];
            }
            else
            {
                materials[i].text = "";
            }
        }

        setting(value, step);

        flowerName.text = flowerName.text + " (" + buildCost[step, 1] + ")";
        for (int i = 0; i < 6; i++)
        {
            if (buildConditionCount[value - 1, i + 1] != 0)
            {
                materials[i].text = materials[i].text + " " + buildConditionCount[value - 1, i + 1];
            }
            else
            {
                materials[i].text = "";
            }
        }
    }

    private string[,] buildConditions = new string[7, 8] {
        {"포목점", "디키탈리스", "판자", "동물의 가죽", "거친 가죽", "금괴", "양털", ""},
        {"공방", "비올라", "연목재", "경목재", "나뭇가지", "돌", "", ""},
        {"호수", "로벨리아", "판자", "부드러운 가죽", "철괴", "", "", ""},
        {"대장간", "그레빌레아", "연목재", "경목재", "나뭇가지", "돌", "철광석", "금광석"},
        {"", "", "", "", "", "", "", ""},
        {"목장", "온시디움", "판자", "동물의 가죽", "철괴", "고기", "밀", ""},
        {"사냥꾼의 오두막", "프리뮬라", "판자", "거친 가죽", "철괴", "돌", "", ""}
    };

    private int[,] buildConditionCount = new int[7, 7] {
        {0, 60, 20, 20, 5, 60, 0},
        {0, 15, 15, 20, 20, 0, 0},
        {0, 35, 15, 15, 0, 0, 0},
        {0, 12, 12, 20, 20, 10, 2},
        {0, 0, 0, 0, 0, 0, 0},
        {0, 40, 20, 10, 20, 25, 0},
        {0, 35, 15, 15, 100, 0, 0}
    };

    private int[,] buildCost = new int[,] {
        {0, 1}, {2000,2}, {25000,3}, {100000,4}, {300000,5}, {500000,6}
    };
}

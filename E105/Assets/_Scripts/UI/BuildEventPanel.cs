using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public void closeWindow()
    {
        isOnPanel = false;
        gameObject.SetActive(false);
        ui.runControllPlayer();
        _failmodal.SetActive(false);
    }

    public void OnPanel(int value)
    {
        isOnPanel = true;
        gameObject.SetActive(true);
        setBuildCondition(value);
    }

    public void clickOkBtn()
    {
        _failmodal.SetActive(true);
        Invoke("closeFailModal", 1.5f);
    }

    private void closeFailModal()
    {
        _failmodal.SetActive(false);
    }

    private void setBuildCondition(int value)
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

        for (int i = 0; i < 6; i++)
        {
            if (buildConditionCount[value - 1, i + 1] != 0)
            {
                materials[i].text = buildConditions[value - 1, i + 2] + buildConditionCount[value - 1, i + 1];
            }
            else
            {
                materials[i].text = "";
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        isOnPanel = false;

        ui = gameObject.GetComponentInParent<UIManager>();
        buildIcon = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        buildName = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        flowerName = transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();

        for (int i = 0; i < 6; i++)
        {
            materials[i] = transform.GetChild(0).GetChild(2).GetChild(i + 2).GetComponent<TextMeshProUGUI>();
        }

        _failmodal = transform.GetChild(4).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private string[,] buildConditions = new string[7, 8] {
        {"포목점", "디키탈리스 ", "판자 ", "동물가죽 ", "거친가죽 ", "금주괴 ", "양털 ", ""},
        {"공방", "비올라 ", "연목재 ", "경목재 ", "나뭇가지 ", "돌 ", "", ""},
        {"호수", "로벨리아 ", "판자 ", "부드러운가죽 ", "철주괴 ", "", "", ""},
        {"대장간", "그레빌레아 ", "연목재 ", "경목재 ", "나뭇가지 ", "돌 ", "철광석 ", "금광석 "},
        {"", "", "", "", "", "", "", ""},
        {"목장", "온시디움 ", "판자 ", "동물가죽 ", "철주괴 ", "고기 ", "밀 ", ""},
        {"사냥꾼의 오두막", "프리뮬라 ", "판자 ", "거친가죽 ", "철주괴 ", "돌 ", "", ""}
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
}

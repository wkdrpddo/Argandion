using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class combFoodEffectObject
{
    public int ItemCode;
    public int Health;
    public int Stamina;
    public string Effect;
}

public class CookingPanel : MonoBehaviour
{
    private UIManager ui;
    public CookingInteraction _cookingInteraction;

    public GameObject RecipeCard;
    public GameObject RecipeText;
    public GameObject NomalText;

    private GameObject DishContent;
    private GameObject RecipeContent;

    private combObject[] itemData;
    private combFoodEffectObject[] effectData;
    private int[] canMakeList;
    private int index;

    public void closeWindow()
    {
        gameObject.SetActive(false);
        gameObject.transform.GetChild(0).GetComponent<Button>().interactable = true;

        _cookingInteraction.CookingEnd();

        ui.runControllPlayer();
    }

    public void openCooking()
    {
        _cookingInteraction.CookingStart();
        gameObject.SetActive(true);
        setDishList();
        gameObject.transform.GetChild(0).GetComponent<Button>().interactable = false;
    }

    public void onClickCooking()
    {
        Debug.LogWarning("========== 요리하기 클릭 ============");
        ui.runCookingAnimation();
        Invoke("completeCooking", 7f);

        Debug.LogWarning("========== after animation call ============");
        bool isContainInventory;
        ItemObject craftItem = ui.findItem(itemData[index].Result);
        isContainInventory = ui.checkInventory(craftItem, 1);

        if (isContainInventory)
        {
            gameObject.GetComponent<CombFood>().Trade(index, 1);
        }
        else
        {
            ui.OnResultNotificationPanel("인벤토리에 빈 공간이 없습니다.");
        }

        gameObject.transform.GetChild(0).GetComponent<Button>().interactable = false;
        syncCanmakeList();
        deleteRecipeList();
    }

    public void completeCooking()
    {
        gameObject.transform.GetChild(3).GetComponent<Image>().sprite = ui.getItemIcon(itemData[index].Result);
        gameObject.transform.GetChild(3).gameObject.SetActive(true);
        Invoke("endShowFoodIcon", 1.5f);
    }

    public void endShowFoodIcon()
    {
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
    }

    private void setDishList()
    {
        deleteDishList();

        gameObject.GetComponent<CombFood>().Hello();
        canMakeList = gameObject.GetComponent<CombFood>().getCanMakeList();

        string jsonInputString = Application.dataPath + "/Data/Json/CombFood.json";
        string jsonString = File.ReadAllText(jsonInputString);
        itemData = JsonHelper.FromJson<combObject>(jsonString);

        for (int i = 0; i < itemData.Length; i++)
        {
            combObject combObj = itemData[i];
            ItemObject itemObj = ui.findItem(combObj.Result);

            GameObject dishBtn = Instantiate(RecipeCard, DishContent.transform);

            dishBtn.transform.GetChild(0).GetComponent<Image>().sprite = ui.getItemIcon(itemObj.ItemCode);
            dishBtn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemObj.Name;
            dishBtn.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = itemObj.Desc;

            int index = i;
            dishBtn.GetComponent<Button>().onClick.AddListener(() => setRecipeList(index));
        }
    }

    private void setRecipeList(int value)
    {
        if (canMakeList[value] <= 0)
        {
            gameObject.transform.GetChild(0).GetComponent<Button>().interactable = false;
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<Button>().interactable = true;
        }

        index = value;

        deleteRecipeList();

        combObject combObj = itemData[value];

        GameObject nomalText1 = Instantiate(NomalText, RecipeContent.transform);
        nomalText1.GetComponent<TextMeshProUGUI>().text = "재  료";

        if (combObj.Material1 != 0)
        {
            createRecipeCard(combObj.Material1, combObj.Cost1);
        }
        if (combObj.Material2 != 0)
        {
            createRecipeCard(combObj.Material2, combObj.Cost2);
        }
        if (combObj.Material3 != 0)
        {
            createRecipeCard(combObj.Material3, combObj.Cost3);
        }
        if (combObj.Material4 != 0)
        {
            createRecipeCard(combObj.Material4, combObj.Cost4);
        }
        if (combObj.Material5 != 0)
        {
            createRecipeCard(combObj.Material5, combObj.Cost5);
        }
        if (combObj.Material6 != 0)
        {
            createRecipeCard(combObj.Material6, combObj.Cost6);
        }

        GameObject dividLine = Instantiate(NomalText, RecipeContent.transform);
        dividLine.GetComponent<TextMeshProUGUI>().text = "-------------------------";
        dividLine.GetComponent<TextMeshProUGUI>().fontSize = 12;

        GameObject nomalText2 = Instantiate(NomalText, RecipeContent.transform);
        nomalText2.GetComponent<TextMeshProUGUI>().text = "효  과";

        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/CombFoodEffect.json");
        effectData = JsonHelper.FromJson<combFoodEffectObject>(jsonString);

        // 효과 text 추가
        combFoodEffectObject effectObj = effectData[value];

        GameObject effectText1 = Instantiate(NomalText, RecipeContent.transform);
        effectText1.GetComponent<TextMeshProUGUI>().text = "   체력 +" + effectObj.Health;
        effectText1.GetComponent<TextMeshProUGUI>().fontSize = 16;
        GameObject effectText2 = Instantiate(NomalText, RecipeContent.transform);
        effectText2.GetComponent<TextMeshProUGUI>().text = "   기력 +" + effectObj.Stamina;
        effectText2.GetComponent<TextMeshProUGUI>().fontSize = 16;
        if (effectObj.Effect != null)
        {
            GameObject effectText3 = Instantiate(NomalText, RecipeContent.transform);
            effectText3.GetComponent<TextMeshProUGUI>().text = "   " + effectObj.Effect;
            effectText3.GetComponent<TextMeshProUGUI>().fontSize = 16;
        }
    }

    private void createRecipeCard(int material, int count)
    {
        GameObject metarialCard = Instantiate(RecipeText, RecipeContent.transform);
        ItemObject itemObj = ui.findItem(material);

        metarialCard.transform.GetChild(0).GetComponent<Image>().sprite = ui.getItemIcon(material);
        metarialCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemObj.Name + "  " + count;
    }

    private void deleteDishList()
    {
        RectTransform[] craftObjs = DishContent.GetComponentsInChildren<RectTransform>();
        for (int i = 1; i < craftObjs.Length; i++)
        {
            if (craftObjs[i] != DishContent.GetComponent<RectTransform>())
            {
                Destroy(craftObjs[i].gameObject);
            }
        }
    }

    private void deleteRecipeList()
    {
        RectTransform[] craftObjs = RecipeContent.GetComponentsInChildren<RectTransform>();
        for (int i = 1; i < craftObjs.Length; i++)
        {
            if (craftObjs[i] != RecipeContent.GetComponent<RectTransform>())
            {
                Destroy(craftObjs[i].gameObject);
            }
        }
    }

    public void syncCanmakeList()
    {
        canMakeList = gameObject.GetComponent<CombFood>().getCanMakeList();
    }

    // Start is called before the first frame update
    void Start()
    {
        ui = gameObject.GetComponentInParent<UIManager>();
        DishContent = transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).gameObject;
        RecipeContent = transform.GetChild(2).GetChild(1).GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

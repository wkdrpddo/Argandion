using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultNotificationPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _notitext;
    // Start is called before the first frame update
    void Awake()
    {
        _notitext = transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
    }

    public void handelNoti(string inputText)
    {
        Debug.Log("핸들노티");
        Debug.Log("bool " + gameObject.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);

        if (inputText != "")
        {
            Debug.Log("inputText " + inputText);
            _notitext.text = inputText;
        }

        Invoke("closeNoti", 1.5f);
    }

    private void closeNoti()
    {
        gameObject.SetActive(false);
    }
}

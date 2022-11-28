using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultNotificationPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _notitext;

    void Awake()
    {
        _notitext = transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
    }

    public void handelNoti(string inputText)
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if (inputText != "")
        {
            _notitext.text = inputText;
        }

        Invoke("closeNoti", 1.5f);
    }

    private void closeNoti()
    {
        gameObject.SetActive(false);
    }
}

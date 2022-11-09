using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultNotificationPanel : MonoBehaviour
{
    private TextMeshProUGUI _notitext;
    // Start is called before the first frame update
    void Start()
    {
        _notitext = transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
    }

    public void handelNoti(string inputText)
    {
        gameObject.SetActive(!gameObject.activeSelf);
        _notitext.text = inputText;
    }
}

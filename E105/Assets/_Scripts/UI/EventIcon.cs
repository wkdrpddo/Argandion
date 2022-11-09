using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

// public class EventIcon : MonoBehaviour
public class EventIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int _iconNum;
    public DescriptObj _descriptObj;

    public GameObject _announce; // 패널
    public TextMeshProUGUI _announceNameText; // 이름
    public TextMeshProUGUI _announceDescText; // 설명

    // 기본 세팅
    void Start()
    {
        _announce = GameObject.Find("UIManager").GetComponent<UIManager>()._eventAnnounce;
        _announceNameText = GameObject.Find("UIManager").GetComponent<UIManager>()._announceTitle;
        _announceDescText = GameObject.Find("UIManager").GetComponent<UIManager>()._announceText;
        // Debug.Log(_announce);
    }

    // hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter Mouse");
        _announce.SetActive(true);
        _announce.transform.position = Input.mousePosition - new Vector3(-5, 5, 0);
        // _announce.transform.GetChild(0).GetComponent<Text>().text = "asdfasdfasdfasdf";
        _announceNameText.text = _descriptObj.Name;
        _announceDescText.text = _descriptObj.Description;
    }

    // hover 해제
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit Mouse");
        _announce.SetActive(false);
    }



}

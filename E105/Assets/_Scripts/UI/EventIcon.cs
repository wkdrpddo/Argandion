using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class EventIcon : MonoBehaviour
// public class EventIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int _iconNum;

    // public GameObject _announce;
    // public TextMeshProUGUI _announceText;

    // // hover
    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     Debug.Log("Enter Mouse");
    //     _announce.SetActive(true);
    //     _announce.transform.position = Input.mousePosition - new Vector3(-5, 5, 0);
    //     // _announce.transform.GetChild(0).GetComponent<Text>().text = "asdfasdfasdfasdf";
    //     _announceText.text = "!#12312312312";
    // }

    // // hover 해제
    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     Debug.Log("Exit Mouse");
    //     _announce.SetActive(false);
    // }

    // // 기본 세팅
    // void Start()
    // {
    //     _announce = GameObject.Find("UIManager").GetComponent<UIManager>()._eventAnnounce;
    //     _announceText = GameObject.Find("UIManager").GetComponent<UIManager>()._announceText;
    //     // Debug.Log(_announce);
    // }

}

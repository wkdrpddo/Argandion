using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UIEventMousePointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject _announce;
    public TextMeshProUGUI _announceText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _announce.SetActive(true);
        _announce.transform.position = Input.mousePosition - new Vector3(-5, 5, 0);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _announce.SetActive(false);
    }

    void Start()
    {
        _announce = GameObject.Find("UIManager").GetComponent<UIManager>()._eventAnnounce;
        _announceText = GameObject.Find("UIManager").GetComponent<UIManager>()._announceText;
    }
}

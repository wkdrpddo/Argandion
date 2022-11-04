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
        Debug.Log("Enter Mouse");
        _announce.SetActive(true);
        _announce.transform.position = Input.mousePosition - new Vector3(-5, 5, 0);
        // _announce.transform.GetChild(0).GetComponent<Text>().text = "asdfasdfasdfasdf";
        _announceText.text = "!#12312312312";
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit Mouse");
        _announce.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        _announce = GameObject.Find("UIManager").GetComponent<UIManager>()._eventAnnounce;
        _announceText = GameObject.Find("UIManager").GetComponent<UIManager>()._announceText;
        Debug.Log(_announce);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

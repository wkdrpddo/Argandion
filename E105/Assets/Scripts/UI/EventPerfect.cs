using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventPerfect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject _announce;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter Mouse");
        _announce.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit Mouse");
        _announce.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

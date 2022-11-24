using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainPageMouseEvent
 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter Mouse");
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit Mouse");
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }
}

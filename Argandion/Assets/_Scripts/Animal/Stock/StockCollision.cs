using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 가축이 울타리나 다른 오브젝트 부딪히면 멈추고 Idle로 돌아가기
public class StockCollision : MonoBehaviour
{

    [HideInInspector]
    public Wander2 wander;
    private bool _checker;

    private void Awake()
    {

    }
    void Start()
    {
        wander = GetComponent<Wander2>();
    }

    IEnumerator OnCollisionEnter(Collision collision)
    {
        if (!_checker)
        {
            _checker = true;
            // Debug.Log("trigger");
            if ((collision.gameObject.tag != "Untagged") || (collision.gameObject.layer != 0))
            {
                // Debug.Log("Collide");
                StartCoroutine(wander.StopWalking());
            }
            yield return new WaitForSeconds(1);
            _checker = false;
        }
    }
}

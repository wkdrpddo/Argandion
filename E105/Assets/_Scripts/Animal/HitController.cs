using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{

    [SerializeField] private PlayerSystem _playerSystem;



    private void OnTriggerEnter(Collider other)
    {
        if (_playerSystem._equipList[_playerSystem._equipItem, 0] == 304)
        {
            Debug.Log("연습용 검");
            if (other.tag == "Rabbit")
            {
                Debug.Log("토끼 충돌");
                other.transform.GetComponent<Rabbit>().Damage(10, transform.position);
            }
            else if (other.tag == "Deer")
            {
                Debug.Log("순록 충돌");
                other.transform.GetComponent<Deer>().Damage(10, transform.position);
            }
            else if (other.tag == "Wolf")
            {
                Debug.Log("늑대 충돌");
                other.transform.GetComponent<Wolf>().Damage(10);
            }
            else if (other.tag == "Bear")
            {
                Debug.Log("곰 충돌");
                other.transform.GetComponent<Bear>().Damage(10);
            }
        }

        if (_playerSystem._equipList[_playerSystem._equipItem, 0] == 309)
        {
            Debug.Log("철 검");
            if (other.tag == "Rabbit")
            {
                Debug.Log("토끼 충돌");
                other.transform.GetComponent<Rabbit>().Damage(15, transform.position);
            }
            else if (other.tag == "Deer")
            {
                Debug.Log("순록 충돌");
                other.transform.GetComponent<Deer>().Damage(15, transform.position);
            }
            else if (other.tag == "Wolf")
            {
                Debug.Log("늑대 충돌");
                other.transform.GetComponent<Wolf>().Damage(15);
            }
            else if (other.tag == "Bear")
            {
                Debug.Log("곰 충돌");
                other.transform.GetComponent<Bear>().Damage(15);
            }
        }

        if (_playerSystem._equipList[_playerSystem._equipItem, 0] == 314)
        {
            Debug.Log("금 검");
            if (other.tag == "Rabbit")
            {
                Debug.Log("토끼 충돌");
                other.transform.GetComponent<Rabbit>().Damage(20, transform.position);
            }
            else if (other.tag == "Deer")
            {
                Debug.Log("순록 충돌");
                other.transform.GetComponent<Deer>().Damage(20, transform.position);
            }
            else if (other.tag == "Wolf")
            {
                Debug.Log("늑대 충돌");
                other.transform.GetComponent<Wolf>().Damage(20);
            }
            else if (other.tag == "Bear")
            {
                Debug.Log("곰 충돌");
                other.transform.GetComponent<Bear>().Damage(20);
            }
        }

        if (_playerSystem._equipList[_playerSystem._equipItem, 0] == 319)
        {
            Debug.Log("드라우프 검");
            if (other.tag == "Rabbit")
            {
                Debug.Log("토끼 충돌");
                other.transform.GetComponent<Rabbit>().Damage(30, transform.position);
            }
            else if (other.tag == "Deer")
            {
                Debug.Log("순록 충돌");
                other.transform.GetComponent<Deer>().Damage(30, transform.position);
            }
            else if (other.tag == "Wolf")
            {
                Debug.Log("늑대 충돌");
                other.transform.GetComponent<Wolf>().Damage(30);
            }
            else if (other.tag == "Bear")
            {
                Debug.Log("곰 충돌");
                other.transform.GetComponent<Bear>().Damage(30);
            }
        }


    }
}

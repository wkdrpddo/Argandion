using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSign : MonoBehaviour
{
    public GameObject _signPrefab;

    private GameObject sign;

    public void makeSign(float signPX, float signPY, float signPZ, Transform transform, Material icon)
    {
        // 표지판 생성
        sign = Instantiate(_signPrefab, new Vector3(signPX, signPY - 0.7f, signPZ), Quaternion.Euler(0, 180, 0), transform);
        // 아이콘 바꾸기
        sign.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = icon;
    }
    public void moveSign(float signPX, float signPZ)
    {
        // sign.GetComponent<Transform>().position.x = signPX;
        // sign.transform.position.z = signPZ;
        sign.transform.position = new Vector3(signPX, sign.transform.position.y, signPZ);
    }
}

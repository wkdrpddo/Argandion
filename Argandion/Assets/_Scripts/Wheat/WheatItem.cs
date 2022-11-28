using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatItem : MonoBehaviour
{   
    private Rigidbody rigid;
    private Transform trans;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();
        rigid.AddForce(Vector3.up * 1.5f, ForceMode.Impulse);
    }

    void Update()
    {
        transform.Rotate(Vector3.up * 100 * Time.deltaTime);
        if (trans.position.y < 0.5f) {
            trans.position = new Vector3(trans.position.x, 0.5f, trans.position.z);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("wateredDirt") || other.gameObject.CompareTag("dirt")) {
            rigid.useGravity = false;
            rigid.isKinematic = true;
        }
    }
}

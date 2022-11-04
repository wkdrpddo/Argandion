using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropGravity : MonoBehaviour
{
    public Collider _collider;
    public Rigidbody _rigidbody;

    void Start()
    {
        _collider = gameObject.GetComponent<Collider>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 0)
        {
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Jump : MonoBehaviour
{
    private Rigidbody _rigidbody;


    private void Awake()
    {
        _rigidbody = transform.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (transform.position.y > 1.5f)
        {
            _rigidbody.AddForce(Vector3.down * 40);
        } else
        {
            _rigidbody.AddForce(Vector3.up * 50);
        }

    }
}

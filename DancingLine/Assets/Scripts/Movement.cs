using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Speed = 1f;
    public Vector3 Direction1 = Vector3.forward;
    public Vector3 Direction2 = Vector3.left;
    private Rigidbody _rigidbody;
    private int _directionIndex;

    public static event Action<Vector3> onDirectionChanged;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _directionIndex = _directionIndex == 0 ?  1 : 0;
            onDirectionChanged?.Invoke(GetDirection());
        }
    }

    private void FixedUpdate()
    {
        Vector3 _direction = GetDirection();
        Vector3 _velocity = _direction * Speed;
        _velocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = _velocity;
        PlayerRotate();
    }

    private Vector3 GetDirection()
    {
        return _directionIndex == 0 ? Direction1 : Direction2;
    }

    private void OnDisable()
    {
        Vector3 velocity = Vector3.zero;
        velocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = velocity;
    }

    private void PlayerRotate()
    {
       if (_directionIndex == 1)
        {
            transform.Rotate(0f, 0f, Speed);
        } else
        {
            transform.Rotate(Speed, 0f, 0f);
        }

    }
}

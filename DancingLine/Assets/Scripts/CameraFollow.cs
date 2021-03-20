using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _followTarget;
    public float _speed;

    private float _fixedY;
    private Vector3 _offset;


    private void Awake()
    {
        Vector3 _initialPosition = transform.position;
        _offset = _initialPosition - _followTarget.position;
        _fixedY = _initialPosition.y;
        Movement.onDirectionChanged += RotateCameraToDir;

    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 newPosition = _followTarget.position + _offset;
        newPosition.y = _fixedY;
        transform.position = newPosition;
        //var currentRotation = Quaternion.LookRotation(_followTarget.position - transform.position);
        //transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, _speed * Time.deltaTime);
    }

    private void RotateCameraToDir(Vector3 _direction)
    {
        //поворот персонажа в сторону направления перемещения
        Vector3 direct = Vector3.RotateTowards(transform.forward, _direction, _speed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(direct);
    }

}

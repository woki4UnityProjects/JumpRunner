using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform capsule;

    private Vector3 _offset;
    void Start()
    {
        _offset = capsule.position - transform.position;
    }

    
    void Update()
    {
        transform.position = capsule.position - _offset;
    }
}

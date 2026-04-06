using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject _player;  
    public Vector3 _offset;

    public void Start()
    {
        _offset = transform.position - _player.transform.position;
    }
    
    private void LateUpdate()
    {
        CameraMoveToPlayer();
    }

    public void CameraMoveToPlayer()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = _player.transform.position + _offset;
        }
    }
}

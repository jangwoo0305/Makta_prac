using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject _player;  
    public Vector3 _offset;
    public float _moveSpeed = 20f;
    public float _edgeSize = 20f;

    public void Start()
    {
        _offset = transform.position - _player.transform.position;
        
        
    }
    
    private void LateUpdate()
    {
        CameraMoveToPlayer();
        CameraMoveByMouse();
    }

    public void CameraMoveToPlayer()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = _player.transform.position + _offset;
        }
    }

    public void CameraMoveByMouse()
    {
        Vector3 moveDir = Vector3.zero;
        Vector3 mousePos = Input.mousePosition;

        if (mousePos.x <= _edgeSize)
            moveDir += -transform.right;
        if (mousePos.x >= Screen.width - _edgeSize)
            moveDir += transform.right;
        if (mousePos.y <= _edgeSize)
            moveDir += -transform.forward;
        if (mousePos.y >= Screen.height - _edgeSize)
            moveDir += transform.forward;

        moveDir.y = 0; 
        moveDir.Normalize();
        transform.position += moveDir * _moveSpeed * Time.deltaTime;
    }
}

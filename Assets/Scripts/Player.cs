using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float _speed = 3f;
    
    Camera _camera;
    Animator _anim;
    
    private Vector3 _destPos;
    
    void Start()
    {
        _camera = Camera.main;
        _anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            OnMouseClick();
        }
        Move();
    }

    void OnMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);
        
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,100f,LayerMask.GetMask("Ground")))
        {
            _destPos = hit.point;
        }
    }

    void Move()
    {
        
        Vector3 dir = _destPos - transform.position;
        if(dir.magnitude < 0.00001f)
        {
            _anim.SetFloat("_speed", 0);
            return;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0f, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 15f ); 
        }
        
        _anim.SetFloat("_speed", _speed);
    }
}

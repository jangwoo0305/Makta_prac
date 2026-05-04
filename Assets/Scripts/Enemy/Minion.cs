using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    [SerializeField]
    private int _maxhp = 100;
    
    private int _hp;
    private MinionPool _pool;
    public Define.MinionType Type;
    public bool IsDead {get ; private set;}
    public Action<Minion> OnDeath;
    
    MinionPool _minionPool;

    void OnEnable()
    {
        _hp = _maxhp;
    }

    public void Init(MinionPool pool, Define.MinionType type)
    {
        _pool = pool;
        Type = type;
        
        gameObject.SetActive(false);
        _hp = _maxhp;
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        IsDead = true;
        
        OnDeath?.Invoke(this);
        
        gameObject.SetActive(false);
        _pool.ReturnMinion(this);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    private int _hp;
    private int _maxhp = 100;
    private MinionPool _pool;
    public Define.MinionType Type;
    
    MinionPool _minionPool;
    
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
        gameObject.SetActive(false);
        _pool.ReturnMinion(this);
    }
}

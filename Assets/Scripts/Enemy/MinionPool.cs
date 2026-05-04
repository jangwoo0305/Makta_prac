using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class MinionPool : MonoBehaviour
{
    public GameObject _minionPrefab;
    public int MiniPoolSize = 7;
    public Define.MinionType Type;
    
    private Queue<Minion> _Pool = new Queue<Minion>();

    private void Awake()
    {
        for (int i = 0; i < MiniPoolSize; i++)
        {
            GameObject obj = Instantiate(_minionPrefab, transform);
            obj.SetActive(false);
            
            Minion mini = obj.GetComponent<Minion>();
            mini.Init(this, Type);
            _Pool.Enqueue(mini);
        }
    }

    public Minion GetMinion()
    {
        Minion mini;
        
        if(_Pool.Count > 0)
        {
            mini = _Pool.Dequeue();
        }
        else
        {
            GameObject obj = Instantiate(_minionPrefab, transform);
            mini = obj.GetComponent<Minion>();
            mini.Init(this, Type);
        }
        
        mini.gameObject.SetActive(true);
        return mini;
    }

    public void ReturnMinion(Minion mini)
    {
        mini.gameObject.SetActive(false);
        _Pool.Enqueue(mini);
    }
}

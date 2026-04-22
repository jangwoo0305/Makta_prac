using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    public MinionPool meleePool;
    public MinionPool rangePool;
    public MinionPool siegePool;
    
    public Transform spawnPoint;
    
    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        // 근거리 3마리
        for (int i = 0; i < 3; i++)
        {
            Spawn(meleePool);
            yield return new WaitForSeconds(1f);
        }

        // 원거리 3마리
        for (int i = 0; i < 3; i++)
        {
            Spawn(rangePool);
            yield return new WaitForSeconds(1f);
        }
        
        yield return new WaitForSeconds(1f);
        // 대포 1마리
        Spawn(siegePool);
    }

    void Spawn(MinionPool pool)
    {
        Debug.Log("Spawn at: " + Time.time);
        Minion mini = pool.GetMinion();
        mini.transform.position = spawnPoint.position;
    }

}


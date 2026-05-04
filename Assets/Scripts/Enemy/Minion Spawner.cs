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

    // 🔥 현재 살아있는 미니언 리스트
    List<Minion> _aliveMinions = new List<Minion>();

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        _aliveMinions.Clear(); // 이전 웨이브 정리

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

        // 🔥 죽음 이벤트 연결
        mini.OnDeath -= HandleMinionDeath; // 중복 방지
        mini.OnDeath += HandleMinionDeath;

        _aliveMinions.Add(mini);
    }

    // 🔥 미니언 죽었을 때 호출
    void HandleMinionDeath(Minion mini)
    {
        _aliveMinions.Remove(mini);
        // 전부 죽었으면
        if (_aliveMinions.Count == 0)
        {
            Debug.Log("모든 미니언 처치 완료!");
            StartCoroutine(RespawnAfterDelay());
        }
    }

    // 🔥 30초 후 다시 스폰
    IEnumerator RespawnAfterDelay()
    {
        Debug.Log("30초 후 재스폰 시작");
        yield return new WaitForSeconds(30f);
        StartCoroutine(SpawnWave());
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [Header("스폰 오브젝트 종류")]
    [SerializeField] private GameObject[] normalTargets;   // 종류?크기?별 타겟
    [SerializeField] private GameObject minusTarget;       // 체력 감소용 오브젝트
    [SerializeField] private GameObject blockerObject;     // 시야 방해?용

    /// <summary>
    /// 소환 방식 지정 필요 : 가령 중앙에서 갑자기 소환되는 경우는 없는가?
    /// </summary>
    [Header("스폰 위치 영역")]
    [SerializeField] private Vector3 spawnCenter;
    [SerializeField] private Vector3 spawnRange;

    [Header("스폰 속도 설정")]
    [SerializeField] private float baseSpawnInterval = 1.0f;
    [SerializeField] private float spawnIntervalReduction = 0.1f;
    [SerializeField] private float minSpawnInterval = 0.3f;

    private Coroutine spawnRoutine;
    private float currentSpawnInterval;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartSpawning()
    {
        Debug.Log("[SpawnManager] 스폰 시작");
        currentSpawnInterval = baseSpawnInterval;
        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    public void StopSpawning()
    {
        Debug.Log("[SpawnManager] 스폰 중지");
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);
    }

    private IEnumerator SpawnLoop()
    {
        while (GameManager.Instance.IsGamePlaying)
        {
            SpawnRandomObject();
            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }

    /// <summary>
    /// 난이도에 따라 spawn interval을 감소시켜 난이도를 증가시킬 수 있음
    /// </summary>
    public void ReduceSpawnInterval()
    {
        currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - spawnIntervalReduction);
    }

    private void SpawnRandomObject()
    {
        float rand = Random.value;
        GameObject prefabToSpawn = null;

        if (rand < 0.7f) // 일반 타겟 70%
        {
            prefabToSpawn = normalTargets[Random.Range(0, normalTargets.Length)];
        }
        else if (rand < 0.9f) // 마이너스 오브젝트 20%
        {
            prefabToSpawn = minusTarget;
        }
        else // 시야 방해 오브젝트 10%
        {
            prefabToSpawn = blockerObject;
        }

        Vector3 spawnPos = GetRandomSpawnPosition();
        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 offset = new Vector3(
            Random.Range(-spawnRange.x / 2, spawnRange.x / 2),
            Random.Range(-spawnRange.y / 2, spawnRange.y / 2),
            Random.Range(-spawnRange.z / 2, spawnRange.z / 2)
        );

        return spawnCenter + offset;
    }
}

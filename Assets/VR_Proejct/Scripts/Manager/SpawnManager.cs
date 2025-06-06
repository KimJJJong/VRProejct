using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static TargetObject;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [Header("스폰 오브젝트 종류")]
    //[SerializeField] private GameObject[] normalTargets;   // 종류?크기?별 타겟
    //[SerializeField] private GameObject minusTarget;       // 체력 감소용 오브젝트
    //[SerializeField] private GameObject blockerObject;     // 시야 방해?용

    /// <summary>
    /// 소환 방식 지정 필요 : 가령 중앙에서 갑자기 소환되는 경우는 없는가?
    /// </summary>
    [Header("스폰 방향 구역")]
    [SerializeField] private Transform[] spawnZones; // ← 여기 추가
    [SerializeField] private Transform centerPoint;  // 중앙 (보통 플레이어 위치)


    [Header("스폰 속도 설정")]
    [SerializeField] private float baseSpawnInterval = 1.0f;
    [SerializeField] private float spawnIntervalReduction = 0.1f;
    [SerializeField] private float minSpawnInterval = 0.3f;
    [SerializeField] private float minSpeed = 5f;
    [SerializeField] private float maxSpeed = 10f;

    private Coroutine SpawnRoutine;
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
        SpawnRoutine = StartCoroutine(SpawnLoop());
    }

    public void StopSpawning()
    {
        Debug.Log("[SpawnManager] 스폰 중지");
        if (SpawnRoutine != null)
            StopCoroutine(SpawnRoutine);
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
        TargetObject.TargetType targetType;

        float rand = Random.value;

        if (rand < 0.85f) // 일반 타겟 70
        {
            // TargetObjectPoolManager생성후 추가, 변경
            var normalTypes = new[]
            {
            TargetObject.TargetType.Large,
            TargetObject.TargetType.Medium,
            TargetObject.TargetType.Small,
            TargetObject.TargetType.Tiny
             };

            targetType = normalTypes[Random.Range(0, normalTypes.Length)];
        }
        else if (rand < 0.9f)
        {
            var minusTypes = new[]
           {
            TargetObject.TargetType.Heal
             };

            targetType = minusTypes[Random.Range(0, minusTypes.Length)];
        }
        else if (rand < 0.95f) // 마이너스 오브젝트 20
        {
            var minusTypes = new[]
            {
            TargetObject.TargetType.Minus_Large,
            TargetObject.TargetType.Minus_Medium,
            TargetObject.TargetType.Minus_Small
             };

            targetType = minusTypes[Random.Range(0, minusTypes.Length)];
        }
        else // 시야 방해 오브젝트 10
        {
            targetType = TargetObject.TargetType.Blocker;
        }

        // 랜덤 스폰 위치 결정
        Transform selectedZone = spawnZones[Random.Range(0, spawnZones.Length)];
        Vector3 spawnPos = selectedZone.position + Random.insideUnitSphere * 0.5f;

        // 풀에서 가져옴
        TargetObject spawned = TargetObjectPoolManager.Instance.Get(targetType, spawnPos);

        // 날아가는 방향 설정
        Rigidbody rb = spawned.GetComponent<Rigidbody>();
        if (rb != null && centerPoint != null)
        {
            Vector3 dir = (centerPoint.position - spawnPos).normalized;
            dir.y += 0.5f; // 위로 살짝 튀게
            dir.Normalize();

            float speed = Random.Range(minSpeed, maxSpeed);
            rb.velocity = dir * speed;
        }
    }

}
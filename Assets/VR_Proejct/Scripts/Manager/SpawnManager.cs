using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static TargetObject;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [Header("���� ������Ʈ ����")]
    //[SerializeField] private GameObject[] normalTargets;   // ����?ũ��?�� Ÿ��
    //[SerializeField] private GameObject minusTarget;       // ü�� ���ҿ� ������Ʈ
    //[SerializeField] private GameObject blockerObject;     // �þ� ����?��

    /// <summary>
    /// ��ȯ ��� ���� �ʿ� : ���� �߾ӿ��� ���ڱ� ��ȯ�Ǵ� ���� ���°�?
    /// </summary>
    [Header("���� ���� ����")]
    [SerializeField] private Transform[] spawnZones; // �� ���� �߰�
    [SerializeField] private Transform centerPoint;  // �߾� (���� �÷��̾� ��ġ)


    [Header("���� �ӵ� ����")]
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
        Debug.Log("[SpawnManager] ���� ����");
        currentSpawnInterval = baseSpawnInterval;
        SpawnRoutine = StartCoroutine(SpawnLoop());
    }

    public void StopSpawning()
    {
        Debug.Log("[SpawnManager] ���� ����");
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
    /// ���̵��� ���� spawn interval�� ���ҽ��� ���̵��� ������ų �� ����
    /// </summary>
    public void ReduceSpawnInterval()
    {
        currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - spawnIntervalReduction);
    }

  
    private void SpawnRandomObject()
    {
        TargetObject.TargetType targetType;

        float rand = Random.value;

        if (rand < 0.85f) // �Ϲ� Ÿ�� 70
        {
            // TargetObjectPoolManager������ �߰�, ����
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
        else if (rand < 0.95f) // ���̳ʽ� ������Ʈ 20
        {
            var minusTypes = new[]
            {
            TargetObject.TargetType.Minus_Large,
            TargetObject.TargetType.Minus_Medium,
            TargetObject.TargetType.Minus_Small
             };

            targetType = minusTypes[Random.Range(0, minusTypes.Length)];
        }
        else // �þ� ���� ������Ʈ 10
        {
            targetType = TargetObject.TargetType.Blocker;
        }

        // ���� ���� ��ġ ����
        Transform selectedZone = spawnZones[Random.Range(0, spawnZones.Length)];
        Vector3 spawnPos = selectedZone.position + Random.insideUnitSphere * 0.5f;

        // Ǯ���� ������
        TargetObject spawned = TargetObjectPoolManager.Instance.Get(targetType, spawnPos);

        // ���ư��� ���� ����
        Rigidbody rb = spawned.GetComponent<Rigidbody>();
        if (rb != null && centerPoint != null)
        {
            Vector3 dir = (centerPoint.position - spawnPos).normalized;
            dir.y += 0.5f; // ���� ��¦ Ƣ��
            dir.Normalize();

            float speed = Random.Range(minSpeed, maxSpeed);
            rb.velocity = dir * speed;
        }
    }

}
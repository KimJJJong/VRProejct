using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [Header("���� ������Ʈ ����")]
    [SerializeField] private GameObject[] normalTargets;   // ����?ũ��?�� Ÿ��
    [SerializeField] private GameObject minusTarget;       // ü�� ���ҿ� ������Ʈ
    [SerializeField] private GameObject blockerObject;     // �þ� ����?��

    /// <summary>
    /// ��ȯ ��� ���� �ʿ� : ���� �߾ӿ��� ���ڱ� ��ȯ�Ǵ� ���� ���°�?
    /// </summary>
    [Header("���� ��ġ ����")]
    [SerializeField] private Vector3 spawnCenter;
    [SerializeField] private Vector3 spawnRange;

    [Header("���� �ӵ� ����")]
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
        Debug.Log("[SpawnManager] ���� ����");
        currentSpawnInterval = baseSpawnInterval;
        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    public void StopSpawning()
    {
        Debug.Log("[SpawnManager] ���� ����");
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
    /// ���̵��� ���� spawn interval�� ���ҽ��� ���̵��� ������ų �� ����
    /// </summary>
    public void ReduceSpawnInterval()
    {
        currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - spawnIntervalReduction);
    }

    private void SpawnRandomObject()
    {
        float rand = Random.value;
        GameObject prefabToSpawn = null;

        if (rand < 0.7f) // �Ϲ� Ÿ�� 70%
        {
            prefabToSpawn = normalTargets[Random.Range(0, normalTargets.Length)];
        }
        else if (rand < 0.9f) // ���̳ʽ� ������Ʈ 20%
        {
            prefabToSpawn = minusTarget;
        }
        else // �þ� ���� ������Ʈ 10%
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

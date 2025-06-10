//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using static TargetObject;

//public class SpawnManager : MonoBehaviour
//{
//    public static SpawnManager Instance { get; private set; }

//    [Header("���� ������Ʈ ����")]
//    //[SerializeField] private GameObject[] normalTargets;   // ����?ũ��?�� Ÿ��
//    //[SerializeField] private GameObject minusTarget;       // ü�� ���ҿ� ������Ʈ
//    //[SerializeField] private GameObject blockerObject;     // �þ� ����?��

//    /// <summary>
//    /// ��ȯ ��� ���� �ʿ� : ���� �߾ӿ��� ���ڱ� ��ȯ�Ǵ� ���� ���°�?
//    /// </summary>
//    [Header("���� ���� ����")]
//    [SerializeField] private Transform[] spawnZones; // < ���� �߰�
//    [SerializeField] private Transform centerPoint;  // �߾� 


//    [Header("���� �ӵ� ����")]
//    [SerializeField] private float baseSpawnInterval = 1.0f;
//    [SerializeField] private float spawnIntervalReduction = 0.1f;
//    [SerializeField] private float minSpawnInterval = 0.3f;
//    [SerializeField] private float minSpeed = 5f;
//    [SerializeField] private float maxSpeed = 10f;

//    private Coroutine SpawnRoutine;
//    private float currentSpawnInterval;

//    private void Awake()
//    {
//        if (Instance == null)
//            Instance = this;
//        else
//            Destroy(gameObject);
//    }

//    public void StartSpawning()
//    {
//        Debug.Log("[SpawnManager] ���� ����");
//        currentSpawnInterval = baseSpawnInterval;
//        SpawnRoutine = StartCoroutine(SpawnLoop());
//    }

//    public void StopSpawning()
//    {
//        Debug.Log("[SpawnManager] ���� ����");
//        if (SpawnRoutine != null)
//            StopCoroutine(SpawnRoutine);
//    }

//    private IEnumerator SpawnLoop()
//    {
//        while (GameManager.Instance.IsGamePlaying)
//        {
//            SpawnRandomObject();
//            yield return new WaitForSeconds(currentSpawnInterval);
//        }
//    }

//    /// <summary>
//    /// ���̵��� ���� spawn interval�� ���ҽ��� ���̵��� ������ų �� ����
//    /// </summary>
//    public void ReduceSpawnInterval()
//    {
//        currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - spawnIntervalReduction);
//    }


//    private void SpawnRandomObject()
//    {
//        TargetObject.TargetType targetType;

//        float rand = Random.value;

//        if (rand < 0.85f) // �Ϲ� Ÿ�� 70
//        {
//            // TargetObjectPoolManager������ �߰�, ����
//            var normalTypes = new[]
//            {
//            TargetObject.TargetType.Large,
//            TargetObject.TargetType.Medium,
//            TargetObject.TargetType.Small,
//            TargetObject.TargetType.Tiny
//             };

//            targetType = normalTypes[Random.Range(0, normalTypes.Length)];
//        }
//        else if (rand < 0.9f)
//        {
//            var minusTypes = new[]
//           {
//            TargetObject.TargetType.Heal
//             };

//            targetType = minusTypes[Random.Range(0, minusTypes.Length)];
//        }
//        else if (rand < 0.95f) // ���̳ʽ� ������Ʈ 20
//        {
//            var minusTypes = new[]
//            {
//            TargetObject.TargetType.Minus_Large,
//            TargetObject.TargetType.Minus_Medium,
//            TargetObject.TargetType.Minus_Small
//             };

//            targetType = minusTypes[Random.Range(0, minusTypes.Length)];
//        }
//        else // �þ� ���� ������Ʈ 10
//        {
//            targetType = TargetObject.TargetType.Blocker;
//        }

//        // ���� ���� ��ġ ����
//        Transform selectedZone = spawnZones[Random.Range(0, spawnZones.Length)];
//        Vector3 spawnPos = selectedZone.position + Random.insideUnitSphere * 0.5f;

//        // Ǯ���� ������
//        TargetObject spawned = TargetObjectPoolManager.Instance.Get(targetType, spawnPos);

//        // ���ư��� ���� ����
//        Rigidbody rb = spawned.GetComponent<Rigidbody>();
//        if (rb != null && centerPoint != null)
//        {
//            Vector3 dir = (centerPoint.position - spawnPos).normalized;
//            dir.y += 0.5f; // ���� ��¦ Ƣ��
//            dir.Normalize();

//            float speed = Random.Range(minSpeed, maxSpeed);
//            rb.velocity = dir * speed;
//        }
//    }

//}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static TargetObject;

[System.Serializable]
public class SpawnStage
{
    public float startTime;
    public float endTime;
    public float spawnInterval;
    public float speedMultiplier = 1f;

    [Range(0, 10)] public int largeRate = 0;
    [Range(0, 10)] public int mediumRate = 0;
    [Range(0, 10)] public int smallRate = 0;
    [Range(0, 10)] public int tinyRate = 0;

    [Range(0, 100)] public float minusProbability = 0f;
    [Range(0, 100)] public float blockerProbability = 0f;
    [Range(0, 100)] public float healProbability = 0f;
}

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [Header("���� ������Ʈ ����")]
    [SerializeField] private Transform[] spawnZones;
    [SerializeField] private Transform centerPoint;

    [Header("���� �ܰ� ����")]
    public List<SpawnStage> spawnStages;

    private Coroutine SpawnRoutine;
    private float elapsedTime;

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
        elapsedTime = 0f;
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
            SpawnStage stage = GetCurrentStage();
            if (stage != null)
            {
                SpawnRandomObject(stage);
                yield return new WaitForSeconds(stage.spawnInterval);
                elapsedTime += stage.spawnInterval;
            }
            else
            {
                yield return null;
            }
        }
    }

    private SpawnStage GetCurrentStage()
    {
        foreach (var stage in spawnStages)
        {
            if (elapsedTime >= stage.startTime && elapsedTime < stage.endTime)
                return stage;
        }
        return null;
    }

    private void SpawnRandomObject(SpawnStage stage)
    {
        TargetObject.TargetType targetType = GetTargetType(stage);

        Transform selectedZone = GetWeightedRandomZone();
        Vector3 spawnPos = selectedZone.position + Random.insideUnitSphere * 0.5f;

        TargetObject spawned = TargetObjectPoolManager.Instance.Get(targetType, spawnPos);

        if (spawned == null)
        {
            Debug.LogError($"[SpawnManager] ���� ����: {targetType}");
            return;
        }

        Rigidbody rb = spawned.GetComponent<Rigidbody>();
        if (rb != null && centerPoint != null)
        {
            Vector3 dir = (centerPoint.position - spawnPos).normalized;
            dir.y += 0.5f;
            dir.Normalize();

            float baseSpeed = 10f;
            float speed = baseSpeed * stage.speedMultiplier;
            rb.velocity = dir * speed;
        }
    }

    private TargetObject.TargetType GetTargetType(SpawnStage stage)
    {
        float rand = Random.Range(0f, 100f);

        if (rand < stage.healProbability)
            return TargetType.Heal;
        rand -= stage.healProbability;

        if (rand < stage.minusProbability)
            return GetRandomMinus();
        rand -= stage.minusProbability;

        if (rand < stage.blockerProbability)
            return TargetType.Blocker;

        // Normal objects by weight
        List<TargetObject.TargetType> normalList = new List<TargetObject.TargetType>();
        normalList.AddRange(AddMultiple(TargetType.Large, stage.largeRate));
        normalList.AddRange(AddMultiple(TargetType.Medium, stage.mediumRate));
        normalList.AddRange(AddMultiple(TargetType.Small, stage.smallRate));
        normalList.AddRange(AddMultiple(TargetType.Tiny, stage.tinyRate));

        if (normalList.Count == 0)
            return TargetType.Small;

        return normalList[Random.Range(0, normalList.Count)];
    }

    private TargetType GetRandomMinus()
    {
        TargetType[] minusTypes = new[]
        {
            TargetType.Minus_Large,
            TargetType.Minus_Medium,
            TargetType.Minus_Small
        };
        return minusTypes[Random.Range(0, minusTypes.Length)];
    }

    private List<TargetObject.TargetType> AddMultiple(TargetObject.TargetType type, int count)
    {
        List<TargetObject.TargetType> list = new List<TargetObject.TargetType>();
        for (int i = 0; i < count; i++)
            list.Add(type);
        return list;
    }

    private Transform GetWeightedRandomZone()
    {
        float[] weights = new float[] { 20, 20, 20, 20, 10 }; // ��, ��, ��, ��, ����
        float total = 0f;
        foreach (float w in weights) total += w;

        float rand = Random.Range(0, total);
        for (int i = 0; i < weights.Length; i++)
        {
            if (rand < weights[i])
                return spawnZones[i % spawnZones.Length];
            rand -= weights[i];
        }

        return spawnZones[0];
    }
}

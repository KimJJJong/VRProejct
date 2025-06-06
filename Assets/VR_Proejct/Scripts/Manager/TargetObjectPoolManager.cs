using UnityEngine;
using System.Collections.Generic;

public class TargetObjectPoolManager : MonoBehaviour
{
    public static TargetObjectPoolManager Instance { get; private set; }

    [SerializeField] private TargetObject[] prefabTypes;
    [SerializeField] private int defaultSize = 20;

    private Dictionary<TargetObject.TargetType, Queue<TargetObject>> pools = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        foreach (var prefab in prefabTypes)
        {
            var type = prefab.targetType;
            pools[type] = new Queue<TargetObject>();

            for (int i = 0; i < defaultSize; i++)
            {
                var obj = Instantiate(prefab, transform);
                obj.gameObject.SetActive(false);
                pools[type].Enqueue(obj);
            }
        }
    }

    public TargetObject Get(TargetObject.TargetType type, Vector3 position)
    {
        if (!pools.ContainsKey(type))
        {
            Debug.LogError($"[ObjectPoolManager] No pool for type: {type}");
            return null;
        }

        TargetObject obj;
        if (pools[type].Count > 0)
        {
            obj = pools[type].Dequeue();
        }
        else
        {
            var prefab = FindPrefab(type);
            obj = Instantiate(prefab, transform);
        }

        obj.transform.position = position;
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Release(TargetObject obj)
    {
        obj.gameObject.SetActive(false);
        pools[obj.targetType].Enqueue(obj);
    }

    private TargetObject FindPrefab(TargetObject.TargetType type)
    {
        foreach (var p in prefabTypes)
        {
            if (p.targetType == type)
                return p;
        }

        Debug.LogError($"[ObjectPoolManager] Prefab for {type} not found!");
        return null;
    }
}

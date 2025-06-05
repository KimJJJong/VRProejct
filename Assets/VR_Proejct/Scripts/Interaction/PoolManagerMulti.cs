using UnityEngine;
using System.Collections.Generic;

public class PoolManagerMulti : MonoBehaviour
{
    [System.Serializable]
    public class PoolItem
    {
        public GameObject prefab;
        public int poolSize = 10;
    }

    public List<PoolItem> poolItems;

    private Dictionary<GameObject, List<GameObject>> pools;

    void Awake()
    {
        pools = new Dictionary<GameObject, List<GameObject>>();

        foreach (var item in poolItems)
        {
            List<GameObject> pool = new List<GameObject>();
            for (int i = 0; i < item.poolSize; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pool.Add(obj);
            }
            pools[item.prefab] = pool;
        }
    }

    public GameObject GetObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!pools.ContainsKey(prefab))
        {
            Debug.LogWarning($"[PoolManager] '{prefab.name}'�� ���� Ǯ�� �������� �ʽ��ϴ�.");
            return null;
        }

        foreach (var obj in pools[prefab])
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }
        }

        return null; // Ǯ�� ���� ������ null
    }
}

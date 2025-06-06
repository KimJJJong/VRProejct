using UnityEngine;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;

    private Rigidbody rb;
    private HashSet<GameObject> hitTargets = new(); // �ߺ� ������
    private int comboCount = 0;
    private float shootTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        comboCount = 0;
        hitTargets.Clear();
        shootTime = Time.time;

        Invoke(nameof(ReturnToPool), lifeTime);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.IsGamePlaying)
            return;

        if (hitTargets.Contains(other.gameObject))
            return;

        hitTargets.Add(other.gameObject);

        // Target�� �����ߴ��� Ȯ��
        TargetObject target = other.GetComponent<TargetObject>();
        if (target != null)
        {
            target.OnHit(transform.position);
            comboCount++;

            if (comboCount >= 2)
            {
                ComboManager.Instance.EvaluateCombo(comboCount, transform.position);
            }


        }
    }

    private void ReturnToPool()
    {
        //if (comboCount >= 2)
        //{
        //    ComboManager.Instance.EvaluateCombo(comboCount);
        //}

        gameObject.SetActive(false); // ObjectPool�� �ִٸ� Ǯ�� ��ȯ
    }
}

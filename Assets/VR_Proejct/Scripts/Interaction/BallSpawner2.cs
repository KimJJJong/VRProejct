using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner2 : MonoBehaviour
{
    public PoolManager ballPool; // 공도 bullet pool을 재활용
    public Transform firePoint;
    public float fireInterval = 2f;
    public float fireForce = 10f;

    void Start()
    {
        InvokeRepeating("FireBall", 0f, fireInterval);
    }

    void FireBall()
    {
        GameObject ball = ballPool.GetObject(firePoint.position, firePoint.rotation);

        if (ball != null)
        {
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float randomX = Random.Range(-0.5f, 0.5f);
                Vector3 direction = new Vector3(randomX, 2f, 0f);
                rb.velocity = direction.normalized * fireForce;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (firePoint == null) return;

        Gizmos.color = Color.red;

        for (float x = -0.5f; x <= 0.5f; x += 0.25f)
        {
            Vector3 dir = new Vector3(x, 1f, 0f).normalized;
            Gizmos.DrawRay(firePoint.position, dir * 3f);
        }
    }
}

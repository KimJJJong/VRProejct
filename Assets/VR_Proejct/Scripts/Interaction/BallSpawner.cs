using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform firePoint;
    public float fireInterval = 2f;
    public float fireForce = 10f;
    public float ballLifetime = 5f;
    public float valval;

    void Start()
    {
        InvokeRepeating("FireBall", 0f, fireInterval);
    }

    void FireBall()
    {
        GameObject ball = Instantiate(ballPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float randomX = Random.Range(-0.5f, 0.5f);
            Vector3 direction = new Vector3(randomX, 2f, 0f);
            rb.AddForce(direction * fireForce, ForceMode.Impulse);

            Debug.DrawRay(firePoint.position, direction.normalized * 5f, Color.cyan, 5f);
        }

        Destroy(ball, ballLifetime);
    }

    private void OnDrawGizmos()
    {
        if (firePoint == null) return;

        Gizmos.color = Color.red;

        for (float x = -0.5f; x <= 0.5f; x += 0.25f)
        {
            Vector3 dir = new Vector3(x, 2f, 0f).normalized;
            Gizmos.DrawRay(firePoint.position, dir * 3f);
        }
    }
}

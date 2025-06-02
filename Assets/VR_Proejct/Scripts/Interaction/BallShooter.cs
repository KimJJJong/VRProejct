using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public float forceMultiplier = 10f; // 발사 세기

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 shootDirection = new Vector3(0f, 2f, 0.5f).normalized;
            rb.AddForce(shootDirection * forceMultiplier, ForceMode.Impulse);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public PoolManager bulletPool;
    public Transform firePoint;
    public float fireforce = 20f;

    public void Fire()
    {
        if (GameManager.Instance.IsGamePlaying == true)
        {
            if (bulletPool == null || firePoint == null) return;

            Vector3 offset = firePoint.right * 0.05f + firePoint.forward * 0.23f + firePoint.up * -0.08f;
            Vector3 spawnPos = firePoint.position + offset;
            GameObject bullet = bulletPool.GetObject(spawnPos, firePoint.rotation);

            if (bullet != null)
            {
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.velocity = firePoint.forward * fireforce;
            }
        }
    }
}

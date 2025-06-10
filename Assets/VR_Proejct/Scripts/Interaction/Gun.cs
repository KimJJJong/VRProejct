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
        if (bulletPool == null || firePoint == null) return;

        AudioManager.Instance.PlaySFX("SFX_Shot");

        Vector3 offset = firePoint.right * 0.02f + firePoint.forward * 0.21f + firePoint.up * 0.01f;
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

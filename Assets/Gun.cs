using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint; // �Ѿ� ������ ��ġ
    public float fireForce = 20f;

    public void Fire()
    {
        if (bulletPrefab == null) return;

        Vector3 offset = firePoint.right * 0.02f + firePoint.forward * 0.21f + firePoint.up * 0.01f; // �ѱ� ���������� 5cm
        Vector3 spawnPos = firePoint.position + offset;
        GameObject bullet = Instantiate(bulletPrefab, spawnPos, firePoint.rotation);
        StartCoroutine(ApplyForceNextFrame(bullet));
    }


    private IEnumerator ApplyForceNextFrame(GameObject bullet)
    {
        yield return null; // �� ������ ���

        if (bullet != null)
        {
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
                rb.velocity = firePoint.forward * fireForce;
        }
    }
}

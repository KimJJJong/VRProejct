using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 5f;

    void Start()
    {
        /*Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * speed;
        }*/

        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ShootingObj")) 
        {
            Debug.Log("¿ÀºêÁ§Æ®¿¡ ºÎµúÈû!");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}

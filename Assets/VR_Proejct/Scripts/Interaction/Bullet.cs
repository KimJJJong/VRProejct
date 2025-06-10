using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ShootingObj"))
        {
            Debug.Log("충돌 감지");
            //collision.gameObject.SetActive(false); 
            //gameObject.SetActive(false);          
        }
    }
}

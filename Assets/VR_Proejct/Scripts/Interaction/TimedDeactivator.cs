using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeactivator : MonoBehaviour
{
    public float Lifetime = 1.5f;

    void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(Deactivate), Lifetime);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

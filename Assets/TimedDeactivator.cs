using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeactivator : MonoBehaviour
{
    public float lifetime = 1.5f;

    void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(Deactivate), lifetime);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

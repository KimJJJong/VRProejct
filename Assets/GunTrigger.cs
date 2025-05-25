using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunTrigger : MonoBehaviour
{
    private InputAction triggerAction;
    public Gun gun;  // Fire() 함수가 들어 있는 스크립트

    void OnEnable()
    {
        triggerAction = new InputAction(
            type: InputActionType.Button,
            binding: "<XRController>{RightHand}/trigger"
        );
        triggerAction.Enable();
    }

    void OnDisable()
    {
        triggerAction.Disable();
    }

    void Update()
    {
        if (triggerAction.WasPerformedThisFrame())
        {
            gun.Fire();
        }
    }
}

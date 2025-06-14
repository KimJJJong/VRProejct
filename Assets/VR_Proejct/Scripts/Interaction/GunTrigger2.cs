using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunTrigger2 : MonoBehaviour
{
    private InputAction triggerAction;
    public Gun gun;

   void OnEnable()
    {
        triggerAction = new InputAction(
            type: InputActionType.Button,
            binding: "<XRController>{RightHand}/trigger"
        );

        Debug.Log("�۵�??");
        triggerAction.performed += ctx => gun.Fire();
        triggerAction.Enable();
    }

    void OnDisable()
    {
        triggerAction.Disable();
        triggerAction.performed -= ctx => gun.Fire();
    }
}

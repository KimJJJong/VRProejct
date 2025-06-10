using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class VRButtonInvoker : MonoBehaviour
{
    [System.Serializable]
    public class ButtonActionPair
    {
        public Button button;
        public InputActionReference action;
    }

    [SerializeField] private List<ButtonActionPair> buttonActions;

    private void OnEnable()
    {
        foreach (var pair in buttonActions)
        {
            pair.action.action.Enable();
            pair.action.action.performed += ctx => OnButtonPressed(pair.button);
        }
    }

    private void OnDisable()
    {
        foreach (var pair in buttonActions)
        {
            pair.action.action.performed -= ctx => OnButtonPressed(pair.button);
            pair.action.action.Disable();
        }
    }

    private void OnButtonPressed(Button button)
    {
        button.onClick.Invoke();
    }
}

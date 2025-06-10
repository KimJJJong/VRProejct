using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VRButtonInvoker : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private InputActionReference aButtonAction;

    private void OnEnable()
    {
        aButtonAction.action.Enable();
        aButtonAction.action.performed += OnAPressed;
    }

    private void OnDisable()
    {
        aButtonAction.action.performed -= OnAPressed;
        aButtonAction.action.Disable();
    }

    private void OnAPressed(InputAction.CallbackContext ctx)
    {
        startButton.onClick.Invoke();  // °Á ¿Ã∞‘ «ŸΩ…!
    }
}

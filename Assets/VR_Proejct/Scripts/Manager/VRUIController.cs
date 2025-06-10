using UnityEngine;
using UnityEngine.InputSystem;

public class VRUIController : MonoBehaviour
{
    public InputActionReference clickAction; // 인스펙터에 연결할 액션

    private void OnEnable()
    {
        clickAction.action.Enable();
        clickAction.action.performed += OnClick;
    }

    private void OnDisable()
    {
        clickAction.action.performed -= OnClick;
        clickAction.action.Disable();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log("눌려짐");
        UIManager.Instance.OnClickStartButton_BeforeGuide(); // 예시로 Start 버튼 눌린 효과
    }
}

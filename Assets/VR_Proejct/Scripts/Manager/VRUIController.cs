using UnityEngine;
using UnityEngine.InputSystem;

public class VRUIController : MonoBehaviour
{
    public InputActionReference clickAction; // �ν����Ϳ� ������ �׼�

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
        Debug.Log("������");
        UIManager.Instance.OnClickStartButton_BeforeGuide(); // ���÷� Start ��ư ���� ȿ��
    }
}

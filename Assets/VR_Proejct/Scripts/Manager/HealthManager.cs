using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }

    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UIManager.Instance.UpdateHealth(currentHealth);
    }

    /// <summary>
    /// ���̳ʼ� Object Hit�� ȣ��
    /// </summary>
    public void TakeDamage()
    {
        if (GameManager.Instance.IsGamePlaying == false)
            return;

        currentHealth--;
        Debug.Log($"[HealthManager] ���� �߻� ���� HP: {currentHealth}");
        ComboBlcokManager.Instance.GetDamageEffect();
        UIManager.Instance.UpdateHealth(currentHealth);

        if (currentHealth <= 0)
        {
            GameManager.Instance.ForceEndGame();
        }
    }
    /// <summary>
    /// �� ������Ʈ ���� �� ȣ��
    /// </summary>
    public void Heal()
    {
        if (!GameManager.Instance.IsGamePlaying)
            return;

        if (currentHealth >= maxHealth)
        {
            Debug.Log("[HealthManager] HP�� �ִ��Դϴ�. ȸ�� ����");
            return;
        }

        currentHealth++;
        Debug.Log($"[HealthManager] ȸ��! ���� HP: {currentHealth}");

        UIManager.Instance.UpdateHealth(currentHealth);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public bool IsAlive => currentHealth > 0;
}

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
    /// 마이너서 Object Hit시 호출
    /// </summary>
    public void TakeDamage()
    {
        if (GameManager.Instance.IsGamePlaying == false)
            return;

        currentHealth--;
        Debug.Log($"[HealthManager] 피해 발생 현재 HP: {currentHealth}");
        ComboBlcokManager.Instance.GetDamageEffect();
        UIManager.Instance.UpdateHealth(currentHealth);

        if (currentHealth <= 0)
        {
            GameManager.Instance.ForceEndGame();
        }
    }
    /// <summary>
    /// 힐 오브젝트 적중 시 호출
    /// </summary>
    public void Heal()
    {
        if (!GameManager.Instance.IsGamePlaying)
            return;

        if (currentHealth >= maxHealth)
        {
            Debug.Log("[HealthManager] HP가 최대입니다. 회복 생략");
            return;
        }

        currentHealth++;
        Debug.Log($"[HealthManager] 회복! 현재 HP: {currentHealth}");

        UIManager.Instance.UpdateHealth(currentHealth);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public bool IsAlive => currentHealth > 0;
}

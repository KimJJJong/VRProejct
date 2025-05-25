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

    public void TakeDamage()
    {
        if (GameManager.Instance.IsGamePlaying == false)
            return;

        currentHealth--;
        Debug.Log($"[HealthManager] 피해 발생 현재 HP: {currentHealth}");

        UIManager.Instance.UpdateHealth(currentHealth);

        if (currentHealth <= 0)
        {
            GameManager.Instance.ForceEndGame();
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public bool IsAlive => currentHealth > 0;
}

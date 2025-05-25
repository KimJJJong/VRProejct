using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance { get; private set; }

    [SerializeField] private string comboSFX = "Combo";
    [SerializeField] private GameObject comboEffectPrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void EvaluateCombo(int hitCount, Vector3 displayPos)
    {
        if (hitCount < 2) return;

        ScoreManager.Instance.AddComboBonus(hitCount);

        UIManager.Instance.ShowFloatingText(displayPos, GetBonus(hitCount));
        AudioManager.Instance.PlaySFX(comboSFX);

        if (comboEffectPrefab)
            Instantiate(comboEffectPrefab, displayPos, Quaternion.identity);

        Debug.Log($"[ComboManager] ÄÞº¸ {hitCount} Hit+{GetBonus(hitCount)}Á¡");
    }

    private int GetBonus(int count) => count switch
    {
        2 => 20,
        3 => 50,
        _ => 100
    };
}

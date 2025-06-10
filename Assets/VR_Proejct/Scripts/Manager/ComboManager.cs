using UnityEngine;

public class ComboBlcokManager : MonoBehaviour
{
    public static ComboBlcokManager Instance { get; private set; }

    [SerializeField] private Transform playerPosition;

    [SerializeField] private GameObject[] comboEffectPrefab;
    [SerializeField] private GameObject blockEffectPrefab;
    [SerializeField] private GameObject damagedEffectPrefab;

    [SerializeField] private string comboSFX = "SFX_Combo";

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
        
        if (hitCount == 2 && comboEffectPrefab[0])
            Instantiate(comboEffectPrefab[0], displayPos, Quaternion.identity);
        else if( hitCount == 3 && comboEffectPrefab[1])
            Instantiate(comboEffectPrefab[1], displayPos, Quaternion.identity);
        else
            Instantiate(comboEffectPrefab[2], displayPos, Quaternion.identity);

        Debug.Log($"[ComboManager] ÄÞº¸ {hitCount} Hit+{GetBonus(hitCount)}Á¡");
    }

    private int GetBonus(int count) => count switch
    {
        2 => 20,
        3 => 50,
        _ => 100
    };

    public void EnvokeBlockEffect()
    {
        Instantiate(blockEffectPrefab, playerPosition.position - new Vector3(1,0, 2), Quaternion.identity);

        Debug.Log($"[Blocked] !!!!!!");
    }

    public void GetDamageEffect()
    {
        Instantiate(damagedEffectPrefab, playerPosition.position - new Vector3( 1, 0.5f, 0.8f ), Quaternion.identity);

    }
}

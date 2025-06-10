using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int CurrentScore { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        UIManager.Instance.UpdateScore(CurrentScore);
    }

    public void AddScore(int baseScore, Vector3 pos)
    {
        CurrentScore += baseScore;
        UIManager.Instance.UpdateScore(CurrentScore);
        UIManager.Instance.ShowFloatingText(pos, baseScore);
    }

    public void AddComboBonus(int hitCount)
    {
        int bonus = hitCount switch
        {
            2 => 20,
            3 => 50,
            _ => 100
        };

        CurrentScore += bonus;
        UIManager.Instance.UpdateScore(CurrentScore);
    }
}

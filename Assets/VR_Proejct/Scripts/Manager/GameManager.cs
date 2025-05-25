using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("���� ����")]
    [SerializeField] private float gameDuration = 90f;
    private float remainingTime;
    private bool isPlaying = false;

    // �̺�Ʈ �ݹ�
    public System.Action OnGameStart;
    public System.Action OnGameEnd;

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

    private void Update()
    {
        if (!isPlaying) return;

        remainingTime -= Time.deltaTime;
        UIManager.Instance.UpdateTime(remainingTime);

        if (remainingTime <= 0f)
        {
            EndGame();
        }
    }

    /// <summary>
    /// ���� ���� ó�� from UIManager
    /// </summary>
    public void StartGame()
    {
        Debug.Log("[GameManager] ���� ����");

        remainingTime = gameDuration;
        isPlaying = true;

        UIManager.Instance.ShowHUD(true);
        UIManager.Instance.UpdateTime(remainingTime);

        SpawnManager.Instance.StartSpawning();
        DifficultyManager.Instance.StartDifficulty();

        ScoreManager.Instance.ResetScore();
        HealthManager.Instance.ResetHealth();

        OnGameStart?.Invoke();
    }

    /// <summary>
    /// ���� ���� ó��
    /// </summary>
    private void EndGame()
    {
        if (!isPlaying) return;

        Debug.Log("[GameManager] ���� ����");

        isPlaying = false;

        SpawnManager.Instance.StopSpawning();
        DifficultyManager.Instance.StopDifficulty();

        UIManager.Instance.ShowGameOver(ScoreManager.Instance.CurrentScore);

        OnGameEnd?.Invoke();
    }

    /// <summary>
    /// �ܺο��� ���� ���� (ü�� 0 ��)
    /// </summary>
    public void ForceEndGame()
    {
        if (isPlaying)
        {
            EndGame();
        }
    }

    public bool IsGamePlaying => isPlaying;
    public float RemainingTime => remainingTime;
}

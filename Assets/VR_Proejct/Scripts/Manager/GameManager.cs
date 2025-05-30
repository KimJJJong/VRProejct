using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("게임 설정")]
    [SerializeField] private float gameDuration = 90f;
    private float remainingTime;
    private bool isPlaying = false;

    // 이벤트 콜백
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
    /// 게임 시작 처리 from UIManager
    /// </summary>
    public void StartGame()
    {
        Debug.Log("[GameManager] 게임 시작");

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
    /// 게임 종료 처리
    /// </summary>
    private void EndGame()
    {
        if (!isPlaying) return;

        Debug.Log("[GameManager] 게임 종료");

        isPlaying = false;

        SpawnManager.Instance.StopSpawning();
        DifficultyManager.Instance.StopDifficulty();

        UIManager.Instance.ShowGameOver(ScoreManager.Instance.CurrentScore);

        OnGameEnd?.Invoke();
    }

    /// <summary>
    /// 외부에서 강제 종료 (체력 0 등)
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

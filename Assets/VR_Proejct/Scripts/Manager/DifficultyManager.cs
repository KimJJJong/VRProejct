using UnityEngine;
using System.Collections;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    [Header("난이도 조절 설정")]
    [SerializeField] private float difficultyInterval = 10f; // 몇 초마다 난이도 증가
    [SerializeField] private int maxDifficultyLevel = 5;    // 추후 조정

    private int currentLevel = 0;
    private Coroutine difficultyRoutine;

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

    public void StartDifficulty()
    {
        currentLevel = 0;
        difficultyRoutine = StartCoroutine(DifficultyLoop());
    }

    public void StopDifficulty()
    {
        if (difficultyRoutine != null)
            StopCoroutine(difficultyRoutine);
    }

    private IEnumerator DifficultyLoop()
    {
        while (GameManager.Instance.IsGamePlaying)
        {
            yield return new WaitForSeconds(difficultyInterval);

            if (currentLevel < maxDifficultyLevel)
            {
                currentLevel++;
                Debug.Log($"[DifficultyManager] 난이도 상승! 현재 레벨: {currentLevel}");

                // 난이도 변화에 따른 효과들
                SpawnManager.Instance.ReduceSpawnInterval();

                // 추후 확장 가능 : 옵젝 속토, 확률, etc...
            }
        }
    }

    public int GetDifficultyLevel() => currentLevel;
}

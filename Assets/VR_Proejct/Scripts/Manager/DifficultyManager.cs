using UnityEngine;
using System.Collections;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    [Header("���̵� ���� ����")]
    [SerializeField] private float difficultyInterval = 10f; // �� �ʸ��� ���̵� ����
    [SerializeField] private int maxDifficultyLevel = 5;    // ���� ����

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
                Debug.Log($"[DifficultyManager] ���̵� ���! ���� ����: {currentLevel}");

                // ���̵� ��ȭ�� ���� ȿ����
                SpawnManager.Instance.ReduceSpawnInterval();

                // ���� Ȯ�� ���� : ���� ����, Ȯ��, etc...
            }
        }
    }

    public int GetDifficultyLevel() => currentLevel;
}

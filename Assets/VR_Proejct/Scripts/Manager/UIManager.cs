using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Panels")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject settingsPanel;

    [Header("HUD Elements")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image[] healthIcons;
    [SerializeField] private GameObject floatingTextPrefab;

    [Header("Game Over UI")]
    [SerializeField] private TextMeshProUGUI finalScoreText;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        ShowStartMenu();
    }

    #region 시작 화면 & 설정

    public void ShowStartMenu()
    {
        startPanel.SetActive(true);
        hudPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void OnClickStartButton()
    {
        startPanel.SetActive(false);
        GameManager.Instance.StartGame();
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }

    public void OnClickSettingsButton()
    {
        settingsPanel.SetActive(true);
    }

    public void OnClickCloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    #endregion

    #region HUD

    public void ShowHUD(bool show)
    {
        hudPanel.SetActive(show);
    }

    public void UpdateTime(float time)
    {
        timeText.text = $"Time: {Mathf.CeilToInt(time)}";
    }

    public void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < healthIcons.Length; i++)
        {
            healthIcons[i].enabled = i < currentHealth;
        }
    }

    public void ShowFloatingText(Vector3 worldPos, int amount)
    {
        if (floatingTextPrefab == null) return;

        GameObject obj = Instantiate(floatingTextPrefab, worldPos, Quaternion.identity);
        obj.GetComponentInChildren<TextMeshProUGUI>().text = $"+{amount}";
        Destroy(obj, 1.5f); // 자동 제거
    }

    #endregion

    #region 게임 오버

    public void ShowGameOver(int finalScore)
    {
        hudPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        finalScoreText.text = $"Final Score: {finalScore}";
    }

    public void OnClickRestartButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    #endregion
}

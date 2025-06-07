using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    public float BGMVolume { get; private set; } = 1f;
    public float SFXVolume { get; private set; } = 1f;
    public bool VibrationOn { get; private set; } = true;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // 초기 값 로딩
        BGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // 초기 적용
        AudioManager.Instance?.SetBGMVolume(BGMVolume);
        AudioManager.Instance?.SetSFXVolume(SFXVolume);
    }

    public void SetBGMVolume(float value)
    {
        BGMVolume = value;
        AudioManager.Instance.SetBGMVolume(value);
    }

    public void SetSFXVolume(float value)
    {
        SFXVolume = value;
        AudioManager.Instance.SetSFXVolume(value);
    }

    public void SetVibration(bool on)
    {
        VibrationOn = on;
        Debug.Log($"[Settings] Vibration: {on}");
    }
}

using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Clip List")]
    [SerializeField] private List<AudioClipData> audioClips;

    private Dictionary<string, AudioClip> clipDict = new();

    private float bgmVolume = 1.0f;
    private float sfxVolume = 1.0f;

    [System.Serializable]
    public class AudioClipData
    {
        public string name;
        public AudioClip clip;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (var clip in audioClips)
        {
            if (!clipDict.ContainsKey(clip.name))
                clipDict.Add(clip.name, clip.clip);
        }
    }

    public void PlayBGM(string name, bool loop = true)
    {
        if (clipDict.TryGetValue(name, out AudioClip clip))
        {
            bgmSource.clip = clip;
            bgmSource.loop = loop;
            bgmSource.volume = bgmVolume;
            bgmSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        if (clipDict.TryGetValue(name, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip, sfxVolume);
        }
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        bgmSource.volume = bgmVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
    }
}

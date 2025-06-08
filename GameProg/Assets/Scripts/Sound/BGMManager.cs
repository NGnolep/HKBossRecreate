using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    public AudioSource audioSource;
    public AudioClip defaultBGM;
    public float fadeDuration = 1f;

    private Coroutine currentFade;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            PlayMusic(defaultBGM);
        }
        else
        {
            StopMusic();
        }
    }

    void Start()
    {
        PlayMusic(defaultBGM);
    }

    public void PlayMusic(AudioClip newClip)
    {
        if (audioSource.clip == newClip) return;

        if (currentFade != null)
        {
            StopCoroutine(currentFade);
        }

        currentFade = StartCoroutine(FadeToNewMusic(newClip));
    }

    public void StopMusic()
    {
        if (currentFade != null)
        {
            StopCoroutine(currentFade);
            currentFade = null;
        }
        audioSource.Stop();
        audioSource.clip = null;
    }

    private IEnumerator FadeToNewMusic(AudioClip newClip)
    {
        // Fade out
        float startVolume = audioSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0f, startVolume, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = startVolume;
        currentFade = null;
    }
}

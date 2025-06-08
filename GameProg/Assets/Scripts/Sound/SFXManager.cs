using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    public AudioSource audioSource;

    [Header("Player SFX")]
    public AudioClip playerSwing;
    public AudioClip playerDamage;
    public AudioClip playerDeath;

    [Header("Enemy SFX")]
    public AudioClip enemyDamage;
    public AudioClip enemyDeath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayPlayerSwing() => Play(playerSwing);
    public void PlayPlayerDamage() => Play(playerDamage);
    public void PlayPlayerDeath() => Play(playerDeath);
    public void PlayEnemyDamage() => Play(enemyDamage);
    public void PlayEnemyDeath() => Play(enemyDeath);

    private void Play(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}



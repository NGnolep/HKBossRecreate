using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBGM : MonoBehaviour
{
    public AudioClip bgmToPlay;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BGMManager.Instance.PlayMusic(bgmToPlay);
        }
    }
}

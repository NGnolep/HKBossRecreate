using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateCutscene : MonoBehaviour
{
    public PlayerMovement player;
    // public AudioSource hornetScreech; 
    public float screechDuration = 10f;
    public float fadeDuration = 3f;

    private bool isCutscenePlaying = false;

    void Start()
    {
        if (player == null)
            Debug.LogWarning("Player reference is missing in GateCutscene.");
        // if (hornetScreech == null)
        // Debug.LogWarning("Hornet screech AudioSource is missing in GateCutscene.");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayCutscene();
        }
    }
    public void PlayCutscene()
    {
        if (!isCutscenePlaying)
        {
            StartCoroutine(CutsceneRoutine());
        }
    }

    private IEnumerator CutsceneRoutine()
    {
        isCutscenePlaying = true;

        player.canMove = false;

        // hornetScreech.Play();

        yield return new WaitForSeconds(screechDuration);

        player.canMove = true;
    }

    
}


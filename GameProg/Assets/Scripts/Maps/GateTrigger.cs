using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public Animator gateAnimator;

    public CinemachineVirtualCamera cameraToActivate;
    public CinemachineVirtualCamera cameraToDeactivate;

    public ScreenFadeOut blackScreenFade;
    private bool triggered = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player") && triggered == false)
        {
            cameraToActivate.Priority = 20;
            cameraToDeactivate.Priority = 5;
            gateAnimator.SetTrigger("Arise");
            blackScreenFade.FadeOut();
            triggered = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public Animator gateAnimator;

    public CinemachineVirtualCamera cameraToActivate;
    public CinemachineVirtualCamera cameraToDeactivate;

    private bool triggered = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            cameraToActivate.Priority = 20;
            cameraToDeactivate.Priority = 5;
            gateAnimator.SetTrigger("Arise");
            triggered = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager: MonoBehaviour
{
    public Transform cam;
    public float parallaxFactor = 0.1f; // Very small

    private Vector3 lastCamPos;

    void Start()
    {
        if (cam == null) cam = Camera.main.transform;
        lastCamPos = cam.position;
    }

    void LateUpdate()
    {
        Vector3 delta = cam.position - lastCamPos;
        transform.position += new Vector3(delta.x * parallaxFactor, delta.y * parallaxFactor, 0);
        lastCamPos = cam.position;
    }
}

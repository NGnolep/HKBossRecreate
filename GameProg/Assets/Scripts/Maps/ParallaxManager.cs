using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layerTransform;
        public float parallaxFactor = 0.5f;
    }

    public ParallaxLayer[] layers;

    private Transform cam;
    private Vector3 previousCamPosition;

    private void Start()
    {
        cam = Camera.main.transform; // Cinemachine's main camera still works here
        previousCamPosition = cam.position;
    }

    private void LateUpdate()
    {
        Vector3 delta = cam.position - previousCamPosition;

        foreach (var layer in layers)
        {
            if (layer.layerTransform != null)
            {
                Vector3 move = new Vector3(delta.x * layer.parallaxFactor, delta.y * layer.parallaxFactor, 0);
                layer.layerTransform.position += move;
            }
        }

        previousCamPosition = cam.position;
    }
}

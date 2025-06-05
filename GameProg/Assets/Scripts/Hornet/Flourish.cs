using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flourish : MonoBehaviour
{
    private Animator animator;
    private float timer = 0f;
    public float flourishInterval = 3f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= flourishInterval)
        {
            timer = 0f;
            animator.SetTrigger("Flourish");
            Debug.Log("Flourish");
        }
    }
}

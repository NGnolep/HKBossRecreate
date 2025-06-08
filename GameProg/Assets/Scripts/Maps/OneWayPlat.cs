using UnityEngine;

public class OneWayPlat : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime = 0.5f;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(DisablePlatformTemporarily());
        }
    }

    private System.Collections.IEnumerator DisablePlatformTemporarily()
    {
        effector.rotationalOffset = 180f;
        yield return new WaitForSeconds(waitTime);
        effector.rotationalOffset = 0f; 
    }
}

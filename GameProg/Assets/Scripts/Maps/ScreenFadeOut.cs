using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFadeOut : MonoBehaviour
{
    public float fadeDuration = 1f;
    private Material fadeMat;
    private Color originalColor;
    private bool isFading = false;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        fadeMat = renderer.material;
        originalColor = fadeMat.color;
    }

    public void FadeOut()
    {
        if (!isFading)
            StartCoroutine(FadeToClear());
    }

    private IEnumerator FadeToClear()
    {
        isFading = true;
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            Color newColor = originalColor;
            newColor.a = Mathf.Lerp(1, 0, t / fadeDuration);
            fadeMat.color = newColor;
            yield return null;
        }

        // Fully clear
        Color finalColor = originalColor;
        finalColor.a = 0;
        fadeMat.color = finalColor;

        gameObject.SetActive(false); // Optional
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    private Image blackScreenImage;
    private bool isFading = false;

    private void Start()
    {
        blackScreenImage = GetComponent<Image>();
        blackScreenImage.color = new Color(0, 0, 0, 0);
    }

    public void FadeToBlack(float fadeDuration)
    {
        if (!isFading)
        {
            StartCoroutine(Fade(fadeDuration, Color.clear, Color.black));
        }
    }

    public void FadeFromBlack(float fadeDuration)
    {
        if (!isFading)
        {
            StartCoroutine(Fade(fadeDuration, Color.black, Color.clear));
        }
    }

    private IEnumerator Fade(float fadeDuration, Color startColor, Color targetColor)
    {
        blackScreenImage.enabled = true;
        isFading = true;
        var elapsedTime = 0f;
        var currentColor = startColor;

        while (elapsedTime < fadeDuration)
        {
            currentColor = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            blackScreenImage.color = currentColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        blackScreenImage.color = targetColor;
        blackScreenImage.enabled = false;
        isFading = false;
    }
}
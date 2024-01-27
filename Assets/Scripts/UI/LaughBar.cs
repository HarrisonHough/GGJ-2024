using System;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class LaughBar : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float progress = 0.5f;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    [SerializeField] private RectTransform handle;
    
    private float minRange = -195;
    private float maxRange = 175f;

    private void Start()
    {
        SetProgress();
        //this will apply the progress that was set in AddProgress
        DirectorActions.OnDirectorResponse += SetProgress;
    }

    public void AddProgress(PromptResponse response)
    {
        progress = Mathf.Clamp01(progress + response.FunnyRating/GameManager.SCORE_TARGET);
    }

    private void SetProgress()
    {
        fill.color = gradient.Evaluate(progress);
        handle.anchoredPosition = new Vector2(0, progress * (maxRange - minRange) + minRange);
    }
}

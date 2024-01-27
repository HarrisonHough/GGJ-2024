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

    public void NudgeProgress(float amount)
    {
        progress = Mathf.Clamp01(progress + amount);
        
        fill.color = gradient.Evaluate(progress);
        handle.anchoredPosition = new Vector2(0, progress * (maxRange - minRange) + minRange);
    }
}

using TMPro;
using UnityEngine;

public class ResponseButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetText(string newText)
    {
        text.SetText(newText);
    }
}

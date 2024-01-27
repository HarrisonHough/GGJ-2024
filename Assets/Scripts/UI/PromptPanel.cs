using TMPro;
using UnityEngine;

public class PromptPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptTMP;
    [SerializeField] private ResponseButton[] button;
    [SerializeField] private GameObject panel;
    
    private void Start()
    {
        panel.SetActive(false);
        InteractionPoint.OnPromptTriggered += LoadPrompt;
    }
    
    public void LoadPrompt(PromptData promptData)
    {
        panel.SetActive(true);
        promptTMP.text = promptData.promptText;
        for (var i = 0; i < button.Length; i++)
        {
            if (i >= promptData.responses.Length)
            {
                button[i].gameObject.SetActive(false);
                return;
            }
            button[i].SetResponseData(promptData.responses[i]);
        }
    }
}

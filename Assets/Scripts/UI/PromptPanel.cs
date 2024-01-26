using UnityEngine;

public class PromptPanel : MonoBehaviour
{
    [SerializeField] private string promptText;
    [SerializeField] private ResponseButton[] button;
    [SerializeField] private GameObject panel;
    
    private void Start()
    {
        PromptTrigger.OnPromptTriggered += LoadPrompt;
        
    }
    
    public void LoadPrompt(PromptData promptData)
    {
        panel.SetActive(false);
        promptText = promptData.promptText;
        for (var i = 0; i < button.Length; i++)
        {
            if (i >= promptData.responses.Length)
            {
                button[i].gameObject.SetActive(false);
                return;
            }
            button[i].SetText(promptData.responses[i].Text);
        }
    }
}

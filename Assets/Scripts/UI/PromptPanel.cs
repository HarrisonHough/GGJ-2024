using UnityEngine;

public class PromptPanel : MonoBehaviour
{
    [SerializeField] private string promptText;
    [SerializeField] private ResponseButton[] button;
    
    public void LoadPrompt(PromptData promptData)
    {
        this.promptText = promptData.promptText;
        for (int i = 0; i < promptData.responses.Length; i++)
        {
            button[i].SetText(promptData.responses[i].Text);
        }
    }
}

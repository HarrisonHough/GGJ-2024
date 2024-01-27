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
        var responses = promptData.responses;
        var myShuffledArray = ArrayShuffler.Shuffle(responses);
        for (var i = 0; i < button.Length; i++)
        {
            if (i >= myShuffledArray.Length)
            {
                button[i].gameObject.SetActive(false);
                return;
            }
            button[i].SetResponseData(myShuffledArray[i]);
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ResponseButton : MonoBehaviour
{
    public UnityEvent<PromptResponse> OnResponseSelected;
    [SerializeField] private TextMeshProUGUI text;
    private PromptResponse promptResponse;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    public void SetResponseData(PromptResponse response)
    {
        promptResponse = response;
        text.SetText(promptResponse.Text);
        
    }

    private void OnButtonClick()
    {
        OnResponseSelected?.Invoke(promptResponse);
        GameManager.Instance.SwitchToMainCamera();
        GameManager.Instance.SetGameState(GameState.Playing);
    }
}

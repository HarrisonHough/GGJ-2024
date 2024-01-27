using System;
using UnityEngine;

public class PromptTrigger : MonoBehaviour
{
    public static Action<PromptData> OnPromptTriggered;
    [SerializeField] private PromptData promptData;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPromptTriggered?.Invoke(promptData);
        }
    }
}

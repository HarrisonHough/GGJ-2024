using System;
using Cinemachine;
using UnityEngine;

public class InteractionPoint : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera newCamera;
    [SerializeField] private Transform viewPoint;
    [SerializeField] private PromptData promptData;
    public static Action<PromptData> OnPromptTriggered;
    private bool isTriggeredOnce;

    public Vector3 GetDestinationPosition()
    {
        return viewPoint.position;
    }
    
    public Quaternion GetDestinationRotation()
    {
        return viewPoint.rotation;
    }
    
    public void ShowPrompt()
    {
        if (isTriggeredOnce) return;
        OnPromptTriggered?.Invoke(promptData);
        isTriggeredOnce = true;
    }
    
    public void SwitchCamera()
    {
        GameManager.Instance.SwitchToCamera(newCamera);
    }
}

using System;
using Cinemachine;
using UnityEngine;

public class InteractionPoint : MonoBehaviour
{
    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private CinemachineVirtualCamera newCamera;
    [SerializeField] private Transform viewPoint;
    [SerializeField] private PromptData promptData;
    public static Action<PromptData> OnPromptTriggered;
    private ICinemachineCamera previousCamera;
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
        previousCamera = cinemachineBrain.ActiveVirtualCamera;
        previousCamera.VirtualCameraGameObject.SetActive(false);
        newCamera.gameObject.SetActive(true);
    }

    public void ResetCamera()
    {
        newCamera.gameObject.SetActive(false);
        previousCamera.VirtualCameraGameObject.SetActive(true);
    }
}

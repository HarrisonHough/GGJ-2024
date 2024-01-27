using Cinemachine;
using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private CinemachineVirtualCamera newCamera;
    private ICinemachineCamera previousCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PLAYER_TAG)) return;
        previousCamera = cinemachineBrain.ActiveVirtualCamera;
        previousCamera.VirtualCameraGameObject.SetActive(false);
        newCamera.gameObject.SetActive(true);
    }
    
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(PLAYER_TAG)) return;
        newCamera.gameObject.SetActive(false);
        previousCamera.VirtualCameraGameObject.SetActive(true);
    }
}

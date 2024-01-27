using Cinemachine;
using UnityEngine;

public enum GameState
{
    Playing,
    Dialogue,
    Paused
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set; }
    public GameState GameState {get; private set; } = GameState.Playing;
    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    
    private void Start()
    {
        SetGameState(GameState.Playing);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SetGameState(GameState gameState)
    {
        GameState = gameState;
    }
    
    public void SwitchToCamera(CinemachineVirtualCamera newCamera)
    {
        newCamera.gameObject.SetActive(true);
    }
    
    public void SwitchToMainCamera()
    {
        cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
        mainCamera.VirtualCameraGameObject.SetActive(true);
    }
}

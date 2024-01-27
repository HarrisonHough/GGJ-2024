using System;
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

    private float playerScore;
    public const float SCORE_TARGET = 8;
    public static Action OnGameOver;
    public static Action OnScoreReached;
    private int numberOfResponses;
    
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
    
    public void HandleInteractionResponse(float funnyRating)
    {
        numberOfResponses++;
        playerScore += funnyRating;
        playerScore = Mathf.Clamp(playerScore, 0f,SCORE_TARGET);
        if (playerScore >= SCORE_TARGET)
        {
            Debug.Log("You win!");
            OnGameOver?.Invoke();
        }
        if(playerScore <=  0 || numberOfResponses >= 12)
        {
            Debug.Log("You lose!");
            OnGameOver?.Invoke();
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

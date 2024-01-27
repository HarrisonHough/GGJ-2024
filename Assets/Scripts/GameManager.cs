using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

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
    [SerializeField] private AudioClip goodEndingClip;
    [SerializeField] private AudioClip neutralEndingClip;
    [SerializeField] private AudioClip badEndingClip;
    [SerializeField] private AudioClip introClip;
    [SerializeField] private ScreenFader screenFader;
    private AudioSource audioSource;
    private float playerScore;
    public const float SCORE_TARGET = 8;
    private int numberOfResponses;
    private const string ENDING_PREF = "ENDING_INDEX";
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetGameState(GameState.Paused);
        DirectorActions.OnDirectorResponse += AfterDirectorResponse;
        StartCoroutine(WaitForStart());
    }

    private void AfterDirectorResponse()
    {
        if (playerScore >= SCORE_TARGET)
        {
            PlayerPrefs.SetInt(ENDING_PREF, 0);
            StartCoroutine(WaitAndFinish(goodEndingClip));
            return;
        }
        if(numberOfResponses >= 12)
        {
            PlayerPrefs.SetInt(ENDING_PREF, 1);
            StartCoroutine(WaitAndFinish(neutralEndingClip));
            return;
        }
        if(playerScore <=  0 )
        {
            PlayerPrefs.SetInt(ENDING_PREF, 2);
            StartCoroutine(WaitAndFinish(badEndingClip));
        }    
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
    
    public void HandleInteractionResponse(PromptResponse response)
    {
        numberOfResponses++;
        playerScore += response.FunnyRating;
        playerScore = Mathf.Clamp(playerScore, 0f,SCORE_TARGET);
    }

    
    public void SetGameState(GameState gameState)
    {
        GameState = gameState;
    }
    
    private IEnumerator WaitForStart()
    {   
        PlayAudio(introClip);
        screenFader.FadeFromBlack(2f);
        yield return new WaitForSeconds(introClip.length);
        SetGameState(GameState.Playing);
    }

    private void PlayAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;  
        audioSource.Play();
    }

    private IEnumerator WaitAndFinish(AudioClip audioClip)
    {
        PlayAudio(audioClip);
        yield return new WaitForSeconds(audioClip.length);
        var duration = 1f;
        screenFader.FadeToBlack(duration);
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(2);
    }
}

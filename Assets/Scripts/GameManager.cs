using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private CinemachineVirtualCamera directorCamera;
    [SerializeField] private AudioClip goodEndingClip;
    [SerializeField] private AudioClip neutralEndingClip;
    [SerializeField] private AudioClip badEndingClip;
    [SerializeField] private AudioClip introClip;
    [SerializeField] private ScreenFader screenFader;
    private AudioSource audioSource;
    private float playerScore;
    public const float SCORE_TARGET = 1;
    private int numberOfResponses;
    private const string ENDING_PREF = "ENDING_INDEX";
    
    public static Dictionary<FunnyRating, float> FunnyRatingToScore = new Dictionary<FunnyRating, float>
    {
        {FunnyRating.Neutral, 0f},
        {FunnyRating.LittleFunny, 0.05f},
        {FunnyRating.Funny, 0.1f},
        {FunnyRating.NotFunny, -0.05f},
        {FunnyRating.ReallyNotFunny, -0.1f}
    };
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerScore = 0.5f;
        SetGameState(GameState.Paused);
        DirectorActions.OnDirectorResponse += AfterDirectorResponse;
        StartCoroutine(WaitForStart());
    }

    private void AfterDirectorResponse()
    {
        Debug.Log($"AfterDirectorResponse score: {playerScore}");
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
        playerScore += FunnyRatingToScore[response.FunnyRating];
        Debug.Log($"added {response.FunnyRating} to score, score is now {playerScore}");

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
        directorCamera.gameObject.SetActive(true);
        yield return new WaitForSeconds(introClip.length);
        directorCamera.gameObject.SetActive(false);
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
        directorCamera.gameObject.SetActive(true);
        yield return new WaitForSeconds(audioClip.length);
        var duration = 1f;
        screenFader.FadeToBlack(duration);
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(2);
    }
}

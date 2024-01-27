using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;
using Random = UnityEngine.Random;

public class DirectorActions : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;
    
    
    [SerializeField] private AudioClip[] goodLines;
    [SerializeField] private AudioClip[] neutralLines;
    [SerializeField] private AudioClip[] badLines;
    [SerializeField] private CinemachineVirtualCamera newCamera;
    [SerializeField] private AudioClip audienceLaugh;
    private Dictionary<LineType, (AudioClip[], string)> lines;

    public static Action OnDirectorResponse;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        
        lines = new Dictionary<LineType, (AudioClip[], string)>
        {
            {LineType.Good, (goodLines, "Good")},
            {LineType.Neutral, (neutralLines, "Neutral")},
            {LineType.Bad, (badLines, "Bad")}
        };
    }
    
    public void PlayLine(LineType lineType)
    {
        var (clips, trigger) = lines[lineType];
        var clip = clips[Random.Range(0, clips.Length)];
        audioSource.clip = clip;
        audioSource.PlayOneShot(clip);
        animator.SetTrigger(trigger);
    }
    
    public void PlayLine(int line)
    {
        var lineType = (LineType) line;
        PlayLine(lineType);
    }
    
    public void HandleResponse(PromptResponse response)
    {
        StartCoroutine(WaitAndPlayAudio(response));
    }
    
    private IEnumerator WaitAndPlayAudio(PromptResponse response)
    {
        var lineType = response.FunnyRating switch
        {
            < -0.5f => LineType.Bad,
            > 0.5f => LineType.Good,
            _ => LineType.Neutral
        };
        
        yield return new WaitForSeconds(response.audioClip.length);
        if (lineType == LineType.Good && audienceLaugh != null )
        {
            yield return new WaitForSeconds(audienceLaugh.length - 4f);
        }
        newCamera.gameObject.SetActive(true);
        PlayLine(lineType);
        yield return new WaitForSeconds(audioSource.clip.length + 0.5f);
        OnDirectorResponse?.Invoke();
        newCamera.gameObject.SetActive(false);
    }
}

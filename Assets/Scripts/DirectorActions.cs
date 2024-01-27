using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;
using ReadyPlayerMe.Core;
using Random = UnityEngine.Random;

public class DirectorActions : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;
    private VoiceHandler voiceHandler;
    
    [SerializeField] private AudioClip[] goodLines;
    [SerializeField] private AudioClip[] neutralLines;
    [SerializeField] private AudioClip[] badLines;
    [SerializeField] private CinemachineVirtualCamera newCamera;
    private Dictionary<LineType, (AudioClip[], string)> lines;

    public static Action OnDirectorResponse;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        voiceHandler = GetComponent<VoiceHandler>();
        
        lines = new Dictionary<LineType, (AudioClip[], string)>
        {
            {LineType.Good, (goodLines, "Good")},
            {LineType.Neutral, (neutralLines, "Neutral")},
            {LineType.Bad, (badLines, "Bad")}
        };
    }

    private void OnDestroy()
    {
        OnDirectorResponse = null;
    }

    public void PlayLine(LineType lineType)
    {
        var (clips, trigger) = lines[lineType];
        var clip = clips[Random.Range(0, clips.Length)];
        audioSource.clip = clip;
        audioSource.PlayOneShot(clip);
        animator.SetTrigger(trigger);
        voiceHandler.PlayAudioClip(clip);
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
        if (lineType == LineType.Good)
        {
            yield return new WaitForSeconds(3f);
        }
        newCamera.gameObject.SetActive(true);
        PlayLine(lineType);
        yield return new WaitForSeconds(audioSource.clip.length + 0.5f);
        OnDirectorResponse?.Invoke();
        newCamera.gameObject.SetActive(false);
    }
}

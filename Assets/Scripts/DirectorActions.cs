using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class DirectorActions : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;
    
    [SerializeField] private AudioClip[] goodLines;
    [SerializeField] private AudioClip[] neutralLines;
    [SerializeField] private AudioClip[] badLines;

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
        
        audioSource.PlayOneShot(clip);
        animator.SetTrigger(trigger);
    }
    
    public void PlayLine(int line)
    {
        var lineType = (LineType) line;
        PlayLine(lineType);
    }
    
    public void PlayLine(PromptResponse response)
    {
        var lineType = response.FunnyRating switch
        {
            < 0 => LineType.Bad,
            > 0 => LineType.Good,
            _ => LineType.Neutral
        };
        PlayLine(lineType);
        StartCoroutine(WaitForDirectorsAudio(response.audioClip.length));
    }
    
    private IEnumerator WaitForDirectorsAudio(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        OnDirectorResponse?.Invoke();
    }
}

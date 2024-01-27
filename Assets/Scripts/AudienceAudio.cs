using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]  
public class AudienceAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public Action OnAudienceResponse;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void HandleResponse(PromptResponse response)
    {
        if (response.FunnyRating <= 0) return;
        StartCoroutine(WaitAndPlayAudio(response.audioClip.length));
    }
    
    private IEnumerator WaitAndPlayAudio(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        OnAudienceResponse?.Invoke();
    }
}

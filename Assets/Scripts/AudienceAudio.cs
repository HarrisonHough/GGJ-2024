using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]  
public class AudienceAudio : MonoBehaviour
{
    [SerializeField] private AudioClip laughingClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void HandleResponse(PromptResponse response)
    {
        audioSource.clip = laughingClip;
        audioSource.Play();
    }
    
    public void PlayLaughingAudio(PromptResponse response)
    {
        audioSource.clip = laughingClip;
        audioSource.Play();
    }
    
    private IEnumerator WaitForDirectorsAudio(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
    }
}

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerVoice : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPromptAudio(PromptResponse response)
    {
        audioSource.clip = response.audioClip;
        audioSource.Play();
    }
}

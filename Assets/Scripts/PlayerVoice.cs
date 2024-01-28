using ReadyPlayerMe.Core;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerVoice : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
#if UNITY_WEBGL
        var voiceHandler = GetComponent<VoiceHandler>();
        if (voiceHandler != null)
        {
            voiceHandler.enabled = false;
        }
#endif
    }

    public void PlayPromptAudio(PromptResponse response)
    {
        audioSource.clip = response.audioClip;
        audioSource.Play();
        animator.SetTrigger("Talk");
        animator.SetInteger("TalkIndex", Random.Range(0, 3));
    }
}

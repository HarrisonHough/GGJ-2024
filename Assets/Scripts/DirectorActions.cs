using UnityEngine;
using System.Collections.Generic;

public class DirectorActions : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;
    
    [SerializeField] private AudioClip[] goodLines;
    [SerializeField] private AudioClip[] neutralLines;
    [SerializeField] private AudioClip[] badLines;

    private Dictionary<LineType, (AudioClip[], string)> lines;
    
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
}

using System;
using UnityEngine;

[Serializable]
public struct PromptResponse
{
    public string Text;
    public float FunnyRating;
    public AudioClip audioClip;
}

[CreateAssetMenu(fileName = "PromptData", menuName = "ScriptableObjects/PromptData", order = 1)]
public class PromptData : ScriptableObject
{
    public PromptResponse[] responses;
}

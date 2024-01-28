using System;
using UnityEngine;

public enum FunnyRating
{
    Neutral,
    LittleFunny,
    Funny,
    NotFunny,
    ReallyNotFunny
}

[Serializable]
public struct PromptResponse
{
    public string Text;
    public FunnyRating FunnyRating;
    public AudioClip audioClip;
}

[CreateAssetMenu(fileName = "PromptData", menuName = "ScriptableObjects/PromptData", order = 1)]
public class PromptData : ScriptableObject
{
    public PromptResponse[] responses;
}

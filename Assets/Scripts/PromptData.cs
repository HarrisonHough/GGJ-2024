using System;
using UnityEngine;

[Serializable]
public struct PromptResponse
{
    public string ResponseText;
    public int FunnyRating;
}

[CreateAssetMenu(fileName = "PromptData", menuName = "ScriptableObjects/PromptData", order = 1)]
public class PromptData : ScriptableObject
{
    public string promptText;
    public PromptResponse[] responses;
}

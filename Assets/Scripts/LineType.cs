using System;

[Serializable, Flags]
public enum LineType
{
    Good = 0,
    Neutral = 1,
    Bad = 2
}
using System;
using System.Linq;

public static class ArrayShuffler
{
    private static Random random = new Random();

    public static T[] Shuffle<T>(T[] array)
    {
        return array.OrderBy(x => random.Next()).ToArray();
    }
}
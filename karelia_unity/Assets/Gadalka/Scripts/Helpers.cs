using UnityEngine;

public class Helpers
{
    public T GetRandomArrayElement<T>(T[] arr)
    {
        int index = Random.Range(0, arr.Length);
        return arr[index];
    }
}

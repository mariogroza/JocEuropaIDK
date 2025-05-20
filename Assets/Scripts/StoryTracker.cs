using System.Collections.Generic;
using UnityEngine;

public class StoryState : MonoBehaviour
{
    public static StoryState Instance;

    private HashSet<string> flags = new HashSet<string>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetFlag(string flag)
    {
        flags.Add(flag);
    }

    public bool HasFlag(string flag)
    {
        return flags.Contains(flag);
    }
}

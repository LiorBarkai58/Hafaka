using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputBlocker", menuName = "Input/InputBlocker")]
public class InputBlocker : ScriptableObject
{
    private List<string> blockers = new List<string>();

    public bool isBlocked { get { return blockers.Count > 0; } }

    public void AddBlocker(string blockerName)
    {
        blockers.Add(blockerName);
    }

    public void RemoveBlocker(string blockerName)
    {
        if(blockers.Contains(blockerName)) blockers.Remove(blockerName);
    }

}
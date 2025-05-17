using UnityEngine;

[CreateAssetMenu(fileName = "NewAnimationState", menuName = "Animation/State")]
public class AnimationStateSO : ScriptableObject
{
    public string stateName;
    public int hash;

    public float duration; // optional
}
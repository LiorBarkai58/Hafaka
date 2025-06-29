using UnityEngine;


[CreateAssetMenu(fileName = "PlayerTransform", menuName = "Player/PlayerTransform")]
public class PlayerTransform : ScriptableObject
{
    public Transform Transform { get; set; }
}
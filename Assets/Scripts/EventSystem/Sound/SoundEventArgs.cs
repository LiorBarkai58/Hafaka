using UnityEngine;

namespace EventSystem.Sound
{
    public enum SoundEventType
    {
        Sfx,
        Music
    }
    public struct SoundEventArgs
    {
        public Vector3 Position;
        public SoundEventType SoundType;
        public AudioClip Clip;
        public float Volume;
    }
}
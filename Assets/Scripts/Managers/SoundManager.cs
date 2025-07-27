using System.Collections.Generic;
using EventSystem.Sound;
using UnityEngine;

namespace Managers
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [SerializeField] private AudioSource audioSourcePrefab;
        [SerializeField] private int poolSize = 10;
        
        [SerializeField] private SoundEventListener soundEventListener;

        private Queue<AudioSource> audioPool = new Queue<AudioSource>();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            soundEventListener.OnEvent += HandleClipRequest;

            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize pool
            for (int i = 0; i < poolSize; i++)
            {
                var source = Instantiate(audioSourcePrefab, transform);
                source.spatialBlend = 1f; // 3D sound
                source.gameObject.SetActive(false);
                audioPool.Enqueue(source);
            }
        }

        private void HandleClipRequest(SoundEventArgs soundEventArgs)
        {
            if (soundEventArgs.SoundType == SoundEventType.Sfx)
            {
                PlayClipAtPoint(soundEventArgs.Clip, soundEventArgs.Position, soundEventArgs.Volume);
            }
            else
            {
                PlayLoopingClip(soundEventArgs.Clip, soundEventArgs.Volume);
            }
        }

        public void PlayClipAtPoint(AudioClip clip, Vector3 position, float volume = 1f)
        {
            AudioSource source = GetPooledAudioSource();
            source.clip = clip;
            source.transform.position = position;
            source.volume = volume;
            source.spatialBlend = 1f;
            source.gameObject.SetActive(true);
            source.Play();
            StartCoroutine(ReturnToPoolAfter(source, clip.length));
        }

        public AudioSource PlayLoopingClip(AudioClip clip, float volume = 1f)
        {
            AudioSource source = GetPooledAudioSource();
            source.clip = clip;
            source.volume = volume;
            source.loop = true;
            source.spatialBlend = 0f;
            source.gameObject.SetActive(true);
            source.Play();
            return source;
        }

        public void StopLoopingClip(AudioSource source)
        {
            if (source == null) return;
            source.Stop();
            source.loop = false;
            source.gameObject.SetActive(false);
            audioPool.Enqueue(source);
        }

        private IEnumerator<WaitForSeconds> ReturnToPoolAfter(AudioSource source, float delay)
        {
            yield return new WaitForSeconds(delay);
            source.Stop();
            source.gameObject.SetActive(false);
            audioPool.Enqueue(source);
        }

        private AudioSource GetPooledAudioSource()
        {
            if (audioPool.Count == 0)
            {
                var newSource = Instantiate(audioSourcePrefab, transform);
                newSource.spatialBlend = 1f;
                newSource.gameObject.SetActive(false);
                audioPool.Enqueue(newSource);
            }

            return audioPool.Dequeue();
        }
        }
}
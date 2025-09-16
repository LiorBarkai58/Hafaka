using System.Collections;
using UnityEngine;

namespace Managers {
    public class AudioManager : MonoBehaviour {
        public static AudioManager Instance { get; private set; }

        [Header("Music")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioClip defaultMusic;
        [Range(0f,1f)] [SerializeField] private float musicVolume = 0.8f;

        [Header("SFX")]
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioClip sfx1;
        [SerializeField] private AudioClip sfx2;
        [Range(0f,1f)] [SerializeField] private float sfxVolume = 1f;

        private void Awake() {
            if (Instance && Instance != this) {
                Destroy(gameObject); 
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            if (defaultMusic) 
                PlayMusic(defaultMusic);
        }

        // Music
        public void PlayMusic(AudioClip clip, bool restartIfSame = false, float fadeSeconds = 0f) {
            if (!clip) return;
            if (!restartIfSame && musicSource.clip == clip && musicSource.isPlaying) return;

            if (fadeSeconds <= 0f) {
                musicSource.clip = clip;
                musicSource.volume = musicVolume;
                musicSource.Play();
            } else {
                StopAllCoroutines();
                StartCoroutine(FadeToTrack(clip, fadeSeconds));
            }
        }

        public void StopMusic(float fadeSeconds = 0f) {
            if (fadeSeconds <= 0f) { musicSource.Stop(); return; }
            StartCoroutine(FadeOut(fadeSeconds));
        }

        private IEnumerator FadeToTrack(AudioClip next, float dur) {
            float half = dur * 0.5f;
            float start = musicSource.volume;
            for (float t = 0; t < half; t += Time.unscaledDeltaTime) {
                musicSource.volume = Mathf.Lerp(start, 0f, t / half);
                yield return null;
            }
            
            musicSource.clip = next;
            musicSource.Play();
            for (float t = 0; t < half; t += Time.unscaledDeltaTime) {
                musicSource.volume = Mathf.Lerp(0f, musicVolume, t / half);
                yield return null;
            }
            
            musicSource.volume = musicVolume;
        }

        private IEnumerator FadeOut(float dur) {
            float start = musicSource.volume;
            for (float t = 0; t < dur; t += Time.unscaledDeltaTime) {
                musicSource.volume = Mathf.Lerp(start, 0f, t / dur);
                yield return null;
            }
            musicSource.Stop();
            musicSource.volume = musicVolume;
        }

        // SFX
        public void PlaySfx1(float volume = 1f) => PlaySfx(sfx1, volume);
        public void PlaySfx2(float volume = 1f) => PlaySfx(sfx2, volume);

        public void PlaySfx(AudioClip clip, float volume = 1f) {
            if (!clip) return;
            sfxSource.PlayOneShot(clip, sfxVolume * Mathf.Clamp01(volume));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        [Header("Music Manager")]
        [SerializeField] private List<MusicEntry> _songs;

        private static MusicManager _instance;

        private AudioSource _audioSource;

        public static MusicManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<MusicManager>();
                }

                return _instance;
            }
        }
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Plays the audio with the given internal name. Will raise debug log if no audio is found.
        /// </summary>
        /// <param name="internalName"></param>
        public void PlaySong(string internalName)
        {
            MusicEntry entry = _songs.Find(x => x.InternalName == internalName);
            if (entry == null)
                return;

            Play(entry);
        }

        /// <summary>
        /// Plays an audio clip with optional settings.
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        /// <param name="pitch"></param>
        public void PlaySong(AudioClip clip, float volume = 1f, float pitch = 1f)
        {
            MusicEntry e = new MusicEntry();
            e.Clip = clip;
            e.Volume = volume;
            e.Pitch = pitch;

            Play(e);
        }

        /// <summary>
        /// Lowers the volume to 0 and lerps it back to original
        /// over the supplied duration. Is good to use when starting a
        /// new soundtrack and not make it sudden.
        /// </summary>
        /// <param name="duration"></param>
        public void EaseIn(float duration)
        {
            float start = 0f;
            float end = _audioSource.volume;
            StartCoroutine(VolumeEasingCoroutine(start, end, duration));
        }

        public void EaseOut(float duration)
        {
            float start = _audioSource.volume;
            float end = 0f;
            StartCoroutine(VolumeEasingCoroutine(start, end, duration));
        }

        private void Play(MusicEntry entry)
        {
            _audioSource.clip = entry.Clip;
            _audioSource.volume = entry.Volume;
            _audioSource.pitch = entry.Pitch;
            _audioSource.Play();
        }

        private IEnumerator VolumeEasingCoroutine(float startValue, float endValue, float duration)
        {
            float timer = 0f;
            _audioSource.volume = startValue;

            while (timer < duration)
            {
                float progress =  timer / duration;

                _audioSource.volume = Mathf.Lerp(startValue, endValue, progress);

                timer += Time.unscaledDeltaTime;

                yield return 0;
            }

            _audioSource.volume = endValue;
        }
    }
}
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PeixeAbissal.Audio {

    public class MusicPlayer : MonoBehaviour {

        public static MusicPlayer Instance {

            get { return instance; }
        }
        private static MusicPlayer instance;
        private AudioSource[] audioSource;

        [SerializeField]
        private AudioSource sfxPlayerPrefab;
        private List<AudioSource> instantiatedAudioSources = new List<AudioSource> ();

        void Awake () {

            instance = this;
            audioSource = GetComponents<AudioSource> ();
        }

        public void PlayMusic (AudioClip musicClip, bool loop = true, bool overridePrevious = true, float fadeTime = 1f) {

            if (audioSource[0].isPlaying) {

                if (audioSource[0].clip == musicClip && !overridePrevious)
                    return;

                audioSource[0].DOFade (0, fadeTime)
                    .OnComplete (() => {
                        SetMusicVolume (1);
                        InternalPlay (0, musicClip, loop);
                    });
            } else {

                SetMusicVolume (1);
                InternalPlay (0, musicClip, loop);
            }
        }

        public void PlayAmbience (AudioClip ambienceClip, bool loop = true, bool overridePrevious = true, float fadeTime = 1f) {

            if (audioSource[1].isPlaying) {

                if (audioSource[1].clip == ambienceClip && !overridePrevious)
                    return;

                audioSource[1].DOFade (0, fadeTime)
                    .OnComplete (() => {

                        SetAmbienceVolume (1);
                        InternalPlay (1, ambienceClip, loop);
                    });
            } else {

                SetAmbienceVolume (1);
                InternalPlay (1, ambienceClip, loop);
            }
        }

        public void StopMusic (float fadeTime = 1f) {

            audioSource[0].DOFade (0, fadeTime).
            OnComplete (() => {

                audioSource[0].Stop ();
            });
        }

        public void StopAmbience () {

            audioSource[1].DOFade (0, 1f);
        }

        public void SetMusicVolume (float volume) {

            audioSource[0].volume = volume;
        }

        public void SetMusicPan (float panValue) {

            audioSource[0].panStereo = panValue;
        }

        public void SetAmbienceVolume (float volume) {

            audioSource[1].volume = volume;
        }

        public void SetAmbiencePan (float panValue) {

            audioSource[1].panStereo = panValue;
        }

        public void PlaySFX (AudioClip clip, bool loop = false) {

            var freeAudioSource = FindFreeAudioSource ();
            if (freeAudioSource < 0) {

                var sfxPlayer = Instantiate (sfxPlayerPrefab) as AudioSource;
                sfxPlayer.transform.SetParent (transform);
                instantiatedAudioSources.Add (sfxPlayer);
                freeAudioSource = FindFreeAudioSource ();
            }

            instantiatedAudioSources[freeAudioSource].clip = clip;
            instantiatedAudioSources[freeAudioSource].loop = loop;
            instantiatedAudioSources[freeAudioSource].Play ();
        }

        private int FindFreeAudioSource () {

            int i = 0;
            while (i < instantiatedAudioSources.Count) {

                if (instantiatedAudioSources[i].isPlaying) {
                    i += 1;
                    continue;
                }
                return i;
            }

            return -1;
        }

        public void StopSFX () {

            for (int i = 0; i < instantiatedAudioSources.Count; i++)
                instantiatedAudioSources[i].Stop ();
        }

        private void InternalPlay (int index, AudioClip musicClip, bool loop = true) {
            audioSource[index].clip = musicClip;
            audioSource[index].loop = loop;
            audioSource[index].Play ();
        }
    }
}
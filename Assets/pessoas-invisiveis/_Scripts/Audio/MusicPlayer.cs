using System.Collections;
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

        void Awake () {

            instance = this;
            audioSource = GetComponents<AudioSource> ();
        }

        public void PlayMusic (AudioClip musicClip, bool loop = true) {

            if (audioSource[0].isPlaying) {
                audioSource[0].DOFade (0, 1f)
                    .OnComplete (() => {
                        InternalPlay (0, musicClip, loop);
                    });
            } else {

                InternalPlay (0, musicClip, loop);
            }
        }

        public void PlayAmbience (AudioClip ambienceClip, bool loop = true) {

            if (audioSource[1].isPlaying) {
                audioSource[1].DOFade (0, 1f)
                    .OnComplete (() => {
                        InternalPlay (1, ambienceClip, loop);
                    });
            } else {

                InternalPlay (1, ambienceClip, loop);
            }
        }

        public void StopMusic () {

            audioSource[0].DOFade (0, 1f);
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

            audioSource[2].clip = clip;
            audioSource[2].loop = loop;
            audioSource[2].Play ();
        }

        public void StopSFX () {

            audioSource[2].Stop ();
        }

        private void InternalPlay (int index, AudioClip musicClip, bool loop = true) {
            audioSource[index].clip = musicClip;
            audioSource[index].loop = loop;
            audioSource[index].Play ();
        }
    }
}
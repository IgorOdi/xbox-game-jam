using DG.Tweening;
using PeixeAbissal.Audio;
using PeixeAbissal.Scene.Enum;
using UnityEngine;

namespace PeixeAbissal.Scene.Street {

    public class CaminhoCasaSceneController : SceneController {

        protected override string nextLevel { get { return "Dormir"; } }

        [SerializeField]
        private RectTransform pathArea;
        private const float SCROLL_DURATION = 11;
        private const float AREA_LIMIT = 0;

        [SerializeField]
        private AudioClip streetAmbience;
        [SerializeField]
        private AudioClip skateSound;

        internal override void WillStart () {

            MusicPlayer.Instance.PlayAmbience (streetAmbience);
            MusicPlayer.Instance.SetAmbienceVolume (0.5f);
        }

        internal override void OnStart () {

            MusicPlayer.Instance.PlaySFX (skateSound);
            pathArea.DOAnchorPosX (0, SCROLL_DURATION)
                .SetEase (Ease.InOutSine)
                .OnComplete (() => {

                    MusicPlayer.Instance.StopAmbience ();
                    MusicPlayer.Instance.SetAmbienceVolume (1f);
                    MusicPlayer.Instance.StopSFX ();
                    OnFinishLevel (TransitionSide.Fade);
                });

        }
    }
}
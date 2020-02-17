using DG.Tweening;
using PeixeAbissal.Audio;
using PeixeAbissal.Enum;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class CaminhoCasaSceneController : SceneController {

        protected override string nextLevel { get { return "Dormir"; } }

        [SerializeField]
        private RectTransform pathArea;
        private const float SCROLL_DURATION = 11;
        private const float AREA_LIMIT = 0;

        [SerializeField]
        private AudioClip skateSound;

        internal override void StartScene () {

            MusicPlayer.Instance.PlaySFX (skateSound);
            pathArea.DOAnchorPosX (0, SCROLL_DURATION)
                .SetEase (Ease.InOutSine)
                .OnComplete (() => {

                    MusicPlayer.Instance.StopSFX ();
                    OnFinishLevel (true, Side.Fade);
                });

        }
    }
}
using DG.Tweening;
using PeixeAbissal.Audio;
using PeixeAbissal.Enum;
using PeixeAbissal.UI;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class CaminhoTrabalhoSceneController : SceneController {

        protected override string nextLevel { get { return "CafeMain"; } }

        [SerializeField]
        private RectTransform pathArea;
        [SerializeField]
        private InteractableObject revista;
        [SerializeField]
        private InteractableObject buyButton;
        [SerializeField]
        private BalloonController balloonController;

        [Header ("Audio"), SerializeField]
        private AudioClip streetAmbience;
        [SerializeField]
        private AudioClip buySound;
        [SerializeField]
        private AudioClip streetWalk;

        private const float SCROLL_DURATION = 10;
        private const float AREA_LIMIT = -3840;

        internal override void WillStart () {

            MusicPlayer.Instance.StopMusic ();
            MusicPlayer.Instance.PlayAmbience (streetAmbience);
        }

        internal override void StartScene () {

            MusicPlayer.Instance.PlaySFX (streetWalk, true);
            pathArea.DOAnchorPosX (AREA_LIMIT, SCROLL_DURATION)
                .SetEase (Ease.InOutSine)
                .OnComplete (() => {

                    MusicPlayer.Instance.StopSFX ();
                    revista.gameObject.SetActive (true);
                    revista.OnMouseClick += ZoomRevista;
                });
        }

        private void ZoomRevista () {

            pathArea.DOAnchorPosX (AREA_LIMIT - 1920, SCROLL_DURATION / 3)
                .SetEase (Ease.InOutSine)
                .OnComplete (() => {

                    balloonController.ShowBalloon (() => {

                        buyButton.gameObject.SetActive (true);
                        buyButton.transform.DOScale (1, 1)
                            .From (0)
                            .SetEase (Ease.OutBack)
                            .OnComplete (() => {

                                buyButton.OnMouseClick += () => {
                                    MusicPlayer.Instance.PlaySFX (buySound);
                                    OnFinishLevel (true, Side.Fade);
                                };
                            });
                    }, 1.5f);
                });
        }
    }
}
using DG.Tweening;
using PeixeAbissal.Audio;
using PeixeAbissal.Controller.Street;
using PeixeAbissal.Scene.Enum;
using PeixeAbissal.UI;
using PeixeAbissal.UI.Enum;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.Scene.Street {

    public class CaminhoTrabalhoSceneController : SceneController {

        protected override string nextLevel { get { return "CoffeeMain"; } }

        [SerializeField]
        private RectTransform pathArea;
        [SerializeField]
        private InteractableObject revista;
        [SerializeField]
        private InteractableObject buyButton;
        [SerializeField]
        private RectTransform gradient;
        [SerializeField]
        private StreetClairController clairController;
        [SerializeField]
        private Transform revistaTarget;
        [SerializeField]
        private GameObject zoomed;
        [SerializeField]
        private GameObject peopleSecondDay;

        [Header ("Audio"), SerializeField]
        private AudioClip streetAmbience;
        [SerializeField]
        private AudioClip buySound;
        [SerializeField]
        private AudioClip skateSound;
        [SerializeField]
        private AudioClip magazineOpenSound;
        [SerializeField]
        private AudioClip cafeDoorBell;

        private const float SCROLL_DURATION = 10;
        private const int AREA_LIMIT = -3840;
        private const int AREA_LIMIT_DAY2 = -4800;

        internal override void WillStart () {

            MusicPlayer.Instance.StopMusic ();
            MusicPlayer.Instance.PlayAmbience (streetAmbience);

            if (DayController.day == 1)
                peopleSecondDay.SetActive (true);
        }

        internal override void OnStart () {

            int limit = DayController.day == 0 ? AREA_LIMIT : AREA_LIMIT_DAY2;
            gradient.DOAnchorPosX (limit * -1, SCROLL_DURATION)
                .SetEase (Ease.InOutSine);

            MusicPlayer.Instance.PlaySFX (skateSound, true);
            pathArea.DOAnchorPosX (limit, SCROLL_DURATION)
                .SetEase (Ease.InOutSine)
                .OnComplete (() => {

                    MusicPlayer.Instance.StopSFX ();

                    if (DayController.day == 0) {
                        clairController.SetClairLooking ();
                        clairController.TurnClair (true);
                        revista.transform.position = revistaTarget.position;
                        revista.ShowObject (ShowType.Scale, 0.5f, Ease.OutBack, () => {
                            revista.OnMouseClick += () => {

                                MusicPlayer.Instance.PlaySFX (magazineOpenSound);
                                ZoomRevista ();
                            };
                        });
                    } else {

                        MusicPlayer.Instance.PlaySFX (cafeDoorBell);
                        OnFinishLevel (TransitionSide.Fade);
                    }
                });
        }

        private void ZoomRevista () {

            clairController.FadeClair (true, 2);
            zoomed.SetActive (true);
            buyButton.OnMouseClick += MoveToCafe;
        }

        private void MoveToCafe () {

            clairController.FadeClair (false, 2);
            clairController.SetClairSkating ();
            clairController.TurnClair (true);
            revista.HideObject (ShowType.Fade, null);

            MusicPlayer.Instance.PlaySFX (buySound);
            zoomed.GetComponent<CanvasGroup> ().DOFade (0, 1.5f);

            MusicPlayer.Instance.PlaySFX (skateSound, true);
            gradient.DOAnchorPosX ((AREA_LIMIT - 1920 / 2) * -1, SCROLL_DURATION / 3)
                .SetEase (Ease.InOutSine);

            pathArea.DOAnchorPosX (AREA_LIMIT - 1920 / 2, SCROLL_DURATION / 3)
                .SetEase (Ease.InOutSine)
                .OnComplete (() => {

                    MusicPlayer.Instance.StopSFX ();
                    MusicPlayer.Instance.PlaySFX (cafeDoorBell);
                    OnFinishLevel (TransitionSide.Fade);
                });
        }
    }
}
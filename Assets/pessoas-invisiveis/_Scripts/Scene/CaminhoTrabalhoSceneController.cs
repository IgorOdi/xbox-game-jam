using DG.Tweening;
using PeixeAbissal.Audio;
using PeixeAbissal.Enum;
using PeixeAbissal.Input;
using PeixeAbissal.UI;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.Scene {

    public class CaminhoTrabalhoSceneController : SceneController {

        protected override string nextLevel {
            get {
                return DayController.day == 0 ? "CafeMain" : "CafeMain";
            }
        }

        [SerializeField]
        private RectTransform pathArea;
        [SerializeField]
        private InteractableObject revista;
        [SerializeField]
        private InteractableObject buyButton;
        [SerializeField]
        private RectTransform gradient;
        [SerializeField]
        private Image claire;
        [SerializeField]
        private Sprite moving, looking;
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

        private const float SCROLL_DURATION = 10;
        private const int AREA_LIMIT = -3840;
        private const int AREA_LIMIT_DAY2 = -4800;

        internal override void WillStart () {

            MusicPlayer.Instance.StopMusic ();
            MusicPlayer.Instance.PlayAmbience (streetAmbience);

            if (DayController.day == 1)
                peopleSecondDay.SetActive (true);
        }

        internal override void StartScene () {

            int limit = DayController.day == 0 ? AREA_LIMIT : AREA_LIMIT_DAY2;
            gradient.DOAnchorPosX (limit * -1, SCROLL_DURATION)
                .SetEase (Ease.InOutSine);

            MusicPlayer.Instance.PlaySFX (skateSound, true);
            pathArea.DOAnchorPosX (limit, SCROLL_DURATION)
                .SetEase (Ease.InOutSine)
                .OnComplete (() => {

                    MusicPlayer.Instance.StopSFX ();

                    if (DayController.day == 0) {
                        claire.sprite = looking;
                        claire.transform.localScale = new Vector3 (1, 1, 1);
                        revista.gameObject.SetActive (true);
                        revista.transform.DOScale (1, 1f)
                            .From (0)
                            .SetEase (Ease.OutBack);
                        revista.OnMouseClick += ZoomRevista;
                    } else {

                        OnFinishLevel (true, Side.Fade);
                    }
                });
        }

        private void ZoomRevista () {

            claire.DOFade (0, 2f);
            claire.sprite = moving;
            claire.transform.localScale = new Vector3 (-1, 1, 1);
            zoomed.SetActive (true);
            buyButton.OnMouseClick += MoveToCafe;
        }

        private void MoveToCafe () {

            claire.DOFade (1, 2f);
            MusicPlayer.Instance.PlaySFX (buySound);
            zoomed.GetComponent<CanvasGroup> ().DOFade (0, 1.5f);

            MusicPlayer.Instance.PlaySFX (skateSound, true);
            gradient.DOAnchorPosX ((AREA_LIMIT - 1920) * -1, SCROLL_DURATION / 3)
                .SetEase (Ease.InOutSine);

            pathArea.DOAnchorPosX (AREA_LIMIT - 1920, SCROLL_DURATION / 3)
                .SetEase (Ease.InOutSine)
                .OnComplete (() => {

                    claire.transform.localScale = new Vector3 (1, 1, 1);
                    MusicPlayer.Instance.StopSFX ();
                    OnFinishLevel (true, Side.Fade);
                });
        }
    }
}
using DG.Tweening;
using PeixeAbissal.Enum;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class CaminhoCasaSceneController : SceneController {

        protected override string nextLevel { get { return "Dormir"; } }

        [SerializeField]
        private RectTransform pathArea;
        private const float SCROLL_DURATION = 10;
        private const float AREA_LIMIT = 0;

        internal override void StartScene () {

            pathArea.DOAnchorPosX (0, SCROLL_DURATION)
                .SetEase (Ease.InOutSine)
                .OnComplete (() => {

                    OnFinishLevel (true, Side.Fade);
                });

        }
    }
}
using DG.Tweening;
using PeixeAbissal.Enum;
using PeixeAbissal.UI;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class EspelhoMaravilhosaSceneController : SceneController {

        protected override string nextLevel { get { return "CaminhoTrabalho"; } }

        [SerializeField]
        private BalloonController balloonController;
        [SerializeField]
        private InteractableObject buttonNextScene;

        internal override void StartScene () {

            balloonController.ShowBalloon (() => {

                buttonNextScene.gameObject.SetActive (true);
                buttonNextScene.transform.DOScale (1, 1)
                    .From (0)
                    .SetEase (Ease.OutBack);
                buttonNextScene.OnMouseClick += () => {
                    OnFinishLevel (transform, Side.Fade);
                };
            }, 1.5f);
        }
    }
}
using DG.Tweening;
using PeixeAbissal.Scene.Enum;
using PeixeAbissal.UI;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class EspelhoMaravilhosaSceneController : SceneController {

        protected override string nextLevel { get { return "CaminhoTrabalho"; } }

        [SerializeField]
        private BalloonController balloonController;
        [SerializeField]
        private InteractableObject buttonNextScene;

        internal override void OnStart () {

            balloonController.ShowBalloon (() => {

                buttonNextScene.gameObject.SetActive (true);
                buttonNextScene.transform.DOScale (1, 1)
                    .From (0)
                    .SetEase (Ease.OutBack);
                buttonNextScene.OnMouseClick += () => {
                    OnFinishLevel (TransitionSide.Fade);
                };
            }, 1.5f);
        }
    }
}
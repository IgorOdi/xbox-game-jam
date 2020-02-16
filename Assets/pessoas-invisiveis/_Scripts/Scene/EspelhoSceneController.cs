using System;
using PeixeAbissal.Enum;
using PeixeAbissal.Input;
using PeixeAbissal.UI;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class EspelhoSceneController : SceneController {

        protected override string nextLevel { get { return "CaminhoTrabalho"; } }

        [SerializeField]
        private InteractableObject roupa, bone;
        [SerializeField]
        private Transform roupaFinalPosition, boneFinalPosition;
        [SerializeField]
        private BalloonController balloonController;

        internal override void StartScene () {

            roupa.followMouseOnClick = true;
            bone.followMouseOnClick = true;

            Action resolve = () => {

                AddPoints (0.25f, false);
                if (points >= 0.5f) {
                    balloonController.ShowBalloon (() => ConfigureInputAfterBalloonOnScreen (), 1.5f);
                }
            };
            roupa.OnMouseExit += () => CheckPosition (roupa, roupaFinalPosition, resolve);
            bone.OnMouseExit += () => CheckPosition (bone, boneFinalPosition, resolve);
        }

        private void ConfigureInputAfterBalloonOnScreen () {

            InputManager.RegisterAtKey (KeyCode.Mouse0, InputType.Press, () => {

                InputManager.ClearKeys ();
                balloonController.HideBallon (() => {
                    AddPoints (0.5f, false);
                });
            });
        }
    }
}
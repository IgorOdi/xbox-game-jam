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

        private bool showedBalloon;
        private bool boneOnPlace, roupaOnPlace;

        internal override void StartScene () {

            roupa.followMouseOnClick = true;
            bone.followMouseOnClick = true;

            Action resolve = () => {

                AddPoints (0.25f, false);
                print (points);
                if (points >= 0.5f && !showedBalloon) {

                    InputManager.ClearKeys ();
                    balloonController.ShowBalloon (() => ConfigureInputAfterBalloonOnScreen (), 1.5f);
                    showedBalloon = true;
                }
            };

            roupa.OnMouseDown += () => {
                if (roupaOnPlace) {
                    roupaOnPlace = false;
                    AddPoints (-0.25f, false);
                }
            };
            roupa.OnMouseUp += () => {
                if (CheckPosition (roupa, roupaFinalPosition, resolve)) {
                    roupaOnPlace = true;

                } else {

                    if (roupaOnPlace) {
                        roupaOnPlace = false;
                        AddPoints (-0.25f, false);
                    }
                }
            };
            bone.OnMouseDown += () => {
                if (boneOnPlace) {
                    boneOnPlace = false;
                    AddPoints(-0.25f, false);
                }
            };
            bone.OnMouseUp += () => {
                if (CheckPosition (bone, boneFinalPosition, resolve)) {
                    boneOnPlace = true;

                } else {

                    if (boneOnPlace) {
                        boneOnPlace = false;
                        AddPoints (-0.25f, false);
                    }
                }
            };
        }

        private void ConfigureInputAfterBalloonOnScreen () {

            InputManager.RegisterAtKey (KeyCode.Mouse0, InputType.Press, () => {

                balloonController.HideBallon (() => {
                    AddPoints (0.5f, false);
                });
            });
        }
    }
}
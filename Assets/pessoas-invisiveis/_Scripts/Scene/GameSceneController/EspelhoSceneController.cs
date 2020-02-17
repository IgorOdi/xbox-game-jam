using System;
using DG.Tweening;
using PeixeAbissal.Controller.DessingPuzzle;
using PeixeAbissal.Input;
using PeixeAbissal.Input.Enum;
using PeixeAbissal.Scene.Enum;
using PeixeAbissal.UI;
using PeixeAbissal.UI.Enum;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class EspelhoSceneController : SceneController {

        protected override string nextLevel {
            get {
                return DayController.day == 0 ? "CaminhoTrabalho" : "GelPuzzle";
            }
        }

        [SerializeField]
        private DressingPuzzleController dressingPuzzleController;
        [SerializeField]
        private BalloonController balloonController;
        [SerializeField]
        private InteractableObject revista;

        internal override void OnStart () {

            if (DayController.day == 0)
                dressingPuzzleController.InitializePuzzle (OnFirstPuzzleResolve);
            else
                dressingPuzzleController.InitializePuzzle (OnSecondDayPuzzleResolve);
        }

        private void OnFirstPuzzleResolve () {

            Action AfterShowBalloon = delegate {
                InputManager.RegisterAtKey (KeyCode.Mouse0, InputType.Press, () => {
                    balloonController.HideBallon (() => {
                        OnFinishLevel (TransitionSide.Fade);
                    });
                });
            };
            balloonController.ShowBalloon (AfterShowBalloon, 1.5f);
        }

        private void OnSecondDayPuzzleResolve () {

            revista.ShowObject (ShowType.Scale, () => {

                revista.OnMouseClick += () => OnFinishLevel (TransitionSide.Fade);
            });
        }
    }
}
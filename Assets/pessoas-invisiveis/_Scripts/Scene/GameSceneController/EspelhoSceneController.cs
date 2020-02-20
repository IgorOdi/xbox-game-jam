using System;
using DG.Tweening;
using PeixeAbissal.Controller.DessingPuzzle;
using PeixeAbissal.Controller.Enum;
using PeixeAbissal.Input;
using PeixeAbissal.Input.Enum;
using PeixeAbissal.Scene.Enum;
using PeixeAbissal.UI;
using PeixeAbissal.UI.Enum;
using UnityEngine;

namespace PeixeAbissal.Scene.Morning {

    public class EspelhoSceneController : SceneController {

        protected override string nextLevel { get { return nextLevelToLoad; } }

        private string nextLevelToLoad;

        [SerializeField]
        private DressingPuzzleController dressingPuzzleController;
        [SerializeField]
        private BalloonController balloonController;
        [SerializeField]
        private InteractableObject revista;
        [SerializeField]
        private InteractableObject nextSceneButton;
        [SerializeField]
        private Sprite clairMaravilhosa;

        private static bool madeTopete;

        internal override void WillStart () {

            if (DayController.day == 1 && madeTopete) {

                ShowClairMaravilhosa ();
            }
        }

        internal override void OnStart () {

            if (DayController.day == 0)
                dressingPuzzleController.InitializePuzzle (OnFirstPuzzleResolve);
            else
            if (!madeTopete)
                dressingPuzzleController.InitializePuzzle (OnSecondDayPuzzleResolve);
        }

        private void OnFirstPuzzleResolve () {

            Action AfterShowBalloon = delegate {

                nextSceneButton.ShowObject (ShowType.Scale, 0.5f, Ease.OutBack, null);
                nextSceneButton.OnMouseClick += () => {

                    nextLevelToLoad = "CaminhoTrabalho";
                    OnFinishLevel (TransitionSide.Fade);
                };
            };
            balloonController.ShowBalloon (ShowType.Fade, 2f, Ease.InOutSine, AfterShowBalloon);
        }

        private void OnSecondDayPuzzleResolve () {

            revista.ShowObject (ShowType.Scale, () => {

                madeTopete = true;
                nextLevelToLoad = "GelPuzzle";
                revista.OnMouseClick += () => OnFinishLevel (TransitionSide.Fade);
            });
        }

        private void ShowClairMaravilhosa () {

            dressingPuzzleController.HideClothes (DressingState.APRON_ONLY);
            balloonController.ShowBalloon (ShowType.Fade, 1.25f, Ease.InOutSine, () => {

                nextLevelToLoad = "CaminhoTrabalho";
                nextSceneButton.ShowObject (ShowType.Scale, 0.5f, Ease.OutBack, null);
                nextSceneButton.OnMouseClick += () => OnFinishLevel (TransitionSide.Fade);
            }, clairMaravilhosa);
        }
    }
}
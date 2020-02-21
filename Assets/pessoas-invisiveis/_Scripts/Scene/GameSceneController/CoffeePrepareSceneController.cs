using System;
using PeixeAbissal.Audio;
using PeixeAbissal.Controller.Coffee;
using PeixeAbissal.Controller.Enum;
using PeixeAbissal.Scene.Enum;
using PeixeAbissal.UI;
using PeixeAbissal.UI.Enum;
using UnityEngine;

namespace PeixeAbissal.Scene.Coffee {

    public class CoffeePrepareSceneController : SceneController {

        public static int cafeIndex;
        protected override string nextLevel { get { return "CoffeeMain"; } }

        [SerializeField]
        private CoffeePreparePuzzleController coffeePreparePuzzleController;
        [SerializeField]
        private InteractableObject completeButton;

        [Header ("Audio"), SerializeField]
        private AudioClip coffeeMachine;

        internal override void OnStart () {

            CoffeePreparePuzzle coffeePrepareType = CoffeePreparePuzzle.NO_GEL;
            if (DayController.day == 1) {

                if (cafeIndex == 0) {
                    cafeIndex = +1;
                    coffeePrepareType = CoffeePreparePuzzle.ONE_GEL;
                } else {
                    coffeePrepareType = CoffeePreparePuzzle.FULL_GEL;
                }
            }

            Action backToCoffeeMain = () => OnFinishLevel (TransitionSide.Right);
            coffeePreparePuzzleController.StartPuzzle (coffeePrepareType, () => {

                completeButton.ShowObject (ShowType.Fade, 0f);
                completeButton.OnMouseClick += () => {

                    MusicPlayer.Instance.PlaySFX (coffeeMachine);
                    backToCoffeeMain ();
                };
            }, backToCoffeeMain);
        }
    }
}
using PeixeAbissal.Audio;
using PeixeAbissal.Controller.Coffee;
using PeixeAbissal.Scene.Enum;
using PeixeAbissal.UI;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.Scene.Coffee {

    public class CoffeeMainSceneController : SceneController {

        protected override string nextLevel { get { return nextLevelToLoad; } }
        private string nextLevelToLoad;

        [SerializeField]
        private CoffeeMainPuzzleController coffeeMainPuzzleController;
        [SerializeField]
        private Image clair;
        [SerializeField]
        private Sprite[] claires;
        [SerializeField]
        private LuneController luneController;

        [Header ("Audio"), SerializeField]
        private AudioClip cafeMusic;
        [SerializeField]
        private AudioClip cafeAmbience;

        public static int coffeeMainPuzzleIndex;

        internal override void WillStart () {

            MusicPlayer.Instance.PlayMusic (cafeMusic, false, false);
            MusicPlayer.Instance.PlayAmbience (cafeAmbience, false, false);

            if (DayController.day == 0)
                clair.sprite = claires[0];
            else
                clair.sprite = claires[2];

            coffeeMainPuzzleController.StartPuzzle (coffeeMainPuzzleIndex, OnPuzzleComplete);
        }

        private void OnPuzzleComplete () {

            if (coffeeMainPuzzleIndex == 4) {

                clair.sprite = claires[1];
                luneController.ShowLune (() => {

                    DayController.metLune = true;
                    nextLevelToLoad = "GelPuzzle";
                    OnFinishLevel (TransitionSide.Fade);
                });
            } else if (coffeeMainPuzzleIndex == 1) {

                nextLevelToLoad = "CaminhoCasa";
                MusicPlayer.Instance.StopMusic ();
                MusicPlayer.Instance.StopAmbience ();
                OnFinishLevel (TransitionSide.Fade);
            } else {

                nextLevelToLoad = "CoffeePreparePuzzle";
                OnFinishLevel (TransitionSide.Left);
            }

            coffeeMainPuzzleIndex += 1;
        }
    }
}
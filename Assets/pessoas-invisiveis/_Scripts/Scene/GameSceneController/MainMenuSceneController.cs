using PeixeAbissal.Scene.Enum;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.Scene.Menu {

    public class MainMenuSceneController : SceneController {

        [SerializeField]
        private Button playButton;
        [SerializeField]
        private Button creditsButton;

        internal override void OnStart () {

            playButton.onClick.AddListener (OnPlayButtonClick);
            creditsButton.onClick.AddListener (OnCreditsButtonClick);
        }

        public void OnPlayButtonClick () {

            sceneManager.LoadScene ("Acordar", TransitionSide.Fade);
        }

        public void OnCreditsButtonClick () {

            sceneManager.LoadScene ("Creditos", TransitionSide.Fade);
        }
    }
}
using PeixeAbissal.Scene.Enum;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.Scene.Menu {

    public class MainMenuSceneController : SceneController {

        [SerializeField]
        private Button playButton;
        [SerializeField]
        private Button creditsButton;
        [SerializeField]
        private Button quitButton;

        internal override void OnStart () {

            playButton.onClick.AddListener (OnPlayButtonClick);
            creditsButton.onClick.AddListener (OnCreditsButtonClick);
            quitButton.onClick.AddListener (OnQuitButtonClick);
        }

        public void OnPlayButtonClick () {

            sceneManager.LoadScene ("Acordar", TransitionSide.Fade);
        }

        public void OnCreditsButtonClick () {

            sceneManager.LoadScene ("Creditos", TransitionSide.Fade);
        }

        public void OnQuitButtonClick () {

            Application.Quit ();
        }
    }
}
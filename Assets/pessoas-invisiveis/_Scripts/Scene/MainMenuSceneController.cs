using PeixeAbissal.Enum;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.Scene {

    [SerializePrivateVariables]
    public class MainMenuSceneController : SceneController {

        private Button playButton;
        private Button testButton;

        internal override void StartScene () {

           /*  playButton.onClick.AddListener (OnPlayButtonClick);
            testButton.onClick.AddListener (OnCreditsButtonClick); */
        }

        public void OnPlayButtonClick () {

            sceneManager.LoadScene ("Acordar", Side.Fade);
        }

        public void OnCreditsButtonClick () {

            sceneManager.LoadScene ("Creditos", Side.Fade);
        }
    }
}
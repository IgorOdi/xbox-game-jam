using PeixeAbissal.Enum;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.Scene {

    [SerializePrivateVariables]
    public class MainMenuSceneController : SceneController {

        private Button playButton;
        private Button testButton;

        internal override void StartScene () {

            playButton.onClick.AddListener (OnPlayButtonClick);
            testButton.onClick.AddListener (null);
        }

        private void OnPlayButtonClick () {

            sceneManager.LoadScene ("Acordar", Side.Fade);
        }
    }
}
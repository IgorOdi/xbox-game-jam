using PeixeAbissal.Audio;
using PeixeAbissal.Input;
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

        [SerializeField]
        private AudioClip titleMusic;

        internal override void WillStart () {

            InputManager.ClearKeys ();
#if UNITY_ANDROID
            quitButton.gameObject.SetActive (false);
#endif
        }

        internal override void OnStart () {

            playButton.onClick.AddListener (OnPlayButtonClick);
            creditsButton.onClick.AddListener (OnCreditsButtonClick);
            quitButton.onClick.AddListener (OnQuitButtonClick);
            MusicPlayer.Instance.PlayMusic (titleMusic);
        }

        public void OnPlayButtonClick () {

            DayController.ResetSave ();
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
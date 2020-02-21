using PeixeAbissal.Audio;
using PeixeAbissal.Scene.Enum;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class DesfechoSceneHelper : MonoBehaviour {

        [SerializeField]
        private AudioClip cityFar;
        [SerializeField]
        private AudioClip drumRoll;
        [SerializeField]
        private AudioClip plimSound;

        public void StartBrushing () {

            MusicPlayer.Instance.StopMusic ();
            MusicPlayer.Instance.PlayAmbience (cityFar, true, false);
            MusicPlayer.Instance.PlaySFX (drumRoll);
        }

        public void PlayPlimSound () {

            MusicPlayer.Instance.PlaySFX (plimSound);
        }

        public void FinalBrushing () {

            FindObjectOfType<SceneManager> ().LoadScene ("Final", TransitionSide.Fade);
        }

        public void FinishGame () {

            FindObjectOfType<SceneManager> ().LoadScene ("Creditos", TransitionSide.Fade, 4f);
        }

        public void LoadBackToMenu () {

            MusicPlayer.Instance.StopMusic (3f);
            FindObjectOfType<SceneManager> ().LoadScene ("MainMenu", TransitionSide.Fade, 4f);
        }
    }
}
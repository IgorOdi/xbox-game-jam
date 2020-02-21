using PeixeAbissal.Audio;
using PeixeAbissal.Input;
using PeixeAbissal.Input.Enum;
using PeixeAbissal.Scene.Enum;
using UnityEngine;

namespace PeixeAbissal.Scene.Menu {

    public class CreditsSceneController : SceneController {

        [SerializeField]
        private AudioClip clairDeLune;

        internal override void WillStart () {

            MusicPlayer.Instance.PlayMusic (clairDeLune);
        }

        internal override void OnStart () {

            InputManager.RegisterAtKey (KeyCode.Mouse0, InputType.Press, () => {

                InputManager.ClearKeys ();
                MusicPlayer.Instance.StopMusic (3f);
                sceneManager.LoadScene ("MainMenu", TransitionSide.Fade);
            });
        }

        void Update() {

            Time.timeScale = UnityEngine.Input.GetKey(KeyCode.Space) ? 10f : 1f;
        }
    }
}
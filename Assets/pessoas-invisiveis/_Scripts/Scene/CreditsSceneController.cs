using PeixeAbissal.Enum;
using PeixeAbissal.Input;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class CreditsSceneController : SceneController {

        internal override void StartScene () {

            InputManager.RegisterAtKey (KeyCode.Mouse0, InputType.Press, () => {

                InputManager.ClearKeys ();
                sceneManager.LoadScene ("MainMenu", Side.Fade);
            });
        }
    }
}
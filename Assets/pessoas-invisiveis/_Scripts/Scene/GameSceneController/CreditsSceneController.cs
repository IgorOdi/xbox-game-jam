using PeixeAbissal.Input;
using PeixeAbissal.Input.Enum;
using PeixeAbissal.Scene.Enum;
using UnityEngine;

namespace PeixeAbissal.Scene.Menu {

    public class CreditsSceneController : SceneController {

        internal override void OnStart () {

            InputManager.RegisterAtKey (KeyCode.Mouse0, InputType.Press, () => {

                InputManager.ClearKeys ();
                sceneManager.LoadScene ("MainMenu", TransitionSide.Fade);
            });
        }
    }
}
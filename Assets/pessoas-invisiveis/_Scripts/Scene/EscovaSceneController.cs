using PeixeAbissal.Enum;
using PeixeAbissal.Input;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class EscovaSceneController : SceneController {

        internal override void StartScene () {

            InputManager.RegisterAtKey (KeyCode.A, InputType.Press, () => {

                Debug.Log ("Apertou A");
            });

            InputManager.RegisterAtKey (KeyCode.D, InputType.Press, () => {

                Debug.Log ("Apertou D");
            });
        }
    }
}
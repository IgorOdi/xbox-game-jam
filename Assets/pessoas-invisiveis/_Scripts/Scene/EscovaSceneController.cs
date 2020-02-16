using PeixeAbissal.Enum;
using PeixeAbissal.Input;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class EscovaSceneController : SceneController {

        protected override string nextLevel {
            get {
                return DayController.day == 0 ? "Espelho" : "EspelhoRevista";
            }
        }
        private string lastKeyString;

        internal override void StartScene () {

            InputManager.RegisterAtKey (KeyCode.A, InputType.Press, () => {

                OnPressAnyKey ("A");
            });

            InputManager.RegisterAtKey (KeyCode.D, InputType.Press, () => {

                OnPressAnyKey ("D");
            });
        }

        private void OnPressAnyKey (string lastKey) {

            if (lastKeyString != lastKey) {
                AddPoints ();
                lastKeyString = lastKey;
            }
        }
    }
}
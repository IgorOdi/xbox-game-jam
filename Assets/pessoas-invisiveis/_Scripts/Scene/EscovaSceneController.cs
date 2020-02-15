using PeixeAbissal.Enum;
using PeixeAbissal.Input;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class EscovaSceneController : SceneController {

        protected override string nextLevel { get { return "Espelho"; } }
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
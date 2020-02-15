using PeixeAbissal.Enum;
using PeixeAbissal.Input;
using PeixeAbissal.Utils;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class WakeSceneController : SceneController {

        protected override string nextLevel { get { return "Escova"; } }

        internal override void StartScene () {

            this.RunDelayed (1, () => {

                InputManager.RegisterAtKey (KeyCode.Mouse0, InputType.Press, () => {

                    AddPoints (finishPoints, false);
                });
            });
        }
    }
}
using PeixeAbissal.Input;
using PeixeAbissal.Input.Enum;
using PeixeAbissal.UI;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class ReflexiveMirrorSceneController : SceneController {

        protected override string nextLevel { get { return "CaminhoTrabalho"; } }

        [SerializeField]
        private BalloonController balloonController;

        internal override void OnStart () {

            balloonController.ShowBalloon (() => ConfigureInputAfterBalloonOnScreen (), 1.5f);
        }

        private void ConfigureInputAfterBalloonOnScreen () {

            InputManager.RegisterAtKey (KeyCode.Mouse0, InputType.Press, () => {

                InputManager.ClearKeys ();
                balloonController.HideBallon (() => {
                    OnFinishLevel ();
                });
            });
        }
    }
}
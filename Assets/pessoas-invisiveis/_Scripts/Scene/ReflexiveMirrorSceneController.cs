using PeixeAbissal.Enum;
using PeixeAbissal.Input;
using PeixeAbissal.UI;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class ReflexiveMirrorSceneController : SceneController {

        protected override string nextLevel { get { return "CaminhoTrabalho"; } }

        [SerializeField]
        private BalloonController balloonController;

        internal override void StartScene () {

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
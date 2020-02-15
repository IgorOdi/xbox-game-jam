using PeixeAbissal.Enum;
using PeixeAbissal.Input;
using PeixeAbissal.UI;
using PeixeAbissal.Utils;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class WakeSceneController : SceneController {

        protected override string nextLevel { get { return "Escova"; } }
        [SerializeField]
        private InteractableObject despertador;

        internal override void StartScene () {

            InputManager.RegisterAtKey (KeyCode.Mouse0, InputType.Press, () => {

                despertador.OnMouseClick += () => AddPoints (finishPoints, false);
            });
        }
    }
}
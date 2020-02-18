using PeixeAbissal.Input;
using PeixeAbissal.Input.Enum;
using UnityEngine;

namespace PeixeAbissal.Scene.Morning {

    public class EscovaSceneController : PointSceneController {

        protected override string nextLevel { get { return "Espelho"; } }

        public Vector3 leftPos, rightPos;
        public RectTransform brush;
        public RectTransform foam;
        private string lastKeyString;

        internal override void OnStart () {

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
                brush.anchoredPosition = lastKeyString == "A" ? leftPos : rightPos;
                foam.localScale *= 1.2f;
                lastKeyString = lastKey;
            }
        }
    }
}
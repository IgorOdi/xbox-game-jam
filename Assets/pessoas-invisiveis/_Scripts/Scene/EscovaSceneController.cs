using PeixeAbissal.Enum;
using PeixeAbissal.Input;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class EscovaSceneController : SceneController {
		public Vector3 leftPos, rightPos;
		public RectTransform brush;
		public RectTransform foam;

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

				if (lastKeyString == "A")
				{
					brush.anchoredPosition = leftPos;
				}
				else
				{
					brush.anchoredPosition = rightPos;
				}

				foam.localScale *= 1.2f;
				lastKeyString = lastKey;
            }
        }
    }
}
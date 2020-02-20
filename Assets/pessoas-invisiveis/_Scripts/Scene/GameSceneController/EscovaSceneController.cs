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

        [SerializeField]
        private GameObject keyButtons;

        internal override void WillStart () {
#if UNITY_ANDROID
            keyButtons.SetActive (false);
#endif
        }

        internal override void OnStart () {

#if UNITY_ANDROID
            InputManager.RegisterAtKey (KeyCode.Mouse0, InputType.Press, () => {
                OnPressAndroid ();
            });
#else
            InputManager.RegisterAtKey (KeyCode.A, InputType.Press, () => {
                OnPressAnyKey ("A");
            });

            InputManager.RegisterAtKey (KeyCode.D, InputType.Press, () => {
                OnPressAnyKey ("D");
            });
#endif
        }

        private void OnPressAnyKey (string lastKey) {

            if (lastKeyString != lastKey) {

                AddPoints ();
                AnimateBrush ();
                lastKeyString = lastKey;
            }
        }

        private void OnPressAndroid () {

            AddPoints ();
            AnimateBrush ();
        }

        private void AnimateBrush () {
            brush.anchoredPosition = lastKeyString == "A" ? leftPos : rightPos;
            foam.localScale *= 1.2f;
        }
    }
}
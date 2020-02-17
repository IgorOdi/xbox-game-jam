using System;
using System.Collections.Generic;
using System.Linq;
using PeixeAbissal.Input.Enum;
using UnityEngine;

namespace PeixeAbissal.Input {

    public class InputManager : MonoBehaviour {

        public static List<InputKey> inputKeys = new List<InputKey> ();

        public static void RegisterKeys () {

            inputKeys.Add (new InputKey (KeyCode.W));
            inputKeys.Add (new InputKey (KeyCode.A));
            inputKeys.Add (new InputKey (KeyCode.S));
            inputKeys.Add (new InputKey (KeyCode.D));
            inputKeys.Add (new InputKey (KeyCode.Mouse0));
        }

        public static void RegisterAtKey (KeyCode key, InputType inputType, Action callback) {

            var inputKey = inputKeys.Where (k => k.keyCode == key).FirstOrDefault ();
            if (inputType.Equals (InputType.Press))
                inputKey.OnPress += callback;
            else if (inputType.Equals (InputType.Hold))
                inputKey.OnHold += callback;
            else if (inputType.Equals (InputType.Release))
                inputKey.OnRelease += callback;

        }

        public static void UnregisterAtKey (KeyCode key, InputType inputType, Action callback) {

            var inputKey = inputKeys.Find (k => k.keyCode == key);
            if (inputType.Equals (InputType.Press))
                inputKey.OnPress -= callback;
            else if (inputType.Equals (InputType.Hold))
                inputKey.OnHold -= callback;
            else if (inputType.Equals (InputType.Release))
                inputKey.OnRelease -= callback;
        }

        public static void ClearKeys () {

            for (int i = 0; i < inputKeys.Count; i++) {

                inputKeys[i].OnPress = null;
                inputKeys[i].OnHold = null;
                inputKeys[i].OnRelease = null;
            }
        }

        public static Vector2 GetMousePosition () {

            return UnityEngine.Input.mousePosition;
        }

        private void Update () {

            for (int i = 0; i < inputKeys.Count; i++) {

                KeyCode keyCode = inputKeys[i].keyCode;
                if (UnityEngine.Input.GetKeyDown (keyCode))
                    inputKeys[i].OnPress?.Invoke ();

                if (UnityEngine.Input.GetKey (keyCode))
                    inputKeys[i].OnHold?.Invoke ();

                if (UnityEngine.Input.GetKeyUp (keyCode))
                    inputKeys[i].OnRelease?.Invoke ();
            }
        }
    }
}
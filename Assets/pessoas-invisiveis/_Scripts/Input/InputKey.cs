using System;
using UnityEngine;

namespace PeixeAbissal.Input {

    public class InputKey {

        public KeyCode keyCode;

        public Action OnPress;
        public Action OnRelease;
        public Action OnHold;

        public InputKey (KeyCode _keyCode) {

            keyCode = _keyCode;
        }
    }
}
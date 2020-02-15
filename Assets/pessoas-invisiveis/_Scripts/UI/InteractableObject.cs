using System;
using PeixeAbissal.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PeixeAbissal.UI {

    public class InteractableObject : Image, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {

        public Action OnMouseClick;
        public Action OnMouseHover;
        public Action OnMouseExit;
        public Action OnMouseDown;
        public Action OnMouseUp;

        public Vector3 originPosition;
        public bool followMouseOnClick;
        private bool isHoldingObject;

        public void OnPointerClick (PointerEventData data) => OnMouseClick?.Invoke ();
        public void OnPointerEnter (PointerEventData data) => OnMouseHover?.Invoke ();
        public void OnPointerExit (PointerEventData data) => OnMouseExit?.Invoke ();

        public void InitializeObject () {

            originPosition = transform.position;
        }

        public void ResetPosition() {

            transform.position = originPosition;
        }

        public void OnPointerDown (PointerEventData data) {

            if (followMouseOnClick)
                isHoldingObject = true;

            OnMouseDown?.Invoke ();
        }

        public void OnPointerUp (PointerEventData data) {

            if (followMouseOnClick && isHoldingObject)
                isHoldingObject = false;

            OnMouseUp?.Invoke ();
        }

        void Update () {

            if (isHoldingObject) {

                transform.position = InputManager.GetMousePosition ();
            }
        }
    }
}
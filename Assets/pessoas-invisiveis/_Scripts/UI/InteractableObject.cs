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
        public bool interactable = true;

        public bool followMouseOnClick;
        private bool isHoldingObject;

        public void OnPointerClick (PointerEventData data) {

            if (interactable) OnMouseClick?.Invoke ();
        }
        public void OnPointerEnter (PointerEventData data) {

            if (interactable) OnMouseHover?.Invoke ();
        }
        public void OnPointerExit (PointerEventData data) {

            if (interactable) OnMouseExit?.Invoke ();
        }

        public void InitializeObject (bool followMouseOnClick = false) {

            originPosition = transform.position;
            this.followMouseOnClick = followMouseOnClick;
        }

        public void ResetPosition () {

            transform.position = originPosition;
        }

        public void OnPointerDown (PointerEventData data) {

            if (!interactable) return;
            if (followMouseOnClick)
                isHoldingObject = true;

            OnMouseDown?.Invoke ();
        }

        public void OnPointerUp (PointerEventData data) {

            if (!interactable) return;
            if (followMouseOnClick && isHoldingObject)
                isHoldingObject = false;

            OnMouseUp?.Invoke ();
        }

        public virtual void SetInteractable (bool _interactable) {

            interactable = _interactable;
        }

        void Update () {

            if (isHoldingObject && interactable) {

                transform.position = InputManager.GetMousePosition ();
            }
        }
    }
}
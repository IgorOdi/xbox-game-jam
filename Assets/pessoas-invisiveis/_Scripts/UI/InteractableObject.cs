using System;
using DG.Tweening;
using PeixeAbissal.Input;
using PeixeAbissal.UI.Enum;
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

        public void InitializeObject (bool followMouseOnClick = false) {

            originPosition = transform.position;
            this.followMouseOnClick = followMouseOnClick;
        }

        public void ResetPosition () {

            transform.position = originPosition;
        }

        public void ShowObject (ShowType showType, float duration = 1, Ease ease = Ease.OutBack, Action onShow = null) {

            InternalToggleObject (showType, duration, ease, onShow, true);
        }

        public void ShowObject (ShowType showType, Action onShow) {

            InternalToggleObject (showType, 1, Ease.OutBack, onShow, true);
        }

        public void HideObject (ShowType showType, float duration = 1, Ease ease = Ease.OutBack, Action onShow = null) {

            InternalToggleObject (showType, duration, ease, onShow, false);
        }

        public void HideObject (ShowType showType, Action onShow) {

            InternalToggleObject (showType, 1, Ease.OutBack, onShow, false);
        }

        private void InternalToggleObject (ShowType showType, float duration, Ease ease, Action onShow, bool showing) {

            if (showing) {
                gameObject.SetActive (true);
            } else {
                onShow += () => gameObject.SetActive (false);
            }

            int from = showing ? 0 : 1;
            int to = showing ? 1 : 0;
            switch (showType) {

                case ShowType.Fade:
                    this.DOFade (to, duration)
                        .From (from)
                        .SetEase (ease)
                        .OnComplete (() => {
                            onShow?.Invoke ();
                        });
                    break;
                case ShowType.Scale:
                    transform.DOScale (to, duration)
                        .From (from)
                        .SetEase (ease)
                        .OnComplete (() => {
                            onShow?.Invoke ();
                        });
                    break;
            }
        }

        public void OnPointerClick (PointerEventData data) {

            if (interactable) OnMouseClick?.Invoke ();
        }
        public void OnPointerEnter (PointerEventData data) {

            if (interactable) OnMouseHover?.Invoke ();
        }
        public void OnPointerExit (PointerEventData data) {

            if (interactable) OnMouseExit?.Invoke ();
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
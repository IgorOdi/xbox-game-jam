using System;
using DG.Tweening;
using PeixeAbissal.Input;
using PeixeAbissal.Scene.Enum;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class SceneController : MonoBehaviour {

        internal SceneManager sceneManager;
        protected virtual string nextLevel { get; private set; }

        [SerializeField]
        protected Canvas canvas;

        private RectTransform background;

        private (Vector2, Vector2) onScreenAnchor = (new Vector2 (0, 0), new Vector2 (1, 1));
        private (Vector2, Vector2) leftAnchor = (new Vector2 (-1, 0), new Vector2 (0, 1));
        private (Vector2, Vector2) rightAnchor = (new Vector2 (1, 0), new Vector2 (2, 1));
        private (Vector2, Vector2) topAnchor = (new Vector2 (0, 1), new Vector2 (1, 2));
        private (Vector2, Vector2) bottomAnchor = (new Vector2 (0, -1), new Vector2 (1, 0));

        private void Awake () => background = canvas.transform.GetChild (0).GetComponent<RectTransform> ();

        internal virtual void WillStart () { }
        internal virtual void OnStart () { }

        internal void Enter (TransitionSide enterSide, float duration, Action callback) {

            if (enterSide.Equals (TransitionSide.Left)) {

                background.anchorMin = leftAnchor.Item1;
                background.anchorMax = leftAnchor.Item2;
            } else if (enterSide.Equals (TransitionSide.Right)) {

                background.anchorMin = rightAnchor.Item1;
                background.anchorMax = rightAnchor.Item2;
            } else if (enterSide.Equals (TransitionSide.Top)) {

                background.anchorMin = topAnchor.Item1;
                background.anchorMax = topAnchor.Item2;
            } else if (enterSide.Equals (TransitionSide.Bottom)) {

                background.anchorMin = bottomAnchor.Item1;
                background.anchorMax = bottomAnchor.Item2;
            } else if (enterSide.Equals (TransitionSide.Fade)) {

                CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup> ();
                canvas.GetComponent<CanvasGroup> ().DOFade (1, duration)
                    .From (0)
                    .OnComplete (() => callback?.Invoke ());
                return;
            }

            background.DOAnchorMin (onScreenAnchor.Item1, duration);
            background.DOAnchorMax (onScreenAnchor.Item2, duration)
                .OnComplete (() => {
                    callback?.Invoke ();
                });
        }

        internal void Exit (TransitionSide exitSide, float duration, Action callback) {

            SetOnScreen ();
            Vector2 min = Vector2.zero;
            Vector2 max = Vector2.zero;
            if (exitSide.Equals (TransitionSide.Left)) {

                min = leftAnchor.Item1;
                max = leftAnchor.Item2;
            } else if (exitSide.Equals (TransitionSide.Right)) {

                min = rightAnchor.Item1;
                max = rightAnchor.Item2;
            } else if (exitSide.Equals (TransitionSide.Top)) {

                min = topAnchor.Item1;
                max = topAnchor.Item2;
            } else if (exitSide.Equals (TransitionSide.Bottom)) {

                min = bottomAnchor.Item1;
                max = bottomAnchor.Item2;
            } else if (exitSide.Equals (TransitionSide.Fade)) {

                canvas.GetComponent<CanvasGroup> ().DOFade (0, duration)
                    .From (1)
                    .OnComplete (() => callback?.Invoke ());
                return;
            }

            background.DOAnchorMin (min, duration);
            background.DOAnchorMax (max, duration)
                .OnComplete (() => callback?.Invoke ());
        }

        protected virtual void OnFinishLevel (TransitionSide exitSide = TransitionSide.Right) {

            InputManager.ClearKeys ();
            sceneManager.LoadScene (nextLevel, exitSide);
        }

        private void SetOnScreen () {

            background.anchorMin = onScreenAnchor.Item1;
            background.anchorMax = onScreenAnchor.Item2;
        }
    }
}
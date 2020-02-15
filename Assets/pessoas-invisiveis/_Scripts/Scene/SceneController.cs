using System;
using DG.Tweening;
using PeixeAbissal.Enum;
using PeixeAbissal.Input;
using PeixeAbissal.UI;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class SceneController : MonoBehaviour {

        internal SceneManager sceneManager;
        protected virtual string nextLevel { get; private set; }

        protected float points = 0;
        protected float pointsPerAction = 0.1f;
        protected float finishPoints = 1;

        [SerializeField]
        protected Canvas canvas;
        [SerializeField]
        private FillBarController fillBarController;
        private RectTransform background;

        private (Vector2, Vector2) onScreenAnchor = (new Vector2 (0, 0), new Vector2 (1, 1));
        private (Vector2, Vector2) leftAnchor = (new Vector2 (-1, 0), new Vector2 (0, 1));
        private (Vector2, Vector2) rightAnchor = (new Vector2 (1, 0), new Vector2 (2, 1));
        private (Vector2, Vector2) topAnchor = (new Vector2 (0, 1), new Vector2 (1, 2));
        private (Vector2, Vector2) bottomAnchor = (new Vector2 (0, -1), new Vector2 (1, 0));

        private void Awake () => background = canvas.transform.GetChild (0).GetComponent<RectTransform> ();

        internal virtual void StartScene () { }

        internal void Enter (Side enterSide, float duration, Action callback) {

            if (enterSide.Equals (Side.Left)) {

                background.anchorMin = leftAnchor.Item1;
                background.anchorMax = leftAnchor.Item2;
            } else if (enterSide.Equals (Side.Right)) {

                background.anchorMin = rightAnchor.Item1;
                background.anchorMax = rightAnchor.Item2;
            } else if (enterSide.Equals (Side.Top)) {

                background.anchorMin = topAnchor.Item1;
                background.anchorMax = topAnchor.Item2;
            } else if (enterSide.Equals (Side.Bottom)) {

                background.anchorMin = bottomAnchor.Item1;
                background.anchorMax = bottomAnchor.Item2;
            } else if (enterSide.Equals (Side.Fade)) {

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

        internal void Exit (Side exitSide, float duration, Action callback) {

            SetOnScreen ();
            Vector2 min = Vector2.zero;
            Vector2 max = Vector2.zero;
            if (exitSide.Equals (Side.Left)) {

                min = leftAnchor.Item1;
                max = leftAnchor.Item2;
            } else if (exitSide.Equals (Side.Right)) {

                min = rightAnchor.Item1;
                max = rightAnchor.Item2;
            } else if (exitSide.Equals (Side.Top)) {

                min = topAnchor.Item1;
                max = topAnchor.Item2;
            } else if (exitSide.Equals (Side.Bottom)) {

                min = bottomAnchor.Item1;
                max = bottomAnchor.Item2;
            } else if (exitSide.Equals (Side.Fade)) {

                canvas.GetComponent<CanvasGroup> ().DOFade (0, duration)
                    .From (1)
                    .OnComplete (() => callback?.Invoke ());
                return;
            }

            background.DOAnchorMin (min, duration);
            background.DOAnchorMax (max, duration)
                .OnComplete (() => callback?.Invoke ());
        }

        protected bool CheckPosition (InteractableObject originObject, Transform destination, Action callback, bool resetIfFail = true) {

            float acceptableDistance = Vector3.Distance (originObject.originPosition, destination.position) / 10;
            if (Vector3.Distance (originObject.transform.position, destination.position) <= acceptableDistance) {

                originObject.transform.position = destination.position;
                callback?.Invoke ();
                return true;
            }
            if (resetIfFail)
                originObject.ResetPosition ();
            return false;
        }

        protected virtual void AddPoints (float points = 0, bool showBar = true) {

            points = points.Equals (0) ? pointsPerAction : points;
            this.points += points;
            if (showBar)
                fillBarController.ChangePoints (this.points);
            if (this.points >= finishPoints)
                OnFinishLevel (true, Side.Right);
        }

        protected virtual void OnFinishLevel (bool instaLoadNextLevel = true, Side exitSide = Side.Right) {

            InputManager.ClearKeys ();
            if (instaLoadNextLevel)
                sceneManager.LoadScene (nextLevel, exitSide);
        }

        private void SetOnScreen () {

            background.anchorMin = onScreenAnchor.Item1;
            background.anchorMax = onScreenAnchor.Item2;
        }
    }
}
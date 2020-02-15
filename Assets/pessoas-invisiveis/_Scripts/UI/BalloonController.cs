using System;
using DG.Tweening;
using PeixeAbissal.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.UI {

    public class BalloonController : MonoBehaviour {

        private const float FADE_DURATION = 1;

        public void ShowBalloon (float fadeTime, Action callback, float secondsToHide, Action hideCallback) {

            gameObject.SetActive (true);
            GetComponent<CanvasGroup> ().DOFade (1, FADE_DURATION)
                .From (0)
                .OnComplete (() => {
                    callback?.Invoke ();
                    this.RunDelayed (secondsToHide, () => {

                        HideBallon (hideCallback);
                    });
                });
        }

        public void ShowBalloon (Action callback, float fadeTime = FADE_DURATION) {

            gameObject.SetActive (true);
            GetComponent<CanvasGroup> ().DOFade (1, fadeTime)
                .From (0)
                .OnComplete (() => {

                    callback?.Invoke ();
                });
        }

        public void HideBallon (Action callback, float fadeTime = FADE_DURATION) {

            GetComponent<CanvasGroup> ().DOFade (0, fadeTime)
                .From (1)
                .OnComplete (() => {

                    callback?.Invoke ();
                });
        }
    }
}
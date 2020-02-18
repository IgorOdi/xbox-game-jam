using System;
using DG.Tweening;
using PeixeAbissal.UI.Enum;
using PeixeAbissal.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.UI {

    public class BalloonController : InteractableObject {

        [SerializeField]
        private Image balloonImage;
        private const float FADE_DURATION = 1;

        public void ShowBalloon (ShowType showType, float duration = 1, Ease ease = Ease.OutBack, Action onShow = null, Sprite sprite = null) {

            FadeAll (true, duration, null, sprite);
            ShowObject (showType, duration, ease, onShow);
        }

        public void ShowBalloon (ShowType showType, Action onShow, Sprite sprite = null) {

            FadeAll (true, 1f, null, sprite);
            ShowObject (showType, onShow);
        }

        public void HideBalloon (ShowType showType, float duration = 1, Ease ease = Ease.OutBack, Action onShow = null) {

            FadeAll (false, 1f, () => HideObject (showType, duration, ease, onShow));
        }

        public void HideBalloon (ShowType showType, Action onShow) {

            FadeAll (false, 1f, () => HideObject (showType, onShow));
        }

        private void FadeAll (bool showing, float duration, Action callback = null, Sprite sprite = null) {

            int from = showing ? 0 : 1;
            int to = showing ? 1 : 0;
            float delay = showing ? 0f : 0f;
            duration = showing ? duration : duration / 2;
            foreach (Image i in GetComponentsInChildren<Image> (true)) {

                if (i == this) continue;
                if (showing)
                    i.gameObject.SetActive (true);
                if (sprite) {
                    i.sprite = sprite;
                    i.SetNativeSize ();
                }

                var tween = i.DOFade (to, duration)
                    .SetDelay (delay)
                    .From (from)
                    .OnComplete (() => {

                        callback?.Invoke ();
                    });
            }
        }
    }
}
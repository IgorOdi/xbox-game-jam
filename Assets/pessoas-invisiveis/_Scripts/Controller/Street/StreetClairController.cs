using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.Controller.Street {

    public class StreetClairController : MonoBehaviour {

        [SerializeField]
        private Image clairImage;
        [SerializeField]
        private Sprite clairSkating, clairLooking;

        private bool isSkating = true;

        public void SetClairSkating () {

            isSkating = true;
            clairImage.sprite = clairSkating;
        }

        public void SetClairLooking () {

            isSkating = false;
            clairImage.sprite = clairLooking;
        }

        public void TurnClair (bool right) {

            int sideMultiplier = isSkating ? -1 : 1;
            transform.localScale = right ? new Vector3 (1 * sideMultiplier, 1, 1) : new Vector3 (-1 * sideMultiplier, 1, 1);
        }

        public void FadeClair (bool fadeOut, float duration, Action afterFade = null) {

            int from = fadeOut ? 1 : 0;
            int to = fadeOut ? 0 : 1;

            clairImage.DOFade (to, duration)
                .From (from)
                .OnComplete (() => {

                    afterFade?.Invoke ();
                });
        }
    }
}
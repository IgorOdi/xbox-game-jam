using DG.Tweening;
using PeixeAbissal.UI;
using PeixeAbissal.Utils;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class DesfechoSceneController : SceneController {

        [SerializeField]
        private RectTransform scrollArea;
        [SerializeField]
        private InteractableObject nextButton;

        private int actualIndex;

        internal override void StartScene () {

            this.RunDelayed (1f, ConfigureButton);
            nextButton.OnMouseClick = () => {

                MoveNext ();
            };
        }

        public void MoveNext () {

            scrollArea.DOAnchorPosX (scrollArea.anchoredPosition.x - 1920, 3f)
                .OnComplete (() => {

                    actualIndex += 1;
                    ConfigureButton ();
                });
        }

        private void ConfigureButton () {

            nextButton.gameObject.SetActive (true);
            nextButton.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (710 + (1920 * actualIndex), -350);
            nextButton.transform.DOScale (1, 1f)
                .From (0)
                .SetEase (Ease.OutBack);
        }
    }
}
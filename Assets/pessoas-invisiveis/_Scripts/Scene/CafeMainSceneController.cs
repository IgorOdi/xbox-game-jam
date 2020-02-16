using System.Collections.Generic;
using DG.Tweening;
using PeixeAbissal.Enum;
using PeixeAbissal.UI;
using PeixeAbissal.Utils;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class CafeMainSceneController : SceneController {

        protected override string nextLevel { get { return nextLevelToLoad; } }

        [SerializeField]
        private string nextLevelToLoad;
        private float balloonAmountToDestroy = 1;
        private float balloonsDestroyed;

        [SerializeField]
        private BalloonController[] balloonController;
        [SerializeField]
        private InteractableObject[] interactableBalloons;
        [SerializeField]
        private InteractableObject atenderPedidos;

        [SerializeField]
        private ClientController[] clientController;

        private List<float> balloonHealth = new List<float> ();

        private float balloon_damage = 0.34f;
        private const float BALLOON_BASE_HEALTH = 1f;

        internal override void StartScene () {

            if (balloonController.Length != interactableBalloons.Length)
                throw new System.Exception ("Número de balões e interactables são diferentes");

            balloonAmountToDestroy = balloonController.Length;
            balloonHealth = new List<float> (balloonController.Length);
            for (int i = 0; i < balloonController.Length; i++) {

                int index = i;
                this.RunDelayed (i * 2.5f, () => RunCafe (index));
            }
        }

        private void RunCafe (int index) {

            if (clientController != null && clientController.Length > index)
                clientController[index].StartClient ();

            balloonHealth.Add (BALLOON_BASE_HEALTH);
            this.RunDelayed (0.5f, () => {

                balloonController[index].ShowBalloon (() => {

                    interactableBalloons[index].OnMouseClick += () => {
                        balloonHealth[index] -= balloon_damage;
                        if (balloonHealth[index] <= 0f) {

                            atenderPedidos.interactable = true;
                            balloonsDestroyed += 1;
                            if (clientController != null && clientController.Length > index)
                                clientController[index].ServeClient ();

                            Destroy (balloonController[index].gameObject);
                            if (balloonsDestroyed < balloonAmountToDestroy) {
                                atenderPedidos.OnMouseClick += () => {
                                    atenderPedidos.interactable = false;
                                };
                            } else {

                                atenderPedidos.OnMouseClick += () => {
                                    OnFinishLevel (true, Side.Fade);
                                };
                            }
                        }
                    };
                }, .75f);
            });

            if (!atenderPedidos.IsActive ()) {

                atenderPedidos.gameObject.SetActive (true);
                atenderPedidos.transform.DOScale (1, 1f)
                    .From (0)
                    .SetEase (Ease.OutBack);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using DG.Tweening;
using PeixeAbissal.Audio;
using PeixeAbissal.Enum;
using PeixeAbissal.UI;
using PeixeAbissal.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.Scene {

    public class CafeMainSceneController : SceneController {

        protected override string nextLevel {
            get {
                return DayController.day == 0 ? nextLevelToLoadDay0 : nextLevelToLoadDay1;
            }
        }

        [SerializeField]
        private string nextLevelToLoadDay0;
        [SerializeField]
        private string nextLevelToLoadDay1;

        private int balloonAmountToDestroy = 1;
        private int balloonsDestroyed;
        private int clientsAtendidos;

        [SerializeField]
        private BalloonController[] balloonController;
        [SerializeField]
        private InteractableObject[] interactableBalloons;
        [SerializeField]
        private InteractableObject atenderPedidos;

        [SerializeField]
        private List<ClientController> clientController = new List<ClientController> ();

        private List<float> balloonHealth = new List<float> ();

        private float balloon_damage = 1f;
        private const float BALLOON_BASE_HEALTH = 1f;

        [SerializeField]
        private Image clair;
        [SerializeField]
        private Sprite[] claires;
        [SerializeField]
        private LuneController luneController;

        [Header ("Audio"), SerializeField]
        private AudioClip cafeMusic;
        [SerializeField]
        private AudioClip cafeAmbience;
        [SerializeField]
        private AudioClip[] balloonPop;

        internal override void WillStart () {

            MusicPlayer.Instance.PlayMusic (cafeMusic);
            MusicPlayer.Instance.PlayAmbience (cafeAmbience);

            if (DayController.day == 0) {

                clair.sprite = claires[0];
            } else {

                clair.sprite = claires[2];
            }
        }

        internal override void StartScene () {

            if (balloonController.Length != interactableBalloons.Length)
                throw new System.Exception ("Número de balões e interactables são diferentes");

            balloonAmountToDestroy = balloonController.Length;
            balloonHealth = new List<float> (balloonController.Length);
            float dayInterval = DayController.day == 0 ? 2f : .5f;
            for (int i = 0; i < balloonController.Length; i++) {

                int index = i;
                this.RunDelayed (i * dayInterval, () => RunCafe (index));
            }
            atenderPedidos.SetInteractable (false);
        }

        private void RunCafe (int index) {

            balloonHealth.Add (BALLOON_BASE_HEALTH);
            balloonController[index].ShowBalloon (null, 0.75f);

            interactableBalloons[index].OnMouseClick += () => {
                balloonHealth[index] -= balloon_damage;
                if (balloonHealth[index] <= 0f) {

                    atenderPedidos.SetInteractable (true);
                    balloonsDestroyed += 1;

                    DestroyBalloon (index);
                    if (balloonsDestroyed <= balloonAmountToDestroy) {
                        atenderPedidos.OnMouseClick = () => {

                            if (clientController != null && clientController.Count > index) {
                                clientController[clientsAtendidos].ServeClient ();
                            }

                            clientsAtendidos += 1;
                            if (clientsAtendidos >= balloonsDestroyed)
                                atenderPedidos.SetInteractable (false);
                            if (clientsAtendidos >= balloonAmountToDestroy) {

                                Finish ();
                            }
                        };
                    }
                };
            };

            this.RunDelayed (0.5f, () => {
                if (clientController != null && clientController.Count > index)
                    clientController[index].StartClient ();
            });

            if (!atenderPedidos.IsActive ()) {

                atenderPedidos.gameObject.SetActive (true);
                atenderPedidos.transform.DOScale (1, 1f)
                    .From (0)
                    .SetEase (Ease.OutBack);
            }
        }

        private void DestroyBalloon (int index) {

            int r = UnityEngine.Random.Range (0, balloonPop.Length);
            MusicPlayer.Instance.PlaySFX (balloonPop[r]);
            Destroy (balloonController[index].gameObject);
        }

        private void Finish () {

            if (DayController.day == 1 && !DayController.metLune && luneController != null) {

                if (CafePuzzleController.cafeIndex == 1) {

                    nextLevelToLoadDay1 = "CafePuzzle2";
                    OnFinishLevel (true, Side.Fade);
                } else {
                    clair.sprite = claires[1];
                    luneController.ShowLune (() => {

                        DayController.metLune = true;
                        nextLevelToLoadDay1 = "LuneGelPuzzle";
                        OnFinishLevel (true, Side.Fade);
                    });
                }
            } else {

                OnFinishLevel (true, Side.Fade);
            }
        }

    }
}
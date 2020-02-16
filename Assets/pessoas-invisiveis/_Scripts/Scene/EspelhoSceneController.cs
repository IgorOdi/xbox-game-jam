using System;
using DG.Tweening;
using PeixeAbissal.Enum;
using PeixeAbissal.Input;
using PeixeAbissal.UI;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.Scene {

    public class EspelhoSceneController : SceneController {

        public static int day;
        protected override string nextLevel {
            get {
                return DayController.day == 0 ? "CaminhoTrabalho" : "GelPuzzle";
            }
        }

        [SerializeField]
        private InteractableObject roupa, bone;
        [SerializeField]
        private Transform roupaFinalPosition, boneFinalPosition;
        [SerializeField]
        private BalloonController balloonController;
        [SerializeField]
        private InteractableObject revista;

        [SerializeField]
        private Image clair;
        [SerializeField]
        private Sprite noClothes, onlyBone, onlyRoupa, fullClothes;

        private bool showedBalloon;
        private bool boneOnPlace, roupaOnPlace;

        internal override void StartScene () {

            roupa.followMouseOnClick = true;
            bone.followMouseOnClick = true;

            Action resolve = () => {
                AddPoints (0.25f, false);
                if (points >= 0.5f && !showedBalloon) {

                    if (DayController.day == 0) {

                        InputManager.ClearKeys ();
                        balloonController.ShowBalloon (ConfigureInputAfterBalloonOnScreen, 1.5f);
                        showedBalloon = true;
                    } else {

                        ConfigureSecondDayMagazine ();
                    }
                }
            };

            roupa.OnMouseDown += () => {
                if (roupaOnPlace) {
                    roupaOnPlace = false;
                    AddPoints (-0.25f, false);
                }
            };
            roupa.OnMouseUp += () => {
                if (CheckPosition (roupa, roupaFinalPosition, resolve)) {
                    roupaOnPlace = true;
                    roupa.gameObject.SetActive (false);
                }
                ConfigureClothes ();
            };
            bone.OnMouseDown += () => {
                if (boneOnPlace) {
                    boneOnPlace = false;
                    AddPoints (-0.25f, false);
                }
            };
            bone.OnMouseUp += () => {
                if (CheckPosition (bone, boneFinalPosition, resolve)) {
                    boneOnPlace = true;
                    bone.gameObject.SetActive (false);
                }
                ConfigureClothes ();
            };
        }

        private void ConfigureSecondDayMagazine () {

            revista.gameObject.SetActive (true);
            revista.OnMouseClick += () => OnFinishLevel (true, Side.Fade);
            revista.transform.DOScale (1, 1f)
                .From (0)
                .SetEase (Ease.OutBack);
        }

        private void ConfigureInputAfterBalloonOnScreen () {

            InputManager.RegisterAtKey (KeyCode.Mouse0, InputType.Press, () => {
                balloonController.HideBallon (() => {
                    OnFinishLevel (true, Side.Fade);
                });
            });
        }

        private void ConfigureClothes () {

            if (boneOnPlace && !roupaOnPlace) {

                clair.sprite = onlyBone;
            } else if (roupaOnPlace && !boneOnPlace) {

                clair.sprite = onlyRoupa;
            } else if (roupaOnPlace && boneOnPlace) {

                clair.sprite = fullClothes;
            } else {

                clair.sprite = noClothes;
            }
        }
    }
}
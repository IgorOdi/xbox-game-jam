using DG.Tweening;
using PeixeAbissal.Audio;
using PeixeAbissal.Scene.Enum;
using PeixeAbissal.UI;
using UnityEngine;

namespace PeixeAbissal.Scene.Morning {

    public class SleepSceneController : SceneController {

        protected override string nextLevel { get { return "Acordar"; } }

        [SerializeField]
        private InteractableObject sleepButton;
        [Header ("Audio"), SerializeField]
        private AudioClip cityFar;
        [SerializeField]
        private AudioClip sleepButtonSound;

        internal override void WillStart () {

            MusicPlayer.Instance.PlayAmbience (cityFar);
        }

        internal override void OnStart () {

            sleepButton.gameObject.SetActive (true);
            sleepButton.transform.DOScale (1, 1f)
                .From (0)
                .SetEase (Ease.OutBack);
            sleepButton.OnMouseClick += () => {

                DayController.day += 1;
                MusicPlayer.Instance.PlaySFX (sleepButtonSound);
                OnFinishLevel (TransitionSide.Fade);
            };
        }
    }
}
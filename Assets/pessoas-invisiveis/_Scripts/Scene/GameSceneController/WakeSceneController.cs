﻿using PeixeAbissal.Audio;
using PeixeAbissal.Input;
using PeixeAbissal.Input.Enum;
using PeixeAbissal.UI;
using PeixeAbissal.Utils;
using UnityEngine;

namespace PeixeAbissal.Scene.Morning {

    public class WakeSceneController : SceneController {

        protected override string nextLevel { get { return "Escova"; } }

        [SerializeField]
        private InteractableObject despertador;
        [Header ("Audio"), SerializeField]
        private AudioClip clairDeLune;
        [SerializeField]
        private AudioClip homeAmbience;
        [SerializeField]
        private AudioClip alarme;
        [SerializeField]
        private AudioClip alarmFeedback;

        internal override void WillStart () {

            MusicPlayer.Instance.PlayMusic (clairDeLune, true, false);
            MusicPlayer.Instance.PlayAmbience (homeAmbience);
        }

        internal override void OnStart () {

            this.RunDelayed (0.5f, () => {
                MusicPlayer.Instance.PlaySFX (alarme);
            });
            InputManager.RegisterAtKey (KeyCode.Mouse0, InputType.Press, () => {

                MusicPlayer.Instance.PlaySFX (alarmFeedback);
                despertador.OnMouseClick += () => OnFinishLevel ();
            });
        }
    }
}
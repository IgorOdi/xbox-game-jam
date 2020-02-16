using PeixeAbissal.Audio;
using PeixeAbissal.Enum;
using PeixeAbissal.Input;
using PeixeAbissal.UI;
using PeixeAbissal.Utils;
using UnityEngine;

namespace PeixeAbissal.Scene {

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

        internal override void WillStart () {

            MusicPlayer.Instance.PlayMusic (clairDeLune, true, false);
            MusicPlayer.Instance.PlayAmbience (homeAmbience);
        }

        internal override void StartScene () {

            this.RunDelayed (0.5f, () => {
                MusicPlayer.Instance.PlaySFX (alarme);
            });
            InputManager.RegisterAtKey (KeyCode.Mouse0, InputType.Press, () => {

                despertador.OnMouseClick += () => OnFinishLevel ();
            });
        }
    }
}
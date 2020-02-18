using System.Collections;
using System.Collections.Generic;
using PeixeAbissal.Audio;
using PeixeAbissal.Scene.Enum;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class DesfechoSceneHelper : MonoBehaviour {

        [SerializeField]
        private AudioClip clairDeLune;

        public void FinalBrushing () {

            FindObjectOfType<SceneManager> ().LoadScene ("Final", TransitionSide.Fade);
            MusicPlayer.Instance.PlayMusic (clairDeLune);
        }

        public void FinishGame () {

            FindObjectOfType<SceneManager> ().LoadScene ("Creditos", TransitionSide.Fade, 4f);
        }
    }
}
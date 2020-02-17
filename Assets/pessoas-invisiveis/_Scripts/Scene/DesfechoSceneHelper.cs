using System.Collections;
using System.Collections.Generic;
using PeixeAbissal.Audio;
using PeixeAbissal.Enum;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class DesfechoSceneHelper : MonoBehaviour {

        [SerializeField]
        private AudioClip clairDeLune;

        public void FinalBrushing () {

            FindObjectOfType<SceneManager> ().LoadScene ("Final", Side.Fade);
            MusicPlayer.Instance.PlayMusic (clairDeLune);
        }

        public void FinishGame () {

            FindObjectOfType<SceneManager> ().LoadScene ("Creditos", Side.Fade);
        }
    }
}
using System.Collections;
using DG.Tweening;
using PeixeAbissal.Audio;
using PeixeAbissal.UI;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.Controller.Coffee {

    public class ClientController : MonoBehaviour {

        public FillBarController fillBarController;
        [HideInInspector]
        public float clientPatienceDuration = 3f;

        [SerializeField]
        private GameObject angryReaction;

        private Coroutine patienceCoroutine;

        [SerializeField]
        private AudioClip[] clientGrunt;

        public void StartClient (float clientPatience) {

            gameObject.SetActive (true);
            clientPatienceDuration = clientPatience;
            fillBarController.ChangePoints (0);
            GetComponentInChildren<Image> ().DOFade (1, 0.25f)
                .From (0)
                .OnComplete (() => {
                    patienceCoroutine = StartCoroutine (ClientPatience ());
                });
        }

        public void ServeClient () {

            if (patienceCoroutine != null) StopCoroutine (patienceCoroutine);
            fillBarController.gameObject.SetActive (false);
            GetComponentInChildren<Image> ().DOFade (0, 1f)
                .From (1)
                .OnComplete (() => {
                    gameObject.SetActive (false);
                });
        }

        private IEnumerator ClientPatience () {

            float t = 0;
            while (t < 1) {

                fillBarController.ChangePoints (t);
                t += Time.deltaTime / clientPatienceDuration;
                yield return null;
            }
            SetAngryClient ();
        }

        private void SetAngryClient () {

            int r = Random.Range (0, clientGrunt.Length);
            MusicPlayer.Instance.PlaySFX (clientGrunt[r]);
            angryReaction.SetActive (true);
            fillBarController.gameObject.SetActive (false);
        }
    }
}
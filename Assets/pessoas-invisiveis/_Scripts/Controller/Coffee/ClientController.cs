using System.Collections;
using PeixeAbissal.Audio;
using PeixeAbissal.UI;
using UnityEngine;

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
            patienceCoroutine = StartCoroutine (ClientPatience ());
        }

        public void ServeClient () {

            if (patienceCoroutine != null) StopCoroutine (patienceCoroutine);
            gameObject.SetActive (false);
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
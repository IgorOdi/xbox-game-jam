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

        public void StartClient () {

            gameObject.SetActive (true);
            patienceCoroutine = StartCoroutine (ClientPatience ());
        }

        public void ServeClient () {

            StopCoroutine (patienceCoroutine);
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

            angryReaction.SetActive (true);
            fillBarController.gameObject.SetActive (false);
        }
    }
}
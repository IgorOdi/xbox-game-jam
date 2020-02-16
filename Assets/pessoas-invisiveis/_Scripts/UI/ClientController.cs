using PeixeAbissal.UI;
using UnityEngine;

namespace PeixeAbissal {

    public class ClientController : MonoBehaviour {

        public FillBarController fillBarController;
        [SerializeField]
        private GameObject angryReaction;

        public void StartClient () {

            gameObject.SetActive (true);
        }

        public void ServeClient () {

            gameObject.SetActive (false);
        }

        void Update () {

            fillBarController.AddPoints (-0.004f);

            if (fillBarController.GetPoints () <= 0) {

                SetAngryClient ();
            }
        }

        private void SetAngryClient () {

            angryReaction.SetActive (true);
            fillBarController.gameObject.SetActive (false);
        }
    }
}
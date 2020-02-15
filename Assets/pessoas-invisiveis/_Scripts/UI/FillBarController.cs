using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.UI {

    public class FillBarController : MonoBehaviour {

        [SerializeField]
        private Image fillBar;

        public void AddPoints (float points) {

            ActivateIfDisabled ();
            fillBar.fillAmount += points;
        }

        public void ChangePoints (float points) {
            ActivateIfDisabled ();
            fillBar.fillAmount = points;
        }

        private void ActivateIfDisabled () {
            if (!gameObject.activeSelf)
                gameObject.SetActive (true);
        }
    }
}
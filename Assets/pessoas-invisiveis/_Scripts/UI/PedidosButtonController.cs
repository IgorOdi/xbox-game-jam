using UnityEngine;

namespace PeixeAbissal.UI {

    public class PedidosButtonController : InteractableObject {

        public override void SetInteractable (bool _interactable) {

            base.SetInteractable(_interactable);

            var on = transform.Find("ServeCoffeOn").gameObject;
            var off = transform.Find("ServeCoffeOff").gameObject;

            if (_interactable) {

                on.SetActive (true);
                off.SetActive (false);
            } else {

                on.SetActive (false);
                off.SetActive (true);
            }
        }
    }
}
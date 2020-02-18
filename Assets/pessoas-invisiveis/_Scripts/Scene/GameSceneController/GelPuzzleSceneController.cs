using PeixeAbissal.Scene.Enum;
using PeixeAbissal.UI;
using UnityEngine;

namespace PeixeAbissal.Scene.Gel {

    public class GelPuzzleSceneController : SceneController {

        protected override string nextLevel {
            get {
                return DayController.metLune ? "Desfecho" : "Espelho";
            }
        }

        [SerializeField]
        private InteractableObject[] ingredients;
        [SerializeField]
        private GameObject lune;

        private int selectedIndex;

        internal override void WillStart () {

            if (DayController.metLune)
                lune.SetActive (true);
        }

        internal override void OnStart () {

            for (int i = 0; i < ingredients.Length; i++) {

                int index = i;
                ingredients[index].OnMouseClick += () => {

                    CheckIngredient (index);
                };
            }
        }

        private void CheckIngredient (int index) {

            if (index == selectedIndex) {

                selectedIndex += 1;
                ingredients[index].gameObject.SetActive (false);
            }

            if (selectedIndex >= ingredients.Length) {

                OnFinishLevel (TransitionSide.Fade);
            }
        }
    }
}
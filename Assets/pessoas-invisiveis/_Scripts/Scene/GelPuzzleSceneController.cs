using PeixeAbissal.Enum;
using PeixeAbissal.UI;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class GelPuzzleSceneController : SceneController {

        protected override string nextLevel {
            get {
                return DayController.metLune ? "Desfecho" : "EspelhoMaravilhosa";
            }
        }

        [SerializeField]
        private InteractableObject[] ingredients;

        private int selectedIndex;

        internal override void StartScene () {

            for (int i = 0; i < ingredients.Length; i++) {

                int index = i;
                ingredients[index].OnMouseClick += () => {

                    if (index == selectedIndex) {

                        selectedIndex += 1;
                        ingredients[index].gameObject.SetActive (false);
                    }

                    if (selectedIndex >= ingredients.Length) {

                        OnFinishLevel (true, Side.Fade);
                    }
                };
            }
        }
    }
}
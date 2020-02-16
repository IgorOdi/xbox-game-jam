using System;
using System.Collections.Generic;
using PeixeAbissal.Input;
using PeixeAbissal.UI;
using PeixeAbissal.Utils;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class CafePuzzleController : SceneController {

        protected override string nextLevel { get { return nextLevelToLoad; } }

        [SerializeField]
        private string nextLevelToLoad;
        [SerializeField]
        private Transform sceneUIHolder;
        [SerializeField]
        private Transform[] sourcePositions;
        [SerializeField]
        private Transform[] destinationPositions;
        [SerializeField]
        private GameObject[] ingredients;
        [SerializeField]
        private InteractableObject complete;

        [SerializeField]
        private InteractableObject[] instatiatedIngredients;

        [SerializeField]
        private List<InteractableObject> placedIngredients = new List<InteractableObject> ();

        internal override void StartScene () {

            this.RunDelayed (0.5f, () => {

                instatiatedIngredients = new InteractableObject[ingredients.Length];
                for (int i = 0; i < ingredients.Length; i++) {

                    var instantiatedIngredient = Instantiate (ingredients[i], sourcePositions[i].position, Quaternion.identity, sceneUIHolder);
                    instatiatedIngredients[i] = instantiatedIngredient.GetComponent<InteractableObject> ();
                    instatiatedIngredients[i].InitializeObject (true);
                    int index = i;
                    instatiatedIngredients[i].OnMouseExit += () => {
                        if (TestPosition (instatiatedIngredients[index])) {

                            CheckIngredients (index);
                        }
                    };
                }
            });
        }

        private void CheckIngredients (int index) {

            if (!placedIngredients.Contains (instatiatedIngredients[index])) {

                placedIngredients.Add (instatiatedIngredients[index]);
            }

            if (placedIngredients.Count >= ingredients.Length) {

                for (int i = 0; i < ingredients.Length; i++) {

                    if (!CheckPosition (instatiatedIngredients[i], destinationPositions[i], null, false)) {

                        for (int j = 0; j < ingredients.Length; j++) {

                            placedIngredients.Clear ();
                            instatiatedIngredients[j].ResetPosition ();
                        }
                        return;
                    }
                }

                for (int i = 0; i < instatiatedIngredients.Length; i++) {
                    instatiatedIngredients[i].SetInteractable(false);
                }
                complete.gameObject.SetActive (true);
                complete.OnMouseClick += () => OnFinishLevel ();
            }
        }

        private bool TestPosition (InteractableObject originObject) {

            for (int i = 0; i < ingredients.Length; i++) {

                float acceptableDistance = Vector3.Distance (originObject.originPosition, destinationPositions[i].position) / 7;
                if (Vector3.Distance (originObject.transform.position, destinationPositions[i].position) <= acceptableDistance) {

                    originObject.transform.position = destinationPositions[i].position;
                    return true;
                }
            }

            originObject.ResetPosition ();
            if (placedIngredients.Contains (originObject)) {

                placedIngredients.Remove (originObject);
            }
            return false;
        }
    }
}
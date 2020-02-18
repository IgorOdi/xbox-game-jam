using System;
using System.Collections;
using System.Collections.Generic;
using PeixeAbissal.Audio;
using PeixeAbissal.Controller.Enum;
using PeixeAbissal.UI;
using PeixeAbissal.UI.Enum;
using PeixeAbissal.Utils;
using UnityEngine;

namespace PeixeAbissal.Controller.Coffee {

    public class CoffeePreparePuzzleController : MonoBehaviour {

        [SerializeField]
        private Transform[] sourcePositions;
        [SerializeField]
        private Transform[] destinationPositions;
        [SerializeField]
        private GameObject[] ingredients;

        private InteractableObject[] instantiatedIngredients;
        private List<InteractableObject> placedIngredients = new List<InteractableObject> ();

        [SerializeField]
        private FillBarController timeBarController;
        private Coroutine timeBarCoroutine;

        private Action onPuzzleComplete;
        private CoffeePreparePuzzle puzzleType;

        private const float INGREDIENTS_INTERVAL = 0.25f;
        private const int SLOTS = 3;

        public void StartPuzzle (CoffeePreparePuzzle puzzleType, Action onPuzzleComplete, Action onFailPuzzle) {

            this.onPuzzleComplete = onPuzzleComplete;

            this.puzzleType = puzzleType;
            int ingredientsAmount = puzzleType == CoffeePreparePuzzle.NO_GEL ? 3 : 4;
            instantiatedIngredients = new InteractableObject[ingredientsAmount];
            if (puzzleType == CoffeePreparePuzzle.FULL_GEL) {

                for (int i = 0; i < ingredients.Length; i++) {
                    ingredients[i] = ingredients[ingredients.Length - 1];
                }
            }

            for (int i = 0; i < ingredientsAmount; i++) {

                int index = i;
                this.RunDelayed (index * INGREDIENTS_INTERVAL, () => {

                    var instantiatedIngredient = Instantiate (ingredients[index], sourcePositions[index].position, Quaternion.identity, transform);
                    instantiatedIngredients[index] = instantiatedIngredient.GetComponent<InteractableObject> ();
                    instantiatedIngredients[index].InitializeObject (true);
                    instantiatedIngredients[index].ShowObject (ShowType.Scale, INGREDIENTS_INTERVAL);
                    instantiatedIngredients[index].OnMouseClick += () => {

                        TestIngredient (instantiatedIngredients[index]);
                    };
                });
            }

            if (puzzleType != CoffeePreparePuzzle.NO_GEL) {

                timeBarController.gameObject.SetActive (true);
                int interval = puzzleType == CoffeePreparePuzzle.ONE_GEL ? 6 : 3;
                this.RunDelayed (INGREDIENTS_INTERVAL * ingredients.Length + INGREDIENTS_INTERVAL, () => {

                    timeBarCoroutine = StartCoroutine (CompleteBar (interval, onFailPuzzle));
                });
            }
        }

        private void TestIngredient (InteractableObject originObject) {

            if (placedIngredients.Contains (originObject)) {

                placedIngredients.Remove (originObject);
            }

            for (int i = 0; i < SLOTS; i++) {
                if (DragHelper.CheckPosition (originObject, destinationPositions[i], false)) {

                    placedIngredients.Add (originObject);
                    if (placedIngredients.Count >= SLOTS) {

                        CheckWinCondition ();
                    }
                    return;
                }
            }

            originObject.ResetPosition ();
        }

        private void CheckWinCondition () {

            if (puzzleType == CoffeePreparePuzzle.FULL_GEL) return;

            for (int i = 0; i < SLOTS; i++) {

                if (!DragHelper.CheckPosition (instantiatedIngredients[i], destinationPositions[i], false)) {
                    for (int j = 0; j < instantiatedIngredients.Length; j++) {

                        placedIngredients.Clear ();
                        instantiatedIngredients[j].ResetPosition ();
                    }
                    return;
                }
            }

            for (int i = 0; i < instantiatedIngredients.Length; i++) {
                instantiatedIngredients[i].SetInteractable (false);
            }
            //MusicPlayer.Instance.PlaySFX (completeSound);
            onPuzzleComplete ();
            if (timeBarCoroutine != null) StopCoroutine (timeBarCoroutine);
        }

        private IEnumerator CompleteBar (float limitTime, Action callback) {

            timeBarController.gameObject.SetActive (true);
            float t = 0;
            while (t <= limitTime) {

                timeBarController.ChangePoints (t / limitTime);
                t += Time.deltaTime;
                yield return null;
            }

            callback?.Invoke ();
        }
    }
}
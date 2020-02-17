using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PeixeAbissal.Audio;
using PeixeAbissal.Enum;
using PeixeAbissal.Input;
using PeixeAbissal.UI;
using PeixeAbissal.Utils;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class CafePuzzleController : SceneController {

        public static int cafeIndex;
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
        private FillBarController timeBarController;

        private InteractableObject[] instatiatedIngredients;
        private List<InteractableObject> placedIngredients = new List<InteractableObject> ();

        [Header ("Audio"), SerializeField]
        private AudioClip genericPlace;
        [SerializeField]
        private AudioClip completeSound;

        internal override void StartScene () {

            instatiatedIngredients = new InteractableObject[ingredients.Length];
            if (cafeIndex == 1) {

                for (int i = 0; i < ingredients.Length; i++) {

                    ingredients[i] = ingredients[ingredients.Length - 1];
                }
            }
            for (int i = 0; i < ingredients.Length; i++) {

                int index = i;
                this.RunDelayed (index * 0.25f, () => {

                    var instantiatedIngredient = Instantiate (ingredients[index], sourcePositions[index].position, Quaternion.identity, sceneUIHolder);
                    instatiatedIngredients[index] = instantiatedIngredient.GetComponent<InteractableObject> ();
                    instatiatedIngredients[index].InitializeObject (true);
                    instatiatedIngredients[index].transform.DOScale (1, 0.5f)
                        .From (0)
                        .SetEase (Ease.OutBack);
                    instatiatedIngredients[index].OnMouseUp += () => {
                        if (TestPosition (instatiatedIngredients[index])) {

                            CheckIngredients (index);
                        }
                    };
                });
            }

            if (DayController.day == 1) {
                this.RunDelayed (1.25f, () => {

                    StartCoroutine (CompleteBar (6f, () => {

                        cafeIndex += 1;
                        OnFinishLevel (true, Side.Fade);
                    }));
                });
            }
        }

        private void CheckIngredients (int index) {

            if (!placedIngredients.Contains (instatiatedIngredients[index])) {

                placedIngredients.Add (instatiatedIngredients[index]);
            }

            if (placedIngredients.Count >= 3) {

                for (int i = 0; i < ingredients.Length; i++) {

                    if (i >= destinationPositions.Length) continue;
                    if (!CheckPosition (instatiatedIngredients[i], destinationPositions[i], null, false)) {

                        for (int j = 0; j < ingredients.Length; j++) {

                            placedIngredients.Clear ();
                            instatiatedIngredients[j].ResetPosition ();
                        }
                        return;
                    }
                }

                for (int i = 0; i < instatiatedIngredients.Length; i++) {
                    instatiatedIngredients[i].SetInteractable (false);
                }
                MusicPlayer.Instance.PlaySFX (completeSound);
                complete.gameObject.SetActive (true);
                StopAllCoroutines ();
                complete.OnMouseClick += () => {

                    OnFinishLevel ();
                };
            }
        }

        private bool TestPosition (InteractableObject originObject) {

            if (cafeIndex == 1) return false;

            for (int i = 0; i < ingredients.Length; i++) {

                if (i >= destinationPositions.Length) return false;
                float acceptableDistance = Vector3.Distance (originObject.originPosition, destinationPositions[i].position) / 7;
                if (Vector3.Distance (originObject.transform.position, destinationPositions[i].position) <= acceptableDistance) {

                    MusicPlayer.Instance.PlaySFX (genericPlace);
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
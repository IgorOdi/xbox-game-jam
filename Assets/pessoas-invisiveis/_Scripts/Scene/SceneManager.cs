using System;
using PeixeAbissal.Scene.Enum;
using PeixeAbissal.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PeixeAbissal.Scene {

    public class SceneManager : MonoBehaviour {

        internal SceneController currentSceneController;
        private bool changingScene;
        private float sceneTransitionDuration = 2;

        public void Awake () {

            currentSceneController = FindObjectOfType<SceneController> ();
            currentSceneController.WillStart ();
            InitializeScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name);
        }

        public void LoadScene (string sceneName, TransitionSide enterSide) {

            if (changingScene) {
                Debug.LogWarning ("Não pode trocar de cena enquanto outra troca já está acontecendo");
                return;
            }

            string actualScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;

            bool fadeLoad = enterSide.Equals (TransitionSide.Fade);
            if (fadeLoad) FadeScreenController.nextScene = sceneName;
            sceneName = fadeLoad && actualScene != "BlackScreen" ? "BlackScreen" : sceneName;

            if (sceneName.Equals (actualScene))
                throw new InvalidOperationException ("Não pode carregar a mesma cena já carregada");

            changingScene = true;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive)
                .completed += (operation) => {

                    var previousSceneController = currentSceneController;
                    var loadedScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName (sceneName);
                    UnityEngine.SceneManagement.SceneManager.SetActiveScene (loadedScene);

                    var root = loadedScene.GetRootGameObjects ();
                    foreach (GameObject g in root) {

                        if (g.GetComponent<SceneController> () != null) {

                            currentSceneController = g.GetComponent<SceneController> ();
                            break;
                        }
                    }

                    previousSceneController.Exit (GetExitSideFromEnter (enterSide), sceneTransitionDuration, null);
                    currentSceneController.WillStart ();
                    currentSceneController.Enter (enterSide, sceneTransitionDuration, () => {

                        InitializeScene (actualScene);
                    });
                };
        }

        private void InitializeScene (string actualSceneName) {

            changingScene = false;
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync (actualSceneName);

            foreach (InteractableObject i in FindObjectsOfType<InteractableObject> ())
                i.InitializeObject ();

            currentSceneController.sceneManager = this;
            currentSceneController.OnStart ();
        }

        private TransitionSide GetExitSideFromEnter (TransitionSide enterSide) {

            if (enterSide.Equals (TransitionSide.Left)) {

                return TransitionSide.Right;
            } else if (enterSide.Equals (TransitionSide.Right)) {

                return TransitionSide.Left;
            } else if (enterSide.Equals (TransitionSide.Bottom)) {

                return TransitionSide.Top;
            } else if (enterSide.Equals (TransitionSide.Top)) {

                return TransitionSide.Bottom;
            } else {

                return TransitionSide.Fade;
            }
        }
    }
}
using System;
using PeixeAbissal.Enum;
using PeixeAbissal.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PeixeAbissal.Scene {

    public class SceneManager : MonoBehaviour {

        internal SceneController currentSceneController;
        private bool changingScene;

        void Awake () {

            currentSceneController = FindObjectOfType<SceneController> ();
            InitializeScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name);
        }

        public void LoadScene (string sceneName, Side enterSide) {

            if (changingScene) {
                Debug.LogWarning ("Não pode trocar de cena enquanto outra troca já está acontecendo");
                return;
            }

            string actualScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
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

                    previousSceneController.Exit (GetExitSideFromEnter (enterSide), 1, null);
                    currentSceneController.Enter (enterSide, 1, () => {

                        InitializeScene(actualScene);
                    });
                };
        }

        private void InitializeScene (string actualSceneName) {

            changingScene = false;
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync (actualSceneName);

            foreach (InteractableObject i in FindObjectsOfType<InteractableObject> ())
                i.InitializeObject ();

            currentSceneController.sceneManager = this;
            currentSceneController.StartScene ();
        }

        private Side GetExitSideFromEnter (Side enterSide) {

            if (enterSide.Equals (Side.Left)) {

                return Side.Right;
            } else if (enterSide.Equals (Side.Right)) {

                return Side.Left;
            } else if (enterSide.Equals (Side.Bottom)) {

                return Side.Top;
            } else if (enterSide.Equals (Side.Top)) {

                return Side.Bottom;
            } else {

                return Side.Fade;
            }
        }
    }
}
using DG.Tweening;
using PeixeAbissal.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PeixeAbissal.Bootstrapping {

    public static class Bootstrapper {

        public static string MainMenuScene = "MainMenu";
        public static string BoostrapperScene = "Bootstrapper";

        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void LoadBootstrapperScene () {

            DOTween.defaultEaseType = Ease.InOutSine;
            InputManager.RegisterKeys ();
#if UNITY_EDITOR
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync (BoostrapperScene, LoadSceneMode.Additive);
#else
            UnityEngine.SceneManagement.SceneManager.LoadScene (MainMenuScene);
#endif
        }
    }
}
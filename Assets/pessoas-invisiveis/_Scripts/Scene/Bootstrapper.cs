using UnityEngine;
using UnityEngine.SceneManagement;
using PeixeAbissal.Input;
using DG.Tweening;

namespace PeixeAbissal.Scene {

    public static class Bootstrapper {

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void LoadBootstrapperScene () {

            DOTween.defaultEaseType = Ease.InOutSine;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Bootstrapper", LoadSceneMode.Additive);
            InputManager.RegisterKeys();
        }
    }
#endif
}
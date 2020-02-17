using PeixeAbissal.Scene.Enum;

namespace PeixeAbissal.Scene {

    public class FadeScreenController : SceneController {

        public static string nextScene;

        internal override void OnStart () {

            sceneManager.LoadScene (nextScene, TransitionSide.Fade);
        }
    }
}
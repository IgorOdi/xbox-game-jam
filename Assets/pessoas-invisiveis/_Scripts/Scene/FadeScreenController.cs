using PeixeAbissal.Enum;

namespace PeixeAbissal.Scene {

    public class FadeScreenController : SceneController {

        public static string nextScene;

        internal override void StartScene () {

            sceneManager.LoadScene (nextScene, Side.Fade);
        }
    }
}
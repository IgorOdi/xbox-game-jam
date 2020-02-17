using PeixeAbissal.Scene.Enum;
using PeixeAbissal.UI;
using UnityEngine;

namespace PeixeAbissal.Scene {

    public class PointSceneController : SceneController {

        protected float points = 0;
        protected float pointsPerAction = 0.1f;
        protected float finishPoints = 1;

        [SerializeField]
        private FillBarController fillBarController;

        protected virtual void AddPoints (float points = 0, bool showBar = true) {

            points = points.Equals (0) ? pointsPerAction : points;
            this.points += points;
            if (showBar)
                fillBarController.ChangePoints (this.points);
            if (this.points >= finishPoints)
                OnFinishLevel (TransitionSide.Right);
        }
    }
}